using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using System.Diagnostics;
using CenturionGame;

namespace CenturionGame.Content
{
    public class Water : Actor
    {
        public Water(Vector2 pos, Stage myStage) : base(pos, myStage)
        {
            
        }

        public override void Load()
        {
            base.Load();
        }

        public float Position, Velocity;
        public float Height;
        public override void Update()
        {
            /*const float k = 0.025f; // adjust this value to your liking
            float x = Height - 64;
            float acceleration = -k * x;

            Position += Velocity;
            Velocity += acceleration;

            for (int i = 0; i < springs.Length; i++)
                springs[i].Update(Dampening, Tension);

            float[] leftDeltas = new float[springs.Length];
            float[] rightDeltas = new float[springs.Length];

            // do some passes where springs pull on their neighbours
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < springs.Length; i++)
                {
                    if (i > 0)
                    {
                        leftDeltas[i] = Spread * (springs[i].Height - springs[i - 1].Height);
                        springs[i - 1].Speed += leftDeltas[i];
                    }
                    if (i < springs.Length - 1)
                    {
                        rightDeltas[i] = Spread * (springs[i].Height - springs[i + 1].Height);
                        springs[i + 1].Speed += rightDeltas[i];
                    }
                }

                for (int i = 0; i < springs.Length; i++)
                {
                    if (i > 0)
                        springs[i - 1].Height += leftDeltas[i];
                    if (i < springs.Length - 1)
                        springs[i + 1].Height += rightDeltas[i];
                }

                base.Update();
            }*/
        }

        /*public void Splash(int index, float speed)
        {
            if (index >= 0 && index < springs.Length)
                springs[i].Speed = speed;
        }*/

        public override void Draw(SpriteBatch spriteBatch)
        {
            VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[3];

            vertices[0] = new VertexPositionColorTexture(new Vector3(20, 20, 0), Color.Blue, Vector2.Zero);
            EngineGame.instance.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 1);

            base.Draw(spriteBatch);
        }
    }
}
