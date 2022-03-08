using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

using FizzleTyper.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json.Linq;

namespace FizzleTyper.Managers
{
    public class WordManager : Component
    {
        internal List<WordGenerator> WordBank;
        internal List<WordGenerator> ActiveList = new List<WordGenerator>();

        internal const double SpawnTimeSeconds = 1.00;
        private float currentTime = 0f;

        private Random rand = new Random();
        private int next = 0;

        private TypingManager typeManager = new TypingManager();

        public override void Init(ContentManager Content) { }
        public override void Update(GameTime gameTime)
        {
            typeManager.Update();

            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (currentTime >= SpawnTimeSeconds && WordBank.Count > 0)
            {
                next = rand.Next(0, WordBank.Count);
                ActiveList.Add(new WordGenerator(WordBank[next].Word));
                ActiveList[ActiveList.Count - 1].visible = true;
                WordBank.RemoveAt(next);
                currentTime = 0;
            }

            if (ActiveList.Count > 0)
            {
                char pressed = typeManager.pressed;
                char currentLetter = ActiveList[0].Word[0];

                //foreach (var letter in ActiveList[0].Word)

                Trace.WriteLine(currentLetter);

                if (pressed == currentLetter)
                    ActiveList[0].Word = ActiveList[0].Word.Remove(0, 1);

                if (ActiveList[0].Word == string.Empty)
                    ActiveList[0].visible = false;
            }

            for (int i = 0; i < ActiveList.Count; i++)
            {
                WordGenerator word = ActiveList[i];
                word.Update(gameTime);
                if (!word.visible)
                    ActiveList.RemoveAt(i);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var word in ActiveList)
                if (word.visible)
                    word.Draw(spriteBatch);
        }
        private const string PATH = "wordbank.json";
        public void PopulateList()
        {
            WordBank = new List<WordGenerator>();

            if (!File.Exists(PATH))
            {
                var create = File.Create(PATH);
                create.Close();
            }
            var contents = Read<List<string>>(PATH);

            foreach (var line in contents)
                WordBank.Add(new WordGenerator(line));
        }
        private T Read<T>(string filePath)
        {
            string text = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(text);
        }
    }
}
