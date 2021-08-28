using System;
using CrownEngine;
using System.Collections.Generic;
using Twinshot.Content;
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

namespace Twinshot
{
    public class TwinshotGame : EngineGame
    {
        public TwinshotGame() : base()
        {
            IsMouseVisible = false;
        }

        public override void CustomInitialize()
        {
            foreach (string file in Directory.EnumerateFiles("Content/", "*.png", SearchOption.AllDirectories))
            {
                string fixedPath = file.Substring(Content.RootDirectory.Length).TrimStart(Path.DirectorySeparatorChar);

                instance.Textures[Path.GetFileName(fixedPath)] = Texture2D.FromStream(GraphicsDevice, File.OpenRead(file));
            }

            foreach (string file in Directory.EnumerateFiles("Content/", "*.wav", SearchOption.AllDirectories))
            {
                string fixedPath = file.Substring(Content.RootDirectory.Length).TrimStart(Path.DirectorySeparatorChar);

                //Debug.WriteLine("Found a wav!");

                instance.Audio[Path.GetFileName(fixedPath)] = SoundEffect.FromStream(File.OpenRead(file));
            }

            foreach (string file in Directory.EnumerateFiles("Content/", "*.fx", SearchOption.AllDirectories))
            {
                string fixedPath = file.Substring(Content.RootDirectory.Length).TrimStart(Path.DirectorySeparatorChar);

                //Debug.WriteLine("Found a wav!");

                instance.Effects[Path.GetFileName(fixedPath)] = Content.Load<Effect>(Path.GetFileName(fixedPath));
            }

            InitializeStages(new List<Stage>()
            {
                new MainMenu(),
                new GameStage()
            });

            base.CustomInitialize();
        }

        public override int windowHeight => 128;
        public override int windowWidth => 64;
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

        public override void InitializeStages(List<Stage> _stages)
        {
            base.InitializeStages(_stages);
        }

        public override void CustomPostDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EngineHelpers.GetTexture("Cursor"), mousePos * windowScale, null, Color.White, 0f, Vector2.Zero, windowScale, SpriteEffects.None, 0f);

            base.CustomPostDraw(spriteBatch);
        }
    }
}