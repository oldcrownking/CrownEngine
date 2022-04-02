using CrownEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using LudumDareFifty.Content;

namespace LudumDareFifty.Content
{
    public class Player : Actor
    {
        public Player(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            AddComponent(new BoxCollider(this));
            AddComponent(new Rigidbody(this));

            GetComponent<Rigidbody>().gravityDir = Vector2.UnitY;
            GetComponent<Rigidbody>().gravityForce = 0.2f;

            width = 7;
            height = 13;

            time = maxTime;
        }

        public float time;
        public float maxTime = 600f;

        public int jumpTimer;
        public bool onGround;

        public override void Update()
        {
            if (time > maxTime) time = maxTime;
            if(time > 0) time--;
            else
            {
                Die();
            }

            #region Controls
            GetComponent<Rigidbody>().velocity.X = MathHelper.Clamp(GetComponent<Rigidbody>().velocity.X, -2f, 2f);

            KeyboardState keyState = EngineGame.instance.keyboardState;

            if (EngineGame.instance.keyboardState.IsKeyDown(Keys.A))
                GetComponent<Rigidbody>().velocity.X -= 0.1f;
            if (EngineGame.instance.keyboardState.IsKeyDown(Keys.D))
                GetComponent<Rigidbody>().velocity.X += 0.1f;

            if (!EngineGame.instance.keyboardState.IsKeyDown(Keys.A) && !EngineGame.instance.keyboardState.IsKeyDown(Keys.D))
                GetComponent<Rigidbody>().velocity.X *= 0.85f;

            UpdateCollision();

            if (EngineGame.instance.keyboardState.IsKeyDown(Keys.Space) && GetComponent<Rigidbody>().velocity.Y == 0 && jumpTimer == 0)
            {
                GetComponent<Rigidbody>().velocity.Y -= 4f;

                onGround = false;
                EngineHelpers.PlaySound("Jump");
            }

            if (GetComponent<Rigidbody>().velocity.Y <= -0.01f)
            {
                if (jumpTimer < 20 && EngineGame.instance.keyboardState.IsKeyDown(Keys.Space) && Math.Abs(GetComponent<Rigidbody>().velocity.X) < 3)
                {
                    GetComponent<Rigidbody>().gravityForce -= (GetComponent<Rigidbody>().gravityForce / 20f);
                }
                else
                {
                    GetComponent<Rigidbody>().gravityForce = 0.2f;
                }

                jumpTimer++;
            }
            else
            {
                GetComponent<Rigidbody>().gravityForce = 0.25f;
            }

            if (Math.Abs(GetComponent<Rigidbody>().velocity.Y) == 0 && Math.Abs(GetComponent<Rigidbody>().oldVelocity.Y) > 0)
            {
                if (!onGround)
                {
                    EngineHelpers.PlaySound("Thud");
                    myStage.AddActor(new DustCloud(new Vector2(Center.X, Center.Y + height / 2), myStage));

                    onGround = true;
                }

                jumpTimer = 0;
                GetComponent<Rigidbody>().gravityForce = 0.2f;
            }

            GetComponent<Rigidbody>().Update();
            #endregion
        }

        public void Die()
        {
            Kill();
        }

        public void UpdateCollision()
        {
            for (int i = 0; i < myStage.actors.Count; i++)
            {
                if (myStage.actors[i] != null && myStage.actors[i].HasComponent<BoxCollider>() && myStage.actors[i] is Timepiece)
                {
                    Rectangle tileRect = myStage.actors[i].GetComponent<BoxCollider>().hitbox;

                    Point temp = new Point(height / 2, height / 2);
                    Rectangle playerRect = new Rectangle((int)Center.X - temp.X, (int)Center.Y - temp.Y, height, height);

                    if ((GetComponent<Rigidbody>().velocity.X > 0 && EngineHelpers.IsTouchingLeft(playerRect, tileRect, GetComponent<Rigidbody>().velocity)) ||
                        (GetComponent<Rigidbody>().velocity.X < 0 && EngineHelpers.IsTouchingRight(playerRect, tileRect, GetComponent<Rigidbody>().velocity)))
                    {
                        time += maxTime * 0.5f;
                        myStage.actors[i].Kill();

                        break;
                    }

                    if ((GetComponent<Rigidbody>().velocity.Y > 0 && EngineHelpers.IsTouchingTop(playerRect, tileRect, GetComponent<Rigidbody>().velocity)) ||
                        (GetComponent<Rigidbody>().velocity.Y < 0 && EngineHelpers.IsTouchingBottom(playerRect, tileRect, GetComponent<Rigidbody>().velocity)))
                    {
                        time += maxTime * 0.5f;
                        myStage.actors[i].Kill();

                        break;
                    }
                }
            }

            for (int i = 0; i < myStage.actors.Count; i++)
            {
                if (myStage.actors[i] != null && myStage.actors[i].HasComponent<BoxTrigger>() && (myStage.actors[i] is Rocket || myStage.actors[i] is Sawblade))
                {
                    Rectangle tileRect = myStage.actors[i].GetComponent<BoxTrigger>().hitbox;

                    Point temp = new Point(height / 2, height / 2);
                    Rectangle playerRect = new Rectangle((int)Center.X - temp.X, (int)Center.Y - temp.Y, height, height);

                    if ((GetComponent<Rigidbody>().velocity.X > 0 && EngineHelpers.IsTouchingLeft(playerRect, tileRect, GetComponent<Rigidbody>().velocity)) ||
                        (GetComponent<Rigidbody>().velocity.X < 0 && EngineHelpers.IsTouchingRight(playerRect, tileRect, GetComponent<Rigidbody>().velocity)))
                    {
                        time -= 100;
                        myStage.actors[i].Kill();

                        break;
                    }

                    if ((GetComponent<Rigidbody>().velocity.Y > 0 && EngineHelpers.IsTouchingTop(playerRect, tileRect, GetComponent<Rigidbody>().velocity)) ||
                        (GetComponent<Rigidbody>().velocity.Y < 0 && EngineHelpers.IsTouchingBottom(playerRect, tileRect, GetComponent<Rigidbody>().velocity)))
                    {
                        time -= 100;
                        myStage.actors[i].Kill();

                        break;
                    }
                }
            }

            GetComponent<BoxCollider>().Update();

            //if (Math.Abs(GetComponent<Rigidbody>().velocity.X) * 4f < Math.Abs(GetComponent<Rigidbody>().oldVelocity.X)) EngineHelpers.PlaySound("Thud");
        }

        public Rectangle frame;

        public override void Draw(SpriteBatch spriteBatch)
        {
            frame = new Rectangle(0, 0, 7, 13);

            if(Math.Abs(GetComponent<Rigidbody>().velocity.X) > 0.2f)
            {
                frame = new Rectangle((int)(7 * (((myStage as GameStage).ticker / 10f) % 4)), 13, 7, 13);
            }
            if(!onGround)
            {
                frame = new Rectangle(0, 26, 7, 13);
            }

            Vector2 myPos = position - myStage.screenPosition;

            spriteBatch.Draw(EngineHelpers.GetTexture("PlayerFill"), new Rectangle((int)(myPos.X + 1), (int)(myPos.Y + 1), 5, 9), new Rectangle(0, 0, 5, 9), new Color(43, 37, 84), 0f, Vector2.Zero, SpriteEffects.None, 0f);


            spriteBatch.Draw(EngineHelpers.GetTexture("PlayerFill"),

                new Rectangle((int)(myPos.X + 1), (int)(myPos.Y + 1 + ((1 - ((float)time / (float)maxTime)) * 9)), 5, (int)((((float)time / (float)maxTime)) * 9)),

                new Rectangle(0, (int)((1 - ((float)time / (float)maxTime)) * 9), 5, (int)(((float)time / (float)maxTime) * 9)),

                new Color(35, 102, 180), 0f, Vector2.Zero, SpriteEffects.None, 0f);


            spriteBatch.Draw(EngineHelpers.GetTexture("Player"), new Rectangle((int)(myPos.X), (int)(myPos.Y), 7, 13), frame, Color.White, 0f, Vector2.Zero, (GetComponent<Rigidbody>().velocity.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None), 0f);




            Vector2 eyesPosition = Vector2.Normalize(Game1.instance.mousePos - (position + new Vector2(3.5f, 3))) * 2f;
            spriteBatch.Draw(EngineHelpers.GetTexture("PlayerEyes"), myPos + eyesPosition + new Vector2(2, 2), new Rectangle(0, 0, 3 , 2), Color.White, 0f, Vector2.Zero, 1f, (GetComponent<Rigidbody>().velocity.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None), 0f);



            //base.Draw(spriteBatch);
        }
    }
}
