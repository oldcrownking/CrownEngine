using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CrownEngine
{
    public class Transform : Component
    {
        public Vector2 position = Vector2.Zero;

        public Transform(Actor myActor) : base(myActor)
        {
            
        }
    }
}
