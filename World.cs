//
// World.cs
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace Galaxy
{
    public class CWorld
    {
        public CGalaxy Game { get; set; }
        public Random Random { get; private set; }
        private List<CEntity> Entities { get; set; }
        private List<CEntity> EntitiesToAdd { get; set; }
        private List<CEntity> EntitiesToDelete { get; set; }
        public int Score { get; set; }
        public CStage Stage { get; set; }
        public CScenery BackgroundScenery { get; set; }
        public CScenery ForegroundScenery { get; set; }
        public CCamera GameCamera { get; set; }
        public List<CHud> Huds { get; set; }
        public List<CShip> Players { get; set; }
        public CCollisionGrid CollisionGrid { get; set; }
        public CParticleEffectManager ParticleEffects { get; set; }
        public Thread ParticleUpdateThread { get; set; }
        public bool Paused { get; set; }
        public bool StageEnd { get; set; }
        public int StageEndCounter { get; set; }
        public CFader StageEndFader { get; set; }
        public bool StageFinalEndExitFlag { get; set; }
        public List<string> StageEndText { get; set; }

        public CWorld(CGalaxy game)
        {
            Game = game;
            Random = new Random();
            Entities = new List<CEntity>();
            EntitiesToAdd = new List<CEntity>();
            EntitiesToDelete = new List<CEntity>();
            GameCamera = new CCamera(game);
            GameCamera.Position = Game.PlayerSpawnPosition.ToVector3();
            Huds = new List<CHud>() { new CHud(this, new Vector2(0.0f, Game.GraphicsDevice.Viewport.Height - 60.0f), true) };
            Players = new List<CShip>();
            CollisionGrid = new CCollisionGrid(this, new Vector2(1200.0f, 1200.0f), 10, 10);
            ParticleEffects = new CParticleEffectManager(this);
            StageEndText = new List<string>();
        }

        // TODO: stage definition param
        public void Start()
        {
            CCollision.ClearCache();

            Entities = new List<CEntity>();

            // TODO: load ship from profile
            SProfile profile = CSaveData.GetCurrentProfile();
            CShip ship = CShipFactory.GenerateShip(this, profile, PlayerIndex.One);
            ship.Physics.PositionPhysics.Position = Game.PlayerSpawnPosition;
            EntityAdd(ship);
            Players.Add(ship);

            Stage = new CStage(this, Game.StageDefinition);
            Stage.Start();

            // TODO: should this be in the stage?
            if (!Game.EditorMode)
                CAudio.PlayMusic(Stage.Definition.MusicName);

            MethodInfo bg_method = typeof(CSceneryPresets).GetMethod(Stage.Definition.BackgroundSceneryName);
            BackgroundScenery = bg_method.Invoke(null, new object[] { this }) as CScenery;
            MethodInfo fg_method = typeof(CSceneryPresets).GetMethod(Stage.Definition.ForegroundSceneryName);
            ForegroundScenery = fg_method.Invoke(null, new object[] { this }) as CScenery;
        }

        public void Stop()
        {
            // TODO: should this have ever been here?
            //Game.Music.StopImmediate();

            // TODO: clean up
            if (Stage != null)
            {
                Stage.Finish();
            }

            Entities.Clear();
            EntitiesToAdd.Clear();
            EntitiesToDelete.Clear();
        }

        public void Update()
        {
            if (Paused)
            {
                UpdatePauseInput();
                return;
            }

            Stage.Update();
            BackgroundScenery.Update();
            ForegroundScenery.Update();

            GameCamera.Position += Vector3.UnitY * -Stage.Definition.ScrollSpeed;
            GameCamera.Update();

            UpdatePauseInput();
            UpdateInput();
            UpdateEntities();
            UpdateHuds();

            CollisionGrid.Clear(GameCamera.Position.ToVector2());
            CollisionGrid.Insert(Entities.GetEnumerator());

            // TODO: can be threaded
            ParticleEffects.Update();

            UpdateStageEnd();
        }

        public void UpdatePauseInput()
        {
            if (StageEnd)
                return;

            if (Game.Input.IsPadStartPressedAny() || Game.Input.IsKeyPressed(Keys.P))
            {
                Paused = !Paused;      
            }
        }

        public void UpdateInput()
        {
            // TODO: just allow in-game setting instead?
            if (Players.Count <= 1)
            {
                if (Game.Input.IsPadStartPressed(PlayerIndex.Two) || Game.Input.IsKeyDown(Keys.RightShift))
                {
                    SProfile profile = CSaveData.GetCurrentProfile();
                    CShip ship2 = CShipFactory.GenerateShip(this, profile, PlayerIndex.Two);
                    ship2.Physics.PositionPhysics.Position = Game.PlayerSpawnPosition + Vector2.UnitX * 64.0f;
                    ship2.PlayerIndex = PlayerIndex.Two;
                    EntityAdd(ship2);
                    Players.Add(ship2);
                    Huds.Add(new CHud(this, new Vector2(Game.GraphicsDevice.Viewport.Width - 490.0f, Game.GraphicsDevice.Viewport.Height - 60.0f), false));
                }
            }
        }

        public void UpdateEntities()
        {
            ProcessEntityAdd();
            ProcessEntityUpdates();
            ProcessEntityCollisions();
            ProcessEntityDelete();
        }

        public void UpdateHuds()
        {
            for (int index = 0; index < Players.Count; ++index)
                Huds[index].Update(Players[index]);
        }

        public void UpdateStageEnd()
        {
            if (!StageEnd)
                return;

            StageEndCounter += 1;

            foreach (CShip ship in GetEntitiesOfType(typeof(CShip)))
            {
                ship.Physics.PositionPhysics.Velocity += Vector2.UnitY * -0.1f * StageEndCounter;

                CEffect.StageEndFlyEffect(this,
                                     ship.Physics.PositionPhysics.Position + Random.NextVector2Variable() * 16.0f,
                                     0.5f,
                                     ship.Visual.Color);
            }

            StageEndFader = StageEndFader ?? new CFader(Game) { TransitionTime = 2.0f };
            StageEndFader.Update();

            if (StageEndCounter == 60)
            {
                StageEndText.Add("STAGE CLEAR");
                StageEndText.Add("");
                StageEndText.Add("");
            }

            if (StageEndCounter == 120)
            {
                StageEndText.Add(String.Format("MONEY: {0}", 100));
                StageEndText.Add("");
            }

            if (StageEndCounter == 140)
            {
                StageEndText.Add(String.Format("KILLS: {0}", 100));
                StageEndText.Add("");
            }

            if (StageEndCounter == 160)
            {
                StageEndText.Add(String.Format("COINS: {0}", 100));
                StageEndText.Add("");
            }

            if (StageEndCounter > 240)
            {
                if (Game.Input.IsPadConfirmPressedAny() || Game.Input.IsPadCancelPressedAny() || Game.Input.IsKeyPressed(Keys.Enter))
                {
                    StageFinalEndExitFlag = true;
                }
            }

            if (StageFinalEndExitFlag)
            {
                StageEndFader.StopAtFullFadeOut();
                if (StageEndCounter > 300)
                    SaveAndExit();
            }
            else
            {
                StageEndCounter = Math.Min(StageEndCounter, 300);
                StageEndFader.StopAtHalfFadeOut();
            }
        }

        public void Draw()
        {
            Game.GraphicsDevice.Clear(Color.Black);

            Game.GraphicsDevice.RenderState.ScissorTestEnable = true;
            Game.GraphicsDevice.ScissorRectangle = new Rectangle(
                (int)((Game.GraphicsDevice.Viewport.Width - GameCamera.ScreenSize.X) / 2.0f),
                (int)((Game.GraphicsDevice.Viewport.Height - GameCamera.ScreenSize.Y) / 2.0f),
                (int)GameCamera.ScreenSize.X,
                (int)GameCamera.ScreenSize.Y
            );

            // NOTE: no side panels in editor mode
            if (Game.EditorMode)
                Game.GraphicsDevice.RenderState.ScissorTestEnable = false;

            DrawBackground(GameCamera);
            DrawEntities(GameCamera);
            DrawForeground(GameCamera);

            if (StageEndFader != null)
            {
                Game.DefaultSpriteBatch.Begin();
                StageEndFader.Draw(Game.DefaultSpriteBatch);
                Game.DefaultSpriteBatch.End();
            }

            if (StageEndText.Count > 0)
            {
                Game.DefaultSpriteBatch.Begin();
                Vector2 text_position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2.0f - 90.0f, 350.0f);
                foreach (string text in StageEndText)
                {
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, text, text_position, Color.White);
                    text_position += Vector2.UnitY * 30.0f;
                }
                Game.DefaultSpriteBatch.End();
            }

            Game.GraphicsDevice.RenderState.ScissorTestEnable = false;
            DrawHuds(GameCamera);

#if DEBUG
            if (CInput.IsRawKeyDown(Keys.Q))
            {
                //Console.WriteLine("Total Children: " + QuadTree.Root.CountTotalChildren().ToString());
                CollisionGrid.Draw(GameCamera.WorldMatrix, Color.White);
            }
#endif

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, GameCamera.WorldMatrix);
            ParticleEffects.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();
        }

        public void DrawBackground(CCamera camera)
        {
            BackgroundScenery.Draw(Game.DefaultSpriteBatch);
        }

        public void DrawForeground(CCamera camera)
        {
            ForegroundScenery.Draw(Game.DefaultSpriteBatch);
        }

        public void DrawEntities(CCamera camera)
        {
            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, camera.WorldMatrix);

            foreach (CEntity entity in Entities)
            {
                entity.Draw(Game.DefaultSpriteBatch);
            }

            Game.DefaultSpriteBatch.End();
        }

        public void DrawHuds(CCamera camera)
        {
            // NOTE: no side panels in editor mode!
            if (Game.EditorMode)
            {
                Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, Matrix.Identity);
                foreach (CHud hud in Huds)
                    hud.DrawEditor(Game.DefaultSpriteBatch);
                Game.DefaultSpriteBatch.End();
                return;
            }

            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None, Matrix.Identity);

            foreach (CHud hud in Huds)
                hud.Draw(Game.DefaultSpriteBatch);

            Game.DefaultSpriteBatch.End();
        }

        public void EntityAdd(CEntity entity)
        {
            EntitiesToAdd.Add(entity); 
        }

        public void EntityDelete(CEntity entity)
        {
            EntitiesToDelete.Add(entity);
        }

        public IEnumerable<CEntity> GetEntities()
        {
            return Entities;
        }

        public IEnumerable<CEntity> GetEntitiesOfType(Type type)
        {
            return Entities.Where(entity => entity.GetType() == type);
        }

        public IEnumerable<CEntity> GetEntitiesInBox(CollisionAABB box)
        {
            List<CEntity> results = new List<CEntity>();

            foreach (CEntity entity in Entities)
            {
                // TODO: dont create so many objects
                CollisionCircle collide = CCollision.GetCacheCircle(this, entity.Physics.PositionPhysics.Position, entity.GetRadius());
                if (collide.Intersects(box))
                    results.Add(entity);
            }

            return results;
        }

        public CEntity GetEntityAtPosition(Vector2 position)
        {
            CCollision find = CCollision.GetCacheCircle(this, position, 1.0f);

            foreach (CEntity entity in Entities)
            {
#if !XBOX360
                // TODO: not this hack :|
                if (entity.GetType() == typeof(CEditorEntityPreview))
                    continue;
#endif

                if (entity.Physics == null)
                    continue;

                CCollision collision = entity.Collision ?? CCollision.GetCacheCircle(this, entity.Physics.PositionPhysics.Position, entity.GetRadius());
                if (find.Intersects(collision))
                    return entity;
            }

            return null;
        }

        public CEntity GetHighestEntityAtPosition(Vector2 position)
        {
            CCollision find = CCollision.GetCacheCircle(this, position, 1.0f);

            CEntity result = null;
            float highest = -1.0f;
            foreach (CEntity entity in Entities)
            {
#if !XBOX360
                // TODO: not this hack :|
                if (entity.GetType() == typeof(CEditorEntityPreview))
                    continue;
#endif

                if (entity.Physics == null)
                    continue;

                CCollision collision = entity.Collision ?? CCollision.GetCacheCircle(entity, entity.Physics.PositionPhysics.Position, entity.GetRadius());
                if (!find.Intersects(collision))
                    continue;

                if (highest <= 0.0f)
                {
                    result = entity;
                    highest = 0.0f;
                }

                if (entity.Visual == null)
                    continue;

                if (entity.Visual.Depth >= highest)
                {
                    result = entity;
                    highest = entity.Visual.Depth;
                }
            }

            return result;
        }

        public CShip GetNearestShip(Vector2 position)
        {
            CShip result = null;
            float nearest = float.MaxValue;
            foreach (CShip ship in Players)
            {
                // died
                if (ship.Physics == null)
                    continue;

                Vector2 ship_position = ship.Physics.PositionPhysics.Position;
                Vector2 offset = ship_position - position;
                float length = offset.Length();

                if (length < nearest)
                {
                    result = ship;
                    nearest = length;
                }
            }

            return result;
        }

        public CShip GetNearestShip(Vector2 position, float radius)
        {
            CShip result = null;
            float nearest = float.MaxValue;
            foreach (CShip ship in Players)
            {
                // died
                if (ship.Physics == null)
                    continue;

                Vector2 ship_position = ship.Physics.PositionPhysics.Position;
                Vector2 offset = ship_position - position;
                float length = offset.Length();

                if (length < nearest && length < radius)
                {
                    result = ship;
                    nearest = length;
                }
            }

            return result;
        }

        public CShip GetNearestShipEditor(Vector2 position)
        {
            CShip result = null;
            float nearest = float.MaxValue;
            foreach (CEntity entity in Entities)
            {
                CShip ship = entity as CShip;
                if (ship == null)
                    continue;

                Vector2 ship_position = ship.Physics.PositionPhysics.Position;
                Vector2 offset = ship_position - position;
                float length = offset.Length();

                if (length < nearest)
                {
                    result = ship;
                    nearest = length;
                }
            }

            return result;
        }

        // TODO: generic implementation
        public CEnemy GetNearestEnemy(Vector2 position)
        {
            CEnemy result = null;
            float nearest = float.MaxValue;
            foreach (CEntity entity in Entities)
            {
                if (entity.GetType().IsSubclassOf(typeof(CEnemy)) == false)
                    continue;

                CEnemy enemy = entity as CEnemy;
                Vector2 enemy_position = enemy.Physics.PositionPhysics.Position;
                Vector2 offset = enemy_position - position;
                float length = offset.Length();

                if (length < nearest)
                {
                    result = enemy;
                    nearest = length;
                }
            }

            return result;
        }

        public CEnemy GetNearestEnemySeekable(Vector2 position, bool ignore_already_targeted)
        {
            CEnemy result = null;
            float nearest = float.MaxValue;
            foreach (CEntity entity in Entities)
            {
                if (entity.GetType().IsSubclassOf(typeof(CEnemy)) == false)
                    continue;

                CEnemy enemy = entity as CEnemy;

                if (enemy.CanSeekerTarget == false)
                    continue;

                if (ignore_already_targeted && enemy.IsSeekerTarget == true)
                    continue;

                if (enemy.IsInScreen() == false)
                    continue;

                Vector2 enemy_position = enemy.Physics.PositionPhysics.Position;
                Vector2 offset = enemy_position - position;
                float length = offset.Length();

                if (length < nearest)
                {
                    result = enemy;
                    nearest = length;

                }
            }

            return result;
        }

        private void ProcessEntityUpdates()
        {
            foreach (CEntity entity in Entities)
            {
                entity.Update();
            }
        }

        private void ProcessEntityCollisions()
        {
            CollisionGrid.Collide();
        }

        private void ProcessEntityCollisionsOld()
        {
            Type[] types = new Type[] { null };
            object[] parameters = new object[] { null };

            foreach (CEntity outer in Entities)
            {
                foreach (CEntity inner in Entities)
                {
                    if (inner == outer)
                        continue;

                    if (inner.Collision == null || outer.Collision == null)
                        continue;

                    if (inner.Collision.Enabled == false || outer.Collision.Enabled == false)
                        continue;

                    if (outer.GetType() == inner.GetType())
                    {
                        if (outer.Collision.IgnoreSelfType && inner.Collision.IgnoreSelfType)
                            continue;
                    }

                    if (outer.Collision.Intersects(inner.Collision))
                    {
                        // TODO: something proper
                        System.Type inner_type = inner.GetType();
                        System.Type outer_type = outer.GetType();
                        types[0] = inner_type;
                        MethodInfo method = outer_type.GetMethod("OnCollide", types);

                        if (method == null)
                            continue;

                        try
                        {
                            parameters[0] = inner;
                            method.Invoke(outer, parameters);
                        }
                        catch (Exception exception)
                        {
                            throw exception.InnerException;
                        }
                    }
                }
            }
        }

        public void ProcessEntityAdd()
        {
            foreach (CEntity entity in EntitiesToAdd)
            {
                Entities.Add(entity);
            }
            EntitiesToAdd.Clear();
        }

        private void ProcessEntityDelete()
        {
            foreach (CEntity entity in EntitiesToDelete)
            {
                Entities.Remove(entity);
            }
            EntitiesToDelete.Clear();
        }

        private void SaveAndExit()
        {
            // TODO: is this a good place to add score to money?
            // TODO: 2p
            SProfile profile = CSaveData.GetCurrentProfile();
            profile.Money += Score;

            // TODO: not this, but save current ship upgrades to profile
            CShip ship = GetNearestShip(Vector2.Zero);
            if (ship != null)
            {
                profile.WeaponPrimaryType = ship.PrimaryWeapon.Type;
                profile.WeaponPrimaryLevel = ship.PrimaryWeapon.Level;
                profile.WeaponSecondaryType = ship.SecondaryWeapon.Type;
                profile.WeaponSecondaryLevel = ship.SecondaryWeapon.Level;
                profile.WeaponSidekickLeftType = ship.SidekickLeft.Type;
                profile.WeaponSidekickLeftLevel = ship.SidekickLeft.Level;
                profile.WeaponSidekickRightType = ship.SidekickRight.Type;
                profile.WeaponSidekickRightLevel = ship.SidekickRight.Level;
                profile.WeaponSecondaryLevel = ship.SecondaryWeapon.Level;
            }

            profile.CurrentStage = Stage.Definition.Name;

            CSaveData.SetCurrentProfileData(profile);
            CSaveData.Save();

            // TODO: lobby state
            Game.State = new CStateFadeTo(Game, Game.State, new CStateShop(Game));
        }

    }
}
