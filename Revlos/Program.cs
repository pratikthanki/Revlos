﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Revlos
{
    class Program
    {
        static void Main(string[] args)
        {
            TestSolution();
        }

        public static void TestSolution()
        {
            var rows = new List<string>(new string[]
            {
                "---72---3", 
                "7-5-6---2",
                "---8-1-9-",
                "-7---9---",
                "9---1---4",
                "---4---1-",
                "-4-2-3---",
                "8---4-3-9",
                "2---97---"
            });
            Debug.Assert(rows.Count == 9);

            var solver = new Solver(new Board(rows));
            solver.DancingLink();
        }
    }
}
