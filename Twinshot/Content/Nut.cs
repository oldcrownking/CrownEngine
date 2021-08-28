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
    public class Nut : Actor
    {
        public Nut(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            SpriteRenderer spriteRenderer = new SpriteRenderer(true, this);

            spriteRenderer.tex = EngineHelpers.GetTexture("Nut");

            AddComponent(spriteRenderer);
            AddComponent(new Rigidbody(this));
            AddComponent(new BoxTrigger(this));

            GetComponent<Rigidbody>().velocity = new Vector2(EngineHelpers.NextFloat(-0.5f, 0.5f), 0);
            GetComponent<Rigidbody>().gravityForce = 0.05f;
            GetComponent<Rigidbody>().gravityDir = Vector2.UnitY;

            width = 6;
            height = 6;
        }

        public int ticks;
        public int frame;
        public override void Update()
        {
            ticks++;

            if (ticks % 10 == 0) frame++;
            if (frame > 3) frame = 0;

            GetComponent<SpriteRenderer>().frame = new Point(0, frame);

            Rigidbody rb = GetComponent<Rigidbody>();

            if(position.Y > 131)
            {
                Kill();
            }

            base.Update();
        }
    }
}
