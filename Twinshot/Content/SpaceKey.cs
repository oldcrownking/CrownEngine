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
    public class SpaceKey : Actor
    {
        public SpaceKey(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            SpriteRenderer spriteRenderer = new SpriteRenderer(true, this);

            spriteRenderer.tex = EngineHelpers.GetTexture("SpaceKey");

            AddComponent(spriteRenderer);
            AddComponent(new Rigidbody(this));
            AddComponent(new BoxTrigger(this));

            GetComponent<Rigidbody>().velocity = new Vector2(0, 2);

            width = 24;
            height = 9;

            health = 3;
        }

        public int ticks;
        public int frame;
        public int health = 3;

        public override void Update()
        {
            base.Update();

            for (int i = 0; i < GetComponent<BoxTrigger>().triggers.Count; i++)
            {
                if (GetComponent<BoxTrigger>().triggers[i] != null && GetComponent<BoxTrigger>().triggerNames[i] == "PlayerBolt")
                {
                    health--;

                    GetComponent<BoxTrigger>().triggers[i].Kill();
                    GetComponent<BoxTrigger>().triggerNames[i] = "";

                    if (health > 0)
                    {
                        EngineHelpers.GetSound("EnemyHit").Play();
                    }
                    else
                    {
                        myStage.AddActor(new SmokeBurst(Center, myStage));

                        EngineHelpers.GetSound("EnemyKilled").Play();

                        (myStage as GameStage).wave++;

                        Kill();
                    }
                }
            }

            if (health > 0) GetComponent<Rigidbody>().velocity *= 0.95f;
        }
    }
}
