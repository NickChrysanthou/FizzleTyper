using FizzleTyper.Core;
using FizzleTyper.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FizzleTyper.Scenes
{
    class GameScene : Component
    {
        private WordManager wordManager = new WordManager();
        public static bool playSoundEffect { get; internal set; } = false;
        private SoundEffect loseLife;

        // Health Stuff
        private Texture2D heart;
        private Rectangle heartRect;
        // Random
        private Random random;

        public override void Init(ContentManager Content)
        {
            random = new Random();
            loseLife = Content.Load<SoundEffect>("SoundEffects/explosion");
            Data.wordfont = Content.Load<SpriteFont>("Fonts/WordFont");

            heart = Content.Load<Texture2D>("textures/heart");
            
            wordManager.Init(Content);
            wordManager.PopulateList();
        }
        public override void Update(GameTime gameTime)
        {
            if (playSoundEffect)
            {
                loseLife.Play(1, Data.Pitch(random, -0.0f, 1.0f), 0.0f);
                playSoundEffect = false;
            }

            wordManager.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Data.CurrentState = Data.GameStates.Menu;

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            wordManager.Draw(spriteBatch);
            DrawHealth(spriteBatch);
            spriteBatch.DrawString(Data.wordfont, $"Lives: {Data.Lives}", new Vector2(500, 20), Color.White);

        }

        // health stuff
        private const int SCALE = 3, Y_OFFSET = 5;
        private void DrawHealth(SpriteBatch spriteBatch)
        {
            for (int i = 1; i <= Data.Lives; i++)
            {
                heartRect = new Rectangle(Data.ScreenW - (heart.Width / SCALE * i), Y_OFFSET, heart.Width / SCALE, heart.Height / SCALE);
                spriteBatch.Draw(heart, heartRect, Color.White);
            }
        }
    }
}