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
    public class TestParticleEmitter : Actor
    {
        public TestParticleEmitter(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            width = 1;
            height = 1;

            ParticleEmitter emitter = new ParticleEmitter(this, new List<ParticleComponent>() { new RadialMask(Color.Green, 0.075f), new SlowDown(0.98f), new AlphaFadeOff(0.025f) }, EmissionTypeID.Radial, 0.25f);

            emitter.particleColor = Color.Lime;

            AddComponent(emitter);

            base.Load();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
