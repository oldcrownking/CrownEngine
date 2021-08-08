using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrownEngine.Content;

namespace CrownEngine.Engine
{
    public static class Extensions
    {
        public static float ToRadians(this float num) => num * (float)(3.14f / 180f);

        public static float ToRadians(this double num) => (float)(num * (3.14f / 180f));

        public static float ToRadians(this int num) => num * (float)(3.14f / 180f);

        public static float PositiveSin(this float num) => (num / 2f) + 0.5f;

        public static float PositiveSin(this double num) => (float)(num / 2f) + 0.5f;

        public static Vector2 TextureCenter(this Texture2D tex) => tex.Bounds.Size.ToVector2() / 2f;

        public static Vector2 RotatedBy(this Vector2 vec, float rot)
        {
            float sin = (float)Math.Sin(rot);
            float cos = (float)Math.Cos(rot);

            float tx = vec.X;
            float ty = vec.Y;

            vec.X = (cos * tx) - (sin * ty);
            vec.Y = (sin * tx) + (cos * ty);

            return vec;
        }

        public static float Clamp(this float num, float min, float max)  
        {
            if (num < min) num = min;
            if (num > max) num = max;

            return num;
        }

        public static Vector2 ClampVectorMagnitude(this Vector2 vec, float max)
        {
            if (vec.Length() < max) return vec;

            return Vector2.Normalize(vec) * max;
        }
    }
}
