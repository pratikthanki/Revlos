
namespace Revlos
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var puzzle in TestPuzzles.puzzles)
                new DancingLinks(puzzle).Solve();
        }
    }
}
