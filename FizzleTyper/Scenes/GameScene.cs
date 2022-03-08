using FizzleGame.ParticleSystem;
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
        private static Rectangle heartRect;

        private Random random;

        // Particle Stuff
        private Texture2D snow;
        private static ParticleEngine heartParticle;
        private ParticleEngine snowParticle;

        public override void Init(ContentManager Content)
        {
            random = new Random();
            loseLife = Content.Load<SoundEffect>("SoundEffects/explosion");
            Data.wordfont = Content.Load<SpriteFont>("Fonts/WordFont");

            heart = Content.Load<Texture2D>("textures/heart");

            snow = Content.Load<Texture2D>("Particles/snow");
            heartParticle = new ParticleEngine(new List<Texture2D> { snow }, true, Color.White, 50f, 0f, 2f);
            snowParticle = new ParticleEngine(new List<Texture2D> { snow }, false, Color.White, 1f, 0f, 5f);
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

            // TODO: get center of current image, not too hard to do
            snowParticle.IsVisible = true;
            snowParticle.EmitterLocation = new Vector2(random.Next(0,Data.ScreenW), random.Next(0, Data.ScreenH));
            snowParticle.Update();

            var HEART_OFFSET = 40;
            heartParticle.EmitterLocation = new Vector2(heartRect.X + HEART_OFFSET, heartRect.Y + HEART_OFFSET);
            heartParticle.Update();

            if (heartParticle.IsVisible)
                PlayParticle(heartParticle, gameTime, 0.05);

            // Todo: add gameover logic
            if (Data.Lives <= 0)
                Data.GameOver = true;

        }

        // Particle Stuff
        private float currentTime;
        private void PlayParticle(ParticleEngine engine, GameTime gameTime, double timeToPlay)
        {
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (currentTime >= timeToPlay)
            {
                engine.IsVisible = false;
                currentTime = 0;
            }
        }

        public static void LoseLife(List<WordGenerator> list)
        {
            list[0].visible = false;
            heartParticle.IsVisible = true;
            --Data.Lives;
            WordManager.ActiveList.Clear();
            playSoundEffect = true;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            wordManager.Draw(spriteBatch);

            if (Data.Lives >= 4)
                spriteBatch.DrawString(Data.wordfont, $"Lives: {Data.Lives}", new Vector2(500, 20), Color.Green);
            else if (Data.Lives >= 3)
                spriteBatch.DrawString(Data.wordfont, $"Lives: {Data.Lives}", new Vector2(500, 20), Color.Yellow);
            else if (Data.Lives >= 1)
                spriteBatch.DrawString(Data.wordfont, $"Lives: {Data.Lives}", new Vector2(500, 20), Color.Red);
            
            snowParticle.Draw(spriteBatch);

            DrawHearts(spriteBatch);
            heartParticle.Draw(spriteBatch);

        }
        // Heart stuff
        private const int SCALE = 3, Y_OFFSET = 5;
        private void DrawHearts(SpriteBatch spriteBatch)
        {
            for (int i = 1; i <= Data.Lives; i++)
            {
                heartRect = new Rectangle(Data.ScreenW - (heart.Width / SCALE) * i, Y_OFFSET, heart.Width / SCALE, heart.Height / SCALE);
                spriteBatch.Draw(heart, heartRect, Color.White);
            }
        }
    }
}