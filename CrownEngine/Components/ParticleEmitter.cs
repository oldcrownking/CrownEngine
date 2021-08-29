using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Linq.Expressions;

namespace CrownEngine
{
    public class ParticleEmitter : Component
    {
        public List<Particle> particles = new List<Particle>();

        public float emissionRate;
        public EmissionTypeID emissionType;

        public ParticleComponent[] components;

        public Texture2D particleTex;
        public Color particleColor;
        public float particleScale;
        public float particleAlpha;
        public int particleLifetime;

        public Vector2 emissionDirection;

        public ParticleEmitter(Actor myActor, EmissionTypeID _emissionType, float _emissionRate, params ParticleComponent[] _components) : base(myActor)
        {
            components = _components;
            emissionType = _emissionType;
            emissionRate = _emissionRate;
        }

        public override void Load()
        {
            if (particleTex == default) particleTex = EngineHelpers.GetTexture("MagicPixel");
            if (particleColor == default) particleColor = Color.White;
            if (particleScale == default) particleScale = 1f;
            if (particleAlpha == default) particleAlpha = 1f;
            if (emissionDirection == default) emissionDirection = Vector2.UnitY;

            base.Load();
        }

        public override void Update()
        {
            if (emissionRate <= 1 && EngineHelpers.NextBool(emissionRate))
            {
                SpawnParticle();
            }

            if(emissionRate > 1)
            {
                for(int i = 0; i < (int)emissionRate; i++)
                {
                    SpawnParticle();
                }
            }

            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Update();

                if (particles[i].timeLeft <= 0) particles.RemoveAt(i);
            }

            base.Update();
        }

        public void SpawnParticle()
        {
            Vector2 vel = Vector2.Zero;

            if (emissionType == EmissionTypeID.Radial) vel = Vector2.UnitY.RotatedBy(EngineHelpers.NextFloat(6.28f)) * 2f;
            if (emissionType == EmissionTypeID.Conical) vel = emissionDirection.RotatedBy(EngineHelpers.NextFloat(-1.047f, 1.047f));

            Particle p = new Particle(myActor.position,
                vel,
                particleTex,
                particleColor,
                particleScale,
                particleLifetime,
                particleAlpha,
                components);

            particles.Add(p);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Draw(spriteBatch);
            }

            base.Draw(spriteBatch);
        }
    }

    public enum EmissionTypeID
    {
        Radial,
        Conical,
        Biconical
    }

    public class Particle
    {
        public Color color;
        public Vector2 position;
        public Vector2 velocity;

        public Texture2D tex;

        public float scale;

        public float rotation;

        public float alpha;

        public ParticleComponent[] components;

        public int timeLeft;

        public Vector2 Center => position + new Vector2(tex.Width / 2, tex.Height / 2);

        public Particle(Vector2 pos, Vector2 vel, Texture2D _tex, Color _color, float _scale, int _timeLeft, float _alpha, params ParticleComponent[] _components)
        {
            position = pos;
            velocity = vel;
            color = _color;
            components = _components;
            scale = _scale;
            tex = _tex;

            rotation = 0;

            alpha = _alpha;

            timeLeft = _timeLeft;
        }

        public void Update()
        {
            for (int i = 0; i < components.Length; i++)
            {
                components[i].myParticle = this;

                components[i].Update();
            }

            position += velocity;

            timeLeft--;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < components.Length; i++)
            {
                components[i].myParticle = this;

                components[i].Draw(spriteBatch);
            }

            spriteBatch.Draw(tex, position, null, color * alpha, rotation, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
    }

    public class ParticleComponent
    {
        public Particle myParticle;
        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void Load()
        {

        }
    }

    public class RadialMask : ParticleComponent
    {
        public Color color;
        public float scale;

        public RadialMask(Color _color, float _scale)
        {
            color = _color;
            scale = _scale;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            EngineHelpers.DrawAdditive(spriteBatch, EngineHelpers.GetTexture("RadialGradient"), myParticle.Center, color * myParticle.alpha, scale);

            base.Draw(spriteBatch);
        }
    }

    public class RadialMaskLerped : ParticleComponent
    {
        public Color color1;
        public Color color2;

        public float scale;

        private float total = 0;

        public float rate;

        public RadialMaskLerped(Color _color1, Color _color2, float _rate, float _scale)
        {
            rate = _rate;

            total = 0;

            color1 = _color1;
            color2 = _color2;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color myColor = Color.Lerp(color1, color2, total);

            total += rate;

            EngineHelpers.DrawAdditive(spriteBatch, EngineHelpers.GetTexture("RadialGradient"), myParticle.Center, myColor * myParticle.alpha, scale);

            base.Draw(spriteBatch);
        }
    }

    public class SlowDown : ParticleComponent
    {
        public float rate;

        public SlowDown(float _rate)
        {
            rate = _rate;
        }

        public override void Update()
        {
            myParticle.velocity *= rate;

            base.Update();
        }
    }

    public class SizeOverTime : ParticleComponent
    {
        public float rate;

        public SizeOverTime(float _rate)
        {
            rate = _rate;
        }

        public override void Update()
        {
            myParticle.scale *= rate;

            base.Update();
        }
    }

    public class ColorLerp : ParticleComponent
    {
        public float rate;

        private float total = 0;

        public Color color1;
        public Color color2;

        public ColorLerp(float _rate, Color _color1, Color _color2)
        {
            rate = _rate;

            total = 0;

            color1 = _color1;
            color2 = _color2;
        }

        public override void Update()
        {
            myParticle.color = Color.Lerp(color1, color2, total);

            total += rate;

            base.Update();
        }
    }

    public class AlphaFadeOff : ParticleComponent
    {
        public float rate;

        public AlphaFadeOff(float _rate)
        {
            rate = _rate;
        }

        public override void Update()
        {
            myParticle.alpha -= rate;

            base.Update();
        }
    }
}
