using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using RoomGen;

namespace ncot_assam
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Core
    {
        Scene myScene;
        SpriteBatch spriteBatch;
        RoomManager roomManager;
        SpriteFont font;
        RoomDrawer roomDrawer;

        public Game1() : base(width: 1024, height: 768, isFullScreen: false, enableEntitySystems: false)
        { }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            font = Content.Load<SpriteFont>("DebugFont");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            base.Initialize();
            Window.Title = "Room Generator";
            Window.AllowUserResizing = false;
            roomManager = new RoomManager();
            roomManager.Generate();
            Physics.gravity = Vector2.Zero;
            myScene = Scene.createWithDefaultRenderer(Color.CornflowerBlue);
            Input.gamePads[0].isLeftStickVertcialInverted = true;
            roomDrawer = new RoomDrawer(spriteBatch, roomManager, font, GraphicsDevice);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            roomDrawer.SwitchRoom();
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

            base.Draw(gameTime);
            roomDrawer.DrawRoom(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
        }
    }
}
