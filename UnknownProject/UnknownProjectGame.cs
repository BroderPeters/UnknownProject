using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UnknownProject.Components.Miscellaneous;
using UnknownProject.Components;
using UnknownProject.Core;
using System;
using UnknownProject.Components.Core;
using UnknownProject.Content.Pipeline.Tiled;
using System.Collections.Generic;
using UnknownProject.Engine.Components;
using UnknownProject.Engine;
using UnknownProject.Components.Maps;

namespace UnknownProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class UnknownProjectGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        ComponentCollection collection;
        private GraphicConfiguration graficConf;
        private Camera cam;

        int camSpeed = 1000;

        public UnknownProjectGame(Camera cam, GraphicConfiguration graficConf, ComponentCollection collection, FPSCounterComponent fpsComponent, DesertMapComponent map)
        {

            this.collection = collection;
            this.graficConf = graficConf;
            graphics = new GraphicsDeviceManager(this);
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;

            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
            Content.RootDirectory = "Content";
            collection.Add(fpsComponent);
            collection.Add(map);
            this.cam = cam;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            collection.Initialize();
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

            graficConf.Width = GraphicsDevice.Viewport.Bounds.Width;
            graficConf.Height = GraphicsDevice.Viewport.Bounds.Height;


            collection.LoadContent(Content);
            // TODO: use this.Content to load your game content here
        }



        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            collection.UnloadContent();
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            collection.Update(gameTime);

            float dis = (float)gameTime.ElapsedGameTime.TotalSeconds * camSpeed;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                cam.AddOffset(-dis, 0);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D)) cam.AddOffset(dis, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.W)) cam.AddOffset(0, -dis);
            if (Keyboard.GetState().IsKeyDown(Keys.S)) cam.AddOffset(0, dis);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            collection.Draw(spriteBatch, gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
