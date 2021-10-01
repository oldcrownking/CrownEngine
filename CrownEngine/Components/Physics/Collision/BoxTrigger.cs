using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrownEngine;

namespace CrownEngine
{
    public class BoxTrigger : Component
    {
        public int width;
        public int height;

        public Rectangle hitbox => new Rectangle((int)myActor.position.X, (int)myActor.position.Y, myActor.width, myActor.height);

        public List<Actor> triggers = new List<Actor>();
        public List<string> triggerNames = new List<string>();

        public BoxTrigger(Actor myActor) : base(myActor)
        {

        }

        public override void Update()
        {
            for (int i = 0; i < triggers.Count; i++)
            {
                if (triggers[i] == null || !myActor.rect.Intersects(triggers[i].rect))
                {
                    triggers[i] = null;
                    triggerNames[i] = null;
                }
            }

            for (int i = 0; i < myActor.myStage.actors.Count; i++)
            {
                if (myActor.myStage.actors[i] != null && (myActor.myStage.actors[i].HasComponent<BoxCollider>() || myActor.myStage.actors[i].HasComponent<BoxTrigger>()) && myActor.rect.Intersects(myActor.myStage.actors[i].rect))
                {
                    triggers.Add(myActor.myStage.actors[i]);

                    triggerNames.Add(myActor.myStage.actors[i].GetType().Name);
                }
            }

            base.Update();
        }
    }
}
