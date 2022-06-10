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

        public virtual List<Stage> stages => new List<Stage>();
        public static Stage activeStage;

        private static RenderTarget2D scene;

        public static Random random;

        public static List<GameSystem> systems = new List<GameSystem>();

        public EngineGame()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            instance = this;
        }

        protected override void Initialize()
        {
            scene = new RenderTarget2D(GraphicsDevice, windowWidth, windowHeight, false, SurfaceFormat.Color, DepthFormat.None);

            random = new Random();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            IEnumerable<Type> systemsArray = typeof(GameSystem).Assembly.GetTypes().Where(TheType => TheType.IsClass && !TheType.IsAbstract && TheType.IsSubclassOf(typeof(GameSystem)) && (!TheType.IsGenericType || TheType.IsConstructedGenericType));

            foreach(Type type in systemsArray)
                systems.Add(Activator.CreateInstance(type) as GameSystem);

            base.Initialize();

            InitializeStages();
        }

        public static T GetSystem<T>() where T : GameSystem
        {
            for (int i = 0; i < systems.Count; i++)
                if (systems[i] is T)
                    return systems[i] as T;

            return null;
        }

        public void InitializeStages()
        {
            activeStage = stages[0];

            activeStage.Load();
        }

        protected override void Update(GameTime gameTime)
        {
            activeStage.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            #region Initializing the graphicsdevice
            _graphics.PreferredBackBufferWidth = windowWidth * windowScale;
            _graphics.PreferredBackBufferHeight = windowHeight * windowScale;
            _graphics.ApplyChanges();

            GraphicsDevice.SetRenderTarget(scene);
            GraphicsDevice.Clear(activeStage.bgColor);
            #endregion

            #region Rendering the scene
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            activeStage.PreDraw(_spriteBatch);

            activeStage.Draw(_spriteBatch);

            activeStage.PostDraw(_spriteBatch);

            // Drop the render target
            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.End();
            #endregion

            #region Rendering the scene rendertarget
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            _spriteBatch.Draw(scene, new Rectangle(0, 0, windowWidth * windowScale, windowHeight * windowScale), Color.White);

            _spriteBatch.End();
            #endregion

            base.Draw(gameTime);
        }
    }
}
