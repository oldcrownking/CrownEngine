using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine.Engine;

namespace CrownEngine.Content
{
    public class SolidTilemap : Actor
    {
        public int[,] grid;
        public List<Texture2D> textures;

        public SolidTilemap(Vector2 pos, Stage myStage, int[,] tileGrid, List<Texture2D> _textures) : base(pos, myStage)
        {
            grid = tileGrid;

            textures = _textures;
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

            AddComponent(new TileRenderer(8, dict, grid, Color.White, this));
            AddComponent(new TileCollider(8, grid, this));

            base.Load();
        }
    }
}
