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
    public class MediumPlatform : Actor
    {
        public MediumPlatform(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            AddComponent(new BoxCollider(this));

            width = 24;
            height = 6;
        }


        public override void Update()
        {
            UpdateCollision();

            if (deathTicks > 0) deathTicks++;

            if (deathTicks > 240)
            {
                EngineHelpers.PlaySound("PlatformExplode");

                myStage.AddActor(new SmokeBurst(Center, myStage));

                Kill();
            }
        }

        public void UpdateCollision()
        {
            GetComponent<BoxCollider>().Update();

            for (int i = 0; i < myStage.actors.Count; i++)
            {
                if (myStage.actors[i] != null && myStage.actors[i].HasComponent<BoxCollider>() && myStage.actors[i] is Player)
                {
                    BoxCollider playerCollider = myStage.actors[i].GetComponent<BoxCollider>();
                    Rectangle playerRect = myStage.actors[i].rect;

                    if ((myStage.actors[i].GetComponent<Rigidbody>().velocity.X > 0 && EngineHelpers.IsTouchingLeft(playerRect, rect, myStage.actors[i].GetComponent<Rigidbody>().velocity)) ||
                        (myStage.actors[i].GetComponent<Rigidbody>().velocity.X < 0 && EngineHelpers.IsTouchingRight(playerRect, rect, myStage.actors[i].GetComponent<Rigidbody>().velocity)))
                        Crumble();

                    if ((myStage.actors[i].GetComponent<Rigidbody>().velocity.Y > 0 && EngineHelpers.IsTouchingTop(playerRect, rect, myStage.actors[i].GetComponent<Rigidbody>().velocity)) ||
                        (myStage.actors[i].GetComponent<Rigidbody>().velocity.Y < 0 && EngineHelpers.IsTouchingBottom(playerRect, rect, myStage.actors[i].GetComponent<Rigidbody>().velocity)))
                        Crumble();
                }
            }

            //if (Math.Abs(GetComponent<Rigidbody>().velocity.X) * 4f < Math.Abs(GetComponent<Rigidbody>().oldVelocity.X)) EngineHelpers.PlaySound("Thud");
        }

        public int deathTicks = 0;
        public void Crumble()
        {
            //EngineHelpers.PlaySound("Crumble");

            deathTicks++;
        }

        public Vector2 crumbleOffset = Vector2.Zero;
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (deathTicks > 120)
            {
                crumbleOffset += new Vector2(EngineHelpers.NextFloat(-1, 1), EngineHelpers.Next(-1, 1));

                crumbleOffset = new Vector2(MathHelper.Clamp(crumbleOffset.X, -1, 1), MathHelper.Clamp(crumbleOffset.Y, -1, 1));
            }

            spriteBatch.Draw(EngineHelpers.GetTexture("MediumPlatform"), position - myStage.screenPosition + crumbleOffset, new Rectangle(0, (deathTicks > 120 ? 6 : 0), 24, 6), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            //base.Draw(spriteBatch);
        }
    }
}
