using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using CrownEngine.Systems;

namespace CrownEngine
{
    public class Stage
    {
        public List<Actor> actors = new List<Actor>();

        public List<GameManager> managers = new List<GameManager>();

        public Stage()
        {
            
        }

        public virtual void Load()
        {
            EngineGame.GetSystem<Window>().layers.Add(new WindowLayer());
        }

        public virtual void Update()
        {
            foreach(GameManager manager in managers)
            {
                manager.Update();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameManager manager in managers)
            {
                manager.Draw(spriteBatch);
            }
        }

        public void RemoveActor(Actor actor)
        {
            for(int i = 0; i < actors.Count; i++)
            {
                if(actors[i] == actor)
                {
                    actors[i] = null;
                }
            }
        }

        public void AddActor(Actor actor)
        {
            actors.Add(actor);
        }
    }
}
