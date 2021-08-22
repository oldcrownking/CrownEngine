using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace CenturionGame.Content
{
    public class Shield : Actor
    {
        public Shield(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        private int jumpTimer;

        public float counter;

        private Point frame;

        public int direction;

        private int walkFrame;

        public override void Load()
        {
            AddComponent(new Rigidbody(this));
            //AddComponent(new BoxCollider(this));
            AddComponent(new SpriteRenderer(true, this));

            SpriteRenderer sr = GetComponent<SpriteRenderer>();

            sr.tex = EngineHelpers.GetTexture("Shield");

            SetComponent<SpriteRenderer>(sr);

            width = 4;
            height = 16;

            base.Load();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public Player myPlayer;

        public override void Update()
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            SetComponent<Rigidbody>(rb);

            base.Update();
        }
    }
}
