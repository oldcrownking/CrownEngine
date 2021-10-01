using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using System.Diagnostics;
using Deprecated;
using Microsoft.Xna.Framework.Input;

namespace Deprecated.Content
{
    public class Roboforge : Actor
    {
        public Roboforge(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            AddComponent(new BoxTrigger(this));

            width = 10;
            height = 10;
        }


        public override void Update()
        {
            base.Update();
            for (int i = 0; i < GetComponent<BoxTrigger>().triggers.Count; i++)
            {
                if (GetComponent<BoxTrigger>().triggers[i] != null)
                {
                    if (GetComponent<BoxTrigger>().triggerNames[i] == "Player")
                    {
                        ((myStage as GameStage).player as Player).canUpgrade = true;
                    }
                }
            }
        }

        public override void PreDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EngineHelpers.GetTexture("Roboforge"), position - myStage.screenPosition + new Vector2(-7, -37 + 10), new Rectangle(0, 0, 24, 37), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
