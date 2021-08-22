using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CrownEngine
{
    public static partial class EngineHelpers
    {
        public static float NextFloat(float val) => (float)EngineGame.instance.random.NextDouble() * val;

        public static float NextFloat(float min, float max) => min + ((float)EngineGame.instance.random.NextDouble() * (max - min));

        public static bool NextBool(int val) => EngineGame.instance.random.Next(val) == 0;

        public static bool NextBool(float val) => (float)EngineGame.instance.random.NextDouble() <= val;

        public static int Next(int val) => EngineGame.instance.random.Next(val);

        public static int Next(int min, int max) => min + EngineGame.instance.random.Next(min, max);

        public static Vector2 RotatedByRandom() => Vector2.UnitY * NextFloat(6.28f);

        public static Vector2 RotatedByRandom(float angle) => Vector2.UnitY * NextFloat(angle);

        public static Vector2 RotatedByRandom(float angle, float startingAngle) => Vector2.UnitY * (startingAngle + NextFloat(angle));
    }
}
