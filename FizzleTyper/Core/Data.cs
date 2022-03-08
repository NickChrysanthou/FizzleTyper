using Microsoft.Xna.Framework.Graphics;
using System;

namespace FizzleTyper.Core
{
    public static class Data
    {
        public static string Title { get; set; } = "Fizzle's Typing Game!";
        public static int ScreenW { get; set; } = 1600;
        public static int ScreenH { get; set; } = 900;
        public static bool Exit { get; set; } = default(bool);

        public enum GameStates { Menu, Settings, Game, }
        public static GameStates CurrentState { get; set; } = GameStates.Menu;

        // Game Variables
        public static SpriteFont wordfont { get; set; }
        public static int Lives { get; set; } = 5;
        public static float Pitch(Random random,float min, float max) => (float)(random.NextDouble() * (max - min) + min);
    }
}
/*
TODO:
    People I need to credit:
    Credit   DontMind8.blogspot.com | heart sprite

 */