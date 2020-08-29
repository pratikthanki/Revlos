namespace Revlos
{
    internal class LinkNode<T>
    {
        private ColumnLinkNode<T> _columnLinkNode;
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

            ColumnLinkNode.DecrementSize();
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

            ColumnLinkNode.IncrementSize();
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

        public ColumnLinkNode<T> ColumnLinkNode
        {
            get => _columnLinkNode;
            set => _columnLinkNode = value;
        }

        public int Index => index;

        public T Value
        {
            get => val;
            set => val = value;
        }
    }

    internal class ColumnLinkNode<T> : LinkNode<T>
    {
        private readonly int id;
        
        internal void IncrementSize() => Size++;
        
        internal void DecrementSize() => Size--;

        public int ID => id;

        public int Size { get; private set; } = 0;

        public ColumnLinkNode(int id) : base(-1)
        {
            this.id = id;
            Up = this;
            Down = this;
            ColumnLinkNode = this;
        }
    }
}
