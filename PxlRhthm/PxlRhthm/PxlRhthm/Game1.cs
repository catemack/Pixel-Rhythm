using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PxlRhthm
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;
        Barrier barrier;

        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        int playerMoveSpeed;

        Texture2D pixelTexture;
        List<Pixel> pixels;

        TimeSpan pixelSpawnTime;
        TimeSpan previousSpawnTime;

        Random random;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 320;
            graphics.PreferredBackBufferWidth = 320;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            player = new Player();
            playerMoveSpeed = 10;

            barrier = new Barrier();

            pixels = new List<Pixel>();
            previousSpawnTime = TimeSpan.Zero;
            pixelSpawnTime = TimeSpan.FromSeconds(1.0f);

            random = new Random();
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + (GraphicsDevice.Viewport.TitleSafeArea.Width / 2) - (GraphicsDevice.Viewport.TitleSafeArea.Height / 32),
                GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height - 2 * (GraphicsDevice.Viewport.TitleSafeArea.Height / 32));
            player.Initialize((Content.Load<Texture2D>("paddle")), playerPosition);

            Vector2 barrierPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X,
                GraphicsDevice.Viewport.TitleSafeArea.Y + 7 * (GraphicsDevice.Viewport.TitleSafeArea.Height / 32));
            barrier.Initialize((Content.Load<Texture2D>("barrier")), barrierPosition);

            pixelTexture = Content.Load<Texture2D>("pixel_big");
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
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
                this.Exit();

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            UpdatePlayer(gameTime);
            UpdatePixels(gameTime);

            base.Update(gameTime);
        }

        private void UpdatePlayer(GameTime gameTime)
        {
            player.Update();
            
            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                player.Position.X -= playerMoveSpeed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                player.Position.X += playerMoveSpeed;
            }

            player.Position.X = MathHelper.Clamp(player.Position.X,
                0, GraphicsDevice.Viewport.Width - player.Width);
        }

        private void AddPixel()
        {
            Vector2 position = new Vector2(random.Next(10, GraphicsDevice.Viewport.Width - 10),
                7 * (GraphicsDevice.Viewport.Height / 32) + pixelTexture.Height);

            Pixel pixel = new Pixel();

            pixel.Initialize(pixelTexture, position);

            pixels.Add(pixel);
        }

        private void UpdatePixels(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - previousSpawnTime > pixelSpawnTime)
            {
                previousSpawnTime = gameTime.TotalGameTime;

                AddPixel();
            }

            for (int i = pixels.Count - 1; i >= 0; i--)
            {
                pixels[i].Update(gameTime);

                if (pixels[i].Active == false)
                {
                    pixels.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            player.Draw(spriteBatch);
            barrier.Draw(spriteBatch);

            for (int i = 0; i < pixels.Count; i++)
            {
                pixels[i].Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
