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
    class EnemyShipTier2 : Actor
    {
        public EnemyShipTier2(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            SpriteRenderer spriteRenderer = new SpriteRenderer(false, this);

            spriteRenderer.tex = EngineHelpers.GetTexture("EnemyShipTier2");

            AddComponent(spriteRenderer);
            AddComponent(new Rigidbody(this));
            AddComponent(new BoxTrigger(this));

            width = 10;
            height = 13;
        }

        public int ticks;
        public override void Update()
        {
            ticks++;

            GetComponent<Rigidbody>().velocity.Y = 0.25f;

            GetComponent<Rigidbody>().velocity.X = (float)Math.Sin(ticks / 20f) / 4f;

            base.Update();

            if(GetComponent<BoxTrigger>().triggerNames.Contains("PlayerBolt"))
            {
                EngineHelpers.GetSound("EnemyKilled").Play();

                GetComponent<BoxTrigger>().triggers[0].Kill();

                (myStage as GameStage).player.kills++;

                if((myStage as GameStage).player.kills % 3 == 0)
                {
                    myStage.AddActor(new Nut(Center + new Vector2(-3, -3), myStage));
                }

                Kill();
            }

            if (position.X < 0 || position.X > 56) GetComponent<Rigidbody>().velocity.X = 0;
            position.X = position.X.Clamp(0, 56);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
