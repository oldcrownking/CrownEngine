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
    public class StarBurst : Actor
    {
        public StarBurst(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            ParticleEmitter p = new ParticleEmitter(this, EmissionTypeID.Radial, 2f, new SlowDown(0.96f), new SizeOverTime(0.95f));

            p.particleAlpha = 1f;
            p.particleColor = Color.White;
            p.particleLifetime = 20;
            p.particleScale = 1f;
            p.emissionRate = 5f;
            p.particleTex = EngineHelpers.GetTexture("StarBurstStar");

            AddComponent(p);

            width = 1;
            height = 1;
        }

        public int ticks;
        public override void Update()
        {
            ticks++;

            if (ticks > 3) GetComponent<ParticleEmitter>().emissionRate = 0f;

            if (ticks > 23) Kill();

            base.Update();
        }
    }
}
