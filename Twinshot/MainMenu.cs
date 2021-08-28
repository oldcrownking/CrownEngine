﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;

namespace Twinshot.Content
{
    public class MainMenu : Stage
    {
        public override Color bgColor => Color.Black;

        public override void Update()
        {
            if (playButton.GetComponent<Button>().pressed) loading = true;

            base.Update();
        }

        public int ticks;
        public int coverHeight = 0;
        public bool loading;
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (loading)
            {
                ticks++;

                coverHeight += 4 + (ticks / 4);

                spriteBatch.Draw(EngineHelpers.GetTexture("MagicPixel"), new Rectangle(0, 0, EngineGame.instance.windowWidth, coverHeight), null, Color.White);
            }

            if(coverHeight >= 128 && loading) EngineHelpers.SwitchStages(1);

            base.Draw(spriteBatch);
        }

        PlayButton playButton;
        public override void Load()
        {
            Actor logo = new Actor(new Vector2(16, 40), this);

            logo.width = 32;
            logo.height = 7;

            logo.AddComponent(new SpriteRenderer(false, logo));
            logo.GetComponent<SpriteRenderer>().tex = EngineHelpers.GetTexture("TwinshotLogo");

            AddActor(logo);

            playButton = new PlayButton(new Vector2(23, 54), this);

            AddActor(playButton);
        }
    }
}