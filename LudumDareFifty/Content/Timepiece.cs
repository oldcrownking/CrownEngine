using CrownEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using LudumDareFifty.Content;

namespace LudumDareFifty.Content
{
    public class Timepiece : Actor
    {
        public Timepiece(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        public override void Load()
        {
            AddComponent(new BoxCollider(this));
            AddComponent(new Rigidbody(this));

            GetComponent<Rigidbody>().gravityDir = Vector2.UnitY;
            GetComponent<Rigidbody>().gravityForce = 0.2f;

            width = 7;
            height = 7;
        }

        public Vector2 oldPosition;
        public int ticks;
        public override void Update()
        {
            GetComponent<Rigidbody>().velocity.X *= 0.96f;

            oldPosition = position;

            ticks++;

            if (ticks > 150)
                Kill();

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(ticks < 120 || (ticks % 10 < 5))
                spriteBatch.Draw(EngineHelpers.GetTexture("Timepiece"), position - myStage.screenPosition, new Rectangle(0, 0, 7, 7), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            base.Draw(spriteBatch);
        }
    }
}
