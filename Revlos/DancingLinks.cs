using System;
using System.Collections.Generic;
using System.Linq;

namespace Revlos
{
    public class DancingLinks
    {
        private Board _board;
        private HashSet<BoardSquare> solvedCells;
        private HashSet<BoardSquare> unsolvedCells;
        private Stack<HashSet<BoardSquare>> changedByPropagation;
        private HashSet<BoardSquare>[] cellsSortedByCandidates;
        private int steps;

        public DancingLinks(Board board)
        {
            _board = board;
            solvedCells = new HashSet<BoardSquare>();
            unsolvedCells = new HashSet<BoardSquare>();
            changedByPropagation = new Stack<HashSet<BoardSquare>>();
            cellsSortedByCandidates = new HashSet<BoardSquare>[10];
            steps = 0;

            for (var i = 0; i < cellsSortedByCandidates.Length; i++)
            {
                cellsSortedByCandidates[i] = new HashSet<BoardSquare>();
            }

            PopulateCandidates();
        }

        private void PopulateCandidates()
        {
            for (var row = 0; row < 9; row++)
            {
                for (var col = 0; col < 9; col++)
                {
                    var currentSquare = _board.GetBoardSquare(row, col);
                    if (currentSquare.IsEmpty())
                    {
                        foreach (var candidate in _board.GetRow(row))
                            _board.cellConstraint[row, col][candidate.GetValue()] = false;
                        foreach (var candidate in _board.GetColumn(col))
                            _board.cellConstraint[row, col][candidate.GetValue()] = false;
                        foreach (var candidate in _board.GetSubBoard(currentSquare.GetSubBoard()))
                            _board.cellConstraint[row, col][candidate.GetValue()] = false;

                        cellsSortedByCandidates[_board.cellConstraint[row, col].Count].Add(currentSquare);
                        unsolvedCells.Add(currentSquare);
                        continue;
                    }

                    _board.cellConstraint[row, col].SetAll(false);
                    solvedCells.Add(currentSquare);
                }
            }
        }

        private BoardSquare NextCell()
        {
            if (unsolvedCells.Count == 0)
                return new BoardSquare(-1, -1);

            foreach (var cell in cellsSortedByCandidates)
                if (cell.Count > 0)
                    return cell.First();

            return new BoardSquare(99, 99);
        }

        private bool SolveRecurse(BoardSquare nextCell)
        {
            _board.PrintBoard();

            if (nextCell.row == -1)
                return true;

            foreach (int candidate in _board.cellConstraint[nextCell.row, nextCell.col])
            {
                SelectCandidate(nextCell, candidate);

                if (SolveRecurse(NextCell()))
                    return true;

                ++steps;
                UnselectCandidate(nextCell, candidate);
            }

            return false;
        }

        public bool Solve()
        {
            steps = 1;
            return SolveRecurse(NextCell());
        }

        private void UnselectCandidate(BoardSquare boardSquare, int candidate)
        {
            _board.GetBoardSquare(boardSquare.row, boardSquare.col).SetValue(0);

            _board.cellConstraint[boardSquare.row, boardSquare.col][candidate] = true;
            cellsSortedByCandidates[_board.cellConstraint[boardSquare.row, boardSquare.col].Count].Add(boardSquare);

            _board.rowConstraint[boardSquare.row][candidate] = false;
            _board.columnConstraint[boardSquare.col][candidate] = false;
            _board.subBoardConstraint[boardSquare.row / 3, boardSquare.col / 3][candidate] = false;

            foreach (var c in changedByPropagation.Pop())
                ShiftSquareDownCandidateList(c.row, c.col, candidate);

            solvedCells.Remove(boardSquare);
            unsolvedCells.Add(boardSquare);
        }

        private void SelectCandidate(BoardSquare boardSquare, int candidate)
        {
            var changedCells = new HashSet<BoardSquare>();

            // set the candidate and move board square 
            _board.GetBoardSquare(boardSquare.row, boardSquare.col).SetValue(candidate);
            cellsSortedByCandidates[_board.cellConstraint[boardSquare.row, boardSquare.col].Count].Remove(boardSquare);

            _board.cellConstraint[boardSquare.row, boardSquare.col][candidate] = false;
            _board.columnConstraint[boardSquare.col][candidate] = true;
            _board.rowConstraint[boardSquare.row][candidate] = true;
            _board.subBoardConstraint[boardSquare.row / 3, boardSquare.col / 3][candidate] = true;

            // remove candidates across unsolvedCells cells in the same
            for (var index = 0; index < 9; index++)
            {
                // only change unsolvedCells cells containing the candidate across rows and columns
                if (_board.GetBoardSquare(boardSquare.row, index).IsEmpty() &&
                    _board.cellConstraint[boardSquare.row, index][candidate])
                {
                    ShiftSquareDownCandidateList(boardSquare.row, index, candidate);
                    changedCells.Add(_board.GetBoardSquare(boardSquare.row, index));
                }

                if (_board.GetBoardSquare(index, boardSquare.col).IsEmpty() &&
                    _board.cellConstraint[index, boardSquare.col][candidate])
                {
                    ShiftSquareDownCandidateList(index, boardSquare.col, candidate);
                    changedCells.Add(_board.GetBoardSquare(index, boardSquare.col));
                }
            }

            // remove candidates across unsolvedCells cells in the subBoard
            var grid_row_start = boardSquare.row / 3 * 3;
            var grid_col_start = boardSquare.col / 3 * 3;

            for (var row = grid_row_start; row < grid_row_start + 3; row++)
            for (var col = grid_col_start; col < grid_col_start + 3; col++)
                // only change unsolvedCells cells containing the candidate
                if (_board.GetBoardSquare(row, col).IsEmpty() &&
                    _board.cellConstraint[row, col][candidate])
                {
                    ShiftSquareDownCandidateList(row, col, candidate);
                    changedCells.Add(new BoardSquare(row, col));
                }

            unsolvedCells.Remove(boardSquare);
            solvedCells.Add(boardSquare);
            changedByPropagation.Push(changedCells);
        }

        private void ShiftSquareDownCandidateList(int row, int col, int candidate)
        {
            if (_board.cellConstraint[row, col].Count == 0)
                return;

            // shift affected cells down the bucket list
            cellsSortedByCandidates[_board.cellConstraint[row, col].Count]
                .Remove(_board.GetBoardSquare(row, col));

            cellsSortedByCandidates[_board.cellConstraint[row, col].Count - 1]
                .Add(_board.GetBoardSquare(row, col));

            // remove the candidate
            _board.cellConstraint[row, col][candidate] = false;
        }
    }
}
