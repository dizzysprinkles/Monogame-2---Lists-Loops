using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace Monogame_2___Lists___Loops
{
    //enum ScreenState
    //{
    //    TitleScreen,
    //    MainScreen
    //}

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
        //ScreenState screenState;
        MouseState mouseState, previousMouseState;
        KeyboardState keyboardState;
        float seconds, respawnTime;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
           // screenState = ScreenState.TitleScreen;
            window = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth,  _graphics.PreferredBackBufferHeight);
            generator = new Random();
            planetTextures = new List<Texture2D>();
            textures = new List<Texture2D>();
            planetRects = new List<Rectangle>();

            for (int i = 0; i < 1; i++)
            {
                planetRects.Add(new Rectangle(generator.Next(window.Width - 25), generator.Next(window.Height - 25), 50, 50)); 
            }

            seconds = 0f;
            respawnTime = 5f;// #f = seconds. so 3f = 3 seconds

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
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (mouseState.ScrollWheelValue > respawnTime || mouseState.ScrollWheelValue < respawnTime)
            {
                respawnTime = mouseState.ScrollWheelValue;
            }
            if (seconds > respawnTime)
            {
                planetTextures.Add(textures[generator.Next(textures.Count)]);
                planetRects.Add(new Rectangle(generator.Next(window.Width - 25), generator.Next(window.Height - 25), 50, 50));
                seconds = 0f; // Restarts timer
            }

            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                for (int i = 0; i < planetRects.Count; i++)
                {
                    if (planetRects[i].Contains(mouseState.Position))
                    {
                        planetRects.RemoveAt(i);
                        planetTextures.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (planetTextures.Count == 0)
            {
                Exit();
            }

            //if (screenState == ScreenState.TitleScreen)
            //{
            //    if (mouseState.LeftButton == ButtonState.Pressed)
            //    {
            //        screenState = ScreenState.MainScreen;
            //    }
            //}

            previousMouseState = mouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            _spriteBatch.Begin();
            //if (screenState == ScreenState.TitleScreen)
            //{
            //    GraphicsDevice.Clear(Color.CornflowerBlue);

            //}

            _spriteBatch.Draw(spaceBackgroundTexture, window, Color.White);
            for (int i = 0; i < planetRects.Count; i++)
            {
               _spriteBatch.Draw(planetTextures[i], planetRects[i], Color.White);
            }
    
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
