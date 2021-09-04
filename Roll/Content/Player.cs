using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using System.Diagnostics;
using Roll;
using Microsoft.Xna.Framework.Input;

namespace Roll.Content
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
            AddComponent(new TrailRenderer(this, Color.White, Color.White, Color.White, Color.White, 20, 4));

            GetComponent<Rigidbody>().gravityDir = Vector2.UnitY;
            GetComponent<Rigidbody>().gravityForce = 0.2f;

            width = 8;
            height = 8;
        }

        public int health = 1;
        public int jumpTimer;
        public int dashTimer;
        public Vector2 spawnPoint = Vector2.One * 32;

        public override void Update()
        {
            if (EngineGame.instance.mouseState.RightButton == ButtonState.Pressed) Debug.WriteLine(EngineGame.instance.mousePos + myStage.screenPosition);


            if (deathTicks >= 1)
            {
                deathTicks++;

                if (deathTicks > 60) Respawn();
            }
            else
            {
                if (Math.Abs(GetComponent<Rigidbody>().velocity.X) < 3)
                {
                    MathHelper.Clamp(GetComponent<Rigidbody>().velocity.X, -1.5f, 1.5f);
                    if (GetComponent<TrailRenderer>().segments > 2) GetComponent<TrailRenderer>().segments--;
                }
                else
                {
                    GetComponent<Rigidbody>().velocity.X *= 0.97f;
                }

                KeyboardState keyState = EngineGame.instance.keyboardState;
                if (keyState.IsKeyDown(Keys.W) && (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.D)))
                {
                    dashTimer++;

                    if (Math.Abs(GetComponent<Rigidbody>().velocity.X) <= 3) GetComponent<Rigidbody>().velocity *= 0.93f;

                    UpdateCollision();

                    GetComponent<Rigidbody>().Update();

                    rotation += MathHelper.Clamp(dashTimer * (keyState.IsKeyDown(Keys.A) && !keyState.IsKeyDown(Keys.D) ? -1 : 1), -60f, 60f) / 25f;
                }
                else
                {
                    if (dashTimer >= 90)
                    {
                        if (EngineGame.instance.keyboardState.IsKeyDown(Keys.A))
                        {
                            GetComponent<Rigidbody>().velocity += new Vector2(-6, -2);
                            GetComponent<TrailRenderer>().segments = 20;
                        }

                        if (EngineGame.instance.keyboardState.IsKeyDown(Keys.D))
                        {
                            GetComponent<Rigidbody>().velocity += new Vector2(6, -2);
                            GetComponent<TrailRenderer>().segments = 20;
                        }
                    }

                    dashTimer = 0;

                    if (EngineGame.instance.keyboardState.IsKeyDown(Keys.A))
                        GetComponent<Rigidbody>().velocity.X -= 0.1f;
                    if (EngineGame.instance.keyboardState.IsKeyDown(Keys.D))
                        GetComponent<Rigidbody>().velocity.X += 0.1f;

                    if (!EngineGame.instance.keyboardState.IsKeyDown(Keys.A) && !EngineGame.instance.keyboardState.IsKeyDown(Keys.D))
                        GetComponent<Rigidbody>().velocity.X *= 0.95f;

                    UpdateCollision();

                    if (EngineGame.instance.keyboardState.IsKeyDown(Keys.Space) && GetComponent<Rigidbody>().velocity.Y == 0 && jumpTimer == 0) //scale.X = 0.25, should be 8
                    {
                        GetComponent<Rigidbody>().velocity.Y -= 2f;
                    }


                    if (GetComponent<Rigidbody>().velocity.Y <= -0.01f)
                    {
                        if (jumpTimer < 10 && EngineGame.instance.keyboardState.IsKeyDown(Keys.Space))
                        {
                            GetComponent<Rigidbody>().gravityForce -= (GetComponent<Rigidbody>().gravityForce / 10f);
                        }

                        jumpTimer++;
                    }

                    if (GetComponent<Rigidbody>().velocity.Y <= 0.01f && GetComponent<Rigidbody>().oldVelocity.Y > 0)
                    {
                        jumpTimer = 0;
                        GetComponent<Rigidbody>().gravityForce = 0.2f;
                    }

                    GetComponent<Rigidbody>().Update();

                    rotation += GetComponent<Rigidbody>().velocity.X / 3f;
                }

                if (rotation > 3.14f) rotation = -3.14f;
                if (rotation < -3.14f) rotation = 3.14f;

                if (Math.Abs(GetComponent<Rigidbody>().velocity.X) <= 0.01f)
                {
                    if (rotation < 0f) rotation += (0 - rotation) / 20f;
                    else rotation += (0 - rotation) / 20f;

                    if (Math.Abs(rotation) <= 0.01f) rotation = 0f;
                }
            }
        }

        public void UpdateCollision()
        {
            for (int i = 0; i < myStage.actors.Count; i++)
            {
                if (myStage.actors[i] != null && myStage.actors[i].HasComponent<TileCollider>() && myStage.actors[i] is SpikeTilemap)
                {
                    for (int j = 0; j < myStage.actors[i].GetComponent<TileCollider>().rectangles.Count; j++)
                    {
                        TileCollider grid = myStage.actors[i].GetComponent<TileCollider>();
                        Rectangle tileRect = grid.rectangles[j];

                        Point temp = new Point(height / 2, height / 2);
                        Rectangle playerRect = new Rectangle((int)Center.X - temp.X, (int)Center.Y - temp.Y, height, height);

                        if ((GetComponent<Rigidbody>().velocity.X > 0 && EngineHelpers.IsTouchingLeft(playerRect, tileRect, GetComponent<Rigidbody>().velocity)) ||
                            (GetComponent<Rigidbody>().velocity.X < 0 && EngineHelpers.IsTouchingRight(playerRect, tileRect, GetComponent<Rigidbody>().velocity)))
                            Die();

                        if ((GetComponent<Rigidbody>().velocity.Y > 0 && EngineHelpers.IsTouchingTop(playerRect, tileRect, GetComponent<Rigidbody>().velocity)) ||
                            (GetComponent<Rigidbody>().velocity.Y < 0 && EngineHelpers.IsTouchingBottom(playerRect, tileRect, GetComponent<Rigidbody>().velocity)))
                            Die();
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
        }

        public void Respawn()
        {
            position = spawnPoint + new Vector2(-4, -4);

            deathTicks = 0;
            visible = true;

            GetComponent<Rigidbody>().velocity = Vector2.Zero;
            rotation = 0;
        }

        public int deathTicks = 0;
        public bool visible = true;
        public void Die()
        {
            myStage.AddActor(new SmokeBurst(Center, myStage));

            deathTicks++;
            visible = false;
        }

        public float rotation;
        public Point frame;
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (dashTimer >= 90 && dashTimer < 95) frame = new Point(0, 1);
            else frame = new Point(0, 0);

            if (Math.Abs(GetComponent<Rigidbody>().velocity.X) > 3) GetComponent<TrailRenderer>().Draw(spriteBatch);
            else GetComponent<TrailRenderer>().midpoints = new Vector2[GetComponent<TrailRenderer>().segments];

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

            if(visible) spriteBatch.Draw(EngineHelpers.GetTexture("Player"), position + new Vector2(4, 4) - myStage.screenPosition, new Rectangle(0, frame.Y * 8, 8, 8), Color.White, rotation, new Vector2(4, 4), 1f, SpriteEffects.None, 0f);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            //base.Draw(spriteBatch);
        }
    }
}
