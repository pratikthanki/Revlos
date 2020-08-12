using System;
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
            var board = new Board(TestPuzzles.easy);
            var stopwatch = new Stopwatch();

            var backtrack = new Backtrack(board);
            stopwatch.Start();
            backtrack.Solve();
            stopwatch.Stop();
            Console.WriteLine($"Backtrack time: {stopwatch.Elapsed.Milliseconds} ms");

            var dancingLinks = new DancingLinks(board);
            stopwatch.Restart();
            dancingLinks.Solve();
            stopwatch.Stop();
            Console.WriteLine($"Dancing links time: {stopwatch.Elapsed.Milliseconds} ms");
        }
    }
}
