using FizzleTyper.Core;
using FizzleTyper.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FizzleTyper.Managers
{
    internal class GameStateManager : Component
    {
        private MenuScene ms = new MenuScene();
        private GameScene gs = new GameScene();

        public override void Init(ContentManager Content)
        {
            ms.Init(Content);
            gs.Init(Content);
        }

        public override void Update(GameTime gameTime)
        {
            switch (Data.CurrentState)
            {
                case Data.GameStates.Menu:
                    ms.Update(gameTime);
                    break;
                case Data.GameStates.Game:
                    gs.Update(gameTime);
                    break;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            switch (Data.CurrentState)
            {
                case Data.GameStates.Menu:
                    spriteBatch.Begin();
                    ms.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
                case Data.GameStates.Game:
                    spriteBatch.Begin(blendState: BlendState.AlphaBlend);
                    gs.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
            }

        }
    }
}
