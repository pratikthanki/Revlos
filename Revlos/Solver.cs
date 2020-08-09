using System.Collections.Generic;
using System.Linq;

namespace Revlos
{
    public class Solver
    {
        private Board _board;

        public Solver(Board board)
        {
            _board = board;
            InitializeCandidates();
        }

        public void DancingLink()
        {
            do
            {
                for (var row = 0; row < 9; row++)
                {
                    for (var column = 0; column < 9; column++)
                    {
                        var currentSquare = _board.GetBoardSquare(row, column);
                        if (!currentSquare.IsEmpty()) continue;

                        if (currentSquare.GetCandidates().Count == 1)
                        {
                            currentSquare.SetValue();
                            continue;
                        }

                        var rowSubset = _board.GetRow(currentSquare.GetRowIndex());
                        if (FindSinglesInSubset(currentSquare, rowSubset)) continue;

                        var columnSubset = _board.GetColumn(currentSquare.GetColumnIndex());
                        if (FindSinglesInSubset(currentSquare, columnSubset)) continue;

                        var subBoardSubset = _board.GetSubBoard(currentSquare.GetSubBoard());
                        if (FindSinglesInSubset(currentSquare, subBoardSubset)) continue;
                    }
                }
            } while (!_board.IsSolved());

            _board.PrintBoard();
        }

        public bool Backtrack()
        {
            for (var row = 0; row < 9; row++)
            {
                for (var column = 0; column < 9; column++)
                {
                    var currentSquare = _board.GetBoardSquare(row, column);
                    if (!currentSquare.IsEmpty()) continue;

                    for (var number = 1; number <= 9; number++)
                    {
                        var placedValues = GetNeighbours(currentSquare);

                        if (!IsValidMove(placedValues, number)) continue;
                        currentSquare.SetValue(number);

                        if (Backtrack()) return true;
                        currentSquare.SetValue(0);
                    }

                    return false;
                }
            }

            _board.PrintBoard();
            return true;
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
            var candidates = new HashSet<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9};
            foreach (var square in GetNeighbours(currentSquare))
                candidates.Remove(square.GetValue());

            currentSquare.AddCandidates(candidates.ToList());
        }

        private bool FindSinglesInSubset(BoardSquare currentSquare, IEnumerable<BoardSquare> squareSubset)
        {
            if (!currentSquare.IsEmpty()) return false;

            var candidateCounter = currentSquare.GetCandidates()
                .ToDictionary(Candidate => Candidate, Candidate => 0);

            foreach (var square in squareSubset)
            {
                foreach (var candidate in square.GetCandidates())
                {
                    if (candidateCounter.ContainsKey(candidate)) candidateCounter[candidate]++;
                }
            }

            foreach (var candidate in candidateCounter)
            {
                // If a possible candidate has a count of 1 OR;
                // a count of 2 and there are only two candidates to choose from then set value
                if (candidate.Value == 1 || candidate.Value == 2 & candidateCounter.Count == 2)
                {
                    currentSquare.SetValue(candidate.Key);
                    RemoveCandidateFromNeighbours(currentSquare, candidate.Key);
                    return true;
                }
            }

            return false;
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

        private static bool IsValidMove(IEnumerable<BoardSquare> placedValues, int number)
        {
            return placedValues.All(value => !value.GetValue().Equals(number));
        }
    }
}
