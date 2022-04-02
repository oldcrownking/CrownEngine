using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using System.Diagnostics;
using LudumDareFifty;

namespace LudumDareFifty.Content
{
    public class Sawblade : Actor
    {
        public Sawblade(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            AddComponent(new Rigidbody(this));
            AddComponent(new BoxTrigger(this));

            //GetComponent<Rigidbody>().velocity = new Vector2(EngineHelpers.NextFloat(-0.5f, 0.5f), 0);
            //GetComponent<Rigidbody>().gravityForce = 0.05f;
            //GetComponent<Rigidbody>().gravityDir = Vector2.UnitY;

            width = 32;
            height = 32;
        }

        public float rotation;

        public override void Update()
        {
            rotation += 0.075f;

            position.Y -= 1f;

            base.Update();
        }

        public override void PostDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EngineHelpers.GetTexture("Sawblade"), position + new Vector2(-16, -16) - myStage.screenPosition, null, Color.White, rotation, new Vector2(16, 16), 1f, SpriteEffects.None, 0f);

            base.Draw(spriteBatch);
        }
    }
}
