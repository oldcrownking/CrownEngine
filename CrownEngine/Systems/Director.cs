using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using System.IO;
using Microsoft.Xna.Framework.Audio;

namespace CrownEngine.Systems
{
    /// <summary>
    /// Director manages Stages and dictates what Actors remain present
    /// between stage transitions.
    /// </summary>
    public class Director : GameSystem
    {
        //public static Director Instance { get; private set; }

        public List<Stage> stages => new List<Stage>();
        public List<Actor> offstageActors => new List<Actor>();

        public static Stage activeStage;

        public Director() : base()
        {

        }

        public override void Update()
        {
            activeStage.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
