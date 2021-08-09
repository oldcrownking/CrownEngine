using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CrownEngine.Engine
{
    public class Actor
    {
        public Stage myStage;

        public int width = 16;
        public int height = 16;

        public Vector2 position = Vector2.Zero;

        public List<Component> components = new List<Component>();

        public bool saveBetweenScenes = false;

        public Rectangle rect => new Rectangle((int)position.X, (int)position.Y, width, height);

        public Vector2 Center => new Vector2(position.X + width / 2, position.Y + height / 2);

        public Actor(Vector2 pos, Stage stage)
        {
            position = pos;

            myStage = stage;
        }

        public virtual void Load()
        {
            foreach (Component component in components)
            {
                component.Load();
            }
        }

        public virtual void Update()
        {
            foreach (Component component in components)
            {
                component.Update();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (Component component in components)
            {
                component.Draw(spriteBatch);
            }
        }

        public virtual void Kill()
        {
            myStage.RemoveActor(this);
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
        }

        public void RemoveComponent<T>() where T : Component
        {
            for (int i = 0; i < components.Count; i++)
                if (components[i] is T)
                {
                    components[i] = components[components.Count - 1];
                    components[components.Count - 1] = null;
                }
        }

        public bool HasComponent<T>() where T : Component
        {
            for (int i = 0; i < components.Count; i++)
                if (components[i] is T)
                    return true;

            return false;
        }

        public T GetComponent<T>() where T : Component
        {
            for (int i = 0; i < components.Count; i++)
                if (components[i] is T)
                    return components[i] as T;

            return null;
        }

        public void SetComponent<T>(Component newComponent) where T : Component
        {
            for (int i = 0; i < components.Count; i++)
                if (components[i] is T)
                    components[i] = newComponent;
        }
    }
}
