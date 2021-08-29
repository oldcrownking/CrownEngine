using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using System.Diagnostics;
using Twinshot;

namespace Twinshot.Content
{
    public class SmokeImpact : Actor
    {
        public SmokeImpact(Vector2 pos, Stage myStage, Vector2 _impactForce) : base(pos, myStage)
        {
            impactForce = _impactForce;
        }

        public Vector2 impactForce;
        public override void Load()
        {
            ParticleEmitter p = new ParticleEmitter(this, EmissionTypeID.Conical, 2f, new SlowDown(0.91f), new SizeOverTime(0.93f));

            p.particleAlpha = 1f;
            p.particleColor = Color.White;
            p.particleLifetime = 20;
            p.particleScale = 1f;
            p.particleTex = EngineHelpers.GetTexture("SmokeParticle");
            p.emissionDirection = -impactForce;

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
