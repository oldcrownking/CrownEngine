using CrownEngine;
using CrownEngine.Prefabs;
using LudumDareFifty.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LudumDareFifty
{
    public class GameStage : Stage
    {
        public override Color bgColor => new Color(31, 34, 38);

        public Player player;
        public override void Load()
        {
            player = new Player(new Vector2(128, 196), this);
            

            AddActor(player);

            AddActor(new SolidTilemap(new Vector2(0, 0), this, new int[,] {
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,},
                {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,},
                {1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,},
                {1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,},
                {1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,},
                {1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,},
                {1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,},
                {1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,},
            }, new List<Texture2D> { EngineHelpers.GetTexture("Tile") }, 8));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EngineHelpers.GetTexture("BigClock"), new Rectangle(128 - 72, 56, 144, 144), new Rectangle(0, 0, 144, 144), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);

            Vector2 myPos = new Vector2(128 - 8, 16);

            spriteBatch.Draw(EngineHelpers.GetTexture("HourglassUIFill"), new Rectangle((int)(myPos.X + 1), (int)(myPos.Y + 1), 14, 30), new Rectangle(0, 0, 14, 30), new Color(43, 37, 84), 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.Draw(EngineHelpers.GetTexture("HourglassUIFill"),
                new Rectangle((int)(myPos.X + 1), (int)(myPos.Y + 1 + ((1 - ((float)player.time / (float)player.maxTime)) * 30)), 14, (int)((((float)player.time / (float)player.maxTime)) * 30)),
                new Rectangle(0, (int)((1 - ((float)player.time / (float)player.maxTime)) * 30), 14, (int)(((float)player.time / (float)player.maxTime) * 30)),
                new Color(35, 102, 180), 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.Draw(EngineHelpers.GetTexture("HourglassUI"), new Rectangle((int)(myPos.X), (int)(myPos.Y), 16, 32), new Rectangle(0, 0, 16, 32), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);


            if (gameOver)
            {

            }

            base.Draw(spriteBatch);
        }

        public bool gameOver;

        public int ticker;

        public override void Update()
        {
            if (player == null)
            {
                gameOver = true;
            }

            ticker++;

            if (ticker % 360 == 0)
            {
                Debug.WriteLine(ticker);

                Timepiece clocc = new Timepiece(new Vector2(-7, 40), this);

                AddActor(clocc);

                clocc.GetComponent<Rigidbody>().velocity = new Vector2(5, -2);
            }
            if (ticker % 360 == 180)
            {
                Debug.WriteLine(ticker);

                Timepiece clocc = new Timepiece(new Vector2(256, 40), this);

                AddActor(clocc);

                clocc.GetComponent<Rigidbody>().velocity = new Vector2(-5, -2);
            }

            if (ticker % 30 == 0)
            {
                AddActor(new Rocket(new Vector2((new Random()).Next(16, 240), -10), this));
            }

            base.Update();
        }
    }
}
