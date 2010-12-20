using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Threading;
using System;
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
        public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }
        public new GraphicsDevice GraphicsDevice { get; private set; }
        public SpriteBatch DefaultSpriteBatch { get; private set; }
        public SpriteFont DefaultFont { get; private set; }
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

        public CGalaxy()
        {
#if XBOX360
            // NOTE: still clamps at 60fps somehow, just doesnt do catchup code which makes fps even worse
            this.IsFixedTimeStep = false;
#endif

            Content.RootDirectory = "Content";
            GraphicsDeviceManager = new GraphicsDeviceManager(this);

            // TODO: this needs to be done so we have a valid device by the time we get to Initialize()
#if XBOX360
            GraphicsDeviceManager.PreferredBackBufferWidth = 1920;
            GraphicsDeviceManager.PreferredBackBufferHeight = 1080;
#else
            GraphicsDeviceManager.PreferredBackBufferWidth = 1920;
            GraphicsDeviceManager.PreferredBackBufferHeight = 1080;
#endif

            GraphicsDeviceManager.PreferMultiSampling = false;
            GraphicsDeviceManager.ApplyChanges();

#if XBOX360
            Components.Add(new GamerServicesComponent(this));
#endif

            Debug = new CDebug(this);
            Input = new CInput(this);

            GameFrame = 0;
            PlayersInGame = 1;

            // default
            GraphicsDevice = GraphicsDeviceManager.GraphicsDevice;

            // TODO: resolution
            PlayerSpawnPosition = new Vector2(0.0f, 400.0f);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            if (State != null)
                State.OnExit();

            CAudio.StopMusic();
            CSaveData.StopSaveThread();

            base.OnExiting(sender, args);
        }

        public void SwitchGraphicsDevice(GraphicsDevice graphics_device)
        {
            GraphicsDevice.Dispose();
            GraphicsDevice = graphics_device;
            LoadContent();
            //Initialize();
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
            GraphicsDevice.PresentationParameters.BackBufferHeight = 1920;
            GraphicsDevice.PresentationParameters.BackBufferWidth = 1080;
            GraphicsDevice.PresentationParameters.EnableAutoDepthStencil = false;
            GraphicsDevice.PresentationParameters.FullScreenRefreshRateInHz = 60;
            GraphicsDevice.PresentationParameters.IsFullScreen = false;
            GraphicsDevice.PresentationParameters.MultiSampleQuality = 1;
            GraphicsDevice.PresentationParameters.MultiSampleType = MultiSampleType.NonMaskable;
            GraphicsDevice.PresentationParameters.PresentationInterval = PresentInterval.One;
            GraphicsDevice.PresentationParameters.PresentOptions = PresentOptions.DiscardDepthStencil;
            GraphicsDevice.PresentationParameters.RenderTargetUsage = RenderTargetUsage.DiscardContents;
            GraphicsDevice.PresentationParameters.SwapEffect = SwapEffect.Discard;

            GraphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
            GraphicsDeviceManager.ApplyChanges();

            CAudio.Initialize();

            base.Initialize();
        }

        public void StorageCallback()
        {
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Graphics device does not exist during Initialize().
            DefaultSpriteBatch = new SpriteBatch(GraphicsDevice);
            DefaultFont = Content.Load<SpriteFont>("Fonts/DefaultFont");
            PixelTexture = Content.Load<Texture2D>("Textures/Top/Pixel");

#if XBOX360
            SignedInGamer.SignedOut += new EventHandler<SignedOutEventArgs>(OnGamerSignOut);
            GuideUtil.Game = this;
            GuideUtil.Start();
#else
            CSaveData.Load();

            SProfileOptionsData options = CSaveData.GetCurrentProfile().Options;
            CAudio.SetSFXVolume(options.SFXVolume);
            CAudio.SetMusicVolume(options.MusicVolume);

            GuideUtil.StorageDeviceReady = true;
#endif

            // Save thread.
            CSaveData.StartSaveThread();

            // Frame rate display.
            FrameRateDisplay = new CFrameRateDisplay(this);

            // Menu textures.
            CMenu.LoadMenuTextures(this);

            // Save Icon
            SaveIcon = CVisual.MakeSpriteUncached(this, "Textures/UI/SaveIcon");

            // Hud management.
            HudManager = new CHudManager(this);

            // Enter our default state now that assets are ready.
            State = new CStateMainMenu(this);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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
            Input.Update();

            State.Update();
            HudManager.Update();

            CAudio.Update();
            FrameRateDisplay.Update(game_time);

            base.Update(game_time);

            GameFrame += 1;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime game_time)
        {
            // TODO: use for custom frame timing
            // wait till vsync
            //while (!GraphicsDevice.RasterStatus.InVerticalBlank)
                //Thread.Sleep(0);

            base.Draw(game_time);
            State.Draw();

            HudManager.Draw();

            FrameRateDisplay.Draw(DefaultSpriteBatch);

            if (CSaveData.SaveIconVisible)
            {
                DefaultSpriteBatch.Begin();
                float step = 1.0f / 8.0f;
                float rotation = step * ((GameFrame / 4) % 8);
                // title safe area sucks :(
                Vector2 position = new Vector2(520.0f, GraphicsDevice.Viewport.TitleSafeArea.Top + 38.0f);
                //Vector2 position = new Vector2(520.0f, GraphicsDevice.Viewport.Top + 38.0f);
                SaveIcon.Draw(DefaultSpriteBatch, position, rotation * MathHelper.TwoPi);
                DefaultSpriteBatch.End();
            }

            CDebugRender.Render(this);

            //while (GraphicsDevice.RasterStatus.InVerticalBlank)
                //Thread.Sleep(0);
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
    }
}
