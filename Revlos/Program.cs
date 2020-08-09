using System;
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

        private static void TestSolution()
        {
            var rows = new List<string>(new string[]
            {
                "------3--",
                "1--4-----",
                "------1-5",
                "9--------",
                "-----26--",
                "----53---",
                "-5-8-----",
                "---9---7-",
                "-83----4-"
            });
            Debug.Assert(rows.Count == 9);

            var solver = new Solver(new Board(rows));
            solver.DancingLink();
        }
    }
}
