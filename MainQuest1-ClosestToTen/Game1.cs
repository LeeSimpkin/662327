using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Runtime.CompilerServices;

namespace MainQuest1_ClosestToTen
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Rectangle rectangle;
        private Texture2D simonSplashScreen, rectangleTexture;
        private float timeRemaining = 2f;
        private SpriteFont font, simonFont;
        Vector2 timerPosition;
        enum Screen { flashScreen, titleScreen, creditsScreen, gameScreen, pauseScreen, gameOverScreen };
        private Screen screen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Timer");
            simonFont = Content.Load<SpriteFont>("SIMONFONT");
            simonSplashScreen = Content.Load<Texture2D>("simon");
            rectangleTexture = Content.Load<Texture2D>("Rectangle");

            int simonSplashScreenWidth = 300;
            int simonSplashScreenHeight = 300;
            rectangle = new Rectangle(GraphicsDevice.Viewport.Width / 2 - simonSplashScreenWidth / 2, GraphicsDevice.Viewport.Height / 2 - simonSplashScreenHeight / 2, simonSplashScreenWidth, simonSplashScreenHeight);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            float secondsPassed = gameTime.ElapsedGameTime.Milliseconds / 1000f;
            timeRemaining -= secondsPassed;
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
            }
            timerPosition = new Vector2(_graphics.GraphicsDevice.Viewport.Width - font.MeasureString(timeRemaining.ToString("0.0")).X - 10, 10);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.PeachPuff);
            switch (screen)
            {
                case Screen.flashScreen:
                    DrawFlashScreen();
                    if (timeRemaining == 0)
                    {
                        screen = Screen.titleScreen;
                    }
                    break;
                case Screen.titleScreen:
                    DrawTiteScreen();
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        screen = Screen.gameScreen;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.C))
                    {
                        screen = Screen.creditsScreen;
                    }
                    break;
                case Screen.creditsScreen:
                    DrawCreditsScreen();

                    break;
                case Screen.gameScreen:
                    DrawGameScreen();
                    if (Keyboard.GetState().IsKeyDown(Keys.P))
                    {
                        screen = Screen.pauseScreen;
                    }
                    break;
                case Screen.pauseScreen:
                    DrawPauseScreen();
                    break;
                case Screen.gameOverScreen:
                    DrawGameOverScreen();
                    break;
            }
            //if (timeRemaining > 0)
            //{

            //    _spriteBatch.DrawString(font, timeRemaining.ToString("0.0"), timerPosition + new Vector2(2, 2), new Color(242f / 255, 70f / 255, 80f / 255, 1f));
            //    _spriteBatch.DrawString(font, timeRemaining.ToString("0.0"), timerPosition, new Color(252f / 255, 234f / 255, 51f / 255, 1f));
            //    _spriteBatch.Draw(simonTexture, blueRectangle, Color.Blue);
            //    _spriteBatch.Draw(simonTexture, redRectangle, Color.Red);
            //}
            //if (timeRemaining == 0)
            //{
            //    GraphicsDevice.Clear(Color.Red);
            //    _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //    _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //    _graphics.ApplyChanges();

            //    _spriteBatch.Draw(simonTexture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Red);
            //    _spriteBatch.DrawString(simonFont, "HELP ME", new Vector2(GraphicsDevice.Viewport.Width / 2 - 50, GraphicsDevice.Viewport.Height / 2 + 50), Color.Black);
            //}
            base.Draw(gameTime);
        }

        private void DrawFlashScreen()
        {

            _spriteBatch.Begin();
            _spriteBatch.Draw(simonSplashScreen, rectangle, Color.White);
            Vector2 timerPosition = new Vector2(_graphics.GraphicsDevice.Viewport.Width - font.MeasureString(timeRemaining.ToString("0.0")).X - 10, 10);
            _spriteBatch.DrawString(font, timeRemaining.ToString("0.0"), timerPosition + new Vector2(2, 2), new Color(242f / 255, 70f / 255, 80f / 255, 1f));
            _spriteBatch.DrawString(font, timeRemaining.ToString("0.0"), timerPosition, new Color(252f / 255, 234f / 255, 51f / 255, 1f));
            _spriteBatch.End();
        }

        private void DrawTiteScreen()
        {
            Vector2 location = new Vector2(GraphicsDevice.Viewport.Width / 2 - 100, GraphicsDevice.Viewport.Height / 2);
            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, "Press space to play", location, new Color(252f / 255, 234f / 255, 51f / 255, 1f));
            _spriteBatch.DrawString(font, "Press space to play", location - new Vector2(2, 2), new Color(242f / 255, 70f / 255, 80f / 255, 1f));
            _spriteBatch.DrawString(font, "Press c for credits", location + new Vector2(0, 40), new Color(252f / 255, 234f / 255, 51f / 255, 1f));
            _spriteBatch.DrawString(font, "Press c for credits", (location + new Vector2(0, 40)) - new Vector2(2, 2), new Color(242f / 255, 70f / 255, 80f / 255, 1f));
            _spriteBatch.End();

        }
        private void DrawCreditsScreen()
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, "Made by Lee :)", new Vector2(GraphicsDevice.Viewport.Width / 2 - 100, GraphicsDevice.Viewport.Height / 2), new Color(252f / 255, 234f / 255, 51f / 255, 1f));
            _spriteBatch.DrawString(font, "Made by Lee :)", new Vector2(GraphicsDevice.Viewport.Width / 2 - 102, GraphicsDevice.Viewport.Height / 2), new Color(242f / 255, 70f / 255, 80f / 255, 1f));
            _spriteBatch.End();
        }
        private void DrawGameScreen()
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(rectangleTexture, new Rectangle(GraphicsDevice.Viewport.Width / 2 - 150, GraphicsDevice.Viewport.Height / 2 - 150, 300, 300), Color.Red);
            _spriteBatch.End();
        }
        private void DrawPauseScreen()
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(simonSplashScreen, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Red);
            _spriteBatch.DrawString(simonFont, "paused", new Vector2(GraphicsDevice.Viewport.Width / 2 - 200, GraphicsDevice.Viewport.Height / 2), Color.Black);
            _spriteBatch.End();
        }
        private void DrawGameOverScreen()
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, "Game over", new Vector2(GraphicsDevice.Viewport.Width / 2 - 100, GraphicsDevice.Viewport.Height / 2), new Color(252f / 255, 234f / 255, 51f / 255, 1f));
            _spriteBatch.DrawString(font, "Game over", new Vector2(GraphicsDevice.Viewport.Width / 2 - 102, GraphicsDevice.Viewport.Height / 2), new Color(242f / 255, 70f / 255, 80f / 255, 1f));
            _spriteBatch.End();
        }
    }
}
