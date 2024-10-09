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
            canvasRect = new Rectangle(200, 50, 400, 450);

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
            canvasTexture = Content.Load<Texture2D>("Images/CanvasSprite");

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
            else if (screenState == ScreenState.Paint)
            {
                if (keyboardState.IsKeyDown(Keys.Space) && prevKeyboardState.IsKeyUp(Keys.Space))
                {
                    splatTextures.Add(textures[generator.Next(textures.Count)]);
                    splatRects.Add(new Rectangle(generator.Next(210, 541), generator.Next(75, 361), 50, 50));
                }

                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    for (int i = 0; i < splatRects.Count; i++)
                    {
                        if (splatRects[i].Contains(mouseState.Position))
                        {
                            splatRects.RemoveAt(i);
                            splatTextures.RemoveAt(i);
                            i--;                          
                        }
                    }
                }

                if (mouseState.RightButton == ButtonState.Pressed && prevMouseState.RightButton == ButtonState.Released)
                {
                    splatRects.Clear();
                    splatTextures.Clear();
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
                _spriteBatch.DrawString(instructionFont, "By Christian Moyes", new Vector2(280, 175), Color.Black);
                _spriteBatch.DrawString(titleFont, "Press ENTER to continue:", new Vector2(135, 300), Color.Black);
            }
            else if (screenState == ScreenState.Paint)
            {
                _spriteBatch.Draw(canvasTexture, canvasRect, Color.White);
                _spriteBatch.DrawString(instructionFont, "Press space to paint the canvas:", new Vector2(180, 10), Color.Yellow);
                _spriteBatch.DrawString(instructionFont, "Left click a splat to remove it.", new Vector2(205, 415), Color.Yellow);
                _spriteBatch.DrawString(instructionFont, "Right clcik to clear the canvas.", new Vector2(195, 450), Color.Yellow);
                _spriteBatch.DrawString(instructionFont, $"Splats: {splatRects.Count}", new Vector2(630, 200), Color.Black);

                for (int i = 0; i < splatRects.Count; i++)
                {
                    _spriteBatch.Draw(splatTextures[i], splatRects[i], Color.White);
                }

            }


            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
