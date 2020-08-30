namespace Revlos
{
    internal class LinkNode<T>
    {
        private ColumnNode<T> _columnNode;
        private LinkNode<T> left, right, up, down;
        private T val;
        private readonly int index;

        public LinkNode(int index)
        {
            this.index = index;
        }

        public void RemoveVertical()
        {
            Up.Down = Down;
            Down.Up = Up;

            ColumnNode.DecrementSize();
        }

        public void RemoveHorizontal()
        {
            Right.Left = Left;
            Left.Right = Right;
        }

        public void ReplaceVertical()
        {
            Up.Down = this;
            Down.Up = this;

            ColumnNode.IncrementSize();
        }

        public void ReplaceHorizontal()
        {
            Right.Left = this;
            Left.Right = this;
        }

        public LinkNode<T> Left
        {
            get => left;
            set => left = value;
        }

        public LinkNode<T> Right
        {
            get => right;
            set => right = value;
        }

        public LinkNode<T> Up
        {
            get => up;
            set => up = value;
        }

        public LinkNode<T> Down
        {
            get => down;
            set => down = value;
        }

        public ColumnNode<T> ColumnNode
        {
            get => _columnNode;
            set => _columnNode = value;
        }

        public int Index => index;

        public T Value
        {
            get => val;
            set => val = value;
        }
    }

    internal class ColumnNode<T> : LinkNode<T>
    {
        private readonly int id;
        
        internal void IncrementSize() => Size++;
        
        internal void DecrementSize() => Size--;

        public int ID => id;

        public int Size { get; private set; } = 0;

        public ColumnNode(int id) : base(-1)
        {
            this.id = id;
            Up = this;
            Down = this;
            ColumnNode = this;
        }
    }
}
