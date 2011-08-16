using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Threading;
using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

namespace Galaxy
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class CGalaxy
        : Microsoft.Xna.Framework.Game
    {
        public const int MusicDisplayTime = 60 * 7;

        public static bool ApplicationFocusedFlag { get; set; }

        public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }
        public new GraphicsDevice GraphicsDevice { get; private set; }
        public SpriteBatch DefaultSpriteBatch { get; private set; }
        public SpriteFont DebugFont { get; private set; }
        public SpriteFont GameLargeFont { get; private set; }
        public SpriteFont GameRegularFont { get; private set; }
        public CDebug Debug { get; private set; }
        public CInput Input { get; private set; }
        public Texture2D PixelTexture { get; private set; }
        public CFrameRateDisplay FrameRateDisplay { get; private set; }
        public int GameFrame { get; set; }
        public CState State { get; set; }
        public Vector2 PlayerSpawnPosition { get; set; }
        public bool EditorMode { get; set; }
        public CHudManager HudManager { get; set; }
        public int PlayersInGame { get; set; }
        public CVisual SaveIcon { get; set; }
        public CVisual MusicIcon { get; set; }
        public int MusicDisplayCounter { get; set; }
        public string MusicDisplayName { get; set; }
        public Vector2 Resolution { get; set; }
        public Matrix RenderScaleMatrix { get; set; }
        public float UserScaleValue { get; set; }
        public Stopwatch UpdateStopwatch { get; set; }
        public Stopwatch DrawStopwatch { get; set; }
        public static float GlobalScale { get; set; }

        public CGalaxy()
        {
#if XBOX360
            // NOTE: still clamps at 60fps somehow, just doesnt do catchup code which makes fps even worse
            this.IsFixedTimeStep = false;
#endif
            UpdateStopwatch = new Stopwatch();
            DrawStopwatch = new Stopwatch();

            Content.RootDirectory = "Content";
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            GraphicsDevice = GraphicsDeviceManager.GraphicsDevice;

            // Game resolution.
            Resolution = new Vector2(1920.0f, 1080.0f);

            // User scale value.
            UserScaleValue = 1.0f;

            // Backbuffer resolution configuration.
            GraphicsDeviceManager.PreferredBackBufferWidth = 1920;
            GraphicsDeviceManager.PreferredBackBufferHeight = 1080;

            //
            // NOTE: on the 360, a SDTV resolution of 480i will crash on a requested 1920x1080 buffer
            //       so we need to handle it specially. the 360 will autoscale a 1280x720 buffer to
            //       any target resolution, so for any SD resolution we will use a buffer of that size
            //       and let the 360 handle it
            //
            if (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height < 720)
            {
                GraphicsDeviceManager.PreferredBackBufferWidth = 1280;
                GraphicsDeviceManager.PreferredBackBufferHeight = 720;
            }

#if !XBOX360

            this.Activated += new EventHandler(NotifyActivated);
            this.Deactivated += new EventHandler(NotifyDeactivated);

#if DEBUG
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += new EventHandler(Window_ClientSizeChanged);
#endif
            // NOTE: for PC we will just scale to fit as much of the screen as possible
            if (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height < 1080)
            {
                int width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 8;
                int height = (int)(width * (1080.0f / 1920.0f));
                GraphicsDeviceManager.PreferredBackBufferWidth = width;
                GraphicsDeviceManager.PreferredBackBufferHeight = height;
            }
#endif
            
            //
            // NOTE: this is the correct way to check widescreen, not using aspect ratio
            //
            //if (GraphicsAdapter.DefaultAdapter.IsWideScreen)
            //{
            //}

            GraphicsDeviceManager.PreferMultiSampling = false;
            GraphicsDeviceManager.ApplyChanges();

#if XBOX360
            Components.Add(new GamerServicesComponent(this));
#endif

            Debug = new CDebug(this);
            Input = new CInput(this);

            GameFrame = 0;
            PlayersInGame = 1;

            PlayerSpawnPosition = new Vector2(0.0f, 400.0f);
        }

        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            if (State != null)
                State.OnExit();

            CAudio.Shutdown();
            CSaveData.StopSaveThread();

            Initialize(); 
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            if (State != null)
                State.OnExit();

            CAudio.Shutdown();
            CSaveData.StopSaveThread();

            base.OnExiting(sender, args);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Presentation Parameters
            //public DepthFormat AutoDepthStencilFormat { get; set; }
            //public int BackBufferCount { get; set; }
            //public SurfaceFormat BackBufferFormat { get; set; }
            //public int BackBufferHeight { get; set; }
            //public int BackBufferWidth { get; set; }
            //public bool EnableAutoDepthStencil { get; set; }
            //public int FullScreenRefreshRateInHz { get; set; }
            //public bool IsFullScreen { get; set; }
            //public int MultiSampleQuality { get; set; }
            //public MultiSampleType MultiSampleType { get; set; }
            //public PresentInterval PresentationInterval { get; set; }
            //public PresentOptions PresentOptions { get; set; }
            //public RenderTargetUsage RenderTargetUsage { get; set; }
            //public SwapEffect SwapEffect { get; set; }

            GraphicsDevice = GraphicsDeviceManager.GraphicsDevice;

            GraphicsDevice.PresentationParameters.AutoDepthStencilFormat = DepthFormat.Unknown;
            GraphicsDevice.PresentationParameters.BackBufferCount = 1;
            GraphicsDevice.PresentationParameters.BackBufferFormat = SurfaceFormat.Rgba32;
            GraphicsDevice.PresentationParameters.BackBufferWidth = 1920;
            GraphicsDevice.PresentationParameters.BackBufferHeight = 1080;
            GraphicsDevice.PresentationParameters.EnableAutoDepthStencil = false;
            GraphicsDevice.PresentationParameters.FullScreenRefreshRateInHz = 60;
            GraphicsDevice.PresentationParameters.IsFullScreen = false;
            GraphicsDevice.PresentationParameters.MultiSampleQuality = 0;
            GraphicsDevice.PresentationParameters.MultiSampleType = MultiSampleType.FourSamples;
            GraphicsDevice.PresentationParameters.PresentationInterval = PresentInterval.Immediate;
            GraphicsDevice.PresentationParameters.PresentOptions = PresentOptions.DiscardDepthStencil;
            GraphicsDevice.PresentationParameters.RenderTargetUsage = RenderTargetUsage.DiscardContents;
            GraphicsDevice.PresentationParameters.SwapEffect = SwapEffect.Discard;

            GraphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
            GraphicsDeviceManager.ApplyChanges();

            base.Initialize();
        }

        public void StorageCallback()
        {
            
        }

        public void SetUserScaleValue(float scale)
        {
            // NOTE: just to handle old save data
            if (scale == 0.0f)
                scale = 1.0f;

            // NOTE: we cannot go larger than 1.0f, because it will break in 1080p (see http://forums.create.msdn.com/forums/p/22319/119886.aspx)
            //scale = MathHelper.Clamp(scale, 0.875f, 1.0f);

            UserScaleValue = scale; 
            float width = GraphicsDevice.PresentationParameters.BackBufferWidth;
            float height = GraphicsDevice.PresentationParameters.BackBufferHeight;
            Vector3 scale_factor = new Vector3(width / Resolution.X * scale, height / Resolution.Y * scale, 1.0f);
            Matrix scale_matrix = Matrix.CreateScale(scale_factor);

            if (EditorMode)
                scale_matrix = Matrix.CreateTranslation(new Vector3(width / -2.0f, 0.0f, 0.0f));

            if (GraphicsDevice != null)
            {
                GraphicsDevice.RenderState.ScissorTestEnable = false;
                GraphicsDevice.Clear(Color.Black);
                GraphicsDevice.RenderState.ScissorTestEnable = true;
            }

            Vector3 translation = new Vector3(width - Resolution.X * scale_factor.X, height - Resolution.Y * scale_factor.Y, 0.0f);
            Matrix translation_matrix = Matrix.CreateTranslation(translation * 0.5f);

            RenderScaleMatrix = scale_matrix * translation_matrix;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Graphics device does not exist during Initialize().
            DefaultSpriteBatch = new SpriteBatch(GraphicsDevice);
            DebugFont = Content.Load<SpriteFont>("Fonts/Debug");
            GameLargeFont = Content.Load<SpriteFont>("Fonts/GameLarge");
            GameRegularFont = Content.Load<SpriteFont>("Fonts/GameRegular");
            PixelTexture = Content.Load<Texture2D>("Textures/Top/Pixel");

            // Audio.
            CAudio.Initialize();

#if XBOX360
            SignedInGamer.SignedOut += new EventHandler<SignedOutEventArgs>(OnGamerSignOut);
            GuideUtil.Game = this;
            GuideUtil.Start();
            SetUserScaleValue(1.0f);
#else
            CSaveData.Load();

            SProfileOptionsData options = CSaveData.GetCurrentProfile().Options;
            CAudio.SetSFXVolume(options.SFXVolume);
            CAudio.SetMusicVolume(options.MusicVolume);
            SetUserScaleValue(options.UserScale);

            GuideUtil.StorageDeviceReady = true;
#endif

            // Save thread.
            CSaveData.StartSaveThread();

            // Frame rate display.
            FrameRateDisplay = new CFrameRateDisplay(this);

            // Menu textures.
            CMenu.LoadMenuTextures(this);

            // Save icon
            SaveIcon = CVisual.MakeSpriteUncached(this, "Textures/UI/SaveIcon");

            // Music icon.
            MusicIcon = CVisual.MakeSpriteUncached(this, "Textures/UI/MusicIcon");
            MusicIcon.NormalizedOrigin = new Vector2(0.0f, 0.0f);
            MusicIcon.Color = Color.LightGray;
            MusicIcon.Update();
            CAudio.OnMusicChange += DisplayTrackChange;

            // Hud management.
            HudManager = new CHudManager(this);

            // Enter our default state now that assets are ready.
            State = new CStateMainMenu(this);

            // debug shop testing
            //PlayersInGame = 2;
            //State = new CStateShop(this);

            HudManager.LockHuds();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            if (State != null)
                State.OnExit();

            CAudio.Shutdown();
            CSaveData.StopSaveThread();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime game_time)
        {
#if DEBUG
            GlobalScale = CInput.IsRawKeyDown(Microsoft.Xna.Framework.Input.Keys.S) ? 1.30f : 1.0f;
#else
            GlobalScale = 1.0f;
#endif



            UpdateStopwatch.Reset();
            UpdateStopwatch.Start();

            Input.Update();

            State.Update();
            HudManager.Update();

            CAudio.Update();
            FrameRateDisplay.Update(game_time);

            base.Update(game_time);

            GameFrame += 1;

            UpdateStopwatch.Stop();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime game_time)
        {
            DrawStopwatch.Reset();
            DrawStopwatch.Start();

            // TODO: use for custom frame timing
            // wait till vsync
            //while (!GraphicsDevice.RasterStatus.InVerticalBlank)
                //Thread.Sleep(0);

            base.Draw(game_time);

            GraphicsDevice.RenderState.ScissorTestEnable = false;
            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.RenderState.ScissorTestEnable = true;

            State.Draw();

            GraphicsDevice.RenderState.ScissorTestEnable = false;
            HudManager.Draw();
            State.PostHudDraw();
            FrameRateDisplay.Draw(DefaultSpriteBatch);
            GraphicsDevice.RenderState.ScissorTestEnable = true;

            if (CSaveData.SaveIconVisible)
            {
                DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, RenderScaleMatrix);
                float step = 1.0f / 8.0f;
                float rotation = step * ((GameFrame / 4) % 8);
                // title safe area sucks :(
                Vector2 position = new Vector2(520.0f, GraphicsDevice.Viewport.TitleSafeArea.Top + 38.0f);
                //Vector2 position = new Vector2(520.0f, GraphicsDevice.Viewport.Top + 38.0f);
                SaveIcon.Draw(DefaultSpriteBatch, position, rotation * MathHelper.TwoPi);
                DefaultSpriteBatch.End();
            }

            if (MusicDisplayCounter > 0)
            {
                MusicDisplayCounter -= 1;
                // NOTE: this is non-critical text, so not displaying on 480p wont be a fail
                Vector2 position = new Vector2(476.0f, 1080.0f - 1080.0f * 0.07f);
                DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, RenderScaleMatrix);
                float alpha = Math.Min(1.0f, MusicDisplayCounter > MusicDisplayTime ? 1.0f - (MusicDisplayCounter - MusicDisplayTime) / 60.0f : MusicDisplayCounter / 60.0f);
                MusicIcon.Alpha = alpha;
                MusicIcon.Draw(DefaultSpriteBatch, position + new Vector2(8.0f, 8.0f), 0.0f);
                DefaultSpriteBatch.DrawString(GameRegularFont, MusicDisplayName, position + new Vector2(42.0f, 8.0f), new Color(Color.LightGray, alpha));
                DefaultSpriteBatch.End();
            }

            CDebugRender.Render(this);

            //while (GraphicsDevice.RasterStatus.InVerticalBlank)
                //Thread.Sleep(0);

            DrawStopwatch.Start();

            // simple profiling
            // TODO: why doesnt depth work here :( HUD always renders on top
            DefaultSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None, Matrix.Identity);
            DefaultSpriteBatch.DrawString(GameRegularFont, String.Format("update: {0}ms", UpdateStopwatch.ElapsedMilliseconds), new Vector2(500.0f, 30.0f), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            DefaultSpriteBatch.DrawString(GameRegularFont, String.Format("render: {0}ms", DrawStopwatch.ElapsedMilliseconds), new Vector2(500.0f, 60.0f), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            DefaultSpriteBatch.DrawString(GameRegularFont, String.Format("memory: {0:r2}kb", (float)(GC.GetTotalMemory(false) / 1024)), new Vector2(500.0f, 90.0f), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);

#if !XBOX360
            // NOTE: no GC.CollectionCount on 360
            //DefaultSpriteBatch.DrawString(GameRegularFont, String.Format("collects(0): {0}", (int)(GC.CollectionCount(0))), new Vector2(500.0f, 120.0f), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            //DefaultSpriteBatch.DrawString(GameRegularFont, String.Format("collects(1): {0}", (int)(GC.CollectionCount(1))), new Vector2(500.0f, 150.0f), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
#endif

            DefaultSpriteBatch.End();
        }

        public Vector2 TryGetCameraTopLeft()
        {
            CCamera camera = TryGetGameCamera();
            if (camera == null)
                return Vector2.Zero;

            return camera.GetTopLeft();
        }

        public Vector2 TryGetCameraBottomRight()
        {
            CCamera camera = TryGetGameCamera();
            if (camera == null)
                return Vector2.Zero;
            
            return camera.GetBottomRight();
        }

        private CCamera TryGetGameCamera()
        {
            if (State == null)
                return null;

            if (State.GetType() == typeof(CStateGame))
                return (State as CStateGame).World.GameCamera;

            return null;
        }

        private void OnGamerSignOut(object sender, SignedOutEventArgs args)
        {
            HudManager = new CHudManager(this);
            State = new CStateMainMenu(this);
            GC.Collect();
        }

        private void DisplayTrackChange(string music_name)
        {
            MusicDisplayCounter = MusicDisplayTime;
            MusicDisplayName = "RushJet1 - " + music_name;
        }

        private void NotifyActivated(object sender, EventArgs e)
        {
            ApplicationFocusedFlag = true;
        }

        private void NotifyDeactivated(object sender, EventArgs e)
        {
            ApplicationFocusedFlag = false;
        }
    }
}
