using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using System.Diagnostics;
using Deprecated;

namespace Deprecated.Content
{
    public class HorizontalSawblade : Actor
    {
        public HorizontalSawblade(Vector2 pos, Stage myStage, Vector2 direction) : base(pos, myStage)
        {
            dir = direction;
        }

        public override void Load()
        {
            AddComponent(new Rigidbody(this));
            AddComponent(new BoxTrigger(this));
            AddComponent(new TrailRenderer(this, (myStage as GameStage).blackoutColor, (myStage as GameStage).blackoutColor, (myStage as GameStage).blackoutColor, (myStage as GameStage).blackoutColor, 48, 7));

            GetComponent<TrailRenderer>().offset = new Vector2(-16, -16);

            //GetComponent<Rigidbody>().velocity = new Vector2(EngineHelpers.NextFloat(-0.5f, 0.5f), 0);
            //GetComponent<Rigidbody>().gravityForce = 0.05f;
            //GetComponent<Rigidbody>().gravityDir = Vector2.UnitY;

            width = 16;
            height = 16;
        }

        public float rotation;

        public Vector2 dir;

        public int timer;

        public override void Update()
        {
            if (timer == 60)
            {
                GetComponent<Rigidbody>().velocity = dir;
                EngineHelpers.PlaySound("ShootSawblade");
            }

            if (timer > 60)
            {
                rotation += 0.075f * dir.X;

                GetComponent<Rigidbody>().velocity.X *= 1.02f;
            }


            if (timer >= 300) Kill();

            base.Update();
        }

        public override void PostDraw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            timer++;

            if(timer < 60)
            {
                spriteBatch.Draw(EngineHelpers.GetTexture("SawbladeWarning"), new Vector2(position.X + (dir.X < 0 ? -40 : 12), position.Y - 8) - myStage.screenPosition, new Rectangle(0, (int)((timer / 10f) % 2) * 12, 12, 12), Color.White);
            }

            spriteBatch.Draw(EngineHelpers.GetTexture("HorizontalSawblade"), position + new Vector2(-8, -8) - myStage.screenPosition, null, Color.White, rotation, new Vector2(8, 8), 1f, SpriteEffects.None, 0f);
        }
    }
}
