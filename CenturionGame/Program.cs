using System;
using CrownEngine.Engine;
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
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new EngineGame())
            {
                foreach (string file in Directory.EnumerateFiles("Content/", "*.png", SearchOption.AllDirectories))
                {
                    string fixedPath = file.Substring(game.Content.RootDirectory.Length).TrimStart(Path.DirectorySeparatorChar);
                    game.Textures[Path.GetFileName(fixedPath)] = Texture2D.FromStream(game.GraphicsDevice, File.OpenRead(file));
                }

                foreach (KeyValuePair<string, Texture2D> str in game.Textures)
                {
                    Debug.WriteLine(str.Value);
                }

                game.InitializeStages(new List<Stage>()
                {
                    new MainMenu(),
                    new Sandbox()
                });

                game.Run();
            }
        }
    }
}