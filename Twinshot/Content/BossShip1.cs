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
    class BossShip1 : Actor
    {
        public BossShip1(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            SpriteRenderer spriteRenderer = new SpriteRenderer(false, this);

            spriteRenderer.tex = EngineHelpers.GetTexture("BossShip1");

            AddComponent(spriteRenderer);
            AddComponent(new Rigidbody(this));
            AddComponent(new BoxTrigger(this));

            GetComponent<Rigidbody>().velocity.Y = 2f;

            width = 14;
            height = 16;
        }

        public int ticks;
        public int health = 7;
        public bool dying;
        public override void Update()
        {
            ticks++;

            GetComponent<Rigidbody>().velocity.X = (float)Math.Sin(ticks / 20f) / 1.5f;

            if (health > 0) GetComponent<Rigidbody>().velocity.Y *= 0.93f;

            if (!dying)
            {
                if ((ticks + 45) % 90 == 0)
                {
                    myStage.AddActor(new Boss1Bolt(Center.ToPoint().ToVector2() + new Vector2(-7, 8), myStage));

                    EngineHelpers.PlaySound("LaserShoot");
                }

                if (ticks % 90 == 0)
                {
                    myStage.AddActor(new Boss1Bolt(Center.ToPoint().ToVector2() + new Vector2(1, 8), myStage));

                    EngineHelpers.PlaySound("LaserShoot");
                }

                if (health < 4) GetComponent<SpriteRenderer>().frame = new Point(0, 1);
            }
            else
            {
                GetComponent<SpriteRenderer>().origin += new Vector2(EngineHelpers.NextFloat(-1f, 1f), EngineHelpers.NextFloat(-1f, 1f));

                GetComponent<SpriteRenderer>().origin.X = GetComponent<SpriteRenderer>().origin.X.Clamp(-2f, 2f);
                GetComponent<SpriteRenderer>().origin.Y = GetComponent<SpriteRenderer>().origin.Y.Clamp(-2f, 2f);

                GetComponent<Rigidbody>().velocity.X *= 0.93f;

                if(ticks % 5 == 0)
                    myStage.AddActor(new SmokeBurst(Center, myStage));

                if (ticks > 60)
                {
                    myStage.AddActor(new SmokeBurst(Center, myStage));

                    for (int i = 0; i < 5; i++)
                    {
                        myStage.AddActor(new Nut(Center + new Vector2(-3, 3), myStage));
                    }

                    (myStage as GameStage).player.kills++;

                    EngineHelpers.PlaySound("EnemyKilled" + EngineHelpers.Next(3));

                    Kill();
                }
            }

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
                        myStage.AddActor(new SmokeBurst(Center, myStage));

                        ticks = 0;

                        dying = true;
                    }
                }
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
