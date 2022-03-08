using FizzleTyper.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace FizzleTyper.Scenes
{
    internal class MenuScene : Component
    {
        // Textures
        private static Texture2D[] btns = new Texture2D[3];
        private Rectangle[] btnRects = new Rectangle[btns.Length];
        // Input
        private MouseState ms, oldMs;
        private Rectangle msRect;

        public override void Init(ContentManager Content)
        {
            //TODO: Allocate the size dynamically based on screen size and height
            const int X_OFFSET = 5, Y_OFFSET = 175, RESIZE_SCALE = 4, INCREMENT_VALUE = 150;

            for (int i = 0; i < btns.Length; i++)
            {
                btns[i] = Content.Load<Texture2D>($"Textures/Btn{i}");
                btnRects[i] = new Rectangle(X_OFFSET, Y_OFFSET + (i * INCREMENT_VALUE), btns[i].Width / RESIZE_SCALE, btns[i].Height / RESIZE_SCALE);
            }
        }
        public override void Update(GameTime gameTime)
        {
            oldMs = ms;
            ms = Mouse.GetState();
            msRect = new Rectangle(ms.X, ms.Y, 1, 1);
            UpdateButtons();
        }
        private void UpdateButtons()
        {
            if (ms.LeftButton == ButtonState.Pressed && msRect.Intersects(btnRects[0]))
            {
                Data.CurrentState = Data.GameStates.Game;
            }
            if (ms.LeftButton == ButtonState.Pressed && msRect.Intersects(btnRects[2]))
                Data.Exit = true;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawButtons(spriteBatch);
        }
        private void DrawButtons(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < btns.Length; i++)
            {
                spriteBatch.Draw(btns[i], btnRects[i], Color.White);
                if (msRect.Intersects(btnRects[i]))
                    spriteBatch.Draw(btns[i], btnRects[i], Color.DarkGray);

            }
        }
    }
}
