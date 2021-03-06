﻿//
// HudPilotSelect.cs
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class HudPilotSelect
    {
        public GameControllerIndex GameControllerIndex { get; set; }
        public CGalaxy Game { get; set; }
        public string NameText { get; set; }
        public string MoneyText { get; set; }
        public int Cursor { get; set; }
        public int InputWaitFrameCount { get; set; }

        private CVisual LeftPanelVisual { get; set; }
        private CVisual RightPanelVisual { get; set; }
        private CVisual LeftPanelFramesVisual { get; set; }
        private CVisual RightPanelFramesVisual { get; set; }
        private CVisual EnergyVisual { get; set; }
        private CVisual ShieldVisual { get; set; }
        private CVisual ArmorVisual { get; set; }
        private CVisual MoneyIconVisual { get; set; }
        private CVisual EnergyIconVisual { get; set; }
        private CVisual ShieldIconVisual { get; set; }
        private CVisual ArmorIconVisual { get; set; }
        private List<CVisual> PortraitIconVisuals { get; set; }
        private CVisual PilotSelectArrowVisualLeft { get; set; }
        private CVisual PilotSelectArrowVisualRight { get; set; }

        private Vector2 BasePosition { get; set; }
        private Vector2 LeftPanelPosition { get; set; }
        private Vector2 RightPanelPosition { get; set; }
        public Vector2 NameTextPosition { get; set; }
        public Vector2 MoneyTextPosition { get; set; }
        private Vector2 MoneyIconPosition { get; set; }
        private Vector2 TextDisplayPosition { get; set; }
        private Vector2 PortraitIconPosition { get; set; }

        private Keys StartKey { get; set; }

        public enum EState
        {
            PressStart, // display press start
            Inactive,   // no player
            Active,     // selecting profile
            Selected,   // selected, waiting for players
            Locked,     // locked, cannot change anymore
        }
        public EState State { get; set; }

        public HudPilotSelect(CGalaxy game, Vector2 base_position, GameControllerIndex game_controller_index)
        {
            GameControllerIndex = game_controller_index;
            Game = game;
            BasePosition = base_position;

            NameText = "Profile";
            MoneyText = null;

            Cursor = (int)GameControllerIndex;

            LeftPanelVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/PanelBackground");
            RightPanelVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/PanelBackground");
            LeftPanelFramesVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/PanelFrames");
            RightPanelFramesVisual = CVisual.MakeSpriteUncached(Game, "Textures/UI/PanelFrames");
            PortraitIconVisuals = new List<CVisual>() {
                CVisual.MakeSpriteUncached(Game, "Textures/UI/KazukiPortrait"),
                CVisual.MakeSpriteUncached(Game, "Textures/UI/RabbitPortrait"),
                CVisual.MakeSpriteUncached(Game, "Textures/UI/GunthorPortrait"),
                //CVisual.MakeSpriteUncached(Game, "Textures/UI/MysteryPortrait"),
            };
            PilotSelectArrowVisualLeft = CVisual.MakeSpriteUncached(Game, "Textures/UI/PilotSelectArrow");
            PilotSelectArrowVisualRight = CVisual.MakeSpriteUncached(Game, "Textures/UI/PilotSelectArrow");

            LeftPanelPosition = new Vector2(0.0f, 0.0f);
            RightPanelPosition = new Vector2(Game.Resolution.X, 0.0f);

            NameTextPosition = BasePosition + new Vector2(80.0f, -964.0f);
            MoneyTextPosition = BasePosition + new Vector2(100.0f, -888.0f);
            TextDisplayPosition = BasePosition + new Vector2(238.0f, -720.0f);
            PortraitIconPosition = BasePosition + new Vector2(238.0f, -608.0f);

            LeftPanelVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 0.0f;
            RightPanelVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 0.0f;
            LeftPanelFramesVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 3.0f;
            RightPanelFramesVisual.Depth = CLayers.UI + CLayers.SubLayerIncrement * 3.0f;

            foreach (CVisual portrait in PortraitIconVisuals)
            {
                portrait.Depth = CLayers.UI + CLayers.SubLayerIncrement * 2.0f;
                portrait.Update();
            }
            PilotSelectArrowVisualLeft.Depth = CLayers.UI + CLayers.SubLayerIncrement * 2.0f;
            PilotSelectArrowVisualRight.Depth = CLayers.UI + CLayers.SubLayerIncrement * 2.0f;
            PilotSelectArrowVisualRight.SpriteEffects = SpriteEffects.FlipHorizontally;
            PilotSelectArrowVisualRight.Update();

            LeftPanelVisual.NormalizedOrigin = new Vector2(0.0f, 0.0f);
            RightPanelVisual.NormalizedOrigin = new Vector2(1.0f, 0.0f);
            LeftPanelFramesVisual.NormalizedOrigin = new Vector2(0.0f, 0.0f);
            RightPanelFramesVisual.NormalizedOrigin = new Vector2(1.0f, 0.0f);

            // TODO: force a recache, need to handle non-entity visuals better
            LeftPanelVisual.Update();
            RightPanelVisual.Update();
            LeftPanelFramesVisual.Update();
            RightPanelFramesVisual.Update();
        }

        public void Update()
        {
            InputWaitFrameCount++;
            if (InputWaitFrameCount < 2)
                return;

            switch (State)
            {
                case EState.PressStart:
                    if (Game.Input.PollGameControllerConnected(GameControllerIndex))
                    {
                        State = EState.Inactive;
                    }

                    // NOTE: will pop the input state, so calling again on the other hud wont start P2 as well
                    //if (Game.Input.IsKeyPressed(Keys.Space) || Game.Input.IsKeyPressed(Keys.Enter))
                    //{
                        //Game.Input.SetKeyboardControllerIndex(GameControllerIndex, (PlayerIndex)GameControllerIndex);
                        //State = EState.Inactive;
                    //}

                    break;

                case EState.Inactive:
                    break;

                case EState.Active:
                    // TODO: up/down control for profile select
                    if (Game.Input.IsPadConfirmPressed(GameControllerIndex) || Game.Input.IsKeyPressedGame(GameControllerIndex, Keys.Enter))
                    {
                        ChoosePilotFromCursor();
                        Selected();
                    }

#if SOAK_TEST
                    // force select immediately
                    Cursor = (int)GameControllerIndex;
                    ChoosePilotFromCursor();
                    Selected();
#endif

                    PortraitIconVisuals[Cursor].Scale = Vector2.One;

                    int other_player = GameControllerIndex == GameControllerIndex.One ? 1 : 0;
                    int other_cursor = Game.HudManager.HudsProfileSelect[other_player].Cursor;

                    // just force a value which will be skipped for 1p mode
                    if (Game.PlayersInGame == 1)
                        other_cursor = -1;

                    if (Game.Input.IsPadLeftPressed(GameControllerIndex) || Game.Input.IsKeyPressedGame(GameControllerIndex, Keys.Left))
                    {
                        int next_valid = Cursor;

                        for (int i = Cursor - 1; i >= 0; --i)
                        {
                            if (i == other_cursor)
                                continue;

                            next_valid = i;
                            break;
                        }

                        Cursor = next_valid;
                    }

                    if (Game.Input.IsPadRightPressed(GameControllerIndex) || Game.Input.IsKeyPressedGame(GameControllerIndex, Keys.Right))
                    {
                        int next_valid = Cursor;

                        for (int i = Cursor + 1; i < PortraitIconVisuals.Count; ++i)
                        {
                            if (i == other_cursor)
                                continue;

                            next_valid = i;
                            break;
                        }

                        Cursor = next_valid;
                    }

                    // NOTE: looks crap
                    //PortraitIconVisuals[Cursor].Scale = Vector2.One + Vector2.One * 0.025f * (float)Math.Sin(Game.GameFrame * 0.1f);
                    break;

                case EState.Selected:
                    if (Game.Input.IsPadCancelPressed(GameControllerIndex) || Game.Input.IsKeyPressedGame(GameControllerIndex, Keys.Escape))
                    {
                        State = EState.Active;
                        break;
                    }

                    PortraitIconVisuals[Cursor].Scale = Vector2.One;
                    break;

                case EState.Locked:
                    // NOTE: no more disabling mid-game
                    //if (Game.Input.IsPadStartPressed(GameControllerIndex) || Game.Input.IsKeyPressedGame(GameControllerIndex, (StartKey))
                    //{
                        //Game.HudManager.ToggleProfileActive(GameControllerIndex);
                        //State = EState.Inactive;
                    //}
                    break;
            }
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            switch (State)
            {
                case EState.Inactive:
                    break;

                case EState.Active:
                {
                    Vector2 offset = new Vector2(0.0f, 210.0f);
                    Vector2 position = PortraitIconPosition;

                    Vector2 text_offset = new Vector2(-120.0f, -240.0f);
                    sprite_batch.DrawString(Game.GameRegularFont, "Select Pilot", position + text_offset, Color.White, 0.0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0.0f);

                    sprite_batch.Draw(Game.PixelTexture, new Rectangle((int)position.X - 77, (int)position.Y - 101, 159, 201), null, Color.DarkGray, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
                    CVisual portrait = PortraitIconVisuals[Cursor];
                    portrait.Draw(sprite_batch, position, 0.0f);

                    if (Cursor > 0)
                        PilotSelectArrowVisualLeft.Draw(sprite_batch, position + new Vector2(-116.0f, 0.0f), 0.0f);

                    if (Cursor < PortraitIconVisuals.Count - 1)
                        PilotSelectArrowVisualRight.Draw(sprite_batch, position + new Vector2(+116.0f, 0.0f), 0.0f);

                    DrawPilotInfo(sprite_batch, position + new Vector2(-32.0f, 128.0f));
                    break;
                }

                case EState.Selected:
                {
                    Vector2 position = PortraitIconPosition;
                    Vector2 text_offset = new Vector2(-120.0f, -240.0f);
                    sprite_batch.DrawString(Game.GameRegularFont, "Waiting...", position + text_offset, Color.White, 0.0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0.0f);

                    sprite_batch.Draw(Game.PixelTexture, new Rectangle((int)position.X - 77, (int)position.Y - 101, 159, 201), null, Color.DarkGray, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
                    CVisual portrait = PortraitIconVisuals[Cursor];
                    portrait.Draw(sprite_batch, position, 0.0f);

                    DrawPilotInfo(sprite_batch, position + new Vector2(-32.0f, 128.0f));
                    break;
                }

                case EState.Locked:
                    break;
            }
        }

        public static List<List<string>> PilotInfo = new List<List<string>>()
        {
            new List<string>()
            {
                "Name: Kazuki",
                "Country: Japan",
                "Profession: Ace Pilot",
                "Blood Type: B",
                "",
                "Bonus: Speed+",
                "",
                //"Ability: Dash Burst",
                //"Ability: Shimmer",
                //"Ability: Absorb Energy",
            },
            new List<string>()
            {
                "Name: Rabbit",
                "Country: Russia",
                "Profession: Wpn Research",
                "Blood Type: A",
                "",
                "Bonus: Money+",
                "",
                //"Ability: Bullet Reflect",
                //"Ability: Bullet Detonate",
                //"Ability: Bullet Alchemy",
            },
            new List<string>()
            {
                "Name: Gunthor",
                "Country: Norway",
                "Profession: Demolitions",
                "Blood Type: O",
                "",
                "Bonus: Damage+",
                "",
                //"Ability: Ground Smash",
                //"Ability: Suction Crusher",
                //"Ability: Armor Repair",
            },
            new List<string>()
            {
                "Name: ???",
                "Country: ???",
                "Title: ???",
                "Blood Type: AB",
                "",
                "Bonus: Speed+",
                "Bonus: Damage+",
                "",
                //"Ability: ???",
                //"Ability: ???",
                //"Ability: ???",
            },
        };
        public void DrawPilotInfo(SpriteBatch sprite_batch, Vector2 position)
        {
            List<string> info = PilotInfo[Cursor];

            foreach (string s in info)
            {
                position += new Vector2(0.0f, 32.0f);
                if (String.IsNullOrEmpty(s))
                    continue;

                string[] split = s.Split(':');
                string l = split[0];
                string r = split[1];
                sprite_batch.DrawStringAlignRight(Game.GameRegularFont, position + new Vector2(-8.0f, 0.0f), l, Color.White);
                sprite_batch.DrawStringAlignRight(Game.GameRegularFont, position, ":", Color.White);
                sprite_batch.DrawStringAlignLeft(Game.GameRegularFont, position + new Vector2(+8.0f, 0.0f), r, Color.White);
            }
        }

        public void DrawEditor(SpriteBatch sprite_batch)
        {
            sprite_batch.DrawString(Game.GameRegularFont, String.Format("todo"), new Vector2(8.0f, 1020.0f), Color.White);
        }

        public void PressStart()
        {
            State = EState.PressStart;
        }

        public void Deactivate()
        {
            State = EState.Inactive;    
        }

        public void Activate()
        {
            State = EState.Active;
            InputWaitFrameCount = 0;
        }

        public void Lock()
        {
            State = EState.Locked;    
            Game.HudManager.Huds[(int)GameControllerIndex].UpdatePilot();
        }

        public void Selected()
        {
            State = EState.Selected;    
        }

        public void ChoosePilotFromCursor()
        {
            // TODO: correct mapping somewhere
            string pilot = "";
            switch (Cursor)
            {
                case 0: pilot = "Kazuki"; break;
                case 1: pilot = "Rabbit"; break;
                case 2: pilot = "Gunthor"; break;
                //case 3: pilot = "Mystery"; break;
                default:
                    pilot = "Kazuki";
                    break;
            }

            CSaveData.GetCurrentProfile().Game[Game.PlayersInGame - 1].Pilots[(int)GameControllerIndex].Pilot = pilot;

            // give starting weapons
            SProfileGameData data = CSaveData.GetCurrentGameData(Game);

            int difficulty = CSaveData.GetCurrentGameData(Game).Difficulty;

            CPilot.StartingItems items = CPilot.PilotStartingItems[pilot][difficulty];

            data.Pilots[(int)GameControllerIndex].WeaponPrimaryType = items.PrimaryWeapon;
            data.Pilots[(int)GameControllerIndex].WeaponPrimaryLevel = items.PrimaryWeaponLevel;
            data.Pilots[(int)GameControllerIndex].WeaponSecondaryType = items.SecondaryWeapon;
            data.Pilots[(int)GameControllerIndex].WeaponSecondaryLevel = items.SecondaryWeaponLevel;
            data.Pilots[(int)GameControllerIndex].ChassisType = items.Chassis;
            data.Pilots[(int)GameControllerIndex].GeneratorType = items.Generator;
            data.Pilots[(int)GameControllerIndex].ShieldType = items.Shield;

            SProfile profile = CSaveData.GetCurrentProfile();
            int players_index = Game.PlayersInGame - 1;
            profile.Game[players_index] = data;
            CSaveData.SetCurrentProfileData(profile);
        }
    }
}
