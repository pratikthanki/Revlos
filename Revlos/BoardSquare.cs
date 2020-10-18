
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

        public override string ToString() => _value == 0 ? " " : _value.ToString();
    }
}
