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
            // Choose puzzle from test data
            var puzzle = TestPuzzles.easy;

            var stopwatch = new Stopwatch();
            var board = new Board(puzzle);
            var solver = new Solver(board);

            stopwatch.Start();

            // work in progress!
            // solver.DancingLink();
            solver.Backtrack();

            stopwatch.Stop();

            Console.WriteLine($"Time taken: {stopwatch.Elapsed.Milliseconds} ms");
        }
    }
}
