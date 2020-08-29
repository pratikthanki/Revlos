using System;

namespace Revlos
{
    public class BoardSquare
    {
        private int _value;
        private SubBoard _subBoard;
        private int row;
        private int col;

        public BoardSquare(int value)
        {
            _value = value;
        }

        public int GetValue() => _value;

        public int GetRowIndex() => row;

        public int GetColumnIndex() => col;

        public SubBoard GetSubBoard() => _subBoard;

        public void SetValue(int value) => _value = value;

        public void SetLocation(int rowIndex, int colIndex)
        {
            row = rowIndex;
            col = colIndex;
            _subBoard = GetSubBoard(colIndex, rowIndex);
        }

        public bool IsEmpty() => _value == 0;

        private static SubBoard GetSubBoard(int x, int y)
        {
            if (x < 0 || x > 8 || y < 0 || y > 8) throw new ArgumentOutOfRangeException();
            if (x < 3 && y < 3) return SubBoard.TopLeft;
            if (x > 2 && x < 6 && y < 3) return SubBoard.TopMiddle;
            if (x > 5 && y < 3) return SubBoard.TopRight;
            if (x < 3 && y > 2 && y < 6) return SubBoard.MiddleLeft;
            if (x > 2 && x < 6 && y > 2 && y < 6) return SubBoard.Middle;
            if (x > 5 && y > 2 && y < 6) return SubBoard.MiddleRight;
            if (x < 3 && y > 5) return SubBoard.BottomLeft;
            if (x > 2 && x < 6 && y > 5) return SubBoard.BottomMiddle;

            return SubBoard.BottomRight;
        }
        
        public BoardSquare Clone() => new BoardSquare(this._value);

        public override string ToString() => _value == 0 ? " " : _value.ToString();
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
