using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

using FizzleTyper.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FizzleTyper.Managers
{
    public class WordManager : Component
    {
        private Random rand;
        private int next = 0;
        private TypingManager typeManager;

        internal List<WordGenerator> WordBank;
        internal static List<WordGenerator> ActiveList;
        private float currentTime = 0f;
        internal const double SpawnTimeSeconds = 1.00;

        public override void Init(ContentManager Content) 
        {
            typeManager = new TypingManager();
            ActiveList = new List<WordGenerator>();
            rand = new Random();
        }
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

                if (pressed == currentLetter)
                    ActiveList[0].Word = ActiveList[0].Word.Remove(0, 1);

                if (ActiveList[0].Word == string.Empty)
                    ActiveList[0].visible = false;
            }

            for (int i = 0; i < ActiveList.Count; i++)
            {
                WordGenerator word = ActiveList[i];
                word.Update(gameTime);
                
                // If a word is invisible and there is more then one in the list, remove current word from active list
                if (!word.visible && ActiveList.Count >= 1)
                    ActiveList.RemoveAt(i);
            }
        }
        // Removes all words from activelist
        public void ClearScreen() => ActiveList.Clear();
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var word in ActiveList)
                if (word.visible)
                    word.Draw(spriteBatch);
        }
        public void PopulateList()
        {
            const string path = "wordbank.json";
            WordBank = new List<WordGenerator>();

            if (!File.Exists(path))
            {
                var create = File.Create(path);
                create.Close();
            }
            var contents = Read<List<string>>(path);

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
