using FizzleTyper.Core;
using FizzleTyper.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FizzleTyper.Scenes
{
    class GameScene : Component
    {
        private WordManager wordManager = new WordManager();

        public override void Init(ContentManager Content)
        {
            Data.wordfont = Content.Load<SpriteFont>("Fonts/WordFont");
            wordManager.Init(Content);
            wordManager.PopulateList();
        }
        public override void Update(GameTime gameTime)
        {
            wordManager.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            wordManager.Draw(spriteBatch);

            spriteBatch.DrawString(Data.wordfont, $"Lives: {Data.Lives}", new Vector2(500, 20), Color.White);
        }
    }
}