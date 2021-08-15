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
            Actor logo = new Actor(new Vector2(20, 20), this);

            logo.width = 107;
            logo.height = 20;

            logo.AddComponent(new SpriteRenderer(false, logo));
            logo.GetComponent<SpriteRenderer>().tex = EngineHelpers.GetTexture("GravityGambit");

            AddActor(logo);

            AddActor(new PlayButton(new Vector2(8, 40), this));
        }
    }
}
