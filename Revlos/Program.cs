using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Revlos
{
    class Program
    {
        static void Main(string[] args)
        {
            var row1 = "---72---3";
            var row2 = "7-5-6---2";
            var row3 = "---8-1-9-";
            var row4 = "-7---9---";
            var row5 = "9---1---4";
            var row6 = "---4---1-";
            var row7 = "-4-2-3---";
            var row8 = "8---4-3-9";
            var row9 = "2---97---";

            var rows = new List<string>(new string[] {row1, row2, row3, row4, row5, row6, row7, row8, row9});
            Debug.Assert(rows.Count == 9);

            var board = new Board(rows);
            board.PrintBoard();

        }
    }
}
