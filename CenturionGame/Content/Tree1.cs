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
    public class Tree1 : Actor
    {
        public Tree1(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            width = 48;
            height = 57;

            SpriteRenderer spriteRenderer = new SpriteRenderer(true, this);

            spriteRenderer.tex = EngineHelpers.GetTexture("Tree1");

            AddComponent(spriteRenderer);

            base.Load();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
