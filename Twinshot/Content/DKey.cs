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
    public class DKey : Actor
    {
        public DKey(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            SpriteRenderer spriteRenderer = new SpriteRenderer(true, this);

            spriteRenderer.tex = EngineHelpers.GetTexture("DKey");

            AddComponent(spriteRenderer);
            AddComponent(new Rigidbody(this));

            GetComponent<Rigidbody>().velocity = new Vector2(-1f, 0);

            width = 9;
            height = 9;
        }

        public int ticks;
        public int frame;
        public bool keyPressed = false;
        public override void Update()
        {
            if (EngineGame.instance.keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D))
            {
                keyPressed = true;
            }

            if (!keyPressed) GetComponent<Rigidbody>().velocity *= 0.95f;

            if (keyPressed) 
            { 
                GetComponent<Rigidbody>().velocity.X += 0.1f;
                if (position.X >= 64) 
                {
                    (myStage as GameStage).wave++;

                    Kill();
                }
            }

            base.Update();
        }
    }
}
