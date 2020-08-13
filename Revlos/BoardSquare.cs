using System;
using System.Collections.Generic;
using System.Linq;

namespace Revlos
{
    public class BoardSquare
    {
        private readonly HashSet<int> _candidates = new HashSet<int>(9);
        private int _value;
        private SubBoard _subBoard;
        public int row;
        public int col;

        public BoardSquare(int value)
        {
            _value = value;
        }

        public BoardSquare(int r, int c)
        {
            row = r;
            col = c;
            SetLocation(r, c);
        }

        public bool IsEmpty()
        {
            return _value == 0;
        }

        public void SetValue(int value)
        {
            if (_candidates.Count > 0)
                _candidates.Clear();

            _value = value;
        }

        public void SetValue()
        {
            if (_candidates.Count == 1)
                SetValue(_candidates.First());
        }

        public void SetLocation(int rowIndex, int colIndex)
        {
            this.row = rowIndex;
            this.col = colIndex;
            _subBoard = GetSubBoard(colIndex, rowIndex);
        }

        public void AddCandidates(int value)
        {
            _candidates.Add(value);
        }

        public void AddCandidates(IEnumerable<int> values)
        {
            foreach (var value in values)
                AddCandidates(value);
        }

        public void RemoveCandidates(int value)
        {
            if (_candidates.Count > 0)
            {
                _candidates.Remove(value);
            }
        }

        public HashSet<int> GetCandidates()
        {
            return _candidates;
        }

        public int GetValue()
        {
            return _value;
        }

        public int GetRowIndex()
        {
            return row;
        }

        public int GetColumnIndex()
        {
            return col;
        }

        public SubBoard GetSubBoard()
        {
            return _subBoard;
        }

        private static SubBoard GetSubBoard(int x, int y)
        {
            if (x < 0 || x > 8 || y < 0 || y > 8)
                throw new ArgumentOutOfRangeException();

            if (x < 3 && y < 3)
                return SubBoard.TopLeft;

            if (x > 2 && x < 6 && y < 3)
                return SubBoard.TopMiddle;

            if (x > 5 && y < 3)
                return SubBoard.TopRight;

            if (x < 3 && y > 2 && y < 6)
                return SubBoard.MiddleLeft;

            if (x > 2 && x < 6 && y > 2 && y < 6)
                return SubBoard.Middle;

            if (x > 5 && y > 2 && y < 6)
                return SubBoard.MiddleRight;

            if (x < 3 && y > 5)
                return SubBoard.BottomLeft;

            if (x > 2 && x < 6 && y > 5)
                return SubBoard.BottomMiddle;

            return SubBoard.BottomRight;
        }

        public override string ToString()
        {
            return _value == 0 ? " " : _value.ToString();
        }
    }

    public enum SubBoard
    {
        TopLeft,
        TopMiddle,
        TopRight,
        MiddleLeft,
        Middle,
        MiddleRight,
        BottomLeft,
        BottomMiddle,
        BottomRight
    }
}
