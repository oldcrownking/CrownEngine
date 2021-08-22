using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using System.Diagnostics;
using CenturionGame;
using Microsoft.Xna.Framework.Input;

namespace CenturionGame.Content
{
    public class Water : Actor
    {
        public int depth;
        public int waterWidth;

        public Water(Vector2 pos, Stage myStage, int _width, int _depth) : base(pos, myStage)
        {
            waterWidth = _width;
            depth = _depth;
        }

        public override void Load()
        {
            effect = new BasicEffect(EngineGame.instance.GraphicsDevice)
            {
                VertexColorEnabled = true
            };

            for(int i = 0; i < (waterWidth / 4) + 1; i++)
            {
                waterParticles.Add(new WaterParticle((i * 4) + (int)position.X, (int)position.Y));
            }

            base.Load();
        }

        public override void Update()
        {
            foreach(Actor actor in myStage.actors)
            {
                if(actor.HasComponent<Rigidbody>())
                {
                    if (actor.Center.Y + actor.GetComponent<Rigidbody>().velocity.Y >= position.Y && actor.Center.Y < position.Y) 
                    {
                        Splash((int)actor.Center.X - (int)position.X, ((actor.GetComponent<Rigidbody>().velocity.Y) * 3f).Clamp(0f, 15f));
                    }
                }
            }

            base.Update();
        }

        public List<WaterParticle> waterParticles = new List<WaterParticle>();

        public struct WaterParticle
        {
            public int x; //constant
            public float y; //changing
            public int restingHeight; //constant
            public float speed;

            public WaterParticle(int _x, int _defaultHeight)
            {
                x = _x;
                restingHeight = _defaultHeight;

                y = _defaultHeight;

                speed = 0;
            }

            public void Update(float tension, float dampening)
            {
                float x = restingHeight - y;
                speed += tension * x - speed * dampening;
                y += speed;
            }
        }

        public BasicEffect effect;

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            for (int i = 0; i < waterParticles.Count; i++)
            {
                WaterParticle p = waterParticles[i];

                p.Update(0.025f, 0.025f);

                waterParticles[i] = p;
            }

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            effect.CurrentTechnique.Passes[0].Apply();

            //EngineGame.instance.testEffect.Parameters["Displacement"].SetValue(EngineHelpers.GetTexture("waternormalmap"));
            //EngineGame.instance.testEffect.Parameters["Displacement"].SetValue(EngineHelpers.GetTexture("waternormalmap"));

            for (int i = 0; i < waterParticles.Count - 1; i++)
            {
                WaterParticle p = waterParticles[i];

                Vector2 thisParticle = EngineHelpers.ToPrimCoordinates(new Vector2(p.x, p.y));
                Vector2 nextParticle = EngineHelpers.ToPrimCoordinates(new Vector2(waterParticles[i + 1].x, waterParticles[i + 1].y));

                Vector2 midThisParticle = EngineHelpers.ToPrimCoordinates(new Vector2(p.x, p.y + (depth * 0.2f)));
                Vector2 midNextParticle = EngineHelpers.ToPrimCoordinates(new Vector2(waterParticles[i + 1].x, waterParticles[i + 1].y + (depth * 0.2f)));

                Vector2 belowThisParticle = EngineHelpers.ToPrimCoordinates(new Vector2(p.x, p.restingHeight + depth));
                Vector2 belowNextParticle = EngineHelpers.ToPrimCoordinates(new Vector2(waterParticles[i + 1].x, waterParticles[i + 1].restingHeight + depth));


                EngineHelpers.DrawPrimitive(thisParticle, nextParticle, midThisParticle, Color.LightCyan * 0.75f, Color.LightCyan * 0.75f, Color.Cyan * 0.75f);

                EngineHelpers.DrawPrimitive(midThisParticle, midNextParticle, nextParticle, Color.Cyan * 0.75f, Color.Cyan * 0.75f, Color.LightCyan * 0.75f);


                EngineHelpers.DrawPrimitive(midThisParticle, midNextParticle, belowThisParticle, Color.Cyan * 0.75f, Color.Cyan * 0.75f, Color.DeepSkyBlue * 0.75f);

                EngineHelpers.DrawPrimitive(belowThisParticle, belowNextParticle, midNextParticle, Color.DeepSkyBlue * 0.75f, Color.DeepSkyBlue * 0.75f, Color.Cyan * 0.75f);
            }

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            float[] leftDeltas = new float[waterParticles.Count];
            float[] rightDeltas = new float[waterParticles.Count];

            float spread = 0.1f;

            // do some passes where springs pull on their neighbours
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < waterParticles.Count; i++)
                {
                    if (i > 0)
                    {
                        WaterParticle pBehind = waterParticles[i - 1];

                        leftDeltas[i] = spread * (waterParticles[i].y - pBehind.y);
                        pBehind.speed += leftDeltas[i];

                        waterParticles[i - 1] = pBehind;
                    }
                    if (i < waterParticles.Count - 1)
                    {
                        WaterParticle pNext = waterParticles[i + 1];

                        rightDeltas[i] = spread * (waterParticles[i].y - pNext.y);
                        pNext.speed += rightDeltas[i];

                        waterParticles[i + 1] = pNext;
                    }
                }

                for (int i = 0; i < waterParticles.Count; i++)
                {
                    if (i > 0)
                    {
                        WaterParticle pBehind = waterParticles[i - 1];

                        pBehind.y += leftDeltas[i];

                        waterParticles[i - 1] = pBehind;
                    }
                    if (i < waterParticles.Count - 1)
                    {
                        WaterParticle pNext = waterParticles[i + 1];

                        pNext.y += rightDeltas[i];

                        waterParticles[i + 1] = pNext;
                    }
                }
            }

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            //water code largely from https://gamedevelopment.tutsplus.com/tutorials/make-a-splash-with-dynamic-2d-water-effects--gamedev-236
        }

        public void Splash(float xPosition, float speed)
        {
            int index = (int)MathHelper.Clamp(xPosition / 4, 0, waterParticles.Count - 1);

            for (int i = Math.Max(0, index - 0); i < Math.Min(waterParticles.Count - 1, index + 1); i++)
            {
                WaterParticle bingus = waterParticles[index];

                bingus.speed = speed;

                waterParticles[index] = bingus;
            }
        }
    }
}
