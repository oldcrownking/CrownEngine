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
        public ParticleEmitter(ButtonState mouseButton, Actor myActor) : base(myActor)
        {
            
        }

        public override void Update()
        {
            base.Update();
        }

        public struct Particle
        {
            public Color color;
            public Vector2 position;
            public Vector2 velocity;
        }
    }

    public class ParticleComponent
    {
        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }

    public class RadialMask : ParticleComponent
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            //EngineHelpers.DrawAdditive(spriteBatch, )
            base.Draw(spriteBatch);
        }
    }
}
