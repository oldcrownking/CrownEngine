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
    public class PlayerShip : Actor
    {
        public PlayerShip(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            SpriteRenderer spriteRenderer = new SpriteRenderer(false, this);

            spriteRenderer.tex = EngineHelpers.GetTexture("PlayerShipTier2");

            AddComponent(spriteRenderer);
            AddComponent(new Rigidbody(this));
            AddComponent(new BoxTrigger(this));

            width = 8;
            height = 12;

            base.Load();
        }

        public int shotCooldown;
        public int kills;
        public int nuts = 0;
        public int health = 3;

        public int deathCooldown = -1;
        public override void Update()
        {
            if(EngineGame.instance.keyboardState.IsKeyDown(Keys.A))
                GetComponent<Rigidbody>().velocity.X -= 0.1f;

            if (EngineGame.instance.keyboardState.IsKeyDown(Keys.D))
                GetComponent<Rigidbody>().velocity.X += 0.1f;

            if (!EngineGame.instance.keyboardState.IsKeyDown(Keys.A) && !EngineGame.instance.keyboardState.IsKeyDown(Keys.D))
                GetComponent<Rigidbody>().velocity.X *= 0.93f;

            if (EngineGame.instance.keyboardState.IsKeyDown(Keys.Space) && !EngineGame.instance.oldKeyboardState.IsKeyDown(Keys.Space) && shotCooldown <= 0 && deathCooldown < 0)
            {
                //myStage.AddActor(new PlayerBolt(new Vector2(Center.X - 3, position.Y - 5), myStage));
                //myStage.AddActor(new PlayerBolt(new Vector2(Center.X + 1, position.Y - 5), myStage));

                myStage.AddActor(new PlayerBolt(new Vector2(Center.X - 1, position.Y - 5), myStage));

                shotCooldown = 10;

                EngineHelpers.PlaySound("LaserShoot");
            }

            if (deathCooldown >= 0) deathCooldown--;

            if (deathCooldown == 60 && health < 0) EngineHelpers.SwitchStages(2);

            //if (deathCooldown == 1) (myStage as GameStage).SpawnWave((int)(myStage as GameStage).wave);

            if (deathCooldown > 60)
                GetComponent<SpriteRenderer>().visible = false;
            else if (deathCooldown >= 0 && deathCooldown % 10 == 0)
                GetComponent<SpriteRenderer>().visible = !GetComponent<SpriteRenderer>().visible;
            if (deathCooldown < 0) 
                GetComponent<SpriteRenderer>().visible = true;

            shotCooldown--;

            base.Update();

            if (position.X < 0 || position.X > 56) GetComponent<Rigidbody>().velocity.X = 0;
            //position.X = position.X.Clamp(0, 56);

            for (int i = 0; i < GetComponent<BoxTrigger>().triggers.Count; i++)
            {
                if (GetComponent<BoxTrigger>().triggers[i] != null && deathCooldown < 0)
                {
                    if (GetComponent<BoxTrigger>().triggerNames[i] == "Nut")
                    {
                        EngineHelpers.PlaySound("GetNut");

                        nuts++;

                        GetComponent<BoxTrigger>().triggers[i].Kill();
                        GetComponent<BoxTrigger>().triggerNames[i] = "";
                    }

                    if (GetComponent<BoxTrigger>().triggerNames[i].Contains("Enemy") || GetComponent<BoxTrigger>().triggerNames[i].Contains("Boss"))
                    {
                        GetComponent<BoxTrigger>().triggers[i].Kill();
                        GetComponent<BoxTrigger>().triggerNames[i] = "";

                        Explode();
                    }
                }
            }
        }

        public void Explode()
        {
            EngineHelpers.PlaySound("PlayerKilled");

            GetComponent<SpriteRenderer>().visible = false;
            health--;

            myStage.AddActor(new SmokeBurst(Center, myStage));

            deathCooldown = 120;

            (myStage as GameStage).wave--;

            (myStage as GameStage).ClearEnemies();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
