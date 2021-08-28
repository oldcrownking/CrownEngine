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
    public class Moon : Actor
    {
        float speed;
        public Moon(Vector2 pos, Stage myStage, float _speed) : base(pos, myStage)
        {
            speed = _speed;
        }

        public override void Load()
        {
            SpriteRenderer spriteRenderer = new SpriteRenderer(false, this);

            spriteRenderer.tex = EngineHelpers.GetTexture("Moon");

            AddComponent(spriteRenderer);
            AddComponent(new Button(Twinshot.game.mouseState.LeftButton, this));

            width = 6;
            height = 6;
        }

        public override void Update()
        {
            position.Y += speed;

            base.Update();
        }
    }
}
