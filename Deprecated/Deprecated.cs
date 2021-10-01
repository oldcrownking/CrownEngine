using CrownEngine;
using System;

namespace Deprecated
{
    public static class Deprecated
    {
        public static EngineGame game;

        [STAThread]
        static void Main()
        {
            using (var _game = new DeprecatedGame())
            {
                game = _game;

                _game.Run();
            }
        }
    }
}
