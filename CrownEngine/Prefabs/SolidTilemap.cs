using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace CrownEngine.Prefabs
{
    public class SolidTilemap : Actor
    {
        public int[,] grid;
        public List<Texture2D> textures;
        public int tileSize;

        public SolidTilemap(Vector2 pos, Stage myStage, int[,] tileGrid, List<Texture2D> _textures, int _tileSize) : base(pos, myStage)
        {
            grid = tileGrid;

            tileSize = _tileSize;

            textures = _textures;
        }

        public bool editMode = false;
        public int editId;
        public override void Update()
        {
            if(EngineGame.instance.keyboardState.IsKeyDown(Keys.E) && !EngineGame.instance.oldKeyboardState.IsKeyDown(Keys.E)) editMode = !editMode;

            if (editMode && EngineGame.instance.keyboardState.IsKeyDown(Keys.OemCloseBrackets) && !EngineGame.instance.oldKeyboardState.IsKeyDown(Keys.OemCloseBrackets)) editId--;
            if (editMode && EngineGame.instance.keyboardState.IsKeyDown(Keys.OemOpenBrackets) && !EngineGame.instance.oldKeyboardState.IsKeyDown(Keys.OemOpenBrackets)) editId++;

            if (editId > textures.Count) editId = 0;
            if (editId < 0) editId = textures.Count;

            if (editMode && EngineGame.instance.mouseState.LeftButton == ButtonState.Pressed)
            {
                Debug.WriteLine(position);

                Vector2 pos1 = (position - ((myStage.screenPosition + EngineGame.instance.mousePos) / tileSize));

                Debug.WriteLine(pos1.X);

                Point finalPos = new Point((int)(pos1.X), (int)(pos1.Y));

                Debug.WriteLine(finalPos.X);

                if (finalPos.X < 0)
                {
                    int[,] newTileGrid = new int[grid.GetLength(0), grid.GetLength(1) + Math.Abs(finalPos.X) + 1];

                    for (int i = 0; i < grid.GetLength(1); i++)
                    {
                        for (int j = 0; j < grid.GetLength(0); j++)
                        {
                            newTileGrid[j, i + Math.Abs(finalPos.X)] = grid[j, i];
                        }
                    }

                    Debug.WriteLine(finalPos);

                    grid = newTileGrid;

                    position += new Vector2(finalPos.X * tileSize, 0);

                    finalPos.X = 0;
                }

                if (finalPos.Y < 0)
                {
                    int[,] newTileGrid = new int[grid.GetLength(0) + Math.Abs(finalPos.Y) + 1, grid.GetLength(1)];

                    for (int i = 0; i < grid.GetLength(1); i++)
                    {
                        for (int j = 0; j < grid.GetLength(0); j++)
                        {
                            newTileGrid[j + Math.Abs(finalPos.Y), i] = grid[j, i];
                        }
                    }

                    grid = newTileGrid;

                    position += new Vector2(0, finalPos.Y * tileSize);

                    finalPos.Y = 0;
                }

                if (finalPos.X >= grid.GetLength(1))
                {
                    int[,] newTileGrid = new int[grid.GetLength(0), finalPos.X + 1];

                    for(int i = 0; i < grid.GetLength(1); i++)
                    {
                        for (int j = 0; j < grid.GetLength(0); j++)
                        {
                            newTileGrid[j, i] = grid[j, i];
                        }
                    }

                    grid = newTileGrid;
                }

                if (finalPos.Y >= grid.GetLength(0))
                {
                    int[,] newTileGrid = new int[finalPos.Y + 1, grid.GetLength(1)];

                    for (int i = 0; i < grid.GetLength(1); i++)
                    {
                        for (int j = 0; j < grid.GetLength(0); j++)
                        {
                            newTileGrid[j, i] = grid[j, i];
                        }
                    }

                    grid = newTileGrid;
                }

                grid[finalPos.Y, finalPos.X] = editId;

                GetComponent<TileCollider>().tileGrid = grid;
                GetComponent<TileRenderer>().tileGrid = grid;
            }


            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public int ticks;
        public override void PostDraw(SpriteBatch spriteBatch)
        {
            ticks++;

            if (editMode)
            {
                Vector2 pos1 = ((position - (myStage.screenPosition + EngineGame.instance.mousePos)) / tileSize);
                Point finalPos = new Point((int)(Math.Truncate(pos1.X)), (int)(Math.Truncate(pos1.Y)));
                Vector2 finalDrawPos = (finalPos.ToVector2() * tileSize) + position - myStage.screenPosition;

                Rectangle destrect = new Rectangle((int)finalDrawPos.X, (int)finalDrawPos.Y, tileSize, tileSize);

                if (editId == 0) return;

                spriteBatch.Draw(textures[editId - 1], destrect, new Rectangle(0, 5 * tileSize, tileSize, tileSize), Color.White * (0.5f + (float)(Math.Sin(ticks / 20f) / 4f)));
            }

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
