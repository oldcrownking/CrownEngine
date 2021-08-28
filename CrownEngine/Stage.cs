using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace CrownEngine
{
    public class Stage
    {
        public List<Actor> actors = new List<Actor>();

        public virtual Color bgColor => Color.Black;

        public bool loaded = false;

        public Vector2 screenPosition = Vector2.Zero;

        public bool clearActorsOnUnload = true;

        public Stage()
        {

        }

        public virtual void Update()
        {
            for (int i = 0; i < actors.Count; i++)
            {
                if (actors[i] == null) continue;

                actors[i].Update();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < actors.Count; i++)
            {
                if (actors[i] == null) continue;

                actors[i].Draw(spriteBatch);
            }
        }

        public virtual void PreDraw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < actors.Count; i++)
            {
                if (actors[i] == null) continue;

                actors[i].PreDraw(spriteBatch);
            }
        }

        public virtual void PostDraw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < actors.Count; i++)
            {
                if (actors[i] == null) continue;

                actors[i].PostDraw(spriteBatch);
            }
        }

        public virtual void Load()
        {
            loaded = true;

            for(int i = 0; i < actors.Count; i++)
            {
                if (actors[i] == null) continue;

                actors[i].Load();
            }
        }

        public virtual void Unload()
        {
            loaded = false;
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
            actor.Load();
            actors.Add(actor);
        }
    }
}
