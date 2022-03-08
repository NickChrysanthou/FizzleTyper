using FizzleTyper.Core;
using FizzleTyper.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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

        private Random rand;
        public override void Init(ContentManager Content)
        {
            rand = new Random();
            loseLife = Content.Load<SoundEffect>("SoundEffects/explosion");
            Data.wordfont = Content.Load<SpriteFont>("Fonts/WordFont");

            wordManager.Init(Content);
            wordManager.PopulateList();
        }
        public override void Update(GameTime gameTime)
        {
            if (playSoundEffect)
            {
                float val = (float)(rand.NextDouble() * (1.0f - -1.0f) + -1.0f);
                loseLife.Play(1, val, 0.0f);
                playSoundEffect = false;
            }

            wordManager.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            wordManager.Draw(spriteBatch);

            spriteBatch.DrawString(Data.wordfont, $"Lives: {Data.Lives}", new Vector2(500, 20), Color.White);
        }
    }
}