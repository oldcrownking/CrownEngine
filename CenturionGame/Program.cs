using System;
using CrownEngine;
using System.Collections.Generic;
using CenturionGame.Content;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Reflection;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;

namespace CenturionGame
{
    public static class Centurion
    {
        public static EngineGame game;

        [STAThread]
        static void Main()
        {
            using (var _game = new CenturionGame())
            {
                game = _game;

                _game.Run();
            }
        }
    }
}