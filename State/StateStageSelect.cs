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
                Position = new Vector2(500.0f, 300.0f),
                MenuOptions = new List<CMenu.MenuOption>(),
            };

            List<string> selectable_stages = CMap.GetMapNodeByStageName(CSaveData.GetCurrentProfile().CurrentStage).Next;
            foreach (string stage in selectable_stages)
            {
                // DEBUG: add all stages
                if (stage == "*")
                {
                    Assembly assembly = Assembly.GetAssembly(typeof(Galaxy.CEntity));
                    IEnumerable<Type> types = assembly.GetTypes().Where(t => String.Equals(t.Namespace, "Galaxy.Stages"));
                    IEnumerable<Type> types_sorted = types.OrderBy(t => t.Name.Length);
                    IEnumerable<Type> types_filtered = types.Where(t => selectable_stages.Contains(t.Name) == false);
                    foreach (Type type in types_filtered)
                    {
                        Menu.MenuOptions.Add(new CMenu.MenuOption() { Text = "* " + type.Name, Function = StartGame, Data = type.Name });
                    }
                }
                else
                {
                    Menu.MenuOptions.Add(new CMenu.MenuOption() { Text = stage, Function = StartGame, Data = stage });
                }
            }

            Menu.MenuOptions.Add(new CMenu.MenuOption() { Text = "Back", Function = Back });
            EmptyWorld.GameCamera.Position = Vector3.Zero;
            EmptyWorld.GameCamera.Update();
        }

        public override void Update()
        {
            Menu.Update();
        }

        public override void Draw()
        {
            Game.GraphicsDevice.Clear(Color.Black);

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, EmptyWorld.GameCamera.WorldMatrix);
            Game.DefaultSpriteBatch.End();

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
