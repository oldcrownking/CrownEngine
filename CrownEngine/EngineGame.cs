using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrownEngine;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Reflection;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;

namespace CrownEngine
{
    public class EngineGame : Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        public static EngineGame instance;

        public virtual int windowWidth => 256;
        public virtual int windowHeight => 144;
        public virtual int windowScale => 2;

        public Stage activeStage;
        public List<Stage> stages = new List<Stage>();

        public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public Dictionary<string, SoundEffect> Audio = new Dictionary<string, SoundEffect>();

        //public Dictionary<string, Effect> Effects = new Dictionary<string, Effect>();

        public Random random;

        public KeyboardState oldKeyboardState;
        public KeyboardState keyboardState;

        public MouseState oldMouseState;
        public MouseState mouseState;

        public Vector2 mousePos;
        public Vector2 oldMousePos;

        public PrimitiveType primitiveBatch;

        public Effect outlineEffect;

        public EngineGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            instance = this;
        }

        protected override void Initialize()
        {
            scene = new RenderTarget2D(GraphicsDevice, windowWidth, windowHeight, false, SurfaceFormat.Color, DepthFormat.None);

            /*foreach (string file in Directory.EnumerateFiles("Content/", "*.fx", SearchOption.AllDirectories))
            {
                string fixedPath = file.Substring(Content.RootDirectory.Length).TrimStart(Path.DirectorySeparatorChar);
                Effects[Path.GetFileName(fixedPath)] = Content.Load<Effect>(fixedPath);
            }*/

            random = new Random();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            outlineEffect = Content.Load<Effect>("Outline");

            CustomInitialize();

            base.Initialize();
        }

        public virtual void CustomInitialize()
        {

        }

        public virtual void InitializeStages(List<Stage> _stages)
        {
            stages = _stages;

            activeStage = stages[0];

            activeStage.Load();
        }

        protected override void Update(GameTime gameTime)
        {
            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            oldMouseState = mouseState;
            mouseState = Mouse.GetState();

            mousePos = (mouseState.Position.ToVector2() / windowScale);
            oldMousePos = (oldMouseState.Position.ToVector2() / windowScale);

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            activeStage.Update();

            CustomUpdate();

            base.Update(gameTime);
        }

        public virtual void CustomUpdate()
        {

        }

        protected void DrawSceneToTexture(RenderTarget2D renderTarget)
        {
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(activeStage.bgColor);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            activeStage.PreDraw(_spriteBatch);

            activeStage.Draw(_spriteBatch);

            activeStage.PostDraw(_spriteBatch);

            // Drop the render target
            GraphicsDevice.SetRenderTarget(null);
        }

        //Drawing the scene
        private RenderTarget2D scene;
        protected override void Draw(GameTime gameTime)
        {
            _graphics.PreferredBackBufferWidth = windowWidth * windowScale;
            _graphics.PreferredBackBufferHeight = windowHeight * windowScale;
            _graphics.ApplyChanges();

            DrawSceneToTexture(scene);

            _spriteBatch.End();
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            _spriteBatch.Draw(scene, new Rectangle(0, 0, windowWidth * windowScale, windowHeight * windowScale), Color.White);

            CustomPostDraw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public virtual void CustomPostDraw(SpriteBatch spriteBatch)
        {

        }
    }
}
