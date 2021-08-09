using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine.Engine;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using CrownEngine;

namespace CenturionGame.Content
{
    public class Player : Actor
    {
        public Player(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        private int jumpTimer;
        private float gravityFactor = 0.3f;
        private float squashHeight;

        public float counter;

        private Point frame;

        public int direction;

        private Vector2 scale = Vector2.One;

        private int walkFrame;

        public override void Load()
        {
            /*SpriteRenderer spriteRenderer = new SpriteRenderer(true, this);

            spriteRenderer.tex = EngineHelpers.GetTexture("Player");

            AddComponent(spriteRenderer);*/
            AddComponent(new Rigidbody(this));
            AddComponent(new BoxCollider(this));

            GetComponent<Rigidbody>().gravityDir = Vector2.UnitY;
            GetComponent<Rigidbody>().gravityForce = 0.05f;

            width = 16;
            height = 16;

            base.Load();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EngineHelpers.GetTexture("Player"), new Rectangle((int)position.X + (int)(16 - (scale.X * 16)) - (int)myStage.screenPosition.Y, (int)position.Y + (int)(16 - (scale.Y * 16)) - (int)myStage.screenPosition.Y, (int)(scale.X * 16), (int)(scale.Y * 16)), new Rectangle(frame.X * width, frame.Y * height, width, height), Color.White, 0f, Vector2.Zero, direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
        }

        public override void Update()
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (EngineGame.instance.keyboardState.IsKeyDown(Keys.A))
                rb.velocity.X -= 0.05f;

            if (EngineGame.instance.keyboardState.IsKeyDown(Keys.D))
                rb.velocity.X += 0.05f;

            if (!EngineGame.instance.keyboardState.IsKeyDown(Keys.D) && !EngineGame.instance.keyboardState.IsKeyDown(Keys.A))
            {
                rb.velocity.X *= 0.1f;
            }

            rb.velocity.X = rb.velocity.X.Clamp(-1.5f, 1.5f);

            if (EngineGame.instance.keyboardState.IsKeyDown(Keys.Space) && rb.velocity.Y == 0 && jumpTimer == 0) //scale.X = 0.25, should be 8
            {
                rb.velocity.Y -= 2.5f;
                squashHeight = 0;
            }

            scale.Y = 1 - (squashHeight / 16f);

            if (squashHeight > 0) squashHeight -= 1f;

            if (rb.velocity.Y < 0)
            {
                if (jumpTimer < 10 && EngineGame.instance.keyboardState.IsKeyDown(Keys.Space))
                {
                    rb.gravityForce -= (rb.gravityForce / 10f);
                }

                frame = new Point(0, 2);

                jumpTimer++;
            }

            if(rb.velocity.Y > 0)
            {
                frame = new Point(0, 3);
            }

            if(Math.Abs(rb.velocity.Y) <= 0.01f)
            {
                frame = new Point(0, 0);

                if(Math.Abs(rb.velocity.X) >= 0.01f)
                {
                    counter += rb.velocity.X;

                    if ((int)counter % 6 == 0)
                    {
                        walkFrame++;

                        if (walkFrame >= 4) walkFrame = 0;
                    }

                    frame = new Point(1, walkFrame);
                }
            }

            if (rb.velocity.Y == 0 && rb.oldVelocity.Y > 0)
            {
                jumpTimer = 0;
                squashHeight = (int)(rb.oldVelocity.Y * 2);
                rb.gravityForce = 0.3f;
            }

            if (Math.Abs(rb.velocity.X) >= 0.01f)
            {
                if (rb.velocity.X < 0)
                {
                    direction = -1;
                }
                else
                {
                    direction = 1;
                }
            }

            SetComponent<Rigidbody>(rb);

            base.Update();
        }
    }
}
