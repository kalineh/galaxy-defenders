//
// World.cs
//

using System;
using System.Diagnostics;
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
        public CStage Stage { get; set; }
        public CScenery BackgroundScenery { get; set; }
        public CScenery ForegroundScenery { get; set; }
        public CCamera GameCamera { get; set; }
        public List<CShip> ShipEntitiesCache { get; set; }
        public List<CShip> Ships { get; set; }
        public CCollisionGrid CollisionGrid { get; set; }
        public CParticleEffectManager ParticleEffects { get; set; }
        public bool Paused { get; set; }
        public bool DebugPaused { get; set; }
        public bool WasStepped { get; set; }
        public bool StageEnd { get; set; }
        public int StageEndCounter { get; set; }
        public CFader StageEndFader { get; set; }
        public bool StageFinalEndExitFlag { get; set; }
        public List<string> StageEndText { get; set; }
        public List<string> StageEndAwardText { get; set; }
        public CStats Stats { get; set; }
        public CFader SecretEntryFader { get; set; }
        public string SecretStageName { get; set; }
        public Vector2 SecretEntryPosition { get; set; }
        public int SecretEntryCounter { get; set; }
        public CFader SecretFinishFader { get; set; }
        public int SecretFinishCounter { get; set; }
        public bool IgnoreSecrets { get; set; }
        public CWorld ReturnWorld { get; set; }
        public bool IsSecretWorld { get; set; }
        public float ScrollSpeed { get; set; }
        public Thread CollisionThread { get; set; }
        public Mutex CollisionMutex { get; set; }
        public bool CollisionThreadRun { get; set; }
        public volatile bool CollisionThreadTerminate;
        public CMenu PauseMenu { get; set; }
        public CMenu PauseMenuBase { get; set; }
        public CMenu PauseMenuQuitConfirm { get; set; }
        public CMenu GameOverMenu { get; set; }
        public COptionsMenu PauseMenuOptions { get; set; }
        public int GameOverCounter { get; set; }
        public CFader GameOverFader { get; set; }
        public Stopwatch UpdateStopwatch { get; set; }
        public Stopwatch DrawStopwatch { get; set; }

        public CWorld(CGalaxy game, CStageDefinition stage_definition)
        {
            Game = game;
            Random = new Random();
            Entities = new List<CEntity>();
            EntitiesToAdd = new List<CEntity>();
            EntitiesToDelete = new List<CEntity>();
            GameCamera = new CCamera(game);
            GameCamera.Position = Game.PlayerSpawnPosition.ToVector3();
            ShipEntitiesCache = new List<CShip>();
            Ships = new List<CShip>();
            CollisionGrid = new CCollisionGrid(this, new Vector2(1200.0f, 1200.0f), 10, 10);
            ParticleEffects = new CParticleEffectManager(this);
            ParticleEffects.Initialize(CGalaxyEffects.MakeDefinitions());
            StageEndText = new List<string>();
            StageEndAwardText = new List<string>();
            GameOverCounter = -1;
            UpdateStopwatch = new Stopwatch();
            DrawStopwatch = new Stopwatch();

            PauseMenuBase = new CMenu(Game)
            {
                Position = new Vector2(Game.Resolution.X / 2.0f - 128.0f, 400.0f),
                MenuOptions = new List<CMenu.CMenuOption>()
                {
                    new CMenu.CMenuOption() { Text = "Resume", Select = ResumeGame, CancelOption = true },
                    new CMenu.CMenuOption() { Text = "Options", Select = OptionsMenu },
                    new CMenu.CMenuOption() { Text = "Quit", Select = GotoQuitConfirm, PanelType = CMenu.PanelType.Small },
                },
            };

            PauseMenuQuitConfirm = new CMenu(Game)
            {
                Position = new Vector2(Game.Resolution.X / 2.0f - 128.0f, 400.0f),
                MenuOptions = new List<CMenu.CMenuOption>()
                {
                    // looks kind of crap
                    //new CMenu.CMenuOption() { Text = " * Quit To Menu? *", SelectValidate = (tag) => { return false; }, PanelType = CMenu.PanelType.None },
                    new CMenu.CMenuOption() { Text = "Cancel", Select = BackToMainPauseMenu, CancelOption = true },
                    new CMenu.CMenuOption() { Text = "Quit", Select = QuitGame, PanelType = CMenu.PanelType.Small },
                },
            };

            PauseMenuOptions = new COptionsMenu(Game)
            {
                Position = new Vector2(Game.Resolution.X / 2.0f - 128.0f, 400.0f),
                OnBack = LeaveOptionsMenu,
            };

            GameOverMenu = new CMenu(Game)
            {
                Position = new Vector2(Game.Resolution.X / 2.0f - 128.0f, 400.0f),
                MenuOptions = new List<CMenu.CMenuOption>()
                {
                    new CMenu.CMenuOption() { Text = "Retry!", Select = RetryGame },
                    new CMenu.CMenuOption() { Text = "Quit", Select = QuitGame, PanelType = CMenu.PanelType.Small },
                },
            };

            PauseMenu = PauseMenuBase;

            if (stage_definition != null)
            {
                Stage = new CStage(this, stage_definition);
            }

#if XBOX360
            CollisionMutex = new Mutex(false);
#else
            CollisionMutex = new Mutex(false, "CWorld.CollisionMutex");
#endif

            CollisionGrid.Initialize(Vector2.Zero);
        }

        ~CWorld()
        {
            if (CollisionThread != null)
                CollisionThread.Abort();    
        }

        // TODO: stage definition param
        public void Start()
        {
            CCollision.ClearCache();

            Entities = new List<CEntity>();

            ScrollSpeed = Stage.Definition.ScrollSpeed;
            Stage.Start();

            // TODO: should this be in the stage?
            //if (!Game.EditorMode)
                CAudio.PlayMusic(Stage.Definition.MusicName);

            MethodInfo bg_method = typeof(CSceneryPresets).GetMethod(Stage.Definition.BackgroundSceneryName);
            BackgroundScenery = bg_method.Invoke(null, new object[] { this }) as CScenery;
            MethodInfo fg_method = typeof(CSceneryPresets).GetMethod(Stage.Definition.ForegroundSceneryName);
            ForegroundScenery = fg_method.Invoke(null, new object[] { this }) as CScenery;

            Stats = new CStats();
            Stats.Initialize(this);

            CollisionGrid.Initialize(GameCamera.Position.ToVector2());

            ThreadStart collision_thread_start = new ThreadStart(CollectEntityCollisionsImpl);
            CollisionThread = new Thread(collision_thread_start);
            CollisionThread.Start();

            //ThreadStart particle_thread_start = new ThreadStart(ParticleUpdate);
            //ParticleUpdateThread = new Thread(particle_thread_start);
            //ParticleUpdateThread.Start();

            SProfileGameData profile = CSaveData.GetCurrentGameData(Game);
            int players = Game.PlayersInGame;
            for (int i = 0; i < players; ++i)
            {
                CShip ship = CShipFactory.GenerateShip(this, profile.Pilots[i], (GameControllerIndex)i);
                ship.Physics.Position = Game.PlayerSpawnPosition;
                if (players > 1)
                    ship.Physics.Position += new Vector2(-40.0f + 40.0f * i, 0.0f);
                Game.HudManager.Huds[i].Ship = ship;
                EntityAdd(ship);
                ShipEntitiesCache.Add(ship);
                Ships.Add(ship);
            }
        }

        public void Stop()
        {
            // allow threads to terminate naturally
            CollisionThreadTerminate = true;

            CAudio.StopPauseMusic();

            // clear runtime ship info from hud
            Game.HudManager.Huds[0].Ship = null;
            Game.HudManager.Huds[1].Ship = null;

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
            UpdateStopwatch.Reset();
            UpdateStopwatch.Start();

            if (!Game.IsActive && !Game.EditorMode && !IsGameOverState())
            {
                PauseGame();
            }

            if (Paused && !DebugPaused)
            {
                PauseMenu.Update();
                GameCamera.Update();
            }

            if (Paused)
            {
                // NOTE: hack! frame time should be on world?
                Game.GameFrame -= 1;
                UpdatePauseInput();
                return;
            }

            CheckGameOverConditions();

            Stage.Update();
            BackgroundScenery.Update();
            ForegroundScenery.Update();

            // DEBUG
#if DEBUG
            if (!Game.EditorMode && Game.Input.IsKeyDown(Keys.O))
            {
                GameCamera.Position += Vector3.UnitY * -32.0f;
            }
#endif

            if (!IsGameOverState())
            {
                GameCamera.Position += Vector3.UnitY * -ScrollSpeed;
                GameCamera.Update();
            }

            UpdatePauseInput();
            UpdateInput();

            // resolve previous frame collisions
            ResolveEntityCollisions();

            // delete entities removed as a result of collision
            ProcessEntityDelete();

            // particle effects
            ParticleEffects.Update();

            // kick new collision thread
            UpdateEntitiesThreadedCollision();

            UpdateStageEnd();
            UpdateSecretStageEntry();
            UpdateSecretStageFinish();

            if (WasStepped)
            {
                Paused = true;
                DebugPaused = true;
                WasStepped = false;
            }

            UpdateStopwatch.Stop();
        }

        public void UpdatePauseInput()
        {
            if (StageEnd)
                return;

            if (IsGameOverState())
            {
                if (Paused)
                    ResumeGame(null);

                return;
            }


            if (Game.Input.IsPadStartPressedAny() || Game.Input.IsKeyPressed(Keys.P) || Game.Input.IsKeyPressed(Keys.Escape))
            {
                DebugPaused = false;
                if (Paused)
                    ResumeGame(null);
                else
                    PauseGame();
            }

            if (Game.Input.IsKeyPressed(Keys.T))
            {
                Paused = false;
                DebugPaused = false;
                WasStepped = true;
            }
        }

        public void UpdateInput()
        {
        }

        public void UpdateEntities()
        {
            ProcessEntityAdd();
            ProcessEntityUpdates();
            CollectEntityCollisions();
            //ResolveEntityCollisions();
            ProcessEntityDelete();
        }

        public void UpdateEntitiesThreadedCollision()
        {
            ProcessEntityAdd();
            ProcessEntityUpdates();
            CollectEntityCollisions();
            //ProcessEntityDelete();
        }

        public void UpdateStageEnd()
        {
            if (StageEndCounter <= 0)
                return;

            // frame count of display operations
            const int StageClearShow = 60;
            const int StatsShow = 100;
            const int StatsInterval = 20;
            const int AwardShow = 240;
            const int AwardInterval = 20;
            const int AllowExit = 340;
            const int UpperClamp = AllowExit + 60;

            foreach (CShip ship in ShipEntitiesCache)
            {
                ship.Physics.Velocity += Vector2.UnitY * -0.1f * StageEndCounter;
                ParticleEffects.Spawn(EParticleType.PlayerStageEndShipTrail, ship.Physics.Position, ship.Visual.Color, null, null);
            }

            StageEndFader = StageEndFader ?? new CFader(Game) { TransitionTime = 2.0f };
            StageEndFader.Update();

            if (IsSecretWorld)
                return;

            if (StageEndCounter == StageClearShow)
            {
                StageEndText.Add("STAGE CLEAR");
                StageEndText.Add("");
                StageEndText.Add("");
            }

            if (StageEndCounter == StatsShow + StatsInterval * 0)
                StageEndText.Add(Stats.GetCoinsCollectedString());
            if (StageEndCounter == StatsShow + StatsInterval * 1)
                StageEndText.Add(Stats.GetEnemyKillsString());
            if (StageEndCounter == StatsShow + StatsInterval * 2)
                StageEndText.Add(Stats.GetBuildingKillsString());

            if (StageEndCounter == StatsShow + StatsInterval * 4)
            {
                StageEndText.Add("");
                StageEndText.Add(Stats.GetTotalPercentString());
            }

            if (StageEndCounter == 1)
                Stats.CheckAwards();

            for (int i = 0; i < Stats.GetAwardCount(); ++i)
            {
                if (StageEndCounter == AwardShow + AwardInterval * i)
                {
                    CStats.SAward award = Stats.GetAward(i);
                    StageEndAwardText.Add(String.Format("{0}: +{1}{2}", award.Text, award.Bonus, '￥'));

                    foreach (CShip ship in Ships)
                    {
                        ship.Score += award.Bonus;
                    }
                }
            }

            if (StageEndCounter > AllowExit)
            {
                if (Game.Input.IsPadConfirmPressedAny() || Game.Input.IsPadCancelPressedAny() || Game.Input.IsKeyPressed(Keys.Enter))
                {
                    StageFinalEndExitFlag = true;
                }
            }

            if (StageFinalEndExitFlag)
            {
                StageEndFader.StopAtFullFadeOut();
                if (StageEndCounter > UpperClamp)
                {
                    SaveAndExit();
                    GotoLobby();
                }
            }
            else
            {
                StageEndCounter = Math.Min(StageEndCounter, UpperClamp);
                StageEndFader.StopAtHalfFadeOut();
            }

            StageEndCounter += 1;
        }

        public void UpdateSecretStageEntry()
        {
            if (SecretEntryCounter == 0)
                return;

            StageEnd = true;

            foreach (CShip ship in ShipEntitiesCache)
            {
                ship.Physics.Velocity += Vector2.UnitY * -0.1f * StageEndCounter;
                ParticleEffects.Spawn(EParticleType.PlayerStageEndShipTrail, ship.Physics.Position, ship.Visual.Color, null, null);
            }


            SecretEntryFader = SecretEntryFader ?? new CFader(Game) { TransitionTime = 2.0f };
            SecretEntryFader.Update();

            if (SecretEntryCounter < 60)
            {
                SecretEntryFader.StopAtHalfFadeOut();
            }
            else
            {
                SecretEntryFader.StopAtFullFadeOut();
            }

            foreach (CShip ship in ShipEntitiesCache)
            {
                SecretEntryPosition *= new Vector2(1.01f, 1.0f);
                Vector2 ofs = SecretEntryPosition - ship.Physics.Position;
                ship.Physics.Position += ofs * 0.25f;
                ship.IsInvincible += 1;
            }

            if (SecretEntryCounter > 120)
            {
                IgnoreSecrets = true;

                SaveAndExit();

                CStageDefinition definition = CStageDefinition.GetStageDefinitionByName(SecretStageName);

                CStateGame bonus_stage = new CStateGame(Game, definition);
                bonus_stage.World.ReturnWorld = this;
                bonus_stage.World.IsSecretWorld = true;

                CStateFadeTo fader = new CStateFadeTo(Game, Game.State, bonus_stage);
                fader.NoExitSource = true;
                Game.State = fader;
                
                return;
            }

            SecretEntryCounter += 1;
        }

        public void UpdateSecretStageFinish()
        {
            // TODO: less shit
            if (IsSecretWorld)
            {
                // TODO: faster implementation
                if (ShipEntitiesCache.Count == 0)
                {
                    SecretFinishCounter = Math.Max(SecretFinishCounter, 1);
                }
            }

            if (SecretFinishCounter == 0)
                return;

            SecretFinishFader = SecretFinishFader ?? new CFader(Game) { TransitionTime = 2.0f };
            SecretFinishFader.Update();

            if (SecretFinishCounter < 60)
            {
                SecretFinishFader.StopAtHalfFadeOut();
            }
            else
            {
                SecretFinishFader.StopAtFullFadeOut();
            }

            if (SecretFinishCounter > 120)
            {
                CStateGame return_state = new CStateGame(Game, ReturnWorld);
                Game.State = new CStateFadeTo(Game, Game.State, return_state);
            }

            SecretFinishCounter += 1;
        }

        public void UpdateScissorRectangle()
        {
            // NOTE: no side panels in editor mode
            if (Game.EditorMode)
                return;

            Game.GraphicsDevice.RenderState.ScissorTestEnable = true;

            //
            // NOTE: GameCamera.ScreenSize is the game area screensize (between the two hud elements)
            //       GraphicsDevice.Viewport is the actual screen area
            //

            // Center on the real viewport, and then create a scissor rectangle of the scaled game screen size.
            Viewport vp = Game.GraphicsDevice.Viewport;
            Vector2 size = new Vector2(vp.Width / 2.0f, vp.Height); // TODO: get vp scaled by screensize difference!
            Vector2 center = new Vector2(vp.Width / 2.0f, vp.Height / 2.0f);
            Vector2 screen_size = size * Game.UserScaleValue;

            float half_width = screen_size.X / 2.0f;
            float half_height = screen_size.Y / 2.0f;
            float x = center.X - half_width;
            float y = center.Y - half_height;

            //
            // NOTE: The 360 can break with scissor rectangles outside the screen!
            //
            Game.GraphicsDevice.ScissorRectangle = new Rectangle(
                (int)(x),
                (int)(y),
                (int)(screen_size.X),
                (int)(screen_size.Y)
            );
        }

        public void Draw()
        {
            DrawStopwatch.Reset();
            DrawStopwatch.Start();

            UpdateScissorRectangle();

            DrawBackground(GameCamera);
            DrawEntities(GameCamera);
            DrawForeground(GameCamera);

            // 27ms (sorted), 7ms (immediate)
            Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, GameCamera.WorldMatrix);
            ParticleEffects.Draw(Game.DefaultSpriteBatch);
            Game.DefaultSpriteBatch.End();

            if (GameOverFader != null)
            {
                Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, Game.RenderScaleMatrix);
                GameOverFader.Draw(Game.DefaultSpriteBatch);
                GameOverMenu.Draw(Game.DefaultSpriteBatch);
                Game.DefaultSpriteBatch.End();
            }

            if (StageEndFader != null)
            {
                Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, Game.RenderScaleMatrix);
                StageEndFader.Draw(Game.DefaultSpriteBatch);
                Game.DefaultSpriteBatch.End();
            }

            if (SecretEntryFader != null)
            {
                Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, Game.RenderScaleMatrix);
                SecretEntryFader.Draw(Game.DefaultSpriteBatch);
                Game.DefaultSpriteBatch.End();
            }

            if (SecretFinishFader != null)
            {
                Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, Game.RenderScaleMatrix);
                SecretFinishFader.Draw(Game.DefaultSpriteBatch);
                Game.DefaultSpriteBatch.End();
            }

            if (SecretEntryCounter > 20 && SecretEntryCounter < 120)
            {
                Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, Game.RenderScaleMatrix);
                Vector2 text_position = new Vector2(Game.Resolution.X / 2.0f - 100.0f, 150.0f);
                Game.DefaultSpriteBatch.DrawStringAlignCenter(Game.DefaultFont, text_position, "SECRET WARP", Color.LightGreen);
                Game.DefaultSpriteBatch.End();
            }

            if (StageEndText.Count > 0)
            {
                Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, Game.RenderScaleMatrix);
                
                Vector2 text_position = new Vector2(Game.Resolution.X / 2.0f - 75.0f, 150.0f);

                Game.DefaultSpriteBatch.Draw(Game.PixelTexture, new Rectangle((int)text_position.X - 140, (int)text_position.Y - 35, 450, 570), null, new Color(76, 76, 76, 192), 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
                Game.DefaultSpriteBatch.Draw(Game.PixelTexture, new Rectangle((int)text_position.X - 130, (int)text_position.Y - 25, 430, 550), null, new Color(30, 30, 30, 128), 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);

                Vector2 first_offset = new Vector2(40.0f, 0.0f);
                foreach (string text in StageEndText)
                {
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, text, text_position, Color.White);
                    text_position += Vector2.UnitY * 30.0f;
                    text_position -= first_offset;
                    first_offset = Vector2.Zero;
                }

                Vector2 award_position = new Vector2(Game.Resolution.X / 2.0f - 140.0f, 450.0f);
                foreach (string text in StageEndAwardText)
                {
                    Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, text, award_position, Color.LightGreen);
                    award_position += Vector2.UnitY * 30.0f;
                }
                Game.DefaultSpriteBatch.End();
            }

#if DEBUG
            if (CInput.IsRawKeyDown(Keys.Q))
            {
                //Console.WriteLine("Total Children: " + QuadTree.Root.CountTotalChildren().ToString());
                CollisionGrid.Draw(GameCamera.WorldMatrix, Color.White);
            }
#endif

            if (Paused)
            {
                if (!DebugPaused)
                {
                    Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, Game.RenderScaleMatrix);
                    Vector2 pause_text_position = new Vector2(Game.Resolution.X / 2.0f - 100.0f, Game.Resolution.Y / 2.0f - 250.0f);
                    Game.DefaultSpriteBatch.Draw(Game.PixelTexture, new Rectangle(0, 0, (int)Game.Resolution.X, (int)Game.Resolution.Y), new Color(0, 0, 0, 92));
                    PauseMenu.Draw(Game.DefaultSpriteBatch);
                    Game.DefaultSpriteBatch.End();
                }
            }

            // simple profiling
            //Game.DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, GameCamera.WorldMatrix);
            //Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, String.Format("update: {0}ms", UpdateStopwatch.ElapsedMilliseconds), new Vector2(100.0f, 0.0f) + GameCamera.GetCenter().ToVector2(), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            //Game.DefaultSpriteBatch.DrawString(Game.DefaultFont, String.Format("render: {0}ms", DrawStopwatch.ElapsedMilliseconds), new Vector2(100.0f, 32.0f) + GameCamera.GetCenter().ToVector2(), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            //Game.DefaultSpriteBatch.End();
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
                CollisionCircle collide = CCollision.GetCacheCircle(this, entity.Physics.Position, entity.GetRadius());
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

                CCollision collision = entity.Collision ?? CCollision.GetCacheCircle(this, entity.Physics.Position, entity.GetRadius());
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

                CCollision collision = entity.Collision ?? CCollision.GetCacheCircle(entity, entity.Physics.Position, entity.GetRadius());
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
            foreach (CShip ship in ShipEntitiesCache)
            {
                // died
                if (ship.Physics == null)
                    continue;

                if (ship.IsDead)
                    continue;

                Vector2 ship_position = ship.Physics.Position;
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
            foreach (CShip ship in ShipEntitiesCache)
            {
                // died
                if (ship.Physics == null)
                    continue;

                Vector2 ship_position = ship.Physics.Position;
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
            if (ShipEntitiesCache.Count == 0)
                return null;

            return ShipEntitiesCache[0];
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
                Vector2 enemy_position = enemy.Physics.Position;
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

                Vector2 enemy_position = enemy.Physics.Position;
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

        private void CollectEntityCollisions()
        {
            lock (CollisionMutex)
            {
                CollisionThreadRun = true;
            }
        }

        private void CollectEntityCollisionsImpl()
        {
#if XBOX360
            int[] threads = new int[] { 4 };
            CollisionThread.SetProcessorAffinity(threads);
#endif

            while (!CollisionThreadTerminate)
            {
                while (!CollisionThreadRun)
                {
                    if (CollisionThreadTerminate)
                        return;

                    Thread.Sleep(0);
                }

                lock (CollisionMutex)
                {
                    CollisionGrid.Clear(GameCamera.Position.ToVector2());
                    CollisionGrid.Insert(Entities);
                    CollisionGrid.CollectCollisions();
                    CollisionThreadRun = false;
                }
            }
        }

        private void ResolveEntityCollisions()
        {
            lock (CollisionMutex)
            {
                CollisionGrid.ResolveCollisions();
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

        private bool IsGameOverState()
        {
            return GameOverCounter != -1;    
        }

        private void CheckGameOverConditions()
        {
            if (ShipEntitiesCache.Count == 0)
            {
                if (GameOverCounter == -1)
                {
                    GameOverCounter = 120;
                    GameOverFader = new CFader(Game);
                    CAudio.PlayMusic("Try_Harder");
                }

                GameOverCounter = Math.Max(0, GameOverCounter - 1);
                GameOverMenu.Visible = GameOverCounter == 0;
            }

            if (GameOverFader != null)
            {
                GameOverMenu.Update();
                GameOverFader.Update();
                GameOverFader.StopAtHalfFadeOut();
            }
        }

        private void SaveAndExit()
        {
            // TODO: is this a good place to add score to money?
            // TODO: 2p
            // TODO: secret stage
            SProfile profile = CSaveData.GetCurrentProfile();
            int players_index = Game.PlayersInGame - 1;

            foreach (CShip ship in Ships)
            {
                profile.Game[players_index].Pilots[(int)ship.GameControllerIndex].Money += ship.Score;
                
                // NOTE: this is just to support mid-game upgrades if we add powerup items
                profile.Game[players_index].Pilots[(int)ship.GameControllerIndex].WeaponPrimaryType = ship.PrimaryWeapon.Type;
                profile.Game[players_index].Pilots[(int)ship.GameControllerIndex].WeaponPrimaryLevel = ship.PrimaryWeapon.Level;
                profile.Game[players_index].Pilots[(int)ship.GameControllerIndex].WeaponSecondaryType = ship.SecondaryWeapon.Type;
                profile.Game[players_index].Pilots[(int)ship.GameControllerIndex].WeaponSecondaryLevel = ship.SecondaryWeapon.Level;
                profile.Game[players_index].Pilots[(int)ship.GameControllerIndex].WeaponSidekickLeftType = ship.SidekickLeft.Type;
                profile.Game[players_index].Pilots[(int)ship.GameControllerIndex].WeaponSidekickLeftLevel = ship.SidekickLeft.Level;
                profile.Game[players_index].Pilots[(int)ship.GameControllerIndex].WeaponSidekickRightType = ship.SidekickRight.Type;
                profile.Game[players_index].Pilots[(int)ship.GameControllerIndex].WeaponSidekickRightLevel = ship.SidekickRight.Level;
                profile.Game[players_index].Pilots[(int)ship.GameControllerIndex].WeaponSecondaryLevel = ship.SecondaryWeapon.Level;
            }

            profile.Game[players_index].Stage = Stage.Definition.Name;

            CSaveData.SetCurrentProfileData(profile);
            CSaveData.SaveRequest();
        }

        private void GotoLobby()
        {
            Game.State = new CStateFadeTo(Game, Game.State, new CStateShop(Game));
        }

        public void StartStageEnd()
        {
            StageEndCounter = 1;
        }

        public void StartSecretStageEntry(string stage, Vector2 position)
        {
            if (IgnoreSecrets)
                return;

            IgnoreSecrets = true;
            SecretStageName = stage;
            SecretEntryPosition = position;
            SecretEntryCounter = 1;
        }

        public void StartSecretStageFinish()
        {
            SecretFinishCounter = 1;
        }

        public void ReturnFromSecret()
        {
            SecretEntryCounter = 0;
            SecretEntryFader = null;
            StageEnd = false;

            //if (!Game.EditorMode)
                CAudio.PlayMusic(Stage.Definition.MusicName);

            foreach (CShip ship in ShipEntitiesCache)
            {
                Vector2 to_center = GameCamera.GetCenter().ToVector2() - ship.Physics.Position;
                Vector2 clamped_entry = GameCamera.ClampInside(SecretEntryPosition, 32.0f);
                ship.Physics.Position = clamped_entry;
                ship.Physics.Velocity = to_center.Normal() * 40.0f;
            }
        }

        public void PauseGame()
        {
            if (Paused)
                return;
            Paused = true;
            CAudio.PlayPauseMusic("1-X-0");
        }

        public void ResumeGame(object tag)
        {
            Paused = false;
            CAudio.StopPauseMusic();
        }

        public void RetryGame(object tag)
        {
            CStageDefinition definition = CStageDefinition.GetStageDefinitionByName(Stage.Definition.Name);
            Game.State = new CStateFadeTo(Game, Game.State, new CStateGame(Game, definition));
        }

        public void GotoQuitConfirm(object tag)
        {
            PauseMenu = PauseMenuQuitConfirm;
        }

        public void QuitGame(object tag)
        {
            Game.State = new CStateFadeTo(Game, Game.State, new CStateShop(Game));
        }

        public void OptionsMenu(object tag)
        {
            PauseMenu = PauseMenuOptions;
            PauseMenuOptions.RefreshVolumes();
        }

        public void LeaveOptionsMenu()
        {
            PauseMenu = PauseMenuBase;
        }

        public void BackToMainPauseMenu(object tag)
        {
            PauseMenu = PauseMenuBase;
        }

        public void DestroyAllProjectiles()
        {
            Type[] types = new Type[]
            {
                typeof(CProjectile), 
                typeof(CEnemyCannonShot),
                typeof(CEnemyLaser),
                typeof(CEnemyMissile),
                typeof(CEnemyPellet),
                typeof(CEnemyShot),
                typeof(CBonus),
            };

            foreach (CEntity entity in Entities)
            {
                foreach (Type type in types)
                {
                    if (entity.GetType().IsSubclassOf(type) || entity.GetType() == type)
                    {
                        ParticleEffects.Spawn(EParticleType.ObjectDestroyed, entity.Physics.Position);
                        entity.Die();
                    }
                }
            }
        }
    }
}
