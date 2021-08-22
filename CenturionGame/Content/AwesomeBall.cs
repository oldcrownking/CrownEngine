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
        public AwesomeBall(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            SpriteRenderer sr = new SpriteRenderer(true, this);

            sr.tex = EngineHelpers.GetTexture("AwesomeBall");

            AddComponent(sr);

            AddComponent(new Rigidbody(this));
            AddComponent(new TrailRenderer(this));

            ParticleEmitter emitter = new ParticleEmitter(this, new List<ParticleComponent>() { new RadialMask(Color.LightBlue, 0.075f), new SlowDown(0.98f), new AlphaFadeOff(0.025f) }, EmissionTypeID.Radial, 0.25f);

            emitter.particleColor = Color.Cyan;

            AddComponent(emitter);

            GetComponent<Rigidbody>().gravityDir = Vector2.UnitY;
            GetComponent<Rigidbody>().gravityForce = 0;

            width = 16;
            height = 16;

            base.Load();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            EngineGame.instance.GraphicsDevice.Clear(Color.Black);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            GetComponent<TrailRenderer>().Draw(spriteBatch);

            EngineHelpers.DrawOutline(spriteBatch, 1f, Color.Blue, EngineHelpers.GetTexture("AwesomeBall"), Center, 0f, 1f);

            GetComponent<ParticleEmitter>().Draw(spriteBatch);
            GetComponent<SpriteRenderer>().Draw(spriteBatch);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
        }

        public override void Update()
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (Centurion.game.keyboardState.IsKeyDown(Keys.A))
                rb.velocity.X -= 0.05f;

            if (Centurion.game.keyboardState.IsKeyDown(Keys.D))
                rb.velocity.X += 0.05f;

            if (Centurion.game.keyboardState.IsKeyDown(Keys.W))
                rb.velocity.Y -= 0.05f;

            if (Centurion.game.keyboardState.IsKeyDown(Keys.S))
                rb.velocity.Y += 0.05f;


            rb.velocity.X = rb.velocity.X.Clamp(-1.5f, 1.5f);
            rb.velocity.Y = rb.velocity.Y.Clamp(-1.5f, 1.5f);

            SetComponent<Rigidbody>(rb);

            base.Update();
        }
    }
}
