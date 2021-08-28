using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace CrownEngine
{
    public class AudioListener : Component
    {
        public AudioListener(Actor myActor) : base(myActor)
        {
            
        }

        public override void Update()
        {
            foreach(Actor actor in myActor.myStage.actors)
            {
                if(actor.HasComponent<AudioSource>())
                {

                }
            }

            base.Update();
        }
    }
}
