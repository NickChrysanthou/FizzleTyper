using FizzleTyper.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Song music;
        // Sound Effects
        private SoundEffect select;
        public override void Init(ContentManager Content)
        {
            // Sound Effect Stuff
            select = Content.Load<SoundEffect>("SoundEffects/select");
            // Music stuff
            music = Content.Load<Song>("Music/Menu0");
            // When program starts play the first track
            MediaPlayer.Volume = 0.15f;
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;

            //TODO: Allocate the size dynamically based on screen size and height
            // Button Stuff
            btns = new Texture2D[3];
            btnRects = new Rectangle[btns.Length];
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
            ClickButtons();
        }

        private bool playSound = false;
        private void UpdateButtons()
        {
            Trace.WriteLine(ms.Position);
        }

        private void ClickButtons()
        {
            var soundInstance = select.CreateInstance();

            if (ms.LeftButton == ButtonState.Pressed && msRect.Intersects(btnRects[0]))
            {
                soundInstance.Play();
                Data.CurrentState = Data.GameStates.Game;
            }
            if (ms.LeftButton == ButtonState.Pressed && msRect.Intersects(btnRects[1]))
            {
                soundInstance.Play();
                Data.CurrentState = Data.GameStates.Settings;
            }

            if (ms.LeftButton == ButtonState.Pressed && msRect.Intersects(btnRects[2]))
            {
                soundInstance.Play();
                Data.Exit = true;
            }
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
