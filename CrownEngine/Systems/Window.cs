using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;

namespace CrownEngine.Systems
{
    /// <summary>
    /// Window manages rendering to the screen for all WindowLayers.
    /// </summary>
    public class Window : GameSystem
    {
        public virtual int windowWidth => 256;
        public virtual int windowHeight => 144;
        public virtual int windowScale => 2;

        public List<WindowLayer> layers;

        public GraphicsDeviceManager graphics;

        public Window() : base()
        {
            graphics = new GraphicsDeviceManager(EngineGame.instance);

            graphics.PreferredBackBufferWidth = windowWidth * windowScale;
            graphics.PreferredBackBufferHeight = windowHeight * windowScale;


            layers = new List<WindowLayer>();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            PrepareRenderTargets(spriteBatch);

            DrawRenderTargets(spriteBatch);
        }

        public void PrepareRenderTargets(SpriteBatch spriteBatch)
        {
            foreach(WindowLayer layer in layers)
            {
                EngineGame.instance.GraphicsDevice.SetRenderTarget(layer.renderTarget);
                EngineGame.instance.GraphicsDevice.Clear(layer.clearColor);

                layer.Draw(spriteBatch);
            }

            EngineGame.instance.GraphicsDevice.SetRenderTarget(null);
        }

        public void DrawRenderTargets(SpriteBatch spriteBatch)
        {
            foreach (WindowLayer layer in layers)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, layer.blendState, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

                if(layer.effect != null)
                    foreach(EffectPass pass in layer.effect.CurrentTechnique.Passes)
                        pass.Apply();

                spriteBatch.Draw(layer.renderTarget, new Rectangle(0, 0, windowWidth * windowScale, windowHeight * windowScale), Color.White);

                spriteBatch.End();
            }
        }

        //TODO make this actually take parameters
        public void ChangeResolution()
        {
            graphics.PreferredBackBufferWidth = windowWidth * windowScale;
            graphics.PreferredBackBufferHeight = windowHeight * windowScale;

            graphics.ApplyChanges();
        }
    }

    public class WindowLayer
    {
        public RenderTarget2D renderTarget;

        public Color clearColor;

        public BlendState blendState;

        public int sortingId;

        public Effect effect;

        //TODO make actual params
        public WindowLayer()
        {
            renderTarget = new RenderTarget2D(EngineGame.instance.GraphicsDevice, windowWidth, windowHeight, false, SurfaceFormat.Color, DepthFormat.None);

            clearColor = Color.Transparent;

            blendState = BlendState.AlphaBlend;

            sortingId = 0;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
