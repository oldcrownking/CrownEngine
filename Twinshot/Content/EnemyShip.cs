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
    class EnemyShip : Actor
    {
        public EnemyShip(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            SpriteRenderer spriteRenderer = new SpriteRenderer(false, this);

            spriteRenderer.tex = EngineHelpers.GetTexture("EnemyShipTier1");

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

            for (int i = 0; i < GetComponent<BoxTrigger>().triggers.Count; i++)
            {
                if (GetComponent<BoxTrigger>().triggers[i] != null && GetComponent<BoxTrigger>().triggerNames[i] == "PlayerBolt")
                {
                    EngineHelpers.GetSound("EnemyKilled" + EngineHelpers.Next(3)).Play();

                    GetComponent<BoxTrigger>().triggers[0].Kill();

                    (myStage as GameStage).player.kills++;

                    if ((myStage as GameStage).player.kills % 3 == 0)
                    {
                        myStage.AddActor(new Nut(Center + new Vector2(-3, -3), myStage));
                    }

                    myStage.AddActor(new SmokeBurst(Center, myStage));

                    GetComponent<BoxTrigger>().triggers[i].Kill();
                    GetComponent<BoxTrigger>().triggerNames[i] = "";

                    Kill();
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
