//
// StateShop.cs
//

using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Galaxy
{
    public class CStateShop
        : CState
    {
        public CGalaxy Game { get; set; }
        public CWorld EmptyWorld { get; set; }
        private CShopMenu Menu1P { get; set; }
        private CShopMenu Menu2P { get; set; }
        private int GenerateEnemyDelay { get; set; }

        public CStateShop(CGalaxy game)
        {
            Game = game;
            EmptyWorld = new CWorld(game, null);

            EmptyWorld.BackgroundScenery = new CSceneryChain(EmptyWorld,
                new CBackground(EmptyWorld, new Color(0, 0, 0)),
                new CStars(EmptyWorld, CContent.LoadTexture2D(Game, "Textures/Background/Star"), 0.4f, 3.5f),
                new CStars(EmptyWorld, CContent.LoadTexture2D(Game, "Textures/Background/Star"), 0.6f, 2.1f)
            );
            EmptyWorld.ForegroundScenery = CSceneryPresets.Empty(EmptyWorld);
            EmptyWorld.GameCamera.Position = new Vector3(0.0f, 0.0f, 0.0f);

            Menu1P = new CShopMenu(game);
            Menu2P = new CShopMenu(game);

            Menu1P.Initialize(GameControllerIndex.One, EmptyWorld);
            Menu2P.Initialize(GameControllerIndex.Two, EmptyWorld);

            if (!Game.EditorMode)
                CAudio.PlayMusic("Konami's_Moon_Base");

            //if (CSaveData.GetCurrentGameData(Game).Stage == "Stage1")
                //FrameCount = 1600;
        }

        public override void OnExit()
        {
            base.OnExit();

            EmptyWorld.Stop();

            Game.HudManager.Huds[0].ShowMoney = true;
            Game.HudManager.Huds[1].ShowMoney = true;
        }

        public override void Update()
        {
            if (Game.Input.IsPadBackPressedAny() || Game.Input.IsKeyPressed(Keys.Back))
            {
                Menu1P.SaveLockedProfile();
                if (Game.PlayersInGame > 1)
                    Menu2P.SaveLockedProfile();

                Game.State = new CStateFadeTo(Game, this, new CStateMainMenu(Game));
                return;
            }

            Menu1P.Update();
            if (Game.PlayersInGame > 1)
                Menu2P.Update();

            bool move_to_stage = Menu1P.IsMoveToStage;
            if (Game.PlayersInGame > 1)
                move_to_stage = move_to_stage && Menu2P.IsMoveToStage;

            if (move_to_stage)
                StageSelect();

            UpdateGenerateEnemy();
            EmptyWorld.GameCamera.Update();
            EmptyWorld.UpdateEntitiesSingleThreadCollision();
            EmptyWorld.ProcessEntityAdd();
            EmptyWorld.ProcessEntityDelete();
            EmptyWorld.BackgroundScenery.Update();
            EmptyWorld.ForegroundScenery.Update();
            EmptyWorld.ParticleEffects.Update();
        }

        public override void Draw()
        {
            EmptyWorld.DrawBackground(EmptyWorld.GameCamera);
            EmptyWorld.DrawEntities(EmptyWorld.GameCamera);

            Game.DefaultSpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, EmptyWorld.GameCamera.WorldMatrix);
            Menu1P.DrawSampleShip(Game.DefaultSpriteBatch);
            if (Game.PlayersInGame > 1)
                Menu2P.DrawSampleShip(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

            Game.DefaultSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, EmptyWorld.GameCamera.WorldMatrix);
            EmptyWorld.ParticleEffects.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

            //float alpha = 0.7f + (float)Math.Abs(Math.Sin(FrameCount * 0.01f)) * 0.3f;
            //Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, Game.RenderScaleMatrix);
            ////Game.DefaultSpriteBatch.DrawStringAlignCenter(Game.GameLargeFont, new Vector2(Game.Resolution.X / 2.0f, Game.Resolution.Y * 0.8f), "PRESS START TO ENTER STAGE", new Color(160, 160, 160, (byte)alpha));
            //Game.DefaultSpriteBatch.DrawStringAlignCenter(Game.GameLargeFont, new Vector2(Game.Resolution.X / 2.0f, Game.Resolution.Y * 0.8f), FrameCount.ToString(), new Color(160, 160, 160, (byte)alpha));
            //Game.DefaultSpriteBatch.End();
        }

        public override void PostHudDraw()
        {
            Game.DefaultSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Game.RenderScaleMatrix);

            Menu1P.Draw(Game.DefaultSpriteBatch);
            if (Game.PlayersInGame > 1)
                Menu2P.Draw(Game.DefaultSpriteBatch);

            Game.DefaultSpriteBatch.End();
        }

        private void PressStart(GameControllerIndex index)
        {
            if (index == GameControllerIndex.One)
                Menu1P.FlyToStage();

            if (index == GameControllerIndex.Two)
                Menu2P.FlyToStage();
        }

        private void StageSelect()
        {
#if SOAK_TEST
            SProfileGameData data = CSaveData.GetCurrentGameData(Game);
            int stage = CMap.GetMapNodeByStageName(data.Stage).SaveIndex;
            int next = (stage + 1) % 12;
            next = EmptyWorld.Random.Next() % 12;
            CStageDefinition definition = CStageDefinition.GetStageDefinitionByName(String.Format("Stage{0}", next + 1));
            Game.State = new CStateFadeTo(Game, this, new CStateGame(Game, definition));
            return;
#endif

#if DEBUG
            Game.State = new CStateFadeTo(Game, this, new CStateStageSelect(Game));
#else
            if (CSaveData.GetCurrentGameData(Game).ClearedGame == true)
            {
                Game.State = new CStateFadeTo(Game, this, new CStateStageSelect(Game));
            }
            else
            {
                List<string> selectable_stages = CMap.GetMapNodeByStageName(CSaveData.GetCurrentGameData(Game).Stage).Next;
                string stage = selectable_stages.Count > 0 ? selectable_stages[0] : "Stage1";
                CStageDefinition definition = CStageDefinition.GetStageDefinitionByName((string)stage);
                Game.State = new CStateFadeTo(Game, this, new CStateGame(Game, definition));
            }
#endif
        }

        private void UpdateGenerateEnemy()
        {
            GenerateEnemyDelay += 1;
            if (GenerateEnemyDelay < 30)
                return;

            CEnemy enemy = null;
            int rand = EmptyWorld.Random.Next() % 4;
            switch (rand)
            {
                case 0: enemy = new CBall(); break;
                case 1: enemy = new CShootBall(); break;
                case 2: enemy = new CIsosceles(); break;
                case 3: enemy = new CBigBall(); break;
            }
            enemy.Initialize(EmptyWorld);
            bool left = EmptyWorld.Random.NextBool();
            enemy.Mover = left ? CMoverPresets.DownRight(6.0f, 1.5f) : CMoverPresets.DownLeft(6.0f, 1.5f);
            float offset = left ? -180.0f : 180.0f;
            float spawn_x = 155.0f + offset;
            if (Game.PlayersInGame > 1)
                spawn_x = 0.0f + offset;
            enemy.Physics.Position = new Vector2(spawn_x, -570.0f);
            enemy.Coins = 0;
            EmptyWorld.EntityAdd(enemy);

            GenerateEnemyDelay = 0 - (int)(EmptyWorld.Random.NextFloat() * 60);
        }
    }
}
