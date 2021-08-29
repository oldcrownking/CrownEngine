using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using System.Diagnostics;
using Twinshot;
using Microsoft.Xna.Framework.Input;

namespace Twinshot.Content
{
    class Boss1Bolt : Actor
    {
        public Boss1Bolt(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            SpriteRenderer spriteRenderer = new SpriteRenderer(false, this);

            spriteRenderer.tex = EngineHelpers.GetTexture("EnemyBolt");

            AddComponent(spriteRenderer);
            AddComponent(new Rigidbody(this));
            AddComponent(new TrailRenderer(this, Color.White, Color.White, Color.White, Color.White, 5, 3));
            AddComponent(new BoxTrigger(this));

            width = 4;
            height = 4;
        }

        public int ticks;
        public override void Update()
        {
            ticks++;

            GetComponent<Rigidbody>().velocity.Y = 2;

            GetComponent<Rigidbody>().velocity.X = (float)Math.Sin(ticks / 10f) / 2f;

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
