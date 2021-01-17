using System;

namespace Revlos
{
    public class BoardSquare
    {
        private int _value;
        private readonly int _row;
        private readonly int _column;

        public BoardSquare(int row, int column, int value)
        {
            _value = value;
            _row = row;
            _column = column;
        }

        public int GetValue() => _value;

        public int GetRowIndex() => _row;

        public int GetColumnIndex() => _column;

        public void SetValue(int value) => _value = value;

        public bool IsEmpty() => _value == 0;

        public SubBoard GetSubBoard(int x, int y)
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

        public override string ToString() => _value == 0 ? " " : _value.ToString();
    }
}
