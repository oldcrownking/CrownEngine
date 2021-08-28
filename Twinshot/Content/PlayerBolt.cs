using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using System.Diagnostics;
using Twinshot;
using Microsoft.Xna.Framework.Input;

namespace Twinshot.Content
{
    class PlayerBolt : Actor
    {
        public PlayerBolt(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            SpriteRenderer spriteRenderer = new SpriteRenderer(false, this);

            spriteRenderer.tex = EngineHelpers.GetTexture("PlayerBolt");

            AddComponent(spriteRenderer);
            AddComponent(new Rigidbody(this));
            AddComponent(new BoxCollider(this));

            width = 2;
            height = 4;
        }

        public override void Update()
        {
            GetComponent<Rigidbody>().velocity.Y = -4;

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
