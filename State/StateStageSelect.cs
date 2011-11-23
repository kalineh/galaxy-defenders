//
// StateStageSelect.cs
//

using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxy
{
    public class CStateStageSelect
        : CState
    {
        public CGalaxy Game { get; set; }
        public Texture2D TitleTexture { get; set; }
        private CWorld EmptyWorld { get; set; }
        public CMenu Menu { get; set; }

        public CStateStageSelect(CGalaxy game)
        {
            Game = game;
            EmptyWorld = new CWorld(game, null);
            Menu = new CMenu(game)
            {
                Position = new Vector2(Game.Resolution.X / 2.0f - 128.0f, 350.0f),
                MenuOptions = new List<CMenu.CMenuOption>(),
            };

            bool cleared_game = CSaveData.GetCurrentGameData(Game).ClearedGame == true;
#if DEBUG
            cleared_game = true;
#endif

            if (!cleared_game)
            {
                List<string> selectable_stages = CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).Next;
                foreach (string stage in selectable_stages)
                {
                    Menu.MenuOptions.Add(new CMenu.CMenuOption() { Text = stage, Select = StartGame, Data = stage });
                }
            }
            else
            {
                Menu.Position += new Vector2(0.0f, -250.0f);
                Menu.MenuOptions.Add(new CMenu.CMenuOption() { Text = "Stage 1", Select = StartGame, Data = "Stage1" });
                Menu.MenuOptions.Add(new CMenu.CMenuOption() { Text = "Stage 2", Select = StartGame, Data = "Stage2" });
                Menu.MenuOptions.Add(new CMenu.CMenuOption() { Text = "Stage 3", Select = StartGame, Data = "Stage3" });
                Menu.MenuOptions.Add(new CMenu.CMenuOption() { Text = "Stage 4", Select = StartGame, Data = "Stage4" });
                Menu.MenuOptions.Add(new CMenu.CMenuOption() { Text = "Stage 5", Select = StartGame, Data = "Stage5" });
                Menu.MenuOptions.Add(new CMenu.CMenuOption() { Text = "Stage 6", Select = StartGame, Data = "Stage6" });
                Menu.MenuOptions.Add(new CMenu.CMenuOption() { Text = "Stage 7", Select = StartGame, Data = "Stage7" });
                Menu.MenuOptions.Add(new CMenu.CMenuOption() { Text = "Stage 8", Select = StartGame, Data = "Stage8" });
                Menu.MenuOptions.Add(new CMenu.CMenuOption() { Text = "Stage 9", Select = StartGame, Data = "Stage9" });
                Menu.MenuOptions.Add(new CMenu.CMenuOption() { Text = "Stage 10", Select = StartGame, Data = "Stage10" });
                Menu.MenuOptions.Add(new CMenu.CMenuOption() { Text = "Stage 11", Select = StartGame, Data = "Stage11" });
                Menu.MenuOptions.Add(new CMenu.CMenuOption() { Text = "Stage 12", Select = StartGame, Data = "Stage12" });
            }

            Menu.MenuOptions.Add(new CMenu.CMenuOption() { Text = "Back", Select = Back, CancelOption = true, PanelType = CMenu.PanelType.Small });
            EmptyWorld.GameCamera.Position = Vector3.Zero;
            EmptyWorld.GameCamera.Update();

            EmptyWorld.BackgroundScenery = new CSceneryChain(EmptyWorld,
                new CBackground(EmptyWorld, new Color(0, 0, 0)),
                new CStars(EmptyWorld, CContent.LoadTexture2D(Game, "Textures/Background/Star"), 0.4f, 18.0f),
                new CStars(EmptyWorld, CContent.LoadTexture2D(Game, "Textures/Background/Star"), 0.6f, 14.0f)
            );
            EmptyWorld.ForegroundScenery = CSceneryPresets.Empty(EmptyWorld);

            if (!Game.EditorMode)
                CAudio.PlayMusic("Konami's_Moon_Base");
        }

        public override void Update()
        {
            Menu.Update();
            EmptyWorld.BackgroundScenery.Update();
            EmptyWorld.ForegroundScenery.Update();
        }

        public override void Draw()
        {
            EmptyWorld.DrawBackground(EmptyWorld.GameCamera);

            Game.DefaultSpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Game.RenderScaleMatrix);
            Menu.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

            Game.DefaultSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, EmptyWorld.GameCamera.WorldMatrix);
            EmptyWorld.ParticleEffects.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();
        }

        private void StartGame(object stage)
        {
            CStageDefinition definition = CStageDefinition.GetStageDefinitionByName((string)stage);
            Game.State = new CStateFadeTo(Game, this, new CStateGame(Game, definition));
        }

        private void Back(object tag)
        {
            Game.State = new CStateFadeTo(Game, this, new CStateShop(Game));
        }
    }
}
