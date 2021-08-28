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
        }

        public int shotCooldown;
        public int kills;
        public override void Update()
        {
            if(EngineGame.instance.keyboardState.IsKeyDown(Keys.A))
                GetComponent<Rigidbody>().velocity.X -= 0.1f;

            if (EngineGame.instance.keyboardState.IsKeyDown(Keys.D))
                GetComponent<Rigidbody>().velocity.X += 0.1f;

            if (!EngineGame.instance.keyboardState.IsKeyDown(Keys.A) && !EngineGame.instance.keyboardState.IsKeyDown(Keys.D))
                GetComponent<Rigidbody>().velocity.X *= 0.95f;

            if (EngineGame.instance.keyboardState.IsKeyDown(Keys.Space) && !EngineGame.instance.oldKeyboardState.IsKeyDown(Keys.Space) && shotCooldown <= 0)
            {
                //myStage.AddActor(new PlayerBolt(new Vector2(Center.X - 3, position.Y - 5), myStage));
                //myStage.AddActor(new PlayerBolt(new Vector2(Center.X + 1, position.Y - 5), myStage));

                myStage.AddActor(new PlayerBolt(new Vector2(Center.X - 1, position.Y - 5), myStage));

                shotCooldown = 10;

                EngineHelpers.GetSound("LaserShoot").Play();
            }

            shotCooldown--;

            base.Update();

            if (position.X < 0 || position.X > 56) GetComponent<Rigidbody>().velocity.X = 0;
            position.X = position.X.Clamp(0, 56);

            for (int i = 0; i < GetComponent<BoxTrigger>().triggers.Count; i++)
            {
                if (GetComponent<BoxTrigger>().triggers[i] != null && GetComponent<BoxTrigger>().triggerNames[i] == "Nut")
                {
                    EngineHelpers.GetSound("GetNut").Play();

                    GetComponent<BoxTrigger>().triggers[i].Kill();
                    GetComponent<BoxTrigger>().triggerNames[i] = "";
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
