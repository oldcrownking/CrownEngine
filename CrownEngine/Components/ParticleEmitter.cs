using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace CrownEngine
{
    public class ParticleEmitter : Component
    {
        public List<Particle> particles = new List<Particle>();

        public float emissionRate;
        public EmissionTypeID emissionType;

        public List<ParticleComponent> components = new List<ParticleComponent>();

        public Texture2D particleTex;
        public Color particleColor;
        public float particleScale;
        public float particleAlpha;

        public ParticleEmitter(Actor myActor, List<ParticleComponent> _components, EmissionTypeID _emissionType, float _emissionRate) : base(myActor)
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

            base.Load();
        }

        public override void Update()
        {
            for(int i = 0; i < particles.Count; i++)
            {
                Particle p = particles[i];

                p.Update();

                particles[i] = p;

                if (particles[i].timeLeft <= 0) particles.RemoveAt(i);
            }

            if(EngineHelpers.NextBool(emissionRate))
            {
                particles.Add(new Particle(myActor.position, Vector2.UnitY.RotatedBy(EngineHelpers.NextFloat(6.28f)) * 2f, particleTex, particleColor, particleScale, 60, particleAlpha, components));
            }

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < particles.Count; i++)
            {
                Particle p = particles[i];

                p.Draw(spriteBatch);

                particles[i] = p;
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

        public List<ParticleComponent> components;

        public int timeLeft;

        public Vector2 Center => position + new Vector2(tex.Width / 2, tex.Height / 2);

        public Particle(Vector2 pos, Vector2 vel, Texture2D _tex, Color _color, float _scale, int _timeLeft, float _alpha, List<ParticleComponent> _components)
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
            for (int i = 0; i < components.Count; i++)
            {
                ParticleComponent component = components[i];

                component.myParticle = this;

                component.Update();

                components[i] = component;
            }

            position += velocity;

            timeLeft--;
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < components.Count; i++)
            {
                ParticleComponent component = components[i];

                component.myParticle = this;

                component.Draw(spriteBatch);

                components[i] = component;
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

    public class ColorLerp : ParticleComponent
    {
        public float rate;

        private float total;

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
