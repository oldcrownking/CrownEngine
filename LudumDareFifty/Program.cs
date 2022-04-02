using CrownEngine;
using System;

namespace LudumDareFifty
{
    public static class Program
    {
        public static EngineGame game;

        [STAThread]
        static void Main()
        {
            using (var _game = new Game1())
            {
                game = _game;

                _game.Run();
            }
        }
    }
}
