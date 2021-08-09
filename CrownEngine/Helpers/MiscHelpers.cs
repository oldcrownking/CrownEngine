using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CrownEngine.Engine
{
    public static partial class EngineHelpers
    {
        public static void SwitchStages(int newStage)
        {
            EngineGame.instance.activeStage.Unload();

            EngineGame.instance.activeStage = EngineGame.instance.stages[newStage];

            if(EngineGame.instance.activeStage.loaded == false) EngineGame.instance.activeStage.Load();
        }
    }
}
