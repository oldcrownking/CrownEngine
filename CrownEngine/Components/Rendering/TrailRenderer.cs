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
        public Color color1;
        public Color color2;

        public Color edgeColor1;
        public Color edgeColor2;

        public TrailRenderer(Actor myActor, Color _color1, Color _color2, Color _edgeColor1, Color _edgeColor2, int _segments, int width) : base(myActor)
        {
            color1 = _color1;
            color2 = _color2;

            edgeColor1 = _edgeColor1;
            edgeColor2 = _edgeColor2;

            segments = _segments;
            trailWidth = width;

            midpoints = new Vector2[segments];

            effect = new BasicEffect(EngineGame.instance.GraphicsDevice)
            {
                VertexColorEnabled = true
            };
        }

        public override void Load()
        {
            base.Load();
        }

        public int segments;
        public Vector2[] midpoints;

        public int trailWidth;

        public BasicEffect effect;

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Vector2 origin = myActor.Center;

            Vector2[] newMidpoints = new Vector2[segments];

            newMidpoints[0] = origin;

            if (segments > 0)
            {
                for (int i = 1; i < midpoints.Length; i++)
                {
                    newMidpoints[i] = midpoints[i - 1];
                }

                float len = segments - 1;

                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

                effect.CurrentTechnique.Passes[0].Apply();

                for (int i = 0; i < segments; i++)
                {
                    if (newMidpoints[i] == Vector2.Zero) { len = i - 1; break; }
                }

                for (int i = 0; i < len; i++)
                {
                    Vector2 vectorLeft = Vector2.Normalize(newMidpoints[i + 1] - newMidpoints[i]).RotatedBy(MathHelper.PiOver2); //sticks out to the left
                    Vector2 vectorRight = Vector2.Normalize(newMidpoints[i + 1] - newMidpoints[i]).RotatedBy(-MathHelper.PiOver2); //sticks out to the right

                    Vector2 mp1 = EngineHelpers.ToPrimCoordinates(newMidpoints[i] - myActor.myStage.screenPosition);
                    Vector2 mp2 = EngineHelpers.ToPrimCoordinates(newMidpoints[i + 1] - myActor.myStage.screenPosition);

                    Vector2 lep1 = EngineHelpers.ToPrimCoordinates(newMidpoints[i] + (vectorLeft * ((trailWidth / (float)segments) * (len - i))) - myActor.myStage.screenPosition);
                    Vector2 lep2 = EngineHelpers.ToPrimCoordinates(newMidpoints[i + 1] + (vectorLeft * ((trailWidth / (float)segments) * (len - i))) - myActor.myStage.screenPosition);

                    Vector2 rep1 = EngineHelpers.ToPrimCoordinates(newMidpoints[i] + (vectorRight * ((trailWidth / (float)segments) * (len - i))) - myActor.myStage.screenPosition);
                    Vector2 rep2 = EngineHelpers.ToPrimCoordinates(newMidpoints[i + 1] + (vectorRight * ((trailWidth / (float)segments) * (len - i))) - myActor.myStage.screenPosition);

                    //Debug.WriteLine(lep1);

                    EngineHelpers.DrawPrimitive(mp1, mp2, lep1, Color.Lerp(color1, color2, (float)i / (float)len), Color.Lerp(color1, color2, (float)(i + 1) / (float)len), Color.Lerp(edgeColor1, edgeColor2, (float)i / (float)len));
                    EngineHelpers.DrawPrimitive(mp2, lep1, lep2, Color.Lerp(color1, color2, (float)(i + 1) / (float)len), Color.Lerp(edgeColor1, edgeColor2, (float)i / (float)len), Color.Lerp(edgeColor1, edgeColor2, (float)(i + 1) / (float)len));

                    EngineHelpers.DrawPrimitive(mp1, mp2, rep1, Color.Lerp(color1, color2, (float)i / (float)len), Color.Lerp(color1, color2, (float)(i + 1) / (float)len), Color.Lerp(edgeColor1, edgeColor2, (float)i / (float)len));
                    EngineHelpers.DrawPrimitive(mp2, rep1, rep2, Color.Lerp(color1, color2, (float)(i + 1) / (float)len), Color.Lerp(edgeColor1, edgeColor2, (float)i / (float)len), Color.Lerp(edgeColor1, edgeColor2, (float)(i + 1) / (float)len));
                }

                int j = (int)len - 1;

                if (j > 0)
                {
                    Vector2 vectorLeft2 = Vector2.Normalize(newMidpoints[j] - newMidpoints[j - 1]).RotatedBy(MathHelper.PiOver2); //sticks out to the left
                    Vector2 vectorRight2 = Vector2.Normalize(newMidpoints[j] - newMidpoints[j - 1]).RotatedBy(-MathHelper.PiOver2); //sticks out to the right

                    Vector2 cap = EngineHelpers.ToPrimCoordinates(newMidpoints[(int)len] - myActor.myStage.screenPosition);
                    Vector2 midpoint = EngineHelpers.ToPrimCoordinates(newMidpoints[(int)len - 1] - myActor.myStage.screenPosition);
                    Vector2 lep = EngineHelpers.ToPrimCoordinates(newMidpoints[(int)len - 1] + (vectorLeft2 * ((trailWidth / (float)segments) * (len - j))) - myActor.myStage.screenPosition);
                    Vector2 rep = EngineHelpers.ToPrimCoordinates(newMidpoints[(int)len - 1] + (vectorRight2 * ((trailWidth / (float)segments) * (len - j))) - myActor.myStage.screenPosition);

                    EngineHelpers.DrawPrimitive(cap, lep, midpoint, edgeColor2, Color.Lerp(edgeColor1, edgeColor2, (float)j / (float)len), Color.Lerp(color1, color2, (float)j / (float)len));
                    EngineHelpers.DrawPrimitive(cap, rep, midpoint, edgeColor2, Color.Lerp(edgeColor1, edgeColor2, (float)j / (float)len), Color.Lerp(color1, color2, (float)j / (float)len));
                }

                midpoints = newMidpoints;

                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            }
        }
    }
}
