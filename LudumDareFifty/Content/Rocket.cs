using CrownEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using LudumDareFifty.Content;

namespace LudumDareFifty.Content
{
    public class Rocket : Actor
    {
        public Rocket(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            AddComponent(new BoxTrigger(this));
            AddComponent(new Rigidbody(this));

            GetComponent<Rigidbody>().gravityDir = Vector2.UnitY;
            GetComponent<Rigidbody>().gravityForce = 0f;

            GetComponent<Rigidbody>().velocity.Y = 3f;

            width = 7;
            height = 7;
        }

        public Vector2 oldPosition;
        public int ticks;
        public override void Update()
        {
            GetComponent<Rigidbody>().velocity.Y = 3f;

            ticks++;

            if (ticks > 150)
                Kill();

            for (int i = 0; i < myStage.actors.Count; i++)
            {
                if (myStage.actors[i] != null && myStage.actors[i].HasComponent<TileCollider>() && HasComponent<Rigidbody>())
                {
                    for (int j = 0; j < myStage.actors[i].GetComponent<TileCollider>().rectangles.Count; j++)
                    {
                        TileCollider grid = myStage.actors[i].GetComponent<TileCollider>();
                        Rectangle tileRect = grid.rectangles[j];

                        Point temp = new Point(height / 2, height / 2);
                        Rectangle playerRect = new Rectangle((int)Center.X - temp.X, (int)Center.Y - temp.Y, height, height);

                        if ((GetComponent<Rigidbody>().velocity.X > 0 && EngineHelpers.IsTouchingLeft(playerRect, tileRect, GetComponent<Rigidbody>().velocity)) ||
                            (GetComponent<Rigidbody>().velocity.X < 0 && EngineHelpers.IsTouchingRight(playerRect, tileRect, GetComponent<Rigidbody>().velocity)))
                            GetComponent<Rigidbody>().velocity.X = 0;

                        if ((GetComponent<Rigidbody>().velocity.Y > 0 && EngineHelpers.IsTouchingTop(playerRect, tileRect, GetComponent<Rigidbody>().velocity)) ||
                            (GetComponent<Rigidbody>().velocity.Y < 0 && EngineHelpers.IsTouchingBottom(playerRect, tileRect, GetComponent<Rigidbody>().velocity)))
                            GetComponent<Rigidbody>().velocity.Y = 0;
                    }
                }
            }

            GetComponent<Rigidbody>().Update();

            //base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(ticks < 120 || (ticks % 10 < 5))
                spriteBatch.Draw(EngineHelpers.GetTexture("Rocket"), position - myStage.screenPosition, new Rectangle(0, 0, 7, 10), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            base.Draw(spriteBatch);
        }

        public override void Kill()
        {
            //myStage.AddActor(new Timepiece(position, myStage));

            base.Kill();
        }
    }
}
