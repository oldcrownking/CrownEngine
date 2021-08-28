using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using System.Diagnostics;
using Twinshot;

namespace Twinshot.Content
{
    public class Star : Actor
    {
        public Star(Vector2 pos, Stage myStage, int _oscillationRate, float _speed) : base(pos, myStage)
        {
            oscillationRate = _oscillationRate;
            speed = _speed;
        }

        public override void Load()
        {
            SpriteRenderer spriteRenderer = new SpriteRenderer(true, this);

            spriteRenderer.tex = EngineHelpers.GetTexture("Stars");

            AddComponent(spriteRenderer);
            AddComponent(new Rigidbody(this));

            width = 3;
            height = 3;
        }

        public int oscillationRate;
        public int ticks;
        public float speed;
        public override void Update()
        {
            ticks++;

            if (ticks % oscillationRate * 2 >= oscillationRate)
            {
                GetComponent<SpriteRenderer>().frame = new Point(0, 1);
            }
            else
            {
                GetComponent<SpriteRenderer>().frame = new Point(0, 0);
            }

            Rigidbody rb = GetComponent<Rigidbody>();

            GetComponent<Rigidbody>().velocity.Y = speed;

            if(position.Y > 131)
            {
                Kill();
            }

            base.Update();
        }
    }
}
