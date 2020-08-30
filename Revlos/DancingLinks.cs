using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Revlos
{
    class DancingLinks
    {
        private readonly int size;
        private readonly Board _board;
        private const int constraints = 4;
        private static int listLength;
        private readonly Stopwatch _stopwatch;

        public DancingLinks(Board board)
        {
            _board = board;
            size = board.GetSize();
            listLength = size * size * constraints;
            _stopwatch = new Stopwatch();
        }

        public void Solve()
        {
            _stopwatch.Start();

            var (linkedList, boardSquares) = CalculateMatrix();
            var column = linkedList.Header;
            var solutions = new List<LinkNode<bool>>();

            var results = Search(linkedList, column, solutions);
            foreach (var result in results)
            {
                var square = boardSquares[result.Index];
                _board.SetBoardSquare(square.GetRowIndex(), square.GetColumnIndex(), square.GetValue());
            }
            
            _stopwatch.Stop();

            _board.PrintBoard();
            Console.WriteLine($"Time taken: {_stopwatch.ElapsedMilliseconds}ms\n");
        }

        private static List<LinkNode<bool>> Search(
            DoublyLinkedList<bool> list, ColumnNode<bool> columnNode, List<LinkNode<bool>> solutions)
        {
            if (list.Header.Right == list.Header) 
                return solutions;

            columnNode = GetNextColumn(list);
            Cover(columnNode);

            LinkNode<bool> RowLinkNode = columnNode;

            while (RowLinkNode.Down != columnNode)
            {
                RowLinkNode = RowLinkNode.Down;
                solutions.Add(RowLinkNode);

                var rightNode = RowLinkNode;
                while (rightNode.Right != RowLinkNode)
                {
                    rightNode = rightNode.Right;
                    Cover(rightNode);
                }

                var result = Search(list, columnNode, solutions);

                if (result != null)
                    return result;

                solutions.Remove(RowLinkNode);
                columnNode = RowLinkNode.ColumnNode;

                var leftNode = RowLinkNode;
                while (leftNode.Left != RowLinkNode)
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
            ColumnNode<bool> ChosenNode = null;

            while (node.Right != list.Header)
            {
                node = (ColumnNode<bool>) node.Right;
                if (ChosenNode == null || node.Size < ChosenNode.Size)
                    ChosenNode = node;
            }

            return ChosenNode;
        }

        private static void Cover(LinkNode<bool> linkNode)
        {
            var column = linkNode.ColumnNode;
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
            var column = linkNode.ColumnNode;
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
                            var square = new BoardSquare(row, column, value);

                            matrix.Add(new bool[listLength]);
                            SetMatrixValues(matrix.Last(), square);
                            squares.Add(square);
                        }

                        continue;
                    }

                    matrix.Add(new bool[listLength]);
                    SetMatrixValues(matrix.Last(), boardSquare);
                    squares.Add(boardSquare);
                }
            }
            
            var linkedList = new DoublyLinkedList<bool>(listLength);
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
