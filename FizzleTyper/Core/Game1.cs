using FizzleTyper.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FizzleTyper.Core
{
    public class Game1 : Game
    {
        internal static GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameStateManager gsm;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = Data.ScreenW;
            graphics.PreferredBackBufferHeight = Data.ScreenH;
            graphics.ApplyChanges();
         
            Window.AllowUserResizing = false;
            Window.AllowAltF4 = false;
            Window.Title = Data.Title;

            gsm = new GameStateManager();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gsm.Init(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            gsm.Update(gameTime);
            
            if (Data.Exit)
                Exit();
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            gsm.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
