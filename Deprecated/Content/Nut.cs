using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using System.Diagnostics;
using Deprecated;

namespace Deprecated.Content
{
    public class PlatformNut : Actor
    {
        public PlatformNut(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            AddComponent(new Rigidbody(this));
            AddComponent(new BoxTrigger(this));

            GetComponent<Rigidbody>().velocity = Vector2.Zero;
            GetComponent<Rigidbody>().gravityForce = 0f;
            GetComponent<Rigidbody>().gravityDir = Vector2.UnitY;

            width = 6;
            height = 6;
        }

        public int ticks;
        public int frame;
        public override void Update()
        {
            ticks++;

            if (ticks % 10 == 0) frame++;
            if (frame > 3) frame = 0;

            Rigidbody rb = GetComponent<Rigidbody>();

            base.Update();

            for (int i = 0; i < GetComponent<BoxTrigger>().triggers.Count; i++)
            {
                if (GetComponent<BoxTrigger>().triggers[i] != null)
                {
                    if (GetComponent<BoxTrigger>().triggerNames[i] == "Player")
                    {
                        EngineHelpers.PlaySound("GetNut");

                        ((myStage as GameStage).player as Player).nuts += ((myStage as GameStage).player as Player).multiplierTier + 1;

                        Kill();
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EngineHelpers.GetTexture("Nut"), position - myStage.screenPosition, new Rectangle(0, frame * 6, 6, 6), Color.White);

            base.Draw(spriteBatch);
        }
    }
}
