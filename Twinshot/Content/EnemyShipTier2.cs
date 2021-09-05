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
    class EnemyShip2 : Actor
    {
        public EnemyShip2(Vector2 pos, Stage myStage) : base(pos, myStage)
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
            height = 15;
        }

        public int ticks;
        public int health = 2;
        public override void Update()
        {
            ticks++;

            GetComponent<Rigidbody>().velocity.Y = 0.4f;

            GetComponent<Rigidbody>().velocity.X = (float)Math.Sin(ticks / 20f) / 4f;

            base.Update();

            for (int i = 0; i < GetComponent<BoxTrigger>().triggers.Count; i++)
            {
                if (GetComponent<BoxTrigger>().triggers[i] != null && GetComponent<BoxTrigger>().triggerNames[i] == "PlayerBolt")
                {
                    GetComponent<BoxTrigger>().triggers[i].Kill();
                    GetComponent<BoxTrigger>().triggerNames[i] = "";

                    if (health > 0)
                    {
                        health--;
                        EngineHelpers.PlaySound("EnemyHit" + EngineHelpers.Next(3));
                    }
                    else
                    {
                        (myStage as GameStage).player.kills++;

                        if ((myStage as GameStage).player.kills % 3 == 0)
                        {
                            myStage.AddActor(new Nut(Center + new Vector2(-3, -3), myStage));
                        }

                        EngineHelpers.PlaySound("EnemyKilled" + EngineHelpers.Next(3));

                        myStage.AddActor(new SmokeBurst(Center, myStage));

                        Kill();
                    }
                }
            }

            if (position.X < 0 || position.X > 56) GetComponent<Rigidbody>().velocity.X = 0;
            position.X = MathHelper.Clamp(position.X, 0, 56);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
