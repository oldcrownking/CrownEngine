using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using CrownEngine.Prefabs;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Deprecated.Content
{
    public class MainMenu : Stage
    {
        public Color blackoutColor = new Color(23, 20, 33);

        public override Color bgColor => blackoutColor;

        public override void PostDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EngineHelpers.GetTexture("DeprecatedLogo"), new Vector2(16, 64), Color.White);

            spriteBatch.Draw(EngineHelpers.GetTexture("PlayButton"), new Vector2(37, 128), new Rectangle(0, (new Rectangle(37, 128, 18, 11).Contains(EngineGame.instance.mousePos) ? 11 : 0), 18, 11), Color.White);

            spriteBatch.Draw(EngineHelpers.GetTexture("QuitButton"), new Vector2(37, 141), new Rectangle(0, (new Rectangle(39, 138, 18, 9).Contains(EngineGame.instance.mousePos) ? 9 : 0), 18, 9), Color.White);

            if (new Rectangle(37, 141, 18, 9).Contains(EngineGame.instance.mousePos) && EngineGame.instance.mouseState.LeftButton == ButtonState.Pressed)
            {
                Deprecated.game.Exit();
            }

            if (music.State == SoundState.Stopped)
            {
                music.Play();
            }


            if (new Rectangle(37, 128, 18, 11).Contains(EngineGame.instance.mousePos) && EngineGame.instance.mouseState.LeftButton == ButtonState.Pressed)
            {
                EngineHelpers.SwitchStages(1);
                music.Stop();
            }

            base.PostDraw(spriteBatch);
        }

        public override void Load()
        {
            base.Load();

            music = EngineHelpers.GetSound("MainTheme").CreateInstance();
            music.Volume = 0.5f;

            music.Play();
        }

        public SoundEffectInstance music;
    }
}