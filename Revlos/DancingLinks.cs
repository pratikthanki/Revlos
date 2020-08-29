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
            var solutions = new List<LinkNode<bool>>();

            var results = Search(linkedList, column, solutions);
            foreach (var result in results)
            {
                var square = boardSquares[result.Index];
                _board.SetBoardSquare(square.GetRowIndex(), square.GetColumnIndex(), square.GetValue());
            }

            _board.PrintBoard();
        }

        private static List<LinkNode<bool>> Search(
            DoublyLinkedList<bool> list, ColumnLinkNode<bool> columnLinkNode, List<LinkNode<bool>> solutions)
        {
            if (list.Header.Right == list.Header) 
                return solutions;

            columnLinkNode = GetNextColumn(list);
            Cover(columnLinkNode);

            LinkNode<bool> RowLinkNode = columnLinkNode;

            while (RowLinkNode.Down != columnLinkNode)
            {
                RowLinkNode = RowLinkNode.Down;
                solutions.Add(RowLinkNode);

                var rightNode = RowLinkNode;
                while (rightNode.Right != RowLinkNode)
                {
                    rightNode = rightNode.Right;
                    Cover(rightNode);
                }

                var result = Search(list, columnLinkNode, solutions);

                if (result != null)
                    return result;

                solutions.Remove(RowLinkNode);
                columnLinkNode = RowLinkNode.ColumnLinkNode;

                var leftNode = RowLinkNode;
                while (leftNode.Left != RowLinkNode)
                {
                    leftNode = leftNode.Left;

                    Uncover(leftNode);
                }
            }

            Uncover(columnLinkNode);

            return null;
        }

        private static ColumnLinkNode<bool> GetNextColumn(DoublyLinkedList<bool> list)
        {
            var node = list.Header;
            ColumnLinkNode<bool> ChosenLinkNode = null;

            while (node.Right != list.Header)
            {
                node = (ColumnLinkNode<bool>) node.Right;
                if (ChosenLinkNode == null || node.Size < ChosenLinkNode.Size)
                    ChosenLinkNode = node;
            }

            return ChosenLinkNode;
        }

        private static void Cover(LinkNode<bool> linkNode)
        {
            var column = linkNode.ColumnLinkNode;
            column.RemoveHorizontal();

            LinkNode<bool> VerticalLinkNode = column;
            while (VerticalLinkNode.Down != column)
            {
                VerticalLinkNode = VerticalLinkNode.Down;

                var removeNode = VerticalLinkNode;
                while (removeNode.Right != VerticalLinkNode)
                {
                    removeNode = removeNode.Right;
                    removeNode.RemoveVertical();
                }
            }
        }

        private static void Uncover(LinkNode<bool> linkNode)
        {
            var column = linkNode.ColumnLinkNode;
            LinkNode<bool> VerticalLinkNode = column;

            while (VerticalLinkNode.Up != column)
            {
                VerticalLinkNode = VerticalLinkNode.Up;

                var removeNode = VerticalLinkNode;
                while (removeNode.Left != VerticalLinkNode)
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
