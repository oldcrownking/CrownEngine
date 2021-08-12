using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrownEngine;
using System.Diagnostics;

namespace CrownEngine
{
    public class SpriteRenderer : Component
    {
        public bool drawWithWorld = false;

        public Texture2D tex;

        public Point frame = Point.Zero;

        public Color color = Color.White;

        public float rotation = 0f;

        public float scale = 1f;

        public Vector2 origin = Vector2.Zero;

        public SpriteRenderer(bool _drawWithWorld, Actor myActor) : base(myActor)
        {
            drawWithWorld = _drawWithWorld;
        }

        public override void Load()
        {
            //origin = new Vector2(myActor.width, myActor.height) / 2f;

            base.Load();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 pos = myActor.position;
            if (drawWithWorld) pos -= myActor.myStage.screenPosition;

            spriteBatch.Draw(tex, pos, new Rectangle(frame.X * myActor.width, frame.Y * myActor.height, myActor.width, myActor.height), color, rotation, origin, scale, SpriteEffects.None, 0f);

            base.Draw(spriteBatch);
        }
    }
}
