using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using System.Diagnostics;
using Twinshot;

namespace Twinshot.Content
{
    public class PlayButton : Actor
    {
        public PlayButton(Vector2 pos, Stage myStage) : base(pos, myStage)
        {
            
        }

        public override void Load()
        {
            SpriteRenderer spriteRenderer = new SpriteRenderer(false, this);

            spriteRenderer.tex = EngineHelpers.GetTexture("PlayButton");

            AddComponent(spriteRenderer);
            AddComponent(new Button(Twinshot.game.mouseState.LeftButton, this));

            width = 18;
            height = 9;
        }

        public override void Update()
        {
            if (rect.Contains(EngineGame.instance.mouseState.Position.ToVector2() / EngineGame.instance.windowScale))
            {
                GetComponent<SpriteRenderer>().frame = new Point(1, 0);
            }
            else
            {
                GetComponent<SpriteRenderer>().frame = new Point(0, 0);
            }

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
