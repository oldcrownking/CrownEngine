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
        public override int windowHeight => 216;
        public override int windowWidth => 96;

        public override int windowScale => 2;

        public SandboxGame() : base()
        {
            IsMouseVisible = true;
        }

        public override void CustomInitialize()
        {
            base.CustomInitialize();
        }
    }
}
