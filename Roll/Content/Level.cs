using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using CrownEngine.Prefabs;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;

namespace Roll.Content
{
    public class Level : Stage
    {
        public override Color bgColor => Color.Black;

        public Player player;

        public List<Actor> tilemaps;

        public bool editMode = false;
        public int editId;
        public int editTilemapId;

        public virtual void CustomLoad()
        {

        }

        public override void Load()
        {
            tilemaps = new List<Actor>();

            CustomLoad();

            foreach (Actor actor in actors)
            {
                if (actor.HasComponent<TileRenderer>()) tilemaps.Add(actor);
            }
        }

        public override void Update()
        {
            //screenPosition.X = screenPosition.X.Clamp(0, 200);

            base.Update();

            #region Edit mode
            if (EngineGame.instance.keyboardState.IsKeyDown(Keys.E) && !EngineGame.instance.oldKeyboardState.IsKeyDown(Keys.E)) editMode = !editMode;

            if (editMode)
            {
                if (EngineGame.instance.keyboardState.IsKeyDown(Keys.OemPlus) && !EngineGame.instance.oldKeyboardState.IsKeyDown(Keys.OemPlus)) editTilemapId++;
                if (EngineGame.instance.keyboardState.IsKeyDown(Keys.OemMinus) && !EngineGame.instance.oldKeyboardState.IsKeyDown(Keys.OemMinus)) editTilemapId--;

                if (EngineGame.instance.keyboardState.IsKeyDown(Keys.S) && !EngineGame.instance.oldKeyboardState.IsKeyDown(Keys.S) && EngineGame.instance.oldKeyboardState.IsKeyDown(Keys.LeftControl) && editMode)
                {
                    foreach (Actor actor in tilemaps)
                    {
                        for (int j = 0; j < actor.GetComponent<TileRenderer>().tileGrid.GetLength(0); j++)
                        {
                            Debug.Write("{");

                            for (int i = 0; i < actor.GetComponent<TileRenderer>().tileGrid.GetLength(1); i++)
                            {
                                Debug.Write(actor.GetComponent<TileRenderer>().tileGrid[j, i] + ",");
                            }

                            Debug.Write("},");
                            Debug.WriteLine("");
                        }

                        Debug.WriteLine("\n");
                    }
                }

                if (editTilemapId >= tilemaps.Count) editTilemapId = 0;
                if (editTilemapId < 0) editTilemapId = tilemaps.Count;

                Actor tilemap = tilemaps[editTilemapId];

                if(tilemap.HasComponent<TileRenderer>())
                {
                    TileRenderer renderer = tilemap.GetComponent<TileRenderer>();

                    if (editMode && EngineGame.instance.keyboardState.IsKeyDown(Keys.OemCloseBrackets) && !EngineGame.instance.oldKeyboardState.IsKeyDown(Keys.OemCloseBrackets)) editId--;
                    if (editMode && EngineGame.instance.keyboardState.IsKeyDown(Keys.OemOpenBrackets) && !EngineGame.instance.oldKeyboardState.IsKeyDown(Keys.OemOpenBrackets)) editId++;

                    if (editId > renderer.types.Count) editId = 0;
                    if (editId < 0) editId = renderer.types.Count;
                
                    if (editMode && EngineGame.instance.mouseState.LeftButton == ButtonState.Pressed)
                    {
                        Vector2 pos1 = (((screenPosition + EngineGame.instance.mousePos)) - tilemap.position) / renderer.tileSize;
                
                        Point finalPos = new Point((int)(pos1.X), (int)(pos1.Y));
                
                        if (finalPos.X < 0)
                        {
                            int[,] newTileGrid = new int[renderer.tileGrid.GetLength(0), renderer.tileGrid.GetLength(1) + Math.Abs(finalPos.X)];
                
                            for (int i = 0; i < renderer.tileGrid.GetLength(1); i++)
                            {
                                for (int j = 0; j < renderer.tileGrid.GetLength(0); j++)
                                {
                                    newTileGrid[j, i + Math.Abs(finalPos.X)] = renderer.tileGrid[j, i];
                                }
                            }
                
                            renderer.tileGrid = newTileGrid;
                
                            tilemap.position += new Vector2(finalPos.X * renderer.tileSize, 0);
                
                            finalPos.X = 0;
                        }
                
                        if (finalPos.Y < 0)
                        {
                            int[,] newTileGrid = new int[renderer.tileGrid.GetLength(0) + Math.Abs(finalPos.Y), renderer.tileGrid.GetLength(1)];
                
                            for (int i = 0; i < renderer.tileGrid.GetLength(1); i++)
                            {
                                for (int j = 0; j < renderer.tileGrid.GetLength(0); j++)
                                {
                                    newTileGrid[j + Math.Abs(finalPos.Y), i] = renderer.tileGrid[j, i];
                                }
                            }
                
                            renderer.tileGrid = newTileGrid;
                
                            tilemap.position += new Vector2(0, finalPos.Y * renderer.tileSize);
                
                            finalPos.Y = 0;
                        }
                
                        if (finalPos.X >= renderer.tileGrid.GetLength(1))
                        {
                            int[,] newTileGrid = new int[renderer.tileGrid.GetLength(0), finalPos.X + 1];
                
                            for (int i = 0; i < renderer.tileGrid.GetLength(1); i++)
                            {
                                for (int j = 0; j < renderer.tileGrid.GetLength(0); j++)
                                {
                                    newTileGrid[j, i] = renderer.tileGrid[j, i];
                                }
                            }
                
                            renderer.tileGrid = newTileGrid;
                        }
                
                        if (finalPos.Y >= renderer.tileGrid.GetLength(0))
                        {
                            int[,] newTileGrid = new int[finalPos.Y + 1, renderer.tileGrid.GetLength(1)];
                
                            for (int i = 0; i < renderer.tileGrid.GetLength(1); i++)
                            {
                                for (int j = 0; j < renderer.tileGrid.GetLength(0); j++)
                                {
                                    newTileGrid[j, i] = renderer.tileGrid[j, i];
                                }
                            }
                
                            renderer.tileGrid = newTileGrid;
                        }
                
                        renderer.tileGrid[finalPos.Y, finalPos.X] = editId;
                
                        tilemap.SetComponent<TileRenderer>(renderer);
                
                        if(tilemap.HasComponent<TileCollider>())
                        {
                            tilemap.GetComponent<TileCollider>().tileGrid = renderer.tileGrid;
                        }
                
                        if (tilemap.HasComponent<SemisolidTileCollider>())
                        {
                            tilemap.GetComponent<SemisolidTileCollider>().tileGrid = renderer.tileGrid;
                        }
                    }
                }
            }
            #endregion

            //screenPosition.Y = player.Center.Y - EngineGame.instance.windowHeight / 2;
        }

        public int ticks;
        public override void PostDraw(SpriteBatch spriteBatch)
        {
            ticks++;

            if (editMode)
            {
                Vector2 pos1 = (((screenPosition + EngineGame.instance.mousePos)) - tilemaps[editTilemapId].position) / tilemaps[editTilemapId].GetComponent<TileRenderer>().tileSize;

                Point finalPos = new Point((int)(pos1.X), (int)(pos1.Y));

                Vector2 finalDrawPos = (finalPos.ToVector2() * tilemaps[editTilemapId].GetComponent<TileRenderer>().tileSize) + tilemaps[editTilemapId].position - screenPosition;

                Rectangle destrect = new Rectangle((int)finalDrawPos.X, (int)finalDrawPos.Y, tilemaps[editTilemapId].GetComponent<TileRenderer>().tileSize, tilemaps[editTilemapId].GetComponent<TileRenderer>().tileSize);

                if (editId == 0) return;

                spriteBatch.Draw(tilemaps[editTilemapId].GetComponent<TileRenderer>().types[editId], destrect, new Rectangle(0, 5 * tilemaps[editTilemapId].GetComponent<TileRenderer>().tileSize, tilemaps[editTilemapId].GetComponent<TileRenderer>().tileSize, tilemaps[editTilemapId].GetComponent<TileRenderer>().tileSize), Color.White * (0.5f + (float)(Math.Sin(ticks / 20f) / 4f)));
            }

            base.PostDraw(spriteBatch);
        }
    }
}