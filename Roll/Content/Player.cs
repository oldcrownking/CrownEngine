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

            GetComponent<Rigidbody>().gravityDir = Vector2.UnitY;
            GetComponent<Rigidbody>().gravityForce = 0.2f;

            width = 8;
            height = 8;
        }

        public int health = 1;
        public int jumpTimer;
        public override void Update()
        {
            if (EngineGame.instance.keyboardState.IsKeyDown(Keys.A))
                GetComponent<Rigidbody>().velocity.X -= 0.1f;
            if (EngineGame.instance.keyboardState.IsKeyDown(Keys.D))
                GetComponent<Rigidbody>().velocity.X += 0.1f;

            GetComponent<Rigidbody>().velocity.X = GetComponent<Rigidbody>().velocity.X.Clamp(-1.5f, 1.5f);

            if (!EngineGame.instance.keyboardState.IsKeyDown(Keys.A) && !EngineGame.instance.keyboardState.IsKeyDown(Keys.D))
                GetComponent<Rigidbody>().velocity.X *= 0.95f;

            GetComponent<BoxCollider>().Update();

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

            if (rotation > 3.14f) rotation = -3.14f;
            if (rotation < -3.14f) rotation = 3.14f;

            if (Math.Abs(GetComponent<Rigidbody>().velocity.X) <= 0.01f) 
            { 
                if(rotation < 0f) rotation += (0 - rotation) / 20f; 
                else rotation += (0 - rotation) / 20f;

                if (Math.Abs(rotation) <= 0.01f) rotation = 0f;
            }
        }

        public float rotation;
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EngineHelpers.GetTexture("Player"), position + new Vector2(4, 4) - myStage.screenPosition, new Rectangle(0, 0, 8, 8), Color.White, rotation, new Vector2(4, 4), 1f, SpriteEffects.None, 0f);

            base.Draw(spriteBatch);
        }
    }
}
