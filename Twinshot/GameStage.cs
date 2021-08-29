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

            if (!(Twinshot.game as TwinshotGame).hasSeenTutorial) wave = -2;
            else wave = 0;

            AddActor(player);
        }

        public int coverHeight;
        public int nutFrame;
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Draw(EngineHelpers.GetTexture("MagicPixel"), new Rectangle(0, 128, 64, 16), null, Color.Black);

            spriteBatch.Draw(EngineHelpers.GetTexture("MagicPixel"), new Rectangle(0, 128, 64, 1), null, Color.White);

            if (ticks % 12 == 0 || preTicks % 12 == 0) nutFrame++;
            if (nutFrame > 3) nutFrame = 0;

            spriteBatch.Draw(EngineHelpers.GetTexture("Nut"), new Vector2(44, 132), new Rectangle(0, 6 * nutFrame, 6, 6), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            DrawNumber(spriteBatch, player.nuts, new Vector2(52, 133), 4);

            spriteBatch.Draw(EngineHelpers.GetTexture("Healthbar"), new Vector2(2, 131), new Rectangle(0, (player.health >= 0 ? 0 : 8), 8, 8), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(EngineHelpers.GetTexture("Healthbar"), new Vector2(10, 131), new Rectangle(9, (player.health >= 1 ? 0 : 8), 7, 8), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(EngineHelpers.GetTexture("Healthbar"), new Vector2(17, 131), new Rectangle(9, (player.health >= 2 ? 0 : 8), 7, 8), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(EngineHelpers.GetTexture("Healthbar"), new Vector2(24, 131), new Rectangle(17, (player.health >= 3 ? 0 : 8), 9, 8), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);


            if (preTicks < 60)
            {
                if (preTicks >= 30) coverHeight += 4 + ((preTicks - 30) / 4);
                spriteBatch.Draw(EngineHelpers.GetTexture("MagicPixel"), new Rectangle(0, coverHeight, EngineGame.instance.windowWidth, EngineGame.instance.windowHeight), null, Color.White);
            }
        }

        public int ticks = -1;
        public int preTicks;
        public float wave = -2;

        AKey akey;
        DKey dkey;
        SpaceKey spkey;

        public override void Update()
        {
            //Debug.Write(wave);

            if (wave < 0) 
            {
                preTicks++;

                if (preTicks % 5 == 0) AddActor(new Star(new Vector2(EngineHelpers.Next(0, 64), -3), this, EngineHelpers.Next(15, 30), EngineHelpers.NextFloat(0.25f, 3f)));
            }

            if (wave >= 0)
            {
                if (ticks % 5 == 0) AddActor(new Star(new Vector2(EngineHelpers.Next(0, 64), -3), this, EngineHelpers.Next(15, 30), EngineHelpers.NextFloat(0.25f, 3f)));

                Debug.WriteLine(player.deathCooldown);
                Debug.WriteLine(wave);

                ticks++;

                if (ticks > 300 && AllEnemiesDefeated() && player.deathCooldown < 0)
                {
                    ticks = 0;

                    Debug.WriteLine("ye!");

                    SpawnWave((int)wave);
                    wave++;
                }
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

            for (int i = 0; i < actors.Count; i++)
            {
                if (actors[i] != null && actors[i].GetType().Name.Contains("Ship") && actors[i].position.Y > 128) player.Explode();
            }

            base.Update();
        }

        public void SpawnWave(int _wave)
        {
            if (_wave == 0)
            {
                (Twinshot.game as TwinshotGame).hasSeenTutorial = true;

                preTicks = -1;

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

            if (_wave == 6)
            {
                for (int i = 0; i < 2; i++)
                    AddActor(new EnemyShip3(new Vector2(10 + i * 14, -31), this));

                for (int i = 0; i < 4; i++)
                    AddActor(new EnemyShip(new Vector2(4 + i * 12, -13), this));
            }

            if(_wave == 7)
            {
                AddActor(new BossWarning(new Vector2(22, -11), this));
            }

            if(_wave == 8)
            {
                AddActor(new BossShip1(new Vector2(12, -14), this));
            }
        }

        public bool AllEnemiesDefeated()
        {
            for (int i = 0; i < actors.Count; i++)
            {
                if (actors[i] != null && (actors[i].GetType().Name.Contains("EnemyShip") || actors[i].GetType().Name.Contains("BossShip") || actors[i].GetType().Name.Contains("BossW"))) return false;
            }

            return true;
        }

        public bool ClearEnemies()
        {
            for(int i = 0; i < actors.Count; i++)
            {
                if (actors[i] != null && (actors[i].GetType().Name.Contains("Enemy") || actors[i].GetType().Name.Contains("Boss"))) actors[i] = null;
            }

            return true;
        }

        public void DrawNumber(SpriteBatch spriteBatch, int num, Vector2 pos, int spacing)
        {
            string stringnum = num.ToString();

            for (int i = 0; i < stringnum.Length; i++) 
            {
                spriteBatch.Draw(EngineHelpers.GetTexture("Numbers"), pos + new Vector2(i * spacing, 0), new Rectangle((stringnum[i] - 48) * 3, 0, 3, 5), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        }

        public void OnEnemyDeath()
        {

        } //need to figure out how to reset wave if player dies
    }
}