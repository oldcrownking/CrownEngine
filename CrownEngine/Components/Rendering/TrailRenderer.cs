using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrownEngine;
using System.Diagnostics;

namespace CrownEngine
{
    public class TrailRenderer : Component
    {
        public Color color1 = Color.Red;
        public Color color2 = Color.Yellow;

        public Color edgeColor1 = Color.Red * 0.5f;
        public Color edgeColor2 = Color.Yellow * 0.5f;

        public TrailRenderer(Actor myActor) : base(myActor)
        {
            
        }

        public override void Load()
        {
            midpoints = new Vector2[segments];

            effect = new BasicEffect(EngineGame.instance.GraphicsDevice)
            {
                VertexColorEnabled = true
            };

            base.Load();
        }

        public int segments = 30;
        public Vector2[] midpoints;

        public int trailWidth = 10;

        public BasicEffect effect;

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Vector2 origin = myActor.Center;

            Vector2[] newMidpoints = new Vector2[segments];

            newMidpoints[0] = origin;

            for (int i = 1; i < midpoints.Length; i++)
            {
                newMidpoints[i] = midpoints[i - 1];
            }

            float len = newMidpoints.Length - 1;

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            effect.CurrentTechnique.Passes[0].Apply();

            for (int i = 0; i < len; i++)
            {
                Vector2 vectorLeft = Vector2.Normalize(newMidpoints[i + 1] - newMidpoints[i]).RotatedBy(MathHelper.PiOver2); //sticks out to the left
                Vector2 vectorRight = Vector2.Normalize(newMidpoints[i + 1] - newMidpoints[i]).RotatedBy(-MathHelper.PiOver2); //sticks out to the right

                Vector2 mp1 = EngineHelpers.ToPrimCoordinates(newMidpoints[i]);
                Vector2 mp2 = EngineHelpers.ToPrimCoordinates(newMidpoints[i + 1]);

                Vector2 lep1 = EngineHelpers.ToPrimCoordinates(newMidpoints[i] + (vectorLeft * ((trailWidth / (float)segments) * (len - i))));
                Vector2 lep2 = EngineHelpers.ToPrimCoordinates(newMidpoints[i + 1] + (vectorLeft * ((trailWidth / (float)segments) * (len - i))));

                Vector2 rep1 = EngineHelpers.ToPrimCoordinates(newMidpoints[i] + (vectorRight * ((trailWidth / (float)segments) * (len - i))));
                Vector2 rep2 = EngineHelpers.ToPrimCoordinates(newMidpoints[i + 1] + (vectorRight * ((trailWidth / (float)segments) * (len - i))));

                //Debug.WriteLine(lep1);

                EngineHelpers.DrawPrimitive(mp1, mp2, lep1, Color.Lerp(color1, color2, (float)i / (float)len), Color.Lerp(color1, color2, (float)(i + 1) / (float)len), Color.Lerp(edgeColor1, edgeColor2, (float)i / (float)len));
                EngineHelpers.DrawPrimitive(mp2, lep1, lep2, Color.Lerp(color1, color2, (float)(i + 1) / (float)len), Color.Lerp(edgeColor1, edgeColor2, (float)i / (float)len), Color.Lerp(edgeColor1, edgeColor2, (float)(i + 1) / (float)len));

                EngineHelpers.DrawPrimitive(mp1, mp2, rep1, Color.Lerp(color1, color2, (float)i / (float)len), Color.Lerp(color1, color2, (float)(i + 1) / (float)len), Color.Lerp(edgeColor1, edgeColor2, (float)i / (float)len));
                EngineHelpers.DrawPrimitive(mp2, rep1, rep2, Color.Lerp(color1, color2, (float)(i + 1) / (float)len), Color.Lerp(edgeColor1, edgeColor2, (float)i / (float)len), Color.Lerp(edgeColor1, edgeColor2, (float)(i + 1) / (float)len));
            }

            int j = (int)len - 1;

            Vector2 vectorLeft2 = Vector2.Normalize(newMidpoints[j] - newMidpoints[j - 1]).RotatedBy(MathHelper.PiOver2); //sticks out to the left
            Vector2 vectorRight2 = Vector2.Normalize(newMidpoints[j] - newMidpoints[j - 1]).RotatedBy(-MathHelper.PiOver2); //sticks out to the right

            Vector2 cap = EngineHelpers.ToPrimCoordinates(newMidpoints[(int)len]);
            Vector2 midpoint = EngineHelpers.ToPrimCoordinates(newMidpoints[(int)len - 1]);
            Vector2 lep = EngineHelpers.ToPrimCoordinates(newMidpoints[(int)len - 1] + (vectorLeft2 * ((trailWidth / (float)segments) * (len - j))));
            Vector2 rep = EngineHelpers.ToPrimCoordinates(newMidpoints[(int)len - 1] + (vectorRight2 * ((trailWidth / (float)segments) * (len - j))));

            EngineHelpers.DrawPrimitive(cap, lep, midpoint, edgeColor2, Color.Lerp(edgeColor1, edgeColor2, (float)j / (float)len), Color.Lerp(color1, color2, (float)j / (float)len));
            EngineHelpers.DrawPrimitive(cap, rep, midpoint, edgeColor2, Color.Lerp(edgeColor1, edgeColor2, (float)j / (float)len), Color.Lerp(color1, color2, (float)j / (float)len));

            midpoints = newMidpoints;

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
        }
    }
}
