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

        //public List<Line> borders = new List<Line>();

        public TileCollider(int size, int[,] grid, Actor myActor) : base(myActor)
        {
            tileSize = size;

            tileGrid = grid;

            oldTileGrid = tileGrid;
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

        public int[,] oldTileGrid;
        public override void Update()
        {
            //if(tileGrid != oldTileGrid) //oddly not detected
            //{
                rectangles.Clear();
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

                oldTileGrid = tileGrid;
            //}

            base.Update();
        }

        public bool EnclosedTile(int i, int j) => ((i == 0 || tileGrid[j, i - 1] >= 1)
            && (i == tileGrid.GetLength(1) - 1 || tileGrid[j, i + 1] >= 1)
            && (j == 0 || tileGrid[j - 1, i] >= 1)
            && (j == tileGrid.GetLength(0) - 1 || tileGrid[j + 1, i] >= 1));

        /*public void GetAllOpenFaces(int[,] tileGrid, int i, int j)
        {
            bool top = true;
            bool bottom = true;
            bool left = true;
            bool right = true;

            if(i == 0)
            {
                left = false;
            }
            else
            {
                if(tileGrid[j, i - 1] >= 1)
                {
                    left = false;
                }
            }
            if(i == tileGrid.GetLength(1))
            {
                right = false;
            }
            else
            {
                if (tileGrid[j, i + 1] >= 1)
                {
                    right = false;
                }
            }

            if (j == 0)
            {
                top = false;
            }
            else
            {
                if (tileGrid[j - 1, i] >= 1)
                {
                    top = false;
                }
            }
            if (j == tileGrid.GetLength(0))
            {
                bottom = false;
            }
            else
            {
                if (tileGrid[j + 1, i] >= 1)
                {
                    bottom = false;
                }
            }
        }

        public struct Line
        {
            public Vector2 start;
            public int orientation;
            public int length;

            public Line(Vector2 _start, int _orientation, int _length)
            {
                start = _start;
                orientation = _orientation;
                length = _length;
            }

            
        }*/
    }
}
