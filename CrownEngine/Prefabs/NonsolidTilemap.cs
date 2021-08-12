using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;

namespace CrownEngine.Prefabs
{
    public class NonsolidTilemap : Actor
    {
        public int[,] grid;
        public List<Texture2D> textures;
        public int tileScale;

        public NonsolidTilemap(Vector2 pos, Stage myStage, int[,] tileGrid, List<Texture2D> _textures, int _tileScale) : base(pos, myStage)
        {
            grid = tileGrid;

            textures = _textures;

            tileScale = _tileScale;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Load()
        {
            Dictionary<int, Texture2D> dict = new Dictionary<int, Texture2D>();

            for (int i = 1; i < textures.Count + 1; i++)
            {
                dict[i] = textures[i - 1];
            }

            AddComponent(new TileRenderer(tileScale, dict, grid, Color.White, this));

            base.Load();
        }
    }
}
