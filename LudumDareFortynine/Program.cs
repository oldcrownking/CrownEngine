using System;

namespace LudumDareFortynine
{
    public static class LudumDareFortynine
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
}
