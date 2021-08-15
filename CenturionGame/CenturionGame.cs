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
    public class CenturionGame : EngineGame
    {
        public CenturionGame() : base()
        {
            IsMouseVisible = false;
        }

        public override void CustomInitialize()
        {
            foreach (string file in Directory.EnumerateFiles("Content/", "*.png", SearchOption.AllDirectories))
            {
                string fixedPath = file.Substring(Content.RootDirectory.Length).TrimStart(Path.DirectorySeparatorChar);

                EngineGame.instance.Textures[Path.GetFileName(fixedPath)] = Texture2D.FromStream(GraphicsDevice, File.OpenRead(file));
            }

            InitializeStages(new List<Stage>()
            {
                new MainMenu(),
                new Sandbox()
            });

            base.CustomInitialize();
        }

        public override int windowHeight => 180;
        public override int windowWidth => 320;
        public override int windowScale => 2;

        public override void CustomUpdate()
        {
            /*if (keyboardState.IsKeyDown(Keys.OemPlus) && windowScale < 6)
            {
                windowScale++;
            }
            if (keyboardState.IsKeyDown(Keys.OemMinus) && windowScale > 1)
            {
                windowScale--;
            }*/

            base.CustomUpdate();
        }

        public override void CustomDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EngineHelpers.GetTexture("Cursor"), mousePos * windowScale, null, Color.White, 0f, Vector2.Zero, windowScale, SpriteEffects.None, 0f);

            base.CustomDraw(spriteBatch);
        }

        public override void InitializeStages(List<Stage> _stages)
        {
            base.InitializeStages(_stages);
        }
    }
}