using CrownEngine;
using System;

namespace Sandbox
{
    public static class Sandbox
    {
        public static EngineGame game;

        [STAThread]
        static void Main()
        {
            using (var _game = new SandboxGame())
            {
                game = _game;

                _game.Run();
            }
        }
    }
}
