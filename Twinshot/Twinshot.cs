using System;
using CrownEngine;

namespace Twinshot
{
    public static class Twinshot
    {
        public static EngineGame game;

        [STAThread]
        static void Main()
        {
            using (var _game = new TwinshotGame())
            {
                game = _game;

                _game.Run();
            }
        }
    }
}
