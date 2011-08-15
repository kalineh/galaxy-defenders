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
            // TODO: remove, obsolete
            //if (Game.PlayersInGame > 1)
            //{
                //if (Game.Input.IsL1PressedAny() || Game.Input.IsKeyPressed(Keys.F1))
                    //ChangeShoppingPlayer(GameControllerIndex.One);
                //if (Game.Input.IsR1PressedAny() || Game.Input.IsKeyPressed(Keys.F2))
                    //ChangeShoppingPlayer(GameControllerIndex.Two);
            //}

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

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, EmptyWorld.GameCamera.WorldMatrix);
            Menu1P.DrawSampleShip(Game.DefaultSpriteBatch);
            if (Game.PlayersInGame > 1)
                Menu2P.DrawSampleShip(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, EmptyWorld.GameCamera.WorldMatrix);
            EmptyWorld.ParticleEffects.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();
        }

        public override void PostHudDraw()
        {
            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, Game.RenderScaleMatrix);

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
            Game.State = new CStateFadeTo(Game, this, new CStateStageSelect(Game));
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

        // remove
        //private void SetScoreboardPosition()
        //{
            //if (ScorePanel != null && ScorePanel.IsVisible())
                //ScorePanel.BasePosition = new Vector2(ShoppingPlayer == GameControllerIndex.One ? 774.0f : 1146.0f, 156.0f);
        //}
    }
}
