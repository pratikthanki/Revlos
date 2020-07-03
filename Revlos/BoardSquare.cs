using System;
using System.Collections.Generic;

namespace Revlos
{
    public class BoardSquare
    {
        private readonly HashSet<int> _possibleValues = new HashSet<int>(9);
        private int? _value;
        private int _column;
        private int _row;
        private SubBoard _subBoard;

        public BoardSquare(int? value)
        {
            _value = value;
        }
        
        public BoardSquare()
        {
            _value = null;
        }

        public bool IsSolved()
        {
            return _value != null;
        }

        public void SetValue(int? value)
        {
            if (_possibleValues.Count > 0)
            {
                _possibleValues.Clear();
                _value = value;
            }
            else
            {
                _value = value;
            }
        }

        public void SetLocation(int row, int column)
        {
            _row = row;
            _column = column;
            _subBoard = GetSubBoard(column, row);
        }

        public void AddPossibleValue(int value)
        {
            _possibleValues.Add(value);
        }

        public void RemovePossibleValue(int value)
        {
            if (_possibleValues.Count > 0)
            {
                _possibleValues.Remove(value);
            }
        }

        public HashSet<int> GetPossibleValues()
        {
            return _possibleValues;
        }

        public int? GetValue()
        {
            return _value;
        }

        public int GetRowIndex()
        {
            return _row;
        }

        public int GetColumnIndex()
        {
            return _column;
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
            return _value == null ? " " : _value.ToString();
        }
    }
}
