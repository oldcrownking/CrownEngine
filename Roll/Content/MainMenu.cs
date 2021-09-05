using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;

namespace Roll.Content
{
    public class MainMenu : Stage
    {
        public override Color bgColor => Color.Black;

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            (EngineGame.instance as RollGame).DrawString(spriteBatch, "Hello World 90 AZ az", new Vector2(4, 4), 4, 0, 0f, 0f);

            base.Draw(spriteBatch);
        }

        public override void Load()
        {

        }
    }
}