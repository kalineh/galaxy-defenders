﻿//
// StateMainMenu.cs
//

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CStateMainMenu
        : CState
    {
        public CGalaxy Game { get; set; }
        public Texture2D TitleTexture { get; set; }
        private CWorld EmptyWorld { get; set; }
        public CMenu Menu { get; set; }
        public CMenu MenuMain { get; set; }
        public CMenu MenuNewGameContinue { get; set; }
        public CMenu MenuConfirmNewGame { get; set; }
        public COptionsMenu MenuOptions { get; set; }
        public CMenu MenuQuitConfirm { get; set; }
        public CMenu MenuDifficultySelect { get; set; }
        public CSampleShipManager SampleShipManager { get; set; }
        public bool IsSelectingPilot { get; set; }

        public CStateMainMenu(CGalaxy game)
        {
            Game = game;
            TitleTexture = CContent.LoadTexture2D(Game, "Textures/UI/Title");
            EmptyWorld = new CWorld(game, null);
            MenuMain = new CMenu(game)
            {
                Position = new Vector2(Game.Resolution.X / 2.0f - 128.0f, 400.0f),
                MenuOptions = new List<CMenu.CMenuOption>()
                {
                    new CMenu.CMenuOption() { Text = "Solo Game", Select = GotoNewGameContinue, Data = 1 },
                    new CMenu.CMenuOption() { Text = "Coop Game", Select = GotoNewGameContinue, SelectValidate = CheckPlayers2P, Data = 2 },
                    new CMenu.CMenuOption() { Text = "Options", Select = GotoOptions },
                    new CMenu.CMenuOption() { Text = "Quit", Select = GotoQuitConfirm, PanelType = CMenu.PanelType.Small, },
                },
            };

            MenuNewGameContinue = new CMenu(game)
            {
                Position = new Vector2(Game.Resolution.X / 2.0f - 128.0f, 400.0f),
                MenuOptions = new List<CMenu.CMenuOption>()
                {
                    new CMenu.CMenuOption() { Text = "Continue", Select = ContinueGame, SelectValidate = CheckContinue },
                    new CMenu.CMenuOption() { Text = "New Game", Select = ConfirmNewGame },
                    new CMenu.CMenuOption() { Text = "Back", CancelOption = true, Select = BackToMainMenu, PanelType = CMenu.PanelType.Small, },
                },
            };

            // show special clear mode continue
            if (CSaveData.GetCurrentGameData(Game).ClearedGame == true)
            {
                MenuNewGameContinue.MenuOptions[0].Text = "Continue++";
            }

            MenuConfirmNewGame = new CMenu(game)
            {
                Position = new Vector2(Game.Resolution.X / 2.0f - 128.0f, 400.0f),
                MenuOptions = new List<CMenu.CMenuOption>()
                {
                    new CMenu.CMenuOption() { Text = "Overwrite Save?", SelectValidate = (tag) => { return false; }, },
                    new CMenu.CMenuOption() { Text = "Overwrite", Select = NewGame },
                    new CMenu.CMenuOption() { Text = "Cancel", CancelOption = true, Select = BackToNewGameContinueMenu, PanelType = CMenu.PanelType.Small, },
                },
            };

            MenuConfirmNewGame.AllowHighlightInvalid = false;

            MenuOptions = new COptionsMenu(Game)
            {
                Position = new Vector2(Game.Resolution.X / 2.0f - 128.0f, 400.0f),
                OnBack = OptionsBack,
            };

            MenuQuitConfirm = new CMenu(Game)
            {
                Position = new Vector2(Game.Resolution.X / 2.0f - 128.0f, 400.0f),
                MenuOptions = new List<CMenu.CMenuOption>()
                {
                    new CMenu.CMenuOption() { Text = "Cancel", Select = BackToMainMenu },
                    new CMenu.CMenuOption() { Text = "Quit", Select = QuitGame, PanelType = CMenu.PanelType.Small },
                },
            };

            MenuDifficultySelect = new CMenu(game)
            {
                Position = new Vector2(Game.Resolution.X / 2.0f - 128.0f, 400.0f),
                MenuOptions = new List<CMenu.CMenuOption>()
                {
                    new CMenu.CMenuOption() { Text = "Normal", Select = SelectDifficulty, Data = CDifficulty.DifficultyLevel.Normal },
                    new CMenu.CMenuOption() { Text = "Hard", Select = SelectDifficulty, Data = CDifficulty.DifficultyLevel.Hard },
                    new CMenu.CMenuOption() { Text = "Really Hard", Select = SelectDifficulty, Data = CDifficulty.DifficultyLevel.Extreme },
                    new CMenu.CMenuOption() { Text = "Back", Select = BackToNewGameContinueMenu, CancelOption = true, PanelType = CMenu.PanelType.Small },
                },
            };


            Menu = MenuMain;

            // must wait for savedata/controllers to show main menu
            Menu.Visible = false;

            SampleShipManager = new CSampleShipManager(EmptyWorld);

            EmptyWorld.BackgroundScenery = CSceneryPresets.BlueSky(EmptyWorld);
            EmptyWorld.ForegroundScenery = CSceneryPresets.Empty(EmptyWorld);

            Game.HudManager.ActivatePressStart();

            if (!Game.EditorMode)
                CAudio.PlayMusic("Konami's_Moon_Base");
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void Update()
        {
            if (Game.Input.CountConnectedControllers() == 0)
            {
                Game.HudManager.ActivatePressStart();
            }
            else
            {
                if (GuideUtil.StorageDeviceReady)
                {
                    Menu.Visible = true;

                    // special side-bar pilot selection, no menu needed here
                    if (IsSelectingPilot)
                        Menu.Visible = false;

                    Menu.Update();
                }
            }

            DebugInput();

            // allow application exit from main menu (even without controllers bound)
            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Back) ||
                GamePad.GetState(PlayerIndex.Two).IsButtonDown(Buttons.Back) ||
                GamePad.GetState(PlayerIndex.Three).IsButtonDown(Buttons.Back) ||
                GamePad.GetState(PlayerIndex.Four).IsButtonDown(Buttons.Back) ||
                Game.Input.IsKeyPressed(Keys.Q))
            {
                Game.Exit();
            }

            // will only be true after pilot selection
            if (Game.HudManager.IsPilotSelectCompleteAll())
            {
                Game.HudManager.LockHuds();

                // TODO: a bit of a hack place to put this
                SProfile data = CSaveData.GetCurrentProfile();
                data.Game[Game.PlayersInGame - 1].RandomPartsSeed = (new Random()).Next();
                CSaveData.SetCurrentProfileData(data);
                CSaveData.SaveRequest();

                // NOTE: jump straight to stage 1 now, let player shop after
                //Game.State = new CStateFadeTo(Game, this, new CStateShop(Game));
#if DEBUG
#if !SOAK_TEST
                // debug can just shop
                Game.State = new CStateFadeTo(Game, this, new CStateShop(Game));
                return;
#endif
#endif
                CStageDefinition definition = CStageDefinition.GetStageDefinitionByName("Stage1");
                Game.State = new CStateFadeTo(Game, this, new CStateGame(Game, definition));
                return;
            }

            SampleShipManager.Update();
            EmptyWorld.GameCamera.Update();
            EmptyWorld.UpdateEntitiesSingleThreadCollision();
            EmptyWorld.BackgroundScenery.Update();
            EmptyWorld.ForegroundScenery.Update();
            EmptyWorld.GameCamera.Position = new Vector3(0.0f, 0.0f, 0.0f);

            EmptyWorld.ParticleEffects.Update();
        }

        public override void Draw()
        {
            EmptyWorld.UpdateScissorRectangle();
            EmptyWorld.DrawBackground(EmptyWorld.GameCamera);

            SampleShipManager.Draw();
            EmptyWorld.DrawEntities(EmptyWorld.GameCamera);

            Game.DefaultSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, EmptyWorld.GameCamera.WorldMatrix);
            EmptyWorld.ParticleEffects.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

            Game.DefaultSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Game.RenderScaleMatrix);
            Game.DefaultSpriteBatch.Draw(TitleTexture, new Vector2(Game.Resolution.X / 2.0f - 256.0f, 120.0f), Color.White);
            Menu.Draw(Game.DefaultSpriteBatch);

            // TODO: test removing controller during this state
            if (IsSelectingPilot)
            {
                
            }

            if (Game.Input.CountConnectedControllers() == 0)
            {
                Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, "Press Start", new Vector2(Game.Resolution.X / 2.0f - 20.0f, 650.0f), Color.White, 0.0f, new Vector2(60.0f, 10.0f), 1.5f + (float)Math.Sin(Game.GameFrame * 0.05f) * 0.05f, SpriteEffects.None, 0.0f);
            }
            else
            {
                Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, "@kalin_t", new Vector2(Game.Resolution.X / 2.0f + 400.0f, Game.Resolution.Y - 100.0f), Color.White, 0.0f, new Vector2(60.0f, 10.0f), 0.8f, SpriteEffects.None, 0.0f);
                Game.DefaultSpriteBatch.DrawString(Game.GameRegularFont, "@RushJet1", new Vector2(Game.Resolution.X / 2.0f + 400.0f, Game.Resolution.Y - 80.0f), Color.White, 0.0f, new Vector2(60.0f, 10.0f), 0.8f, SpriteEffects.None, 0.0f);
            }

            Game.DefaultSpriteBatch.End();
        }

        private void GotoNewGameContinue(object tag)
        {
            Game.PlayersInGame = (int)tag;
            Menu = MenuNewGameContinue;
            Menu.MoveToFirstValid();

            // XNA4
//#if XBOX360
            //// handled in Guide.cs
//#else
            //string username = "galaxy";
            //CSaveData.AddNewProfile(username);
            //CSaveData.SetCurrentProfile(username);
//#endif
        }

        private void GotoOptions(object tag)
        {
            Menu = MenuOptions;
            MenuOptions.RefreshVolumes();
        }

        private void OptionsBack()
        {
            Menu = MenuMain;    
        }

        private void ConfirmNewGame(object tag)
        {
            int game_index = Game.PlayersInGame - 1;

            SProfile profile = CSaveData.GetCurrentProfile();

            if (profile.Game[game_index].InProgress)
            {
                Menu = MenuConfirmNewGame;
                Menu.Cursor = 2;
                return;
            }

            NewGame(tag);
        }

        private void NewGame(object tag)
        {
            int game_index = Game.PlayersInGame - 1;

            SProfile profile = CSaveData.GetCurrentProfile();

            profile.Game[game_index].Stage = "Start";
            profile.Game[game_index].InProgress = true;
            profile.Game[game_index].Pilots = new SProfilePilotState[2] {
                SProfilePilotState.MakeDefaultPilot(0),
                SProfilePilotState.MakeDefaultPilot(1),
            };

            CSaveData.SetCurrentProfileData(profile);

            Menu = MenuDifficultySelect;
        }

        private void ContinueGame(object tag)
        {
            Game.HudManager.LockHuds();
            Game.State = new CStateFadeTo(Game, this, new CStateShop(Game));
        }

        private bool CheckPlayers2P(object tag)
        {
            return Game.Input.CountConnectedControllers() > 1;
        }

        private bool CheckContinue(object tag)
        {
            return CSaveData.GetCurrentGameData(Game).Stage != "";
        }

        private void BackToMainMenu(object tag)
        {
            Menu = MenuMain;    
        }

        private void BackToNewGameContinueMenu(object tag)
        {
            Menu = MenuNewGameContinue;    
        }

        private void GotoQuitConfirm(object tag)
        {
            Menu = MenuQuitConfirm;
        }

        private void QuitGame(object tag)
        {
            Game.Exit();
        }

        private void SelectDifficulty(object tag)
        {
            SProfile profile = CSaveData.GetCurrentProfile();
            int difficulty = (int)((CDifficulty.DifficultyLevel)tag);
            profile.Game[Game.PlayersInGame - 1].Difficulty = difficulty;
            profile.Game[Game.PlayersInGame - 1].Pilots[0].Money = CDifficulty.StartingMoney[difficulty];
            profile.Game[Game.PlayersInGame - 1].Pilots[1].Money = CDifficulty.StartingMoney[difficulty];
            profile.Game[Game.PlayersInGame - 1].Pilots[0].WeaponPrimaryLevel = CDifficulty.StartingWeaponLevel[difficulty];
            profile.Game[Game.PlayersInGame - 1].Pilots[1].WeaponPrimaryLevel = CDifficulty.StartingWeaponLevel[difficulty];
            CSaveData.SetCurrentProfileData(profile);

            Game.HudManager.ActivatePilotSelect();
            IsSelectingPilot = true;
        }

        private void DebugInput()
        {
#if DEBUG
            if (!Game.Input.IsKeyDown(Keys.LeftControl))
                return;

            CStageDefinition definition = null;
            if (Game.Input.IsKeyPressed(Keys.D1)) definition = CStageDefinition.GetStageDefinitionByName("Stage1");
            if (Game.Input.IsKeyPressed(Keys.D2)) definition = CStageDefinition.GetStageDefinitionByName("Stage2");
            if (Game.Input.IsKeyPressed(Keys.D3)) definition = CStageDefinition.GetStageDefinitionByName("Stage3");
            if (Game.Input.IsKeyPressed(Keys.D4)) definition = CStageDefinition.GetStageDefinitionByName("Stage4");
            if (Game.Input.IsKeyPressed(Keys.D5)) definition = CStageDefinition.GetStageDefinitionByName("Stage5");
            if (Game.Input.IsKeyPressed(Keys.D6)) definition = CStageDefinition.GetStageDefinitionByName("Stage6");
            if (Game.Input.IsKeyPressed(Keys.D7)) definition = CStageDefinition.GetStageDefinitionByName("Stage7");
            if (Game.Input.IsKeyPressed(Keys.D8)) definition = CStageDefinition.GetStageDefinitionByName("Stage8");
            if (Game.Input.IsKeyPressed(Keys.D0)) definition = CStageDefinition.GetStageDefinitionByName("Stage9");
            if (definition != null)
            {
                Game.State = new CStateGame(Game, definition);
            }
#endif
        }
    }
}
