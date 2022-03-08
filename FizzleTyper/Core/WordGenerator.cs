using FizzleTyper.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace FizzleTyper.Core
{
    internal class WordGenerator : Component
    {
        private Random random;

        internal string Word;
        internal Vector2 Position;
        public Color Color { get; protected set; } = Color.White;

        internal bool visible = false;
        private const int SPEED = 5;

        public WordGenerator(string word)
        {
            Word = word;
            
            random = new Random();
            Position = RandomPosition();
            Color = PickRandomColor();
        }

        public override void Init(ContentManager Content){ }
        public Vector2 RandomPosition()
        {
            const int X_OFFSET_WIDTH = 100, Y_OFFSET = 0;
            int randX = random.Next(X_OFFSET_WIDTH, Data.ScreenW - X_OFFSET_WIDTH);
            return new Vector2(randX, Y_OFFSET);
        }
        private Color PickRandomColor()
        {
            const int START_POINT = 15, END_POINT = 256, MAX_APLHA = 256;
            int R = random.Next(START_POINT, END_POINT), G = random.Next(START_POINT, END_POINT), B = random.Next(START_POINT, END_POINT);
            return new Color(R, G, B, MAX_APLHA);
        }

        public override void Update(GameTime gameTime)
        {
            Position.Y += SPEED;

            if (Position.Y >= Data.ScreenH)
            {
                visible = false;
                --Data.Lives;
                // Only if the word isn't typed out and reaches the bottom of the screen clear the screen 
                WordManager.ActiveList.Clear();
            }
        }
        public override void Draw(SpriteBatch spriteBatch) => spriteBatch.DrawString(Data.wordfont, Word, Position, Color);
    }
}
