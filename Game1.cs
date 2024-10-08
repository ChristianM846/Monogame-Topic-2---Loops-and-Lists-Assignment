using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Monogame_Topic_2___Loops_and_Lists_Assignment
{
    //Christian Moyes
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Random generator = new Random();

        MouseState mouseState;
        MouseState prevMouseState;

        KeyboardState keyboardState;
        KeyboardState prevKeyboardState;

        SpriteFont titleFont;
        SpriteFont instructionFont;

        Texture2D backgroundTexture;
        Texture2D canvasTexture;

        Rectangle backgroundRect;
        Rectangle canvasRect;

        List<Texture2D> textures;
        List<Texture2D> splatTextures;
        List<Rectangle> splatRects;

        enum ScreenState
        {
            Title,
            Paint
        }

        ScreenState screenState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            backgroundRect = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            canvasRect = new Rectangle(150, 100, 200, 400);

            textures = new List<Texture2D>();
            splatTextures = new List<Texture2D>();
            splatRects = new List<Rectangle>();

            screenState = ScreenState.Title;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            backgroundTexture = Content.Load<Texture2D>("Images/RoomBackground");
            canvasTexture = Content.Load<Texture2D>("Images/Canvas");

            titleFont = Content.Load<SpriteFont>("Fonts/Title");
            instructionFont = Content.Load<SpriteFont>("Fonts/Instruction");

            textures.Add(Content.Load<Texture2D>("Images/RedSplat"));
            textures.Add(Content.Load<Texture2D>("Images/OrangeSplat"));
            textures.Add(Content.Load<Texture2D>("Images/GreenSplat"));
            textures.Add(Content.Load<Texture2D>("Images/BlueSplat"));
            textures.Add(Content.Load<Texture2D>("Images/PurpleSplat"));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();

            if (screenState == ScreenState.Title)
            {
                if (keyboardState.IsKeyDown(Keys.Enter) && prevKeyboardState.IsKeyUp(Keys.Enter))
                {
                    screenState = ScreenState.Paint;
                }
            }




            prevMouseState = mouseState;
            prevKeyboardState = keyboardState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTexture, backgroundRect, Color.White);

            if (screenState == ScreenState.Title)
            {
                _spriteBatch.DrawString(titleFont, "Jackson Pollok Simulator!", new Vector2(140, 100), Color.Black);
                _spriteBatch.DrawString(instructionFont, "By Christian Moyes", new Vector2(335, 175), Color.Black);
                _spriteBatch.DrawString(titleFont, "Press ENTER to continue:", new Vector2(135, 300), Color.Black);
            }
            else if (screenState == ScreenState.Paint)
            {
                _spriteBatch.Draw(canvasTexture, canvasRect, Color.White);
            }


            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
