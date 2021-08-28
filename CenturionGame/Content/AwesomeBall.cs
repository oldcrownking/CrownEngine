using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace CenturionGame.Content
{
    public class AwesomeBall : Actor
    {
        public AwesomeBall(Vector2 pos, Stage myStage, int _id, Vector2 orig) : base(pos, myStage)
        {
            id = _id;
            origin = orig;
        }

        public int id;
        public Vector2 origin;

        public override void Load()
        {
            SpriteRenderer sr = new SpriteRenderer(true, this);

            sr.tex = EngineHelpers.GetTexture("AwesomeBall");

            AddComponent(sr);

            AddComponent(new Rigidbody(this));
            AddComponent(new TrailRenderer(this));

            ParticleEmitter emitter = new ParticleEmitter(this, new List<ParticleComponent>() { new RadialMask(Color.Yellow, 0.075f), new SlowDown(0.95f), new AlphaFadeOff(0.05f) }, EmissionTypeID.Radial, 0.25f);

            emitter.particleColor = Color.Orange;

            AddComponent(emitter);

            GetComponent<Rigidbody>().gravityDir = Vector2.UnitY;
            GetComponent<Rigidbody>().gravityForce = 0;

            width = 16;
            height = 16;

            base.Load();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            float ticksin = Math.Sin(ticks / 240f).PositiveSin();
            float ticksinhalf = Math.Sin(ticks / 60f).PositiveSin();

            Color primaryColor = Color.Lerp(Color.Lerp(Color.Red, Color.Yellow, ticksinhalf), Color.Lerp(Color.Blue, Color.Magenta, ticksinhalf), ticksin);

            float tick2sin = Math.Sin((ticks / 240f) + MathHelper.PiOver4).PositiveSin();
            float tick2sinhalf = Math.Sin((ticks / 60f) + (MathHelper.PiOver4 / 2f)).PositiveSin();

            Color secondaryColor = Color.Lerp(Color.Lerp(Color.Red, Color.Yellow, tick2sinhalf), Color.Lerp(Color.Blue, Color.Magenta, tick2sinhalf), tick2sin);

            EngineHelpers.DrawAdditive(spriteBatch, EngineHelpers.GetTexture("RadialGradient"), Center - GetComponent<Rigidbody>().velocity, primaryColor, 0.3f);

            EngineHelpers.DrawAdditive(spriteBatch, EngineHelpers.GetTexture("RadialGradient"), Center - GetComponent<Rigidbody>().velocity, primaryColor * 0.5f, 0.5f);

            GetComponent<TrailRenderer>().color1 = primaryColor;
            GetComponent<TrailRenderer>().color2 = secondaryColor;
            GetComponent<TrailRenderer>().edgeColor1 = primaryColor;
            GetComponent<TrailRenderer>().edgeColor1 = secondaryColor;

            GetComponent<TrailRenderer>().Draw(spriteBatch);

            EngineHelpers.DrawOutline(spriteBatch, 1f, primaryColor, EngineHelpers.GetTexture("AwesomeBall"), Center, 0f, 1f);

            //GetComponent<ParticleEmitter>().Draw(spriteBatch);
            GetComponent<SpriteRenderer>().Draw(spriteBatch);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
        }

        public int dist;
        public int ticks;

        public override void Update()
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            ticks++;
            rb.velocity = (Vector2.UnitY * 2).RotatedBy((id * MathHelper.PiOver4) + (ticks / 30f));

            SetComponent<Rigidbody>(rb);

            base.Update();
        }

        public override void PostDraw(SpriteBatch spriteBatch)
        {
            GetComponent<SpriteRenderer>().Draw(spriteBatch);

            base.PostDraw(spriteBatch);
        }
    }
}
