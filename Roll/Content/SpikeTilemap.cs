using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Roll.Content
{
    public class SpikeTilemap : Actor
    {
        public int[,] grid;
        public List<Texture2D> textures;
        public int tileSize;

        public SpikeTilemap(Vector2 pos, Stage myStage, int[,] tileGrid, List<Texture2D> _textures, int _tileSize) : base(pos, myStage)
        {
            grid = tileGrid;

            tileSize = _tileSize;

            textures = _textures;
        }

        public bool editMode = false;
        public int editId;
        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void PostDraw(SpriteBatch spriteBatch)
        {
            base.PostDraw(spriteBatch);
        }

        public override void Load()
        {
            Dictionary<int, Texture2D> dict = new Dictionary<int, Texture2D>();

            for (int i = 1; i < textures.Count + 1; i++)
            {
                dict[i] = textures[i - 1];
            }

            AddComponent(new TileRenderer(tileSize, dict, grid, Color.White, this));
            AddComponent(new TileCollider(tileSize, grid, this));

            base.Load();
        }
    }
}
