using System;
using System.Collections.Generic;
using System.Linq;

namespace Revlos
{
    public class Solver
    {
        private Board _board;
        private readonly HashSet<int> Candidates = new HashSet<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9};

        public Solver(Board board)
        {
            _board = board;
        }

        public void DancingLink()
        {
            var squaresRemaining = _board.SquaresRemaining();
            InitializeCandidates();
            _board.PrintBoard();

            do
            {
                for (var row = 0; row < 9; row++)
                {
                    for (var column = 0; column < 9; column++)
                    {
                        var currentSquare = _board.GetBoardSquare(row, column);

                        if (currentSquare.GetCandidates().Count == 1)
                        {
                            currentSquare.SetValue();
                            continue;
                        }

                        if (!currentSquare.IsEmpty()) continue;

                        FindSinglesInSubset(currentSquare, _board.GetRow(currentSquare.GetRowIndex()));
                        FindSinglesInSubset(currentSquare, _board.GetColumn(currentSquare.GetColumnIndex()));
                        FindSinglesInSubset(currentSquare, _board.GetSubBoard(currentSquare.GetSubBoard()));
                    }
                }

                if (squaresRemaining == _board.SquaresRemaining()) continue;

                squaresRemaining = _board.SquaresRemaining();
                Console.WriteLine($"Squares remaining: {_board.SquaresRemaining()}");
                _board.PrintBoard();

            } while (_board.SquaresRemaining() > 0);

            _board.PrintBoard();
        }

        private void InitializeCandidates()
        {
            for (var row = 0; row < 9; row++)
            {
                for (var column = 0; column < 9; column++)
                {
                    var currentSquare = _board.GetBoardSquare(row, column);

                    if (!currentSquare.IsEmpty()) continue;

                    PopulateCandidates(currentSquare);
                    currentSquare.SetValue();
                }
            }
        }

        private void PopulateCandidates(BoardSquare currentSquare)
        {
            var Neighbours = GetNeighbours(currentSquare);

            foreach (var Square in Neighbours)
            {
                Candidates.Remove(Square.GetValue());
            }

            currentSquare.AddCandidates(Candidates.ToList());
        }

        private void FindSinglesInSubset(BoardSquare currentSquare, List<BoardSquare> squareSubset)
        {
            if (!currentSquare.IsEmpty()) return;

            var candidateCounter = currentSquare.GetCandidates()
                .ToDictionary(Candidate => Candidate, Candidate => 0);

            foreach (var square in squareSubset)
            {
                foreach (var candidate in square.GetCandidates())
                {
                    if (candidateCounter.ContainsKey(candidate))
                    {
                        candidateCounter[candidate]++;
                    }
                }
            }

            foreach (var candidate in candidateCounter)
            {
                if (candidate.Value != 1 && !(candidate.Value == 2 & candidateCounter.Count == 2)) continue;
                currentSquare.SetValue(candidate.Key);
                RemoveCandidateFromNeighbours(currentSquare, candidate.Key);
                return;
            }
        }

        private void RemoveCandidateFromNeighbours(BoardSquare currentSquare, int value)
        {
            foreach (var square in GetNeighbours(currentSquare))
            {
                if (square.GetCandidates().Contains(value))
                    square.GetCandidates().Remove(value);
            }
        }

        private IEnumerable<BoardSquare> GetNeighbours(BoardSquare currentSquare)
        {
            var Row = currentSquare.GetRowIndex();
            var Column = currentSquare.GetColumnIndex();
            var SubBoard = currentSquare.GetSubBoard();

            var PlacedValues = new List<BoardSquare>();
            PlacedValues.AddRange(_board.GetRow(Row));
            PlacedValues.AddRange(_board.GetColumn(Column));
            PlacedValues.AddRange(_board.GetSubBoard(SubBoard));
            return PlacedValues;
        }
    }
}
