using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using System.Diagnostics;
using Deprecated;
using Microsoft.Xna.Framework.Input;

namespace Deprecated.Content
{
    public class NutCircle : Actor
    {
        public NutCircle(Vector2 pos, Stage myStage, int amount) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            width = 1;
            height = 1;

            for (int i = 0; i < nuts.Length; i++)
            {
                nuts[i] = new PlatformNut(position + Vector2.UnitY.RotatedBy((MathHelper.TwoPi / (float)nuts.Length) * i) * 6f, myStage);

                myStage.AddActor(nuts[i]);
            }
        }

        public PlatformNut[] nuts = new PlatformNut[4];
        public int ticks;
        public override void Update()
        {
            ticks++;

            for (int i = 0; i < nuts.Length; i++)
            {
                nuts[i].position = position + Vector2.UnitY.RotatedBy(((MathHelper.TwoPi / (float)nuts.Length) * i) + (ticks / 20f)) * 6f;
            }
        }
    }
}
