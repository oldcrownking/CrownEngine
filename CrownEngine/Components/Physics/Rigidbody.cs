using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CrownEngine
{
    public class Rigidbody : Component
    {
        public Vector2 velocity;
        public Vector2 oldVelocity;

        public Vector2 gravityDir;
        public float gravityForce;

        public Rigidbody(Actor myActor) : base(myActor)
        {
            
        }

        public override void Update()
        {
            myActor.position += velocity;

            velocity += gravityDir * gravityForce;

            oldVelocity = velocity;
        }
    }
}
