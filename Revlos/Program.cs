using System;
using System.Diagnostics;
using System.Linq;

namespace Revlos
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var puzzle in TestPuzzles.puzzles.Take(1))
            {
                new Backtrack(new Board(puzzle)).Solve();
                // new DancingLinks(new Board(puzzle)).Solve();
            }
        }
    }
}
