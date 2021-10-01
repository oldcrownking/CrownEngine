using CrownEngine;
using System;

namespace NoiseTests
{
    public static class NoiseTest
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

