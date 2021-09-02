using System;
using CrownEngine;

namespace Roll
{
    public static class Roll
    {
        public static EngineGame game;

        [STAThread]
        static void Main()
        {
            using (var _game = new RollGame())
            {
                game = _game;

                _game.Run();
            }
        }
    }
}
