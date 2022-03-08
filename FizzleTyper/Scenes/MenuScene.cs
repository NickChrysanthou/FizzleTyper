using FizzleTyper.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace FizzleTyper.Scenes
{
    internal class MenuScene : Component
    {
        // Textures
        private Texture2D[] btns;
        private Rectangle[] btnRects;
        // Input
        private MouseState ms, oldMs;
        private Rectangle msRect;
        // Music
        protected Song music;

        public override void Init(ContentManager Content)
        {
            btns = new Texture2D[3];
            btnRects = new Rectangle[btns.Length];
            
            //TODO: Allocate the size dynamically based on screen size and height

            // Button Stuff
            const int X_OFFSET = 5, Y_OFFSET = 175, RESIZE_SCALE = 4, INCREMENT_VALUE = 150;

            for (int i = 0; i < btns.Length; i++)
            {
                btns[i] = Content.Load<Texture2D>($"Textures/Btn{i}");
                btnRects[i] = new Rectangle(X_OFFSET, Y_OFFSET + (i * INCREMENT_VALUE), btns[i].Width / RESIZE_SCALE, btns[i].Height / RESIZE_SCALE);
            }

            // Music stuff
            music = Content.Load<Song>("Music/Menu0");
            // When program starts play the first track
            MediaPlayer.Volume = 0.75f;
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;

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
