namespace Revlos
{
    internal class LinkNode<T>
    {
        public LinkNode(int index)
        {
            Index = index;
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

        public LinkNode<T> Left { get; set; }

        public LinkNode<T> Right { get; set; }

        public LinkNode<T> Up { get; set; }

        public LinkNode<T> Down { get; set; }

        public ColumnNode<T> ColumnNode { get; set; }

        public int Index { get; }
    }

    internal class ColumnNode<T> : LinkNode<T>
    {
        internal void IncrementSize() => Size++;
        
        internal void DecrementSize() => Size--;

        public int Size { get; private set; }

        public ColumnNode() : base(-1)
        {
            Up = this;
            Down = this;
            ColumnNode = this;
        }
    }
}
