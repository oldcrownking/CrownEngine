using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;

namespace CenturionGame.Content
{
    public class MainMenu : Stage
    {
        public override Color bgColor => Color.White;

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Load()
        {
            Actor logo = new Actor(new Vector2(2, 5), this); //yea?

            logo.width = 60;
            logo.height = 25;

            logo.AddComponent(new SpriteRenderer(false, logo));
            logo.GetComponent<SpriteRenderer>().tex = EngineHelpers.GetTexture("GravityGambit");

            AddActor(logo);

            AddActor(new PlayButton(new Vector2(8, 40), this));
        }
    }
}
