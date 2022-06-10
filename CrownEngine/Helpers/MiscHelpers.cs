using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using CrownEngine.Systems;

namespace CrownEngine
{
    public static partial class EngineHelpers
    {
        public static void SwitchStages(int newStage)
        {
            EngineGame.activeStage.Unload();

            EngineGame.activeStage = EngineGame.instance.stages[newStage];

            if (EngineGame.activeStage.loaded == false) EngineGame.activeStage.Load();
        }

        public static SoundEffect GetSound(string name) => EngineGame.instance.GetSystem<>.Audio[name + ".wav"];

        public static void PlaySound(string name) => GetSound(name).Play();
    }
}
