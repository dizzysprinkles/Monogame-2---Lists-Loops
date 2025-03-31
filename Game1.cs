using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Monogame_2___Lists___Loops
{
    enum ScreenState
    {
        TitleScreen,
        MainScreen
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Random generator;
        Rectangle window;
        Texture2D spaceBackgroundTexture;
        List<Texture2D> textures;
        List<Rectangle> planetRects;
        List<Texture2D> planetTextures;
        ScreenState screenState;
        MouseState mouseState;
        KeyboardState keyboardState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            screenState = ScreenState.TitleScreen;
            window = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth,  _graphics.PreferredBackBufferHeight);
            generator = new Random();
            planetTextures = new List<Texture2D>();
            textures = new List<Texture2D>();
            planetRects = new List<Rectangle>();

            for (int i = 0; i < generator.Next(10, 51); i++)
            {
                planetRects.Add(new Rectangle(generator.Next(window.Width - 25), generator.Next(window.Height - 25), 50, 50)); 
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            spaceBackgroundTexture = Content.Load<Texture2D>("Images/space_background");


            for (int i = 1; i <= 13; i++)
                textures.Add(Content.Load<Texture2D>("Images/16-bit-planet" + i));

            for (int i = 0; i < planetRects.Count; i++)
                planetTextures.Add(textures[generator.Next(textures.Count)]);

        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (screenState == ScreenState.TitleScreen)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    screenState = ScreenState.MainScreen;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            _spriteBatch.Begin();
            if (screenState == ScreenState.TitleScreen)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

            }
            else if (screenState == ScreenState.MainScreen)
            {
                _spriteBatch.Draw(spaceBackgroundTexture, window, Color.White);
                for (int i = 0; i < planetRects.Count; i++)
                    _spriteBatch.Draw(planetTextures[i], planetRects[i], Color.White);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
