
namespace Revlos
{
    public class BoardSquare
    {
        private int _value;
        private int row;
        private int col;

        public BoardSquare(int value)
        {
            _value = value;
        }

        public int GetValue() => _value;

        public int GetRowIndex() => row;

        public int GetColumnIndex() => col;

        public void SetValue(int value) => _value = value;

        public void SetLocation(int rowIndex, int colIndex)
        {
            row = rowIndex;
            col = colIndex;
        }

        public bool IsEmpty() => _value == 0;

        public BoardSquare Clone() => new BoardSquare(this._value);

        public override string ToString() => _value == 0 ? " " : _value.ToString();
    }
}
