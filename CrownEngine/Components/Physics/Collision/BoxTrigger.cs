using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrownEngine;

namespace CrownEngine.Engine
{
    public class BoxTrigger : Component
    {
        public bool trigger;

        public Rectangle hitbox => new Rectangle((int)myActor.position.X, (int)myActor.position.Y, myActor.width, myActor.height);

        public string desired;

        public BoxTrigger(string _desired, Actor myActor) : base(myActor)
        {
            desired = _desired;
        }

        public override void Update()
        {
            trigger = false;

            for(int i = 0; i < myActor.myStage.actors.Count; i++)
            {
                if(myActor.myStage.actors[i].HasComponent<BoxCollider>() && myActor.rect.Intersects(myActor.myStage.actors[i].rect) && myActor.myStage.actors[i].GetType().Name == desired)
                {
                    trigger = true;
                }
            }

            base.Update();
        }
    }
}
