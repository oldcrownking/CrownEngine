using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using System.Diagnostics;

namespace Twinshot.Content
{
    public class GameStage : Stage
    {
        public override Color bgColor => Color.Black;

        public PlayerShip player;
        public override void Load()
        {
            //AddActor(new Moon(new Vector2(EngineHelpers.Next(-8, 49), 8), this, 0.1f));

            player = new PlayerShip(new Vector2(28, 112), this);

            AddActor(player);
        }

        public int coverHeight;
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (preTicks < 60)
            {
                if (preTicks >= 30) coverHeight += 4 + ((preTicks - 30) / 4);
                spriteBatch.Draw(EngineHelpers.GetTexture("MagicPixel"), new Rectangle(0, coverHeight, EngineGame.instance.windowWidth, EngineGame.instance.windowHeight), null, Color.White);
            }
        }

        public int ticks;
        public int preTicks;
        public float wave = -2;

        AKey akey;
        DKey dkey;
        SpaceKey spkey;

        public override void Update()
        {
            preTicks++;

            if (wave < 0) 
            { 
                if (preTicks % 5 == 0) AddActor(new Star(new Vector2(EngineHelpers.Next(0, 64), -3), this, EngineHelpers.Next(15, 30), EngineHelpers.NextFloat(0.25f, 3f)));
            }

            if (wave >= 0)
            {
                if (ticks % 5 == 0) AddActor(new Star(new Vector2(EngineHelpers.Next(0, 64), -3), this, EngineHelpers.Next(15, 30), EngineHelpers.NextFloat(0.25f, 3f)));

                if (ticks > 300 && AllEnemiesDefeated())
                {
                    ticks = 0;
                    
                    SpawnWave((int)wave);
                    wave++;
                }

                ticks++;
            }

            if (preTicks >= 120 && akey == default && dkey == default && wave == -2)
            {
                akey = new AKey(new Vector2(-9, 114), this);
                dkey = new DKey(new Vector2(64, 114), this);

                AddActor(akey);
                AddActor(dkey);
            }

            if (preTicks >= 90 && spkey == default && wave == -1)
            {
                spkey = new SpaceKey(new Vector2(20, -9), this);

                AddActor(spkey);
            }

            base.Update();
        }

        public void SpawnWave(int _wave)
        {
            if (_wave == 0)
            {
                for (int i = 0; i < 2; i++)
                    AddActor(new EnemyShip(new Vector2(20 + i * 12, -13), this));
            }

            if (_wave == 1)
            {
                for (int i = 0; i < 4; i++) 
                    AddActor(new EnemyShip(new Vector2(4 + i * 12, -13), this));
            }

            if (_wave == 2)
            {
                for (int i = 0; i < 4; i++)
                    AddActor(new EnemyShip(new Vector2(4 + i * 12, -28), this));

                for (int i = 0; i < 4; i++)
                    AddActor(new EnemyShip(new Vector2(4 + i * 12, -13), this));
            }

            if (_wave == 3)
            {
                AddActor(new EnemyShip2(new Vector2(27, -15), this));
            }

            if (_wave == 4)
            {
                for (int i = 0; i < 2; i++)
                    AddActor(new EnemyShip2(new Vector2(20 + i * 12, -15), this));
            }

            //Spawn shop

            if (_wave == 5)
            {
                AddActor(new EnemyShip3(new Vector2(16, -16), this));
            }
        }

        public bool AllEnemiesDefeated()
        {
            foreach(Actor actor in actors)
            {
                if (actor != null && actor.GetType().Name.Contains("EnemyShip")) return false;
            }

            return true;
        }
    }
}