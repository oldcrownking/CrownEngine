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
    public class BossWarning : Actor
    {
        public BossWarning(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            SpriteRenderer spriteRenderer = new SpriteRenderer(true, this);

            spriteRenderer.tex = EngineHelpers.GetTexture("BossWarningText");

            AddComponent(spriteRenderer);
            AddComponent(new Rigidbody(this));
            AddComponent(new BoxTrigger(this));

            GetComponent<Rigidbody>().velocity = new Vector2(0, 2);

            width = 20;
            height = 9;

            health = 3;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EngineHelpers.GetTexture("BossWarningSign"), position + new Vector2(-14, -1), new Rectangle(frame * 12, 0, 12, 11), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(EngineHelpers.GetTexture("BossWarningSign"), position + new Vector2(22, -1), new Rectangle((1 - frame) * 12, 0, 12, 11), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            base.Draw(spriteBatch);
        }

        public int ticks;
        public int frame;
        public int health = 3;

        public override void Update()
        {
            base.Update();

            ticks++;

            if (ticks % 15 == 0) frame++;
            if (frame >= 2) frame = 0;

            for (int i = 0; i < GetComponent<BoxTrigger>().triggers.Count; i++)
            {
                if (GetComponent<BoxTrigger>().triggers[i] != null && GetComponent<BoxTrigger>().triggerNames[i] == "PlayerBolt")
                {
                    health--;

                    GetComponent<BoxTrigger>().triggers[i].Kill();
                    GetComponent<BoxTrigger>().triggerNames[i] = "";

                    if (health > 0)
                    {
                        EngineHelpers.PlaySound("EnemyHit" + EngineHelpers.Next(3));
                    }
                    else
                    {
                        myStage.AddActor(new SmokeBurst(Center, myStage));

                        EngineHelpers.PlaySound("EnemyKilled" + EngineHelpers.Next(3));

                        Kill();
                    }
                }
            }

            if (health > 0) GetComponent<Rigidbody>().velocity *= 0.95f;
        }
    }
}
