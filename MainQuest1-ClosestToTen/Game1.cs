using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq.Expressions;
using System.Net.Mime;

namespace MainQuest1_ClosestToTen
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Rectangle blueRectangle, redRectangle;
        private Texture2D bluePixelTexture, redPixelTexture;
        private Texture2D simonTexture;
        private float timeRemaining = 10f;
        private SpriteFont font;

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

            int rectangleWidth = 200;
            int rectangleHeight = 100;

            int blueRectangleY = GraphicsDevice.Viewport.Height - rectangleHeight;
            int blueRectangleX = 0;
            int redRectangley = GraphicsDevice.Viewport.Height - rectangleHeight;
            int redRectangleX = GraphicsDevice.Viewport.Width - rectangleWidth;

            blueRectangle = new Rectangle(blueRectangleX, blueRectangleY, rectangleWidth, rectangleHeight);
            redRectangle = new Rectangle(redRectangleX, redRectangley, rectangleWidth, rectangleHeight);

            bluePixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            bluePixelTexture.SetData(new Color[] { Color.Blue });
            redPixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            redPixelTexture.SetData(new Color[] { Color.Red });
            simonTexture = Content.Load<Texture2D>("simon");

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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            _spriteBatch.Begin();
            if (timeRemaining > 0)
            {
                _spriteBatch.DrawString(font, timeRemaining.ToString(), new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), Color.Black);
                _spriteBatch.Draw(simonTexture, blueRectangle, Color.Blue);
                _spriteBatch.Draw(simonTexture, redRectangle, Color.Red);
            }
            if (timeRemaining == 0)
            {
                GraphicsDevice.Clear(Color.Red);
                _spriteBatch.Draw(simonTexture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.Red);
                _spriteBatch.DrawString(font, "HELP ME", new Vector2(GraphicsDevice.Viewport.Width / 2 - 50, GraphicsDevice.Viewport.Height / 2 + 50), Color.Black);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
