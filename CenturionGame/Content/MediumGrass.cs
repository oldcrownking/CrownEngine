using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace CenturionGame.Content
{
    public class MediumGrass : Actor
    {
        public MediumGrass(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            width = 2;
            height = 4;

            base.Load();
        }

        public float rotation;

        public int counter;

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EngineHelpers.GetTexture("SmallGrass"), position, null, Color.White, rotation, new Vector2(1, 4), 1f, SpriteEffects.None, 0f);

            base.Draw(spriteBatch);
        }

        public override void Update()
        {
            rotation = (float)Math.Sin(position.X - position.Y + (counter / 20f)) / 5f;

            counter++;

            base.Update();
        }
    }
}
