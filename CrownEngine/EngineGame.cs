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
using CrownEngine.Systems;

namespace CrownEngine
{
    public abstract class EngineGame : Game
    {
        public static EngineGame instance { get; private set; }

        public static List<GameSystem> systems = new List<GameSystem>();

        public static SpriteBatch spriteBatch;

        public EngineGame()
        {
            
        }

        protected override void Initialize()
        {
            IEnumerable<Type> systemsArray = typeof(GameSystem).Assembly.GetTypes().Where(TheType => TheType.IsClass && !TheType.IsAbstract && TheType.IsSubclassOf(typeof(GameSystem)) && (!TheType.IsGenericType || TheType.IsConstructedGenericType));

            foreach (Type type in systemsArray)
                systems.Add(Activator.CreateInstance(type) as GameSystem);

            Load();

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (GameSystem system in systems)
            {
                system.Update();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            foreach(GameSystem system in systems)
            {
                system.Draw(spriteBatch);
            }
        }

        public static T GetSystem<T>() where T : GameSystem
        {
            for (int i = 0; i < systems.Count; i++)
                if (systems[i] is T)
                    return systems[i] as T;

            throw new Exception("System instance not found");
        }

        public void EndGame()
        {
            Save();

            Exit();
        }

        public virtual void Save()
        {
            foreach (GameSystem system in systems)
            {
                system.Save();
            }
        }

        public virtual void Load()
        {
            foreach(GameSystem system in systems)
            {
                system.Load();
            }
        } 
    }
}
