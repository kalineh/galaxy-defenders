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

namespace galaxy
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class CGalaxy
        : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public SpriteFont DefaultFont { get; private set; }
        public CWorld World { get; private set; }
        public CDebug Debug { get; private set; }
        public Texture2D PixelTexture { get; private set; }
        public CMusic Music { get; private set; }
        public int GameFrame { get; private set; }
        //public AudioEngine AudioEngine { get; private set; }
        //public SoundBank SoundBank { get; private set; }
        //public WaveBank WaveBank { get; private set; }

        public CGalaxy()
        {
            Content.RootDirectory = "Content";

            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            World = new CWorld(this);
            Debug = new CDebug(this);
            Music = new CMusic(this);

            GameFrame = 0;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Graphics device does not exist during Initialize().
            SpriteBatch = new SpriteBatch(GraphicsDeviceManager.GraphicsDevice);
            DefaultFont = Content.Load<SpriteFont>("DefaultFont");
            PixelTexture = Content.Load<Texture2D>("Pixel");

            // Create default world state.
            World.Start();
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

            // World update.
            if (!Keyboard.GetState().IsKeyDown(Keys.X))
                World.Update();

            GamePadState input = GamePad.GetState(PlayerIndex.One);

            Music.Update();

            base.Update(game_time);

            GameFrame += 1;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime game_time)
        {
            World.Draw(SpriteBatch);

            base.Draw(game_time);
        }
    }
}
