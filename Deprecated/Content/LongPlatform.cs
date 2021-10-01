using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using System.Diagnostics;
using Deprecated;
using Microsoft.Xna.Framework.Input;

namespace Deprecated.Content
{
    public class LongPlatform : Actor
    {
        public LongPlatform(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            AddComponent(new BoxCollider(this));

            width = 32;
            height = 6;
        }


        public override void Update()
        {
            UpdateCollision();

            //if (deathTicks > 60) Kill();
        }

        public void UpdateCollision()
        {
            GetComponent<BoxCollider>().Update();

            //if (Math.Abs(GetComponent<Rigidbody>().velocity.X) * 4f < Math.Abs(GetComponent<Rigidbody>().oldVelocity.X)) EngineHelpers.PlaySound("Thud");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EngineHelpers.GetTexture("LongPlatform"), position - myStage.screenPosition, new Rectangle(0, 0, 32, 6), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            //base.Draw(spriteBatch);
        }
    }
}
