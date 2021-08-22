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
        public static void DrawAdditive(SpriteBatch spriteBatch, Texture2D tex, Vector2 position, Color color, float scale = 1f, float rotation = 0f)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);

            spriteBatch.Draw(tex, position, tex.Bounds, color, rotation, tex.TextureCenter(), scale, SpriteEffects.None, 0f);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, null);
        }

        public static void DrawPrimitive(Vector2 vec1, Vector2 vec2, Vector2 vec3, Color color1, Color color2, Color color3)
        {
            VertexPositionColor[] vertices = new VertexPositionColor[3];

            vertices[0] = new VertexPositionColor(new Vector3(vec1.X, vec1.Y, 0), color1);
            vertices[1] = new VertexPositionColor(new Vector3(vec2.X, vec2.Y, 0), color2);
            vertices[2] = new VertexPositionColor(new Vector3(vec3.X, vec3.Y, 0), color3);

            EngineGame.instance.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 1);
        }

        public static void DrawPrimitive(Vector2 vec1, Vector2 vec2, Vector2 vec3, Color color)
        {
            VertexPositionColor[] vertices = new VertexPositionColor[3];

            vertices[0] = new VertexPositionColor(new Vector3(vec1.X, vec1.Y, 0), color);
            vertices[1] = new VertexPositionColor(new Vector3(vec2.X, vec2.Y, 0), color);
            vertices[2] = new VertexPositionColor(new Vector3(vec3.X, vec3.Y, 0), color);

            EngineGame.instance.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 1);
        }

        public static Vector2 ToPrimCoordinates(Vector2 point) => new Vector2(
            (point.X - (EngineGame.instance.windowWidth / 2)) / (EngineGame.instance.windowWidth / 2),
            (point.Y - (EngineGame.instance.windowHeight / 2)) / (-EngineGame.instance.windowHeight / 2));

        public static Texture2D GetTexture(string name)
        {
            return EngineGame.instance.Textures[name + ".png"];
        }

        public static void DrawOutline(SpriteBatch spriteBatch, float alpha, Color color, Texture2D tex, Vector2 pos, float rotation, float scale)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null);

            EngineGame.instance.outlineEffect.CurrentTechnique.Passes[0].Apply();

            EngineGame.instance.outlineEffect.Parameters["alpha"].SetValue(alpha);
            EngineGame.instance.outlineEffect.Parameters["color"].SetValue((new Vector3(color.R, color.G, color.B) / 255f) * alpha);

            alpha = MathHelper.Clamp(alpha, 0f, 1f);

            for (int i = 0; i < 4; i++)
            {
                Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * (2 * (alpha / 1) * ((255 - alpha) / 255f));
                spriteBatch.Draw(tex, pos + offsetPositon, null, Color.White * alpha * ((255 - alpha) / 255f), rotation, tex.Bounds.Size.ToVector2() * 0.5f, scale, SpriteEffects.None, 0f);
            }

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
        }

        public static void DrawOutline(SpriteBatch spriteBatch, float alpha, Color color, Texture2D tex, Vector2 pos, Rectangle sourceRectangle, float rotation, float scale)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null);

            EngineGame.instance.outlineEffect.CurrentTechnique.Passes[0].Apply();

            EngineGame.instance.outlineEffect.Parameters["alpha"].SetValue(alpha);
            EngineGame.instance.outlineEffect.Parameters["color"].SetValue((new Vector3(color.R, color.G, color.B) / 255f) * alpha);

            alpha = MathHelper.Clamp(alpha, 0f, 1f);

            for (int i = 0; i < 4; i++)
            {
                Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i);
                spriteBatch.Draw(tex, pos + offsetPositon, sourceRectangle, Color.White * alpha * ((255 - alpha) / 255f), rotation, tex.Bounds.Size.ToVector2() * 0.5f, scale, SpriteEffects.None, 0f);
            }

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
        }

        public static void DrawOutline(SpriteBatch spriteBatch, float alpha, Color color, Texture2D tex, Vector2 pos, Rectangle sourceRectangle, float rotation, float scale, SpriteEffects spriteEffects)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null);

            EngineGame.instance.outlineEffect.CurrentTechnique.Passes[0].Apply();

            EngineGame.instance.outlineEffect.Parameters["alpha"].SetValue(alpha);
            EngineGame.instance.outlineEffect.Parameters["color"].SetValue((new Vector3(color.R, color.G, color.B) / 255f) * alpha);

            alpha = MathHelper.Clamp(alpha, 0f, 1f);

            for (int i = 0; i < 4; i++)
            {
                Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i);
                spriteBatch.Draw(tex, pos + offsetPositon, sourceRectangle, Color.White * alpha * ((255 - alpha) / 255f), rotation, tex.Bounds.Size.ToVector2() * 0.5f, scale, spriteEffects, 0f);
            }

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
        }
    }
}
