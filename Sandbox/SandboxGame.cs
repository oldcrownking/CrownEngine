using CrownEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace Sandbox
{
    public class SandboxGame : EngineGame
    {
        public SandboxGame() : base()
        {
            IsMouseVisible = true;
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }

    public class SandboxStage : Stage
    {
        public SandboxStage()
        {
            Actor bouncingBall = new Actor();
            bouncingBall.AddComponent(new SpriteRenderer());
            bouncingBall.AddComponent(new Transform());
            bouncingBall.AddComponent(new CircleCollider());


            actors.Add();
        }
    }
}
