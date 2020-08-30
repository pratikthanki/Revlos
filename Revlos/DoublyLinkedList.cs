using System.Collections.Generic;

namespace Revlos
{
    class DoublyLinkedList<T>
    {
        public ColumnNode<T> Header { get; } = new ColumnNode<T>(-1);
        private List<ColumnNode<T>> Columns { get; } = new List<ColumnNode<T>>();

        public DoublyLinkedList(int noColumns)
        {
            for (var i = 0; i < noColumns; i++) 
                Columns.Add(new ColumnNode<T>(i));

            Header.Right = Columns[0];
            Columns[0].Left = Header;
            Header.Left = Columns[noColumns - 1];
            Columns[noColumns - 1].Right = Header;

            for (var i = 0; i < noColumns - 1; i++)
            {
                Columns[i].Right = Columns[i + 1];
                Columns[i + 1].Left = Columns[i];
            }
        }

        public void ProcessMatrix(List<bool[]> matrix)
        {
            for (var y = 0; y < matrix.Count; y++)
            {
                var nodes = new List<KeyValuePair<int, LinkNode<T>>>();
                for (var x = 0; x < Columns.Count; x++)
                {
                    if (matrix[y][x])
                        nodes.Add(new KeyValuePair<int, LinkNode<T>>(x, new LinkNode<T>(y)));
                }

                ProcessMatrixRow(nodes);
            }
        }

        private void ProcessMatrixRow(IReadOnlyList<KeyValuePair<int, LinkNode<T>>> nodes)
        {
            for (var i = 0; i < nodes.Count; i++)
            {
                nodes[i].Value.Left = nodes[WrapIndex(i - 1, nodes.Count)].Value;
                nodes[i].Value.Right = nodes[WrapIndex(i + 1, nodes.Count)].Value;

                AddToColumn(nodes[i].Key, nodes[i].Value);
            }
        }

        private static int WrapIndex(int val, int length)
        {
            if (val >= length) 
                return val - length;
            
            if (val < 0) 
                return val + length;

            return val;
        }

        private void AddToColumn(int index, LinkNode<T> linkNode)
        {
            var lowestNode = Columns[index].Up;

            lowestNode.Down = linkNode;
            linkNode.Up = lowestNode;

            Columns[index].Up = linkNode;
            
            linkNode.Down = Columns[index];
            linkNode.ColumnNode = Columns[index];

            Columns[index].IncrementSize();
        }
    }
}
