using System;

namespace CrownEngine.Engine
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new EngineGame())
                game.Run();
        }
    }
}
