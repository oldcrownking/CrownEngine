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
    public class World1_1 : Stage
    {
        public override Color bgColor => Color.Black;

        public Player player;

        public override void Load()
        {
            tilemaps = new List<Actor>();

            AddActor(new SolidTilemap(new Vector2(-32, -40), this, new int[,] {
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,},
{1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,},
{1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,},
{1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,},
{1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,},
{1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,},
{1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,1,1,1,1,1,},
{1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,},
{1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,},
{1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,},
{1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,},
{1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,},
{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,},
{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,}, }, new List<Texture2D> { EngineHelpers.GetTexture("Dirt") }, 8));

            AddActor(new SpikeTilemap(new Vector2(-32, -40), this, new int[,] {
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}, }, new List<Texture2D> { EngineHelpers.GetTexture("Spikes") }, 8));

            AddActor(new SemisolidPlatform(new Vector2(-32, -40), this, new int[,] {
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}, }, new List<Texture2D> { EngineHelpers.GetTexture("SemisolidPlatform") }, 8));

            AddActor(new Checkpoint(new Vector2(336, 32), this));

            player = new Player(Vector2.One * 32, this);

            AddActor(player);

            foreach (Actor actor in actors)
            {
                if (actor.HasComponent<TileRenderer>()) tilemaps.Add(actor);
            }
        }

        public List<Actor> tilemaps;
        public bool editMode = false;
        public int editId;
        public int editTilemapId;

        public override void Update()
        {
            //screenPosition.X = screenPosition.X.Clamp(0, 200);

            base.Update();

            screenPosition.X = player.Center.X - EngineGame.instance.windowWidth / 2;

            if(player.Center.X > 256 && player.Center.X < 336)
            {
                screenPosition.Y = ((player.Center.X - 256) / 80) * -48;
            }

            if (EngineGame.instance.keyboardState.IsKeyDown(Keys.E) && !EngineGame.instance.oldKeyboardState.IsKeyDown(Keys.E)) editMode = !editMode;

            if (editMode)
            {
                if (EngineGame.instance.keyboardState.IsKeyDown(Keys.OemPlus) && !EngineGame.instance.oldKeyboardState.IsKeyDown(Keys.OemPlus)) editTilemapId++;
                if (EngineGame.instance.keyboardState.IsKeyDown(Keys.OemMinus) && !EngineGame.instance.oldKeyboardState.IsKeyDown(Keys.OemMinus)) editTilemapId--;

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
                
                    if (EngineGame.instance.keyboardState.IsKeyDown(Keys.S) && !EngineGame.instance.oldKeyboardState.IsKeyDown(Keys.S) && editMode)
                    {
                        for (int j = 0; j < renderer.tileGrid.GetLength(0); j++)
                        {
                            Debug.Write("{");
                
                            for (int i = 0; i < renderer.tileGrid.GetLength(1); i++)
                            {
                                Debug.Write(renderer.tileGrid[j, i] + ",");
                            }
                
                            Debug.Write("},");
                            Debug.WriteLine("");
                        }
                    }
                
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

            //screenPosition.Y = player.Center.Y - EngineGame.instance.windowHeight / 2;
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