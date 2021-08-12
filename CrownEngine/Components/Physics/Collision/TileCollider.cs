using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrownEngine;

namespace CrownEngine
{
    public class TileCollider : Component
    {
        public int[,] tileGrid;

        public int tileSize;

        public bool trigger;

        public Dictionary<int, Texture2D> types = new Dictionary<int, Texture2D>();

        public List<Rectangle> rectangles = new List<Rectangle>();

        public TileCollider(int size, int[,] grid, Actor myActor) : base(myActor)
        {
            tileSize = size;

            tileGrid = grid;
        }

        public override void Load()
        {
            for (int i = 0; i < tileGrid.GetLength(1); i++)
            {
                for (int j = 0; j < tileGrid.GetLength(0); j++)
                {
                    if (tileGrid[j, i] >= 1)
                    {
                        rectangles.Add(new Rectangle((i * tileSize) + (int)myActor.position.X, (j * tileSize) + (int)myActor.position.Y, tileSize, tileSize));
                    }
                }
            }

            base.Load();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
