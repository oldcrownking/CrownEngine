using CrownEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace LudumDareFifty
{
    public class Game1 : EngineGame
    {
        public Game1() : base()
        {
            IsMouseVisible = false;
        }

        public override int windowHeight => 256;
        public override int windowWidth => 256;
        public override int windowScale => 2;

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
                new GameStage()
            });

            base.CustomInitialize();
        }

        public override void CustomUpdate()
        {
            base.CustomUpdate();
        }

        public override void InitializeStages(List<Stage> _stages)
        {
            base.InitializeStages(_stages);
        }

        public override void CustomPostDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EngineHelpers.GetTexture("Cursor"), mousePos * windowScale, new Rectangle(0, 0, 7, 10), Color.White, 0f, Vector2.Zero, windowScale, SpriteEffects.None, 0f);

            base.CustomPostDraw(spriteBatch);
        }
    }
}
