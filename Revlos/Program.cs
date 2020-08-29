
namespace Revlos
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var puzzle in TestPuzzles.puzzles)
            {
                var board = new Board(puzzle);
                board.PrintBoard();

                var dancingLinks = new DancingLinks(board);
                dancingLinks.Solve();
            }
        }
    }
}
