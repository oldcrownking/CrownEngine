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
    public class RoboforgePlatform : Actor
    {
        public RoboforgePlatform(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public Roboforge forge;

        public override void Load()
        {
            AddComponent(new BoxCollider(this));

            forge = new Roboforge(new Vector2(position.X + 7, position.Y - 10), myStage);

            myStage.AddActor(forge);

            width = 24;
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
            spriteBatch.Draw(EngineHelpers.GetTexture("RoboforgePlatform"), position - myStage.screenPosition, new Rectangle(0, 0, 24, 6), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            //base.Draw(spriteBatch);
        }
    }
}
