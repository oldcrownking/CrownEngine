using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using System.Diagnostics;
using Roll;

namespace Roll.Content
{
    public class Signpost : Actor
    {
        public Signpost(Vector2 pos, Stage myStage, string _str, int _dramatic) : base(pos, myStage)
        {
            str = _str;
            dramatic = _dramatic;
        }

        public override void
            Load()
        {
            AddComponent(new SpriteRenderer(true, this));
            GetComponent<SpriteRenderer>().tex = EngineHelpers.GetTexture("Signpost");

            AddComponent(new BoxTrigger(this));

            width = 8;
            height = 8;
        }

        public string str;
        public int dramatic;
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (GetComponent<BoxTrigger>().triggerNames.Contains("Player"))
            {
                (EngineGame.instance as RollGame).DrawString(spriteBatch, str, new Vector2(Center.X + (str.Length * 4 / -2), position.Y - 6) - myStage.screenPosition, 4, dramatic, 5f, 2f);
            }
        }
    }
}
