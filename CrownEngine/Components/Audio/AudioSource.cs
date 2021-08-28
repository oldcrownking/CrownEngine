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
    public class AudioSource : Component
    {
        public AudioSource(Actor myActor, SoundEffect _sfx) : base(myActor)
        {
            sfx = _sfx;
        }

        public SoundEffect sfx;

        public override void Update()
        {
            base.Update();
        }

        public void Play()
        {
            sfx.Play();
        }
    }
}
