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
        public int windowWidth;
        public int windowHeight;
        public int windowScale;

        public List<WindowLayer> layers;

        public GraphicsDeviceManager graphics;

        public Window() : base()
        {

        }

        public override void Load()
        {
            graphics = new GraphicsDeviceManager(EngineGame.instance);

            if (windowHeight == 0 || windowWidth == 0 || windowScale == 0) throw new Exception("Window dimension parameters not set");

            graphics.PreferredBackBufferWidth = windowWidth * windowScale;
            graphics.PreferredBackBufferHeight = windowHeight * windowScale;

            layers = new List<WindowLayer>();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            PrepareLayers(spriteBatch);

            DrawLayers(spriteBatch);
        }

        public void PrepareLayers(SpriteBatch spriteBatch)
        {
            foreach(WindowLayer layer in layers)
            {
                EngineGame.instance.GraphicsDevice.SetRenderTarget(layer.renderTarget);
                EngineGame.instance.GraphicsDevice.Clear(layer.clearColor);

                layer.Draw(spriteBatch);
            }

            EngineGame.instance.GraphicsDevice.SetRenderTarget(null);
        }

        public void DrawLayers(SpriteBatch spriteBatch)
        {
            foreach (WindowLayer layer in layers)
            {
                spriteBatch.Begin(SpriteSortMode.Immediate, layer.blendState, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

                if(layer.effect != null)
                    foreach(EffectPass pass in layer.effect.CurrentTechnique.Passes)
                        pass.Apply();

                spriteBatch.Draw(layer.renderTarget, layer.outputRect, Color.White);

                spriteBatch.End();
            }
        }

        public void ChangeResolution(int newWindowWidth, int newWindowHeight, int newWindowScale)
        {
            windowWidth = newWindowHeight;
            windowHeight = newWindowHeight;
            windowWidth = newWindowWidth;

            graphics.PreferredBackBufferWidth = windowWidth * windowScale;
            graphics.PreferredBackBufferHeight = windowHeight * windowScale;

            graphics.ApplyChanges();
        }
    }

    public class WindowLayer
    {
        public RenderTarget2D renderTarget;
        public int sortingId;
        public Rectangle outputRect;

        public Color clearColor;
        public BlendState blendState;
        public Effect effect;

        private GraphicsDevice graphicsDevice;

        public WindowLayer(int _sortingId, Rectangle _outputRect, Point dimensions, BlendState _blendState)
        {
            graphicsDevice = EngineGame.instance.GraphicsDevice;

            renderTarget = new RenderTarget2D(graphicsDevice, dimensions.X, dimensions.Y, false, SurfaceFormat.Color, DepthFormat.None);

            clearColor = Color.Transparent;

            blendState = _blendState;

            sortingId = _sortingId;

            effect = new BasicEffect(graphicsDevice);
        }

        public WindowLayer(int _sortingId, Rectangle _outputRect, Point dimensions, BlendState _blendState, Color _clearColor)
        {
            graphicsDevice = EngineGame.instance.GraphicsDevice;

            renderTarget = new RenderTarget2D(graphicsDevice, dimensions.X, dimensions.Y, false, SurfaceFormat.Color, DepthFormat.None);

            clearColor = _clearColor;

            blendState = _blendState;

            sortingId = _sortingId;

            effect = new BasicEffect(graphicsDevice);
        }

        public WindowLayer(int _sortingId, Rectangle _outputRect, Point dimensions, BlendState _blendState, Color _clearColor, Effect _effect)
        {
            graphicsDevice = EngineGame.instance.GraphicsDevice;

            renderTarget = new RenderTarget2D(graphicsDevice, dimensions.X, dimensions.Y, false, SurfaceFormat.Color, DepthFormat.None);

            clearColor = _clearColor;

            blendState = _blendState;

            sortingId = _sortingId;

            effect = _effect;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
