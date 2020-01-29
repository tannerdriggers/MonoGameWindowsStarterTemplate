using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D ball;
        Vector2 ballPosition = Vector2.Zero;
        Vector2 ballVelocity;
        Random random = new Random();
        Texture2D paddle;
        Rectangle paddleRect = new Rectangle();
        int paddleSpeed = 0;

        KeyboardState oldKeyboardState;
        KeyboardState newKeyboardState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// 
        /// Graphics card is not initialized for the game so nothing graphical should be loaded
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1042;
            graphics.PreferredBackBufferHeight = 760;
            graphics.ApplyChanges();

            ballVelocity = new Vector2(
                (float) random.NextDouble(),
                (float) random.NextDouble()
            );
            ballVelocity.Normalize();

            paddleRect.X = 0;
            paddleRect.Y = 0;
            paddleRect.Width = 50;
            paddleRect.Height = 200;

            newKeyboardState = new KeyboardState();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// 
        /// Graphics card is ready for content in the video RAM
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ball = Content.Load<Texture2D>("tennis-ball");
            paddle = Content.Load<Texture2D>("pixel");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            this.oldKeyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // paddle position
            if (newKeyboardState.IsKeyDown(Keys.Up) && !oldKeyboardState.IsKeyDown(Keys.Up))
            {
                paddleSpeed -= 2;
            }

            if (newKeyboardState.IsKeyDown(Keys.Down) && !oldKeyboardState.IsKeyDown(Keys.Down))
            {
                paddleSpeed += 2;
            }

            paddleRect.Y += paddleSpeed;

            if (paddleRect.Y < 0)
            {
                paddleRect.Y = 0;
                paddleSpeed = 0;
            }

            if (paddleRect.Y > GraphicsDevice.Viewport.Height - paddleRect.Height)
            {
                paddleRect.Y = GraphicsDevice.Viewport.Height - paddleRect.Height;
                paddleSpeed = 0;
            }

            // TODO: Add your update logic here
            ballPosition += (float) gameTime.ElapsedGameTime.TotalMilliseconds * ballVelocity;

            if (ballPosition.Y < 0)
            {
                ballVelocity.Y *= -1;
                float delta = 0 - ballPosition.Y;
                ballPosition.Y += delta + delta;
            }

            if (ballPosition.Y > graphics.PreferredBackBufferHeight - 100)
            {
                ballVelocity.Y *= -1;
                float delta = graphics.PreferredBackBufferHeight - ballPosition.Y - 100;
                ballPosition.Y += delta + delta;
            }

            if (ballPosition.X < 0)
            {
                ballVelocity.X *= -1;
                float delta = 0 - ballPosition.X;
                ballPosition.X += delta + delta + 1;
            }

            if (ballPosition.X > graphics.PreferredBackBufferWidth - 100)
            {
                ballVelocity.X *= -1;
                float delta = graphics.PreferredBackBufferWidth - 100 - ballPosition.X;
                ballPosition.X += delta + delta;
            }

            newKeyboardState = oldKeyboardState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(ball, new Rectangle((int) ballPosition.X, (int) ballPosition.Y, 100, 100), Color.White);
            spriteBatch.Draw(paddle, paddleRect, Color.Red);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
