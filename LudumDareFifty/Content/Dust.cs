using CrownEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using LudumDareFifty.Content;

namespace LudumDareFifty.Content
{
    public class DustCloud : Actor
    {
        public DustCloud(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            ParticleEmitter p = new ParticleEmitter(this, EmissionTypeID.Conical, 1f, new SlowDown(0.93f), new SizeOverTime(0.91f));

            p.particleAlpha = 1f;
            p.particleColor = new Color(185, 185, 167);
            p.particleLifetime = 15;
            p.particleScale = 1f;
            p.particleTex = EngineHelpers.GetTexture("SmokeParticle");
            p.coneAngleMin = 1.57f;
            p.coneAngleMax = 4.71f;
            p.emissionDirection = Vector2.UnitY * 1f;

            AddComponent(p);

            width = 1;
            height = 1;
        }

        public int ticks;
        public override void Update()
        {
            ticks++;

            if (ticks > 3) GetComponent<ParticleEmitter>().emissionRate = 0f;

            if (ticks > 18) Kill();

            base.Update();
        }
    }
}
