namespace Revlos
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var puzzle in TestPuzzles.puzzles)
            {
                var dancingLinks = new DancingLinks(puzzle);
                dancingLinks.Solve();

                // var board = new Board(puzzle);
                // var backtrack = new Backtrack(board);
                // backtrack.Solve();
            }
        }
    }
}
