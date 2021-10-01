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
using Microsoft.Xna.Framework.Audio;

namespace Deprecated.Content
{
    public class GameStage : Stage
    {
        public Color blackoutColor = new Color(23, 20, 33);

        public override Color bgColor => blackoutColor;

        public Actor player;

        public override void Update()
        {
            screenPosition.Y = MathHelper.Clamp(player.Center.Y - 108, int.MinValue, 0);

            if (screenPosition.Y > oldscreenpos) screenPosition.Y = oldscreenpos;

            oldscreenpos = (int)screenPosition.Y;

            if (screenPosition.Y - 216 < storedHeight) 
            {
                /*Debug.WriteLine((int)((Math.Abs((int)(screenPosition.Y) - 216) - Math.Abs(storedHeight)) / 2f));

                for (int i = 0; i < heights.Length; i++)
                {
                    heights[i] += (int)((Math.Abs((int)(screenPosition.Y) - 216) - Math.Abs(storedHeight)) / 2f);

                    if (heights[i] < 216 + 40) heights[i] = - 40;
                }

                Debug.WriteLine(heights[0]);*/

                ratchets += Math.Abs((int)screenPosition.Y - 216) - Math.Abs(storedHeight);
                storedHeight = (int)screenPosition.Y - 216; 
            }
            
            if(ratchets >= 24)
            {
                int randomizedPos = lastPlatformPos;

                while (Math.Abs(randomizedPos - lastPlatformPos) < 32 || Math.Abs(randomizedPos - lastPlatformPos) > 56 || randomizedPos < 0 || randomizedPos >= 73) randomizedPos = EngineHelpers.Next(0, 73);

                if (EngineHelpers.NextBool(50))
                {
                    AddActor(new RoboforgePlatform(new Vector2(randomizedPos, storedHeight + 208), this));

                    lastPlatformPos = randomizedPos + 12;
                }
                else
                {
                    switch (EngineHelpers.Next(3))
                    {
                        case 0:
                            AddActor(new MediumPlatform(new Vector2(randomizedPos, storedHeight + 208), this));

                            lastPlatformPos = randomizedPos + 12;
                            break;
                        case 1:
                            AddActor(new ShortPlatform(new Vector2(randomizedPos, storedHeight + 208), this));

                            lastPlatformPos = randomizedPos + 8;
                            break;
                        case 2:
                            AddActor(new LongPlatform(new Vector2(randomizedPos, storedHeight + 208), this));

                            lastPlatformPos = randomizedPos + 16;
                            break;
                    }

                    if (EngineHelpers.NextBool(25)) AddActor(new NutCircle(new Vector2(randomizedPos + 9, storedHeight + 208 - 12), this, EngineHelpers.Next(2, 5)));
                    else if (EngineHelpers.NextBool(3)) AddActor(new PlatformNut(new Vector2(randomizedPos + 9, storedHeight + 208 - 8), this));
                }

                ratchets = 0;
            }

            if((destructTicks) % 240 == 0 && selfDestruct && destructTicks >= 600)
            {
                int dir = (EngineHelpers.Next(2) == 1 ? -1 : 1);

                if(dir == -1)
                    AddActor(new HorizontalSawblade(new Vector2(-8, player.Center.Y + 8), this, new Vector2(EngineHelpers.Next(1, 3), 0)));
                else
                    AddActor(new HorizontalSawblade(new Vector2(120, player.Center.Y + 8), this, new Vector2(EngineHelpers.Next(1, 3) * -1, 0)));
            }

            (player as Player).canUpgrade = false;

            //if(screenPosition.Y > upticks)

            if (!inInventory) base.Update();

            if (inInventory) (player as Player).canUpgrade = true;
            if (EngineGame.instance.keyboardState.IsKeyDown(Keys.E) && !EngineGame.instance.oldKeyboardState.IsKeyDown(Keys.E) && (player as Player).canUpgrade) inInventory = !inInventory;

            if (music.State == SoundState.Stopped) 
            {
                music.Play();
            }
        }

        public override void PreDraw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < heights.Length; i++)
            {
                spriteBatch.Draw(EngineHelpers.GetTexture("FarBackgroundPanels"), new Vector2(0, (((i * 40) + (int)(screenPosition.Y / -3f)) % 280) - 40 - 20), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(EngineHelpers.GetTexture("FarBackgroundPanels"), new Vector2(56, (((i * 40) + (int)(screenPosition.Y / -3f)) % 280) - 40), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
            }

            for (int i = 0; i < heights.Length; i++)
            {
                spriteBatch.Draw(EngineHelpers.GetTexture("BackgroundPanels"), new Vector2(0, (((i * 40) + (int)(screenPosition.Y / -2f)) % 280) - 40), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(EngineHelpers.GetTexture("BackgroundPanels"), new Vector2(64, (((i * 40) + (int)(screenPosition.Y / -2f)) % 280) - 40 - 20), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
            }

            //Debug.WriteLine((((int)(screenPosition.Y / 2f)) % 216));

            base.PreDraw(spriteBatch);
        }

        public override void PostDraw(SpriteBatch spriteBatch)
        {
            ticks++;

            if (!inInventory && isScrolling && selfDestruct) sawbladeHeight -= 0.25f;

            spriteBatch.Draw(EngineHelpers.GetTexture("MagicPixel"), new Rectangle(0, (int)sawbladeHeight - (int)screenPosition.Y - 16, 96, 216), null, blackoutColor, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);

            base.PostDraw(spriteBatch);

            spriteBatch.Draw(EngineHelpers.GetTexture("NutBanner"), new Vector2(2, 2), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);

            spriteBatch.Draw(EngineHelpers.GetTexture("Nut"), new Vector2(5, 4), new Rectangle(0, ((int)(ticks / 10f) % 4) * 6, 6, 6), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);

            DrawString(spriteBatch, "x" + (player as Player).nuts, new Vector2(12, 5), 4, 0, 1f, 1f);

            if (player.Center.Y < 157) selfDestruct = true;

            if (selfDestruct) destructTicks++;

            if(destructTicks < 120 && destructTicks > 0)
            {
                DrawString(spriteBatch, "Self-Destruct Initiated", new Vector2(1, 100), 4, 2, 5f, 2f, blackoutColor);
                DrawString(spriteBatch, "Self-Destruct Initiated", new Vector2(3, 100), 4, 2, 5f, 2f, blackoutColor);

                DrawString(spriteBatch, "Self-Destruct Initiated", new Vector2(2, 99), 4, 2, 5f, 2f, blackoutColor);
                DrawString(spriteBatch, "Self-Destruct Initiated", new Vector2(2, 101), 4, 2, 5f, 2f, blackoutColor);

                DrawString(spriteBatch, "Self-Destruct Initiated", new Vector2(2, 100), 4, 2, 5f, 2f);
            }

            if(inInventory)
            {
                spriteBatch.Draw(EngineHelpers.GetTexture("MagicPixel"), new Rectangle(0, 0, 96, 216), null, blackoutColor * 0.5f, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);

                spriteBatch.Draw(EngineHelpers.GetTexture("UpgradePanel"), new Vector2(8, 76), Color.White);

                spriteBatch.Draw(EngineHelpers.GetTexture("UpgradeMultiplier"), new Vector2(8 + 4, 76 + 4), Color.White);
                spriteBatch.Draw(EngineHelpers.GetTexture("UpgradeHeight"), new Vector2(8 + 4, 76 + 4 + 24), Color.White);

                spriteBatch.Draw(EngineHelpers.GetTexture("UpgradeShield"), new Vector2(8 + 4 + 56, 76 + 4), Color.White);
                spriteBatch.Draw(EngineHelpers.GetTexture("UpgradeBoost"), new Vector2(8 + 4 + 56, 76 + 4 + 24), Color.White);

                spriteBatch.Draw(EngineHelpers.GetTexture("Nut"), new Vector2(8 + 32, 76 + 32), new Rectangle(0, ((int)(ticks / 10f) % 4) * 6, 6, 6), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);

                for(int i = 0; i < 3; i++)
                {
                    if((player as Player).multiplierTier <= i)
                        spriteBatch.Draw(EngineHelpers.GetTexture("UpgradeBall"), new Vector2(8 + 4 + (i * 5), 76 + 4 + 16 + 1), new Rectangle(0, 6, 6, 6), Color.White);
                    else
                        spriteBatch.Draw(EngineHelpers.GetTexture("UpgradeBall"), new Vector2(8 + 4 + (i * 5), 76 + 4 + 16 + 1), new Rectangle(0, 0, 6, 6), Color.White);
                }

                for (int i = 0; i < 3; i++)
                {
                    if ((player as Player).jumpTier <= i)
                        spriteBatch.Draw(EngineHelpers.GetTexture("UpgradeBall"), new Vector2(8 + 4 + (i * 5), 76 + 4 + 16 + 1 + 24), new Rectangle(0, 6, 6, 6), Color.White);
                    else
                        spriteBatch.Draw(EngineHelpers.GetTexture("UpgradeBall"), new Vector2(8 + 4 + (i * 5), 76 + 4 + 16 + 1 + 24), new Rectangle(0, 0, 6, 6), Color.White);
                }

                for (int i = 0; i < 3; i++)
                {
                    if ((player as Player).shieldTier <= i)
                        spriteBatch.Draw(EngineHelpers.GetTexture("UpgradeBall"), new Vector2(8 + 4 + (i * 5) + 56, 76 + 4 + 16 + 1), new Rectangle(0, 6, 6, 6), Color.White);
                    else
                        spriteBatch.Draw(EngineHelpers.GetTexture("UpgradeBall"), new Vector2(8 + 4 + (i * 5) + 56, 76 + 4 + 16 + 1), new Rectangle(0, 0, 6, 6), Color.White);
                }

                for (int i = 0; i < 3; i++)
                {
                    if ((player as Player).extraJumpsTier <= i)
                        spriteBatch.Draw(EngineHelpers.GetTexture("UpgradeBall"), new Vector2(8 + 4 + (i * 5) + 56, 76 + 4 + 16 + 1 + 24), new Rectangle(0, 6, 6, 6), Color.White);
                    else
                        spriteBatch.Draw(EngineHelpers.GetTexture("UpgradeBall"), new Vector2(8 + 4 + (i * 5) + 56, 76 + 4 + 16 + 1 + 24), new Rectangle(0, 0, 6, 6), Color.White);
                }

                if (new Rectangle(8 + 4, 76 + 4, 16, 16).Contains(EngineGame.instance.mousePos))
                {
                    //Multiplier upgrade

                    DrawString(spriteBatch, "Nut Multiplier", new Vector2(16, 76 + 53), 4, 0, 1f, 0f);

                    int cost = 0;

                    switch ((player as Player).multiplierTier)
                    {
                        case 0:
                            cost = 20;
                            break;
                        case 1:
                            cost = 35;
                            break;
                        case 2:
                            cost = 50;
                            break;
                    }

                    if (EngineGame.instance.mouseState.LeftButton == ButtonState.Pressed && EngineGame.instance.oldMouseState.LeftButton == ButtonState.Released)
                    {
                        if ((player as Player).nuts >= cost)
                        {
                            (player as Player).nuts -= cost;
                            (player as Player).multiplierTier++;
                        }
                    }

                    DrawString(spriteBatch, "" + cost, new Vector2(8 + 40, 76 + 33), 4, 0, 1f, 0f);
                }

                if (new Rectangle(8 + 4 + 56, 76 + 4, 16, 16).Contains(EngineGame.instance.mousePos))
                {
                    //Shield upgrade

                    DrawString(spriteBatch, "Stronger Shields", new Vector2(16, 76 + 51), 4, 0, 1f, 0f);

                    int cost = 0;

                    switch ((player as Player).shieldTier)
                    {
                        case 0:
                            cost = 15;
                            break;
                        case 1:
                            cost = 30;
                            break;
                        case 2:
                            cost = 45;
                            break;
                    }

                    if (EngineGame.instance.mouseState.LeftButton == ButtonState.Pressed && EngineGame.instance.oldMouseState.LeftButton == ButtonState.Released)
                    {
                        if ((player as Player).nuts >= cost)
                        {
                            (player as Player).nuts -= cost;
                            (player as Player).multiplierTier++;
                        }
                    }

                    DrawString(spriteBatch, "" + cost, new Vector2(8 + 40, 76 + 33), 4, 0, 1f, 0f);
                }

                if (new Rectangle(8 + 4, 76 + 4 + 24, 16, 16).Contains(EngineGame.instance.mousePos))
                {
                    //Jump height upgrade

                    DrawString(spriteBatch, "Higher Jump", new Vector2(16, 76 + 51), 4, 0, 1f, 0f);

                    int cost = 0;

                    switch ((player as Player).jumpTier)
                    {
                        case 0:
                            cost = 10;
                            break;
                        case 1:
                            cost = 25;
                            break;
                        case 2:
                            cost = 50;
                            break;
                    }

                    if (EngineGame.instance.mouseState.LeftButton == ButtonState.Pressed && EngineGame.instance.oldMouseState.LeftButton == ButtonState.Released)
                    {
                        if ((player as Player).nuts >= cost)
                        {
                            (player as Player).nuts -= cost;
                            (player as Player).jumpTier++;
                        }
                    }

                    DrawString(spriteBatch, "" + cost, new Vector2(8 + 40, 76 + 33), 4, 0, 1f, 0f);
                }

                if (new Rectangle(8 + 4 + 56, 76 + 4 + 24, 16, 16).Contains(EngineGame.instance.mousePos))
                {
                    //Jump boost upgrade

                    DrawString(spriteBatch, "Multi-Jump", new Vector2(16, 76 + 51), 4, 0, 1f, 0f);

                    int cost = 0;

                    switch ((player as Player).extraJumpsTier)
                    {
                        case 0:
                            cost = 25;
                            break;
                        case 1:
                            cost = 50;
                            break;
                        case 2:
                            cost = 75;
                            break;
                    }

                    if (EngineGame.instance.mouseState.LeftButton == ButtonState.Pressed && EngineGame.instance.oldMouseState.LeftButton == ButtonState.Released)
                    {
                        if ((player as Player).nuts >= cost)
                        {
                            (player as Player).nuts -= cost;
                            (player as Player).extraJumpsTier++;
                        }
                    }

                    DrawString(spriteBatch, "" + cost, new Vector2(8 + 40, 76 + 33), 4, 0, 1f, 0f);
                }
            }

            if ((player as Player).deathTicks > 60 && selfDestruct)
            {
                spriteBatch.Draw(EngineHelpers.GetTexture("DeathPanel"), new Vector2(26, 103), Color.White);
                DrawString(spriteBatch, "You Died", new Vector2(32, 105), 4, 0, 1f, 1f);

                spriteBatch.Draw(EngineHelpers.GetTexture("PlayAgainButton"), new Vector2(26 + 3, 103 + 9), new Rectangle(0, (new Rectangle(26 + 3, 103 + 9, 38, 11).Contains(EngineGame.instance.mousePos) ? 11 : 0), 38, 11), Color.White);

                if (new Rectangle(26 + 3, 103 + 9, 38, 11).Contains(EngineGame.instance.mousePos) && EngineGame.instance.mouseState.LeftButton == ButtonState.Pressed) EngineHelpers.SwitchStages(1);
            }
        }

        //PlayButton playButton;
        public bool inInventory = false;
        public bool selfDestruct = false;

        public int ticks;
        public int coverHeight = 0;
        public bool loading;
        public int[] heights = new int[8];
        public float sawbladeHeight = 256;

        public bool isScrolling = true;

        public int destructTicks;

        public int storedHeight;
        public int ratchets;

        public int oldscreenpos;
        public int lastPlatformPos;

        public override void Load()
        {
            actors.Clear();
            coverHeight = 0;
            sawbladeHeight = 256;
            heights = new int[8];
            screenPosition = Vector2.Zero;
            if(music != null) music.Stop();
            ticks = 0;
            destructTicks = 0;
            storedHeight = 0;
            ratchets = 0;
            selfDestruct = false;
            isScrolling = true;
            inInventory = false;
            oldscreenpos = 0;
            


            player = new Player(new Vector2(32, 192), this);
            AddActor(player);

            AddActor(new SolidTilemap(new Vector2(0, 136), this, new int[,] {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                { 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1 },
                { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                { 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                }, new List<Texture2D>()
                {
                    EngineHelpers.GetTexture("PanelTilesheet")
                }, 8));

            AddActor(new SemisolidPlatform(new Vector2(0, 136), this, new int[,] {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                }, new List<Texture2D>()
                {
                    EngineHelpers.GetTexture("SemisolidPlatform")
                }, 8));

            AddActor(new LongPlatform(new Vector2(48, 184), this));


            AddActor(new MediumPlatform(new Vector2(56, 136), this));
            AddActor(new ShortPlatform(new Vector2(32, 112), this));

            AddActor(new LongPlatform(new Vector2(55, 112 - 24), this));
            AddActor(new ShortPlatform(new Vector2(23, 112 - 24 - 24), this));
            AddActor(new RoboforgePlatform(new Vector2(36, 112 - 24 - 24 - 24), this));
            AddActor(new MediumPlatform(new Vector2(72, 112 - 24 - 24 - 24 - 24), this));

            for(int i = 0; i < heights.Length; i++)
            {
                heights[i] = (i * 40);
            }

            AddActor(new Sawblade(new Vector2(16, 256), this));
            AddActor(new Sawblade(new Vector2(32, 256), this));
            AddActor(new Sawblade(new Vector2(56, 256), this));
            AddActor(new Sawblade(new Vector2(80, 256), this));
            AddActor(new Sawblade(new Vector2(104, 256), this));

            music = EngineHelpers.GetSound("MenuTheme").CreateInstance();
            music.Volume = 0.5f;

            music.Play();
        }

        public SoundEffectInstance music;

        public void DrawString(SpriteBatch spriteBatch, string str, Vector2 pos, int spacing, int dramaFactor, float dramaRate, float dramaCharDiff)
        {
            for (int i = 0; i < str.Length; i++)
            {
                int charId = str[i];

                if (charId < 32)
                {
                    Debug.WriteLine("Invalid character detected");
                    continue;
                }

                if (charId == 32)
                    continue;

                if (charId < 48 && charId >= 33)
                    spriteBatch.Draw(EngineHelpers.GetTexture("CharacterSheet"), pos + new Vector2(i * spacing, (float)Math.Sin((ticks + i * dramaCharDiff) / dramaRate) * dramaFactor), new Rectangle((str[i] - 33) * 3, 0, 3, 5), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                else if (charId < 65 && charId >= 48)
                    spriteBatch.Draw(EngineHelpers.GetTexture("CharacterSheet"), pos + new Vector2(i * spacing, (float)Math.Sin((ticks + i * dramaCharDiff) / dramaRate) * dramaFactor), new Rectangle((str[i] - 48) * 3, 5, 3, 5), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                else if (charId < 91 && charId >= 65)
                    spriteBatch.Draw(EngineHelpers.GetTexture("CharacterSheet"), pos + new Vector2(i * spacing, (float)Math.Sin((ticks + i * dramaCharDiff) / dramaRate) * dramaFactor), new Rectangle((str[i] - 65) * 3, 10, 3, 5), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                else if (charId < 123 && charId >= 97)
                    spriteBatch.Draw(EngineHelpers.GetTexture("CharacterSheet"), pos + new Vector2(i * spacing, (float)Math.Sin((ticks + i * dramaCharDiff) / dramaRate) * dramaFactor), new Rectangle((str[i] - 97) * 3, 15, 3, 5), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                else
                    Debug.WriteLine("Invalid character detected");
            }
        }

        public void DrawString(SpriteBatch spriteBatch, string str, Vector2 pos, int spacing, int dramaFactor, float dramaRate, float dramaCharDiff, Color color)
        {
            for (int i = 0; i < str.Length; i++)
            {
                int charId = str[i];

                if (charId < 32)
                {
                    Debug.WriteLine("Invalid character detected");
                    continue;
                }

                if (charId == 32)
                    continue;

                if (charId < 48 && charId >= 33)
                    spriteBatch.Draw(EngineHelpers.GetTexture("CharacterSheet"), pos + new Vector2(i * spacing, (float)Math.Sin((ticks + i * dramaCharDiff) / dramaRate) * dramaFactor), new Rectangle((str[i] - 33) * 3, 0, 3, 5), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                else if (charId < 65 && charId >= 48)
                    spriteBatch.Draw(EngineHelpers.GetTexture("CharacterSheet"), pos + new Vector2(i * spacing, (float)Math.Sin((ticks + i * dramaCharDiff) / dramaRate) * dramaFactor), new Rectangle((str[i] - 48) * 3, 5, 3, 5), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                else if (charId < 91 && charId >= 65)
                    spriteBatch.Draw(EngineHelpers.GetTexture("CharacterSheet"), pos + new Vector2(i * spacing, (float)Math.Sin((ticks + i * dramaCharDiff) / dramaRate) * dramaFactor), new Rectangle((str[i] - 65) * 3, 10, 3, 5), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                else if (charId < 123 && charId >= 97)
                    spriteBatch.Draw(EngineHelpers.GetTexture("CharacterSheet"), pos + new Vector2(i * spacing, (float)Math.Sin((ticks + i * dramaCharDiff) / dramaRate) * dramaFactor), new Rectangle((str[i] - 97) * 3, 15, 3, 5), color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                else
                    Debug.WriteLine("Invalid character detected");
            }
        }
    }
}