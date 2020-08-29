namespace Revlos
{
    internal class Node<T>
    {
        private ColumnNode<T> columnNode;
        private Node<T> left, right, up, down;
        private T val;
        private readonly int index;

        public Node(int index)
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

        public Node<T> Left
        {
            get => left;
            set => left = value;
        }

        public Node<T> Right
        {
            get => right;
            set => right = value;
        }

        public Node<T> Up
        {
            get => up;
            set => up = value;
        }

        public Node<T> Down
        {
            get => down;
            set => down = value;
        }

        public ColumnNode<T> ColumnNode
        {
            get => columnNode;
            set => columnNode = value;
        }

        public int Index => index;

        public T Value
        {
            get => val;
            set => val = value;
        }
    }

    internal class ColumnNode<T> : Node<T>
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
