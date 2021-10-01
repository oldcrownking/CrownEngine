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
            GetComponent<Rigidbody>().gravityForce = 0.3f;

            width = 8;
            height = 8;
        }

        public int health = 1;
        public int jumpTimer;
        public bool onGround;
        public int coyoteFrames;
        public Point frame;
        public int counter;
        public int walkFrame;

        public int extraJumpsTier;
        public int extraJumps;

        public int shieldTier = 0;
        public int jumpTier;
        public int multiplierTier;

        public bool canUpgrade;

        public bool shieldGrace;

        public override void Update()
        {
            if (EngineGame.instance.keyboardState.IsKeyDown(Keys.A))
            {
                direction = -1;
                GetComponent<Rigidbody>().velocity.X -= 0.1f;
            }
            if (EngineGame.instance.keyboardState.IsKeyDown(Keys.D))
            {
                direction = 1;
                GetComponent<Rigidbody>().velocity.X += 0.1f;
            }

            if (!EngineGame.instance.keyboardState.IsKeyDown(Keys.A) && !EngineGame.instance.keyboardState.IsKeyDown(Keys.D))
                GetComponent<Rigidbody>().velocity.X *= 0.95f;


            UpdateCollision();


            if ((EngineGame.instance.keyboardState.IsKeyDown(Keys.Space) && !EngineGame.instance.oldKeyboardState.IsKeyDown(Keys.Space)) && ((GetComponent<Rigidbody>().velocity.Y <= 0.01f && jumpTimer == 0) || extraJumps >= 0))
            {
                GetComponent<Rigidbody>().velocity.Y -= (2.5f + (jumpTier * 0.2f));

                if (extraJumps >= 0) extraJumps--;

                coyoteFrames = -1;

                onGround = false;
                EngineHelpers.PlaySound("Jump");
            }


            if (GetComponent<Rigidbody>().velocity.Y <= -0.01f)
            {
                if (jumpTimer < 15 && EngineGame.instance.keyboardState.IsKeyDown(Keys.Space) && Math.Abs(GetComponent<Rigidbody>().velocity.X) < 3)
                {
                    GetComponent<Rigidbody>().gravityForce -= (GetComponent<Rigidbody>().gravityForce / 15f);
                }

                coyoteFrames--;
                jumpTimer++;

                frame = new Point(1, 2);
            }

            if (GetComponent<Rigidbody>().velocity.Y >= 0.01f)
            {
                frame = new Point(1, 3);
            }

            if (Math.Abs(GetComponent<Rigidbody>().velocity.Y) <= 0.01f && Math.Abs(GetComponent<Rigidbody>().oldVelocity.Y) > 0.01f)
            {
                frame = new Point(1, 0);

                if (!onGround)
                {
                    EngineHelpers.PlaySound("Thud");
                    myStage.AddActor(new DustCloud(new Vector2(Center.X, Center.Y + height / 2), myStage));

                    shieldGrace = false;

                    onGround = true;
                }

                extraJumps = extraJumpsTier;

                coyoteFrames = 5;

                jumpTimer = 0;
                GetComponent<Rigidbody>().gravityForce = 0.2f;
            }

            if(onGround)
            {

                if ((EngineGame.instance.keyboardState.IsKeyDown(Keys.A) || EngineGame.instance.keyboardState.IsKeyDown(Keys.D)))
                {
                    counter++;

                    if ((int)counter % 6 == 0)
                    {
                        walkFrame++;

                        if (walkFrame >= 4) walkFrame = 0;
                    }

                    frame = new Point(0, walkFrame);
                }
            }

            if (shieldGrace) GetComponent<Rigidbody>().gravityForce = 0.025f;

            GetComponent<Rigidbody>().Update();

            GetComponent<Rigidbody>().velocity.X = MathHelper.Clamp(GetComponent<Rigidbody>().velocity.X, -2f, 2f);

            if (position.X < 0) { position.X = 0; GetComponent<Rigidbody>().velocity.X = 0; }

            if (position.X > 88) { position.X = 88; GetComponent<Rigidbody>().velocity.X = 0; }

            if (myStage.screenPosition.Y < Center.Y - 216 && deathTicks <= 0) Die();

            if (deathTicks > 0) deathTicks++;

            //if (position.Y > 208)
        }

        public void UpdateCollision()
        {
            for (int i = 0; i < myStage.actors.Count; i++)
            {
                if (myStage.actors[i] != null && (myStage.actors[i] is Sawblade || myStage.actors[i] is HorizontalSawblade))
                {
                    if(rect.Intersects(new Rectangle((int)myStage.actors[i].position.X - myStage.actors[i].width, (int)myStage.actors[i].position.Y - myStage.actors[i].height, myStage.actors[i].width, myStage.actors[i].height)) && deathTicks <= 0 && !shieldGrace)
                    {
                        if(shieldTier > 0 && !shieldGrace)
                        {
                            shieldGrace = true;
                            GetComponent<Rigidbody>().velocity.Y -= 6f;
                            shieldTier--;
                        }
                        else
                        {
                            Die();
                        }
                    }
                }

                if (myStage.actors[i] != null && myStage.actors[i].HasComponent<SemisolidTileCollider>() && !EngineGame.instance.keyboardState.IsKeyDown(Keys.S))
                {
                    for (int j = 0; j < myStage.actors[i].GetComponent<SemisolidTileCollider>().rectangles.Count; j++)
                    {
                        SemisolidTileCollider grid = myStage.actors[i].GetComponent<SemisolidTileCollider>();
                        Rectangle tileRect = grid.rectangles[j];

                        Point temp = new Point(height / 2, height / 2);
                        Rectangle playerRect = new Rectangle((int)Center.X - temp.X, (int)Center.Y - temp.Y, height, height);

                        if (playerRect.Y + playerRect.Height <= tileRect.Y)
                        {
                            if (GetComponent<Rigidbody>().velocity.Y > 0 && EngineHelpers.IsTouchingTop(playerRect, tileRect, GetComponent<Rigidbody>().velocity))
                                GetComponent<Rigidbody>().velocity.Y = 0;
                        }
                    }
                }
            }

            GetComponent<BoxCollider>().Update();

            //if (Math.Abs(GetComponent<Rigidbody>().velocity.X) * 4f < Math.Abs(GetComponent<Rigidbody>().oldVelocity.X)) EngineHelpers.PlaySound("Thud");
        }

        public int nuts;

        public int deathTicks = 0;
        public bool visible = true;
        public void Die()
        {
            myStage.AddActor(new SmokeBurst(Center, myStage));

            EngineHelpers.PlaySound("PlayerDeath");

            (myStage as GameStage).isScrolling = false;

            deathTicks++;
            visible = false;
        }

        public int direction = 1;
        public override void Draw(SpriteBatch spriteBatch)
        {
            if(visible) spriteBatch.Draw(EngineHelpers.GetTexture("Player"), position - myStage.screenPosition, new Rectangle(frame.X * 8, frame.Y * 8, 8, 8), Color.White, 0f, Vector2.Zero, 1f, (direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally), 0f);

            //base.Draw(spriteBatch);
        }
    }
}
