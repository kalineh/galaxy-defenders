using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

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
        public CMusic Music { get; private set; }
        public CFrameRateDisplay FrameRateDisplay { get; private set; }
        public int GameFrame { get; set; }
        public CState State { get; set; }
        public CStageDefinition StageDefinition { get; set; }

        public CGalaxy()
        {
            Content.RootDirectory = "Content";

            GraphicsDeviceManager = new GraphicsDeviceManager(this);

            // TODO: 1080p!
            // TODO: render to backbuffer and just scale down before display if necessary
            //GraphicsDeviceManager.PreferredBackBufferWidth = 1280;
            //GraphicsDeviceManager.PreferredBackBufferHeight = 720;
            GraphicsDeviceManager.SynchronizeWithVerticalRetrace = false;
            //GraphicsDeviceManager.ToggleFullScreen();
            // TODO: fix crash here sometimes happening (device startup timing not friendly with editor)
            GraphicsDeviceManager.ApplyChanges();

            Debug = new CDebug(this);
            Input = new CInput(this);
            Music = new CMusic(this);

            FrameRateDisplay = new CFrameRateDisplay(this);
            GameFrame = 0;

            StageDefinition = Stages.EditorStage.GenerateDefinition();

            // default
            GraphicsDevice = GraphicsDeviceManager.GraphicsDevice;
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
            //GraphicsDevice.PresentationParameters.PresentationInterval = PresentInterval.Immediate;
            //GraphicsDevice.PresentationParameters.BackBufferCount = 3;
            //GraphicsDevice.PresentationParameters.PresentOptions = PresentOptions.None;
            //GraphicsDevice.PresentationParameters.RenderTargetUsage = RenderTargetUsage.DiscardContents;
            //GraphicsDevice.PresentationParameters.SwapEffect = SwapEffect.Discard;
            GraphicsDeviceManager.ApplyChanges();
            GraphicsDevice = GraphicsDeviceManager.GraphicsDevice;

            base.Initialize();
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

            // Import profiles.
            CSaveData.VerifyProfilesExist();
            CSaveData.Load();

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
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime game_time)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // State update.
            if (!Keyboard.GetState().IsKeyDown(Keys.LeftShift) || State.GetType() != typeof(CStateGame))
                State.Update();

            GamePadState input = GamePad.GetState(PlayerIndex.One);

            Input.Update();
            Music.Update();
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
            base.Draw(game_time);
            State.Draw();
            FrameRateDisplay.Draw(DefaultSpriteBatch);
        }
    }
}
