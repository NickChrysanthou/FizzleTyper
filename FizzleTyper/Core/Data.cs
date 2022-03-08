using Microsoft.Xna.Framework.Graphics;

namespace FizzleTyper.Core
{
    public static class Data
    {
        public static string Title { get; set; } = "Fizzle's Typing Game!";
        public static int ScreenW { get; set; } = 1600;
        public static int ScreenH { get; set; } = 900;
        public static bool Exit { get; set; } = default(bool);

        public enum GameStates { Menu, Game }
        public static GameStates CurrentState { get; set; } = GameStates.Menu;

        // Game Variables
        public static SpriteFont wordfont { get; set; }

    }
}
