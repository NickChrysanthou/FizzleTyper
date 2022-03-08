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

        private Texture2D heart;
        private Rectangle heartRect;

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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Data.CurrentState = Data.GameStates.Menu;
            
            if (playSoundEffect)
            {
                loseLife.Play(1, Data.Pitch(random, -0.0f, 1.0f), 0.0f);
                playSoundEffect = false;
            }
          
            wordManager.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            wordManager.Draw(spriteBatch);

            spriteBatch.DrawString(Data.wordfont, $"Lives: {Data.Lives}", new Vector2(500, 20), Color.White);
            DrawHearts(spriteBatch);
        }
        private void DrawHearts(SpriteBatch spriteBatch)
        {
            for (int i = 1; i <= Data.Lives; i++)
            {
                heartRect = new Rectangle(Data.ScreenW - (heart.Width / 3) * i, 5, heart.Width / 3, heart.Height / 3);
                spriteBatch.Draw(heart, heartRect, Color.White);
            }
        }
    }
}