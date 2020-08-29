using System;
using System.Collections.Generic;
using System.Linq;

namespace Revlos
{
    class DancingLinks
    {
        private readonly int size;
        private readonly Board _board;

        public DancingLinks(Board board)
        {
            _board = board;
            size = board.GetSize();
        }

        public void Solve()
        {
            var (linkedList, boardSquares) = CalculateMatrix();
            var column = linkedList.Header;
            var solutions = new List<Node<bool>>();

            var results = Search(linkedList, column, solutions);
            foreach (var result in results)
            {
                var square = boardSquares[result.Index];
                _board.SetBoardSquare(square.GetRowIndex(), square.GetColumnIndex(), square.GetValue());
            }

            _board.PrintBoard();
        }

        private static List<Node<bool>> Search(
            DoublyLinkedList<bool> list, ColumnNode<bool> columnNode, List<Node<bool>> solutions)
        {
            if (list.Header.Right == list.Header) 
                return solutions;

            columnNode = GetNextColumn(list);
            Cover(columnNode);

            Node<bool> rowNode = columnNode;

            while (rowNode.Down != columnNode)
            {
                rowNode = rowNode.Down;
                solutions.Add(rowNode);

                var rightNode = rowNode;
                while (rightNode.Right != rowNode)
                {
                    rightNode = rightNode.Right;
                    Cover(rightNode);
                }

                var result = Search(list, columnNode, solutions);

                if (result != null)
                    return result;

                solutions.Remove(rowNode);
                columnNode = rowNode.ColumnNode;

                var leftNode = rowNode;
                while (leftNode.Left != rowNode)
                {
                    leftNode = leftNode.Left;

                    Uncover(leftNode);
                }
            }

            Uncover(columnNode);

            return null;
        }

        private static ColumnNode<bool> GetNextColumn(DoublyLinkedList<bool> list)
        {
            var node = list.Header;
            ColumnNode<bool> chosenNode = null;

            while (node.Right != list.Header)
            {
                node = (ColumnNode<bool>) node.Right;
                if (chosenNode == null || node.Size < chosenNode.Size)
                    chosenNode = node;
            }

            return chosenNode;
        }

        private static void Cover(Node<bool> node)
        {
            var column = node.ColumnNode;
            column.RemoveHorizontal();

            Node<bool> verticalNode = column;
            while (verticalNode.Down != column)
            {
                verticalNode = verticalNode.Down;

                var removeNode = verticalNode;
                while (removeNode.Right != verticalNode)
                {
                    removeNode = removeNode.Right;
                    removeNode.RemoveVertical();
                }
            }
        }

        private static void Uncover(Node<bool> node)
        {
            var column = node.ColumnNode;
            Node<bool> verticalNode = column;

            while (verticalNode.Up != column)
            {
                verticalNode = verticalNode.Up;

                var removeNode = verticalNode;
                while (removeNode.Left != verticalNode)
                {
                    removeNode = removeNode.Left;
                    removeNode.ReplaceVertical();
                }
            }

            column.ReplaceHorizontal();
        }

        private (DoublyLinkedList<bool> linkedList, List<BoardSquare> squares) CalculateMatrix()
        {
            var matrix = new List<bool[]>();
            var squares = new List<BoardSquare>();

            for (var row = 0; row < size; row++)
            {
                for (var column = 0; column < size; column++)
                {
                    var boardSquare = _board.GetBoardSquare(row, column);
                    if (boardSquare.IsEmpty())
                    {
                        for (var value = 1; value <= size; value++)
                        {
                            var square = boardSquare.Clone();
                            square.SetValue(value);
                            square.SetLocation(row, column);

                            matrix.Add(new bool[size * size * 4]);
                            SetMatrixValues(matrix.Last(), square);
                            squares.Add(square);
                        }

                        continue;
                    }

                    matrix.Add(new bool[size * size * 4]);
                    SetMatrixValues(matrix.Last(), boardSquare);
                    squares.Add(boardSquare);
                }
            }
            
            var linkedList = new DoublyLinkedList<bool>(size * size * 4);
            linkedList.ProcessMatrix(matrix);

            return (linkedList, squares);
        }

        private void SetMatrixValues(IList<bool> mRow, BoardSquare boardSquare)
        {
            var row = boardSquare.GetRowIndex();
            var column = boardSquare.GetColumnIndex();
            var value = boardSquare.GetValue();

            var positionConstraint = row * size + column;
            var rowConstraint = size * size + row * size + (value - 1);
            var columnConstraint = size * size * 2 + column * size + (value - 1);
            var regionSize = (int) Math.Sqrt(size);
            var regionNum =
                (int) Math.Floor(row / (double) regionSize) * regionSize +
                (int) Math.Floor(column / (double) regionSize);

            var regionConstraint = size * size * 3 + regionNum * size + (value - 1);

            mRow[positionConstraint] = true;
            mRow[rowConstraint] = true;
            mRow[columnConstraint] = true;
            mRow[regionConstraint] = true;
        }
    }
}
