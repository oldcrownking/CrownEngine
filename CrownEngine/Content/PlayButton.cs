using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine.Engine;
using System.Diagnostics;

namespace CrownEngine.Content
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
            AddComponent(new Button(EngineGame.instance.mouseState.LeftButton, this));

            width = 12;
            height = 13;
        }

        public override void Update()
        {
            if (GetComponent<Button>().pressed)
            {
                GetComponent<SpriteRenderer>().frame = new Point(0, 1);
            }
            else
            {
                GetComponent<SpriteRenderer>().frame = new Point(0, 0);
            }

            if (GetComponent<Button>().pressed && EngineGame.instance.mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                EngineHelpers.SwitchStages(1);
            }

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
