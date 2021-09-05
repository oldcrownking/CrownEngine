using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using System.Diagnostics;
using Roll;

namespace Roll.Content
{
    public class Checkpoint : Actor
    {
        public Checkpoint(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            width = 8;
            height = 16;

            midpoints = new Vector2[segments];

            for(int i = 0; i < segments; i++)
            {
                midpoints[i] = position + new Vector2(-1 + i * 2, 0);
            }

            effect = new BasicEffect(EngineGame.instance.GraphicsDevice)
            {
                VertexColorEnabled = true
            };

            AddComponent(new BoxTrigger(this));

            SpriteRenderer spriteRenderer = new SpriteRenderer(true, this);

            spriteRenderer.tex = EngineHelpers.GetTexture("Checkpoint");

            AddComponent(spriteRenderer);
        }

        public int ticks;
        public int segments = 6;
        public Vector2[] midpoints;

        public BasicEffect effect;

        public bool reached = false;

        public override void Draw(SpriteBatch spriteBatch)
        {
            ticks++;

            for (int i = 1; i < midpoints.Length; i++)
            {
                if(i == 1)
                {
                    midpoints[i].Y = Center.Y - 4;
                    continue;
                }

                midpoints[i].Y = Center.Y - 4 + (float)Math.Sin((ticks - i * 10) / 10f);
            }

            float len = midpoints.Length - 1;

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            effect.CurrentTechnique.Passes[0].Apply();

            for (int i = 0; i < midpoints.Length; i++)
            {
                if (midpoints[i] == Vector2.Zero) { len = i - 1; break; }
            }

            if (reached)
            {
                for (int i = 1; i < len; i++)
                {
                    Vector2 mp1 = EngineHelpers.ToPrimCoordinates(midpoints[i] - myStage.screenPosition);
                    Vector2 mp2 = EngineHelpers.ToPrimCoordinates(midpoints[i + 1] - myStage.screenPosition);

                    Vector2 lep1 = EngineHelpers.ToPrimCoordinates(midpoints[i] + (-Vector2.UnitY * 3) - myStage.screenPosition);
                    Vector2 lep2 = EngineHelpers.ToPrimCoordinates(midpoints[i + 1] + (-Vector2.UnitY * 3) - myStage.screenPosition);

                    Vector2 rep1 = EngineHelpers.ToPrimCoordinates(midpoints[i] + (Vector2.UnitY * 3) - myStage.screenPosition);
                    Vector2 rep2 = EngineHelpers.ToPrimCoordinates(midpoints[i + 1] + (Vector2.UnitY * 3) - myStage.screenPosition);

                    //Debug.WriteLine(lep1);

                    EngineHelpers.DrawPrimitive(mp1, mp2, lep1, Color.White);
                    EngineHelpers.DrawPrimitive(mp2, lep1, lep2, Color.White);

                    EngineHelpers.DrawPrimitive(mp1, mp2, rep1, Color.White);
                    EngineHelpers.DrawPrimitive(mp2, rep1, rep2, Color.White);
                }
            }


            if (GetComponent<BoxTrigger>().triggerNames.Contains("Player") && !reached)
            {
                (myStage as World1_1).player.spawnPoint = Center;
                reached = true;
                myStage.AddActor(new StarBurst(Center, myStage));
                EngineHelpers.PlaySound("GetCheckpoint");
            }

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            base.Draw(spriteBatch);
        }
    }
}
