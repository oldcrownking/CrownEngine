using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CrownEngine
{
    public class Actor
    {
        public Stage myStage;

        public List<Component> components = new List<Component>();

        public Actor(Stage stage)
        {
            myStage = stage;
        }

        public void Load()
        {
            foreach (Component component in components)
            {
                component.Load();
            }
        }

        public void Update()
        {
            foreach (Component component in components)
            {
                component.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Component component in components)
            {
                component.Draw(spriteBatch);
            }
        }

        public void Remove()
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
            {
                if (components[i] is T)
                {
                    components[i] = components[components.Count - 1];
                    components[components.Count - 1] = null;
                }
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
