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
            EmptyWorld = new CWorld(game);
            Menu = new CMenu(game)
            {
                Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f - 70.0f, 350.0f),
                MenuOptions = new List<CMenu.MenuOption>(),
            };

            List<string> selectable_stages = CMap.GetMapNodeByStageName(CSaveData.GetCurrentProfile().CurrentStage).Next;
            foreach (string stage in selectable_stages)
            {
                // DEBUG: add all stages
#if !XBOX360
                if (stage == "*")
                {
                    Assembly assembly = Assembly.GetAssembly(typeof(Galaxy.CEntity));
                    IEnumerable<Type> types = assembly.GetTypes().Where(t => String.Equals(t.Namespace, "Galaxy.Stages"));
                    IEnumerable<Type> types_sorted = types.OrderBy(t => t.Name.Length);
                    IEnumerable<Type> types_filtered = types.Where(t => selectable_stages.Contains(t.Name) == false);
                    foreach (Type type in types_filtered)
                    {
                        Menu.MenuOptions.Add(new CMenu.MenuOption() { Text = "* " + type.Name, Select = StartGame, Data = type.Name });
                    }
                }
                else
                {
                    Menu.MenuOptions.Add(new CMenu.MenuOption() { Text = stage, Select = StartGame, Data = stage });
                }
#else // XBOX360
                Menu.MenuOptions.Add(new CMenu.MenuOption() { Text = stage, Select = StartGame, Data = stage });
                // TODO: find a way to automate this on 360
                if (stage == "*")
                {
                    Menu.MenuOptions.Add(new CMenu.MenuOption() { Text = "BonusStage1", Select = StartGame, Data = "Stage1" });
                    Menu.MenuOptions.Add(new CMenu.MenuOption() { Text = "BonusStage1", Select = StartGame, Data = "Stage2" });
                    Menu.MenuOptions.Add(new CMenu.MenuOption() { Text = "BonusStage1", Select = StartGame, Data = "Stage3" });
                    Menu.MenuOptions.Add(new CMenu.MenuOption() { Text = "BonusStage1", Select = StartGame, Data = "BonusStage1" });
                }
#endif
            }

            Menu.MenuOptions.Add(new CMenu.MenuOption() { Text = "Back", Select = Back, CancelOption = true });
            EmptyWorld.GameCamera.Position = Vector3.Zero;
            EmptyWorld.GameCamera.Update();

            EmptyWorld.Scenery = new CSceneryChain(EmptyWorld,
                new CBackground(EmptyWorld, new Color(0, 0, 0)),
                new CStars(EmptyWorld, CContent.LoadTexture2D(Game, "Textures/Background/Star"), 0.4f, 18.0f),
                new CStars(EmptyWorld, CContent.LoadTexture2D(Game, "Textures/Background/Star"), 0.6f, 14.0f)
            );

            Game.Music.Play("Music/Title");
        }

        public override void Update()
        {
            Menu.Update();
            EmptyWorld.Scenery.Update();
        }

        public override void Draw()
        {
            Game.GraphicsDevice.Clear(Color.Black);

            EmptyWorld.DrawBackground(EmptyWorld.GameCamera);
            EmptyWorld.DrawHuds(EmptyWorld.GameCamera);

            Game.DefaultSpriteBatch.Begin();
            Menu.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();
        }

        private void StartGame(object stage)
        {
            CStageDefinition definition = CStageDefinition.GetStageDefinitionByName((string)stage);
            Game.StageDefinition = definition;
            Game.State = new CStateFadeTo(Game, this, new CStateGame(Game));
        }

        private void Back(object tag)
        {
            Game.State = new CStateFadeTo(Game, this, new CStateShop(Game));
        }
    }
}
