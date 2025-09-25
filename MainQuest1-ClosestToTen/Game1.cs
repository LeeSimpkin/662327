using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;

namespace MainQuest1_ClosestToTen
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Rectangle simonSplashScreenRectangle, gameButton;
        private Texture2D simonSplashScreen, rectangleTexture;
        private float timeRemaining = 2f, timer;
        private int score;
        private SpriteFont font, simonFont;
        private bool isPressed;
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
            Vector2 buttonSize = new Vector2(150, 100);

            int simonSplashScreenWidth = 300;
            int simonSplashScreenHeight = 300;

            simonSplashScreenRectangle = new Rectangle(GraphicsDevice.Viewport.Width / 2 - simonSplashScreenWidth / 2, GraphicsDevice.Viewport.Height / 2 - simonSplashScreenHeight / 2, simonSplashScreenWidth, simonSplashScreenHeight);
            gameButton = new Rectangle((int)(GraphicsDevice.Viewport.Width / 2 - (buttonSize.X / 2)), (int)(GraphicsDevice.Viewport.Height / 2 - (buttonSize.Y / 2)), (int)buttonSize.X, (int)buttonSize.Y);
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
            if(screen == Screen.gameScreen)
            {
                timer += gameTime.ElapsedGameTime.Milliseconds;
            }
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
                    if(isPressed) 
                    {
                        if (Keyboard.GetState().IsKeyUp(Keys.P))
                        {
                            isPressed = false;
                        }
                        break;
                    }
                    else
                    if (Keyboard.GetState().IsKeyDown(Keys.P))
                    {
                        screen = Screen.pauseScreen;
                        isPressed = true;
                    }
                    else if (gameButton.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        score = (int)(100 * (timer/1000));
                        if (score > 1000) score = 0;
                        screen = Screen.gameOverScreen;
                    }
                    break;
                case Screen.pauseScreen:
                    DrawPauseScreen();

                    if(isPressed) 
                                            {
                        if (Keyboard.GetState().IsKeyUp(Keys.P))
                        {
                            isPressed = false;
                        }
                        break;
                    }
                    else
                    if (Keyboard.GetState().IsKeyDown(Keys.P))
                    {
                        screen = Screen.gameScreen;
                        isPressed = true;
                    }
                    break;
                case Screen.gameOverScreen:
                    DrawGameOverScreen();
                    break;
            }
            
            base.Draw(gameTime);
        }

        private void DrawFlashScreen()
        {

            _spriteBatch.Begin();
            _spriteBatch.Draw(simonSplashScreen, simonSplashScreenRectangle, Color.White);
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
            _spriteBatch.Draw(rectangleTexture, new Rectangle(gameButton.X, gameButton.Y, gameButton.Width, gameButton.Height), Color.Red);
            _spriteBatch.DrawString(font, "Closest to 10 wins!", new Vector2(GraphicsDevice.Viewport.Width / 2 - 100, 50), new Color(252f / 255, 234f / 255, 51f / 255, 1f));
            _spriteBatch.DrawString(font, "Closest to 10 wins!", new Vector2((GraphicsDevice.Viewport.Width / 2 - 100) + 2, 50 + 2), new Color(242f / 255, 70f / 255, 80f / 255, 1f));
            if (timer <= 5000)
            {
                string timerText = (timer / 1000).ToString("0.00");
                Vector2 textSize = font.MeasureString(timerText);
                Vector2 textPosition = new Vector2(
                    gameButton.X + gameButton.Width / 2 - textSize.X / 2,
                    gameButton.Y + gameButton.Height / 2 - textSize.Y / 2
                );
                _spriteBatch.DrawString(font, timerText, textPosition, Color.Black);
            }
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
            _spriteBatch.DrawString(font, "Score: " + score, new Vector2(GraphicsDevice.Viewport.Width / 2 - 100, GraphicsDevice.Viewport.Height / 2 + 50), new Color(252f / 255, 234f / 255, 51f / 255, 1f));
            _spriteBatch.DrawString(font, "Score: " + score, new Vector2(GraphicsDevice.Viewport.Width / 2 - 102, GraphicsDevice.Viewport.Height / 2 + 50), new Color(242f / 255, 70f / 255, 80f / 255, 1f));
            _spriteBatch.DrawString(font, "Game over", new Vector2(GraphicsDevice.Viewport.Width / 2 - 100, GraphicsDevice.Viewport.Height / 2), new Color(252f / 255, 234f / 255, 51f / 255, 1f));
            _spriteBatch.DrawString(font, "Game over", new Vector2(GraphicsDevice.Viewport.Width / 2 - 102, GraphicsDevice.Viewport.Height / 2), new Color(242f / 255, 70f / 255, 80f / 255, 1f));
            _spriteBatch.DrawString(font, $"Time: {(timer / 1000).ToString("0.00")}", new Vector2(GraphicsDevice.Viewport.Width / 2 - 100, GraphicsDevice.Viewport.Height / 2 - 100), new Color(252f / 255, 234f / 255, 51f / 255, 1f));
            _spriteBatch.DrawString(font, $"Time: {(timer / 1000).ToString("0.00")}", new Vector2(GraphicsDevice.Viewport.Width / 2 - 102, GraphicsDevice.Viewport.Height / 2- 100), new Color(242f / 255, 70f / 255, 80f / 255, 1f));
            _spriteBatch.End();
        }
    }
}
