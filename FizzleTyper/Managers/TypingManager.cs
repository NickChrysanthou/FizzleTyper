using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FizzleTyper.Managers
{
    class TypingManager
    {
        private KeyboardState kb, oldKb;
        public char pressed { get; protected set; }
        public void Update()
        {
            oldKb = kb;
            kb = Keyboard.GetState();
            pressed = GetLetter();
            
        }
        public char GetLetter()
        {
            // First Row
            if (kb.IsKeyDown(Keys.Q) && oldKb.IsKeyUp(Keys.Q))
                return 'q';
            else if (kb.IsKeyDown(Keys.W) && oldKb.IsKeyUp(Keys.W))
                return 'w';
            else if (kb.IsKeyDown(Keys.E) && oldKb.IsKeyUp(Keys.E))
                return 'e';
            else if (kb.IsKeyDown(Keys.R) && oldKb.IsKeyUp(Keys.R))
                return 'r';
            else if (kb.IsKeyDown(Keys.T) && oldKb.IsKeyUp(Keys.T))
                return 't';
            else if (kb.IsKeyDown(Keys.Y) && oldKb.IsKeyUp(Keys.Y))
                return 'y';
            else if (kb.IsKeyDown(Keys.U) && oldKb.IsKeyUp(Keys.U))
                return 'u';
            else if (kb.IsKeyDown(Keys.I) && oldKb.IsKeyUp(Keys.I))
                return 'i';
            else if (kb.IsKeyDown(Keys.O) && oldKb.IsKeyUp(Keys.O))
                return 'o';
            else if (kb.IsKeyDown(Keys.P) && oldKb.IsKeyUp(Keys.P))
                return 'p';
            // Second Row
            else if (kb.IsKeyDown(Keys.A) && oldKb.IsKeyUp(Keys.A))
                return 'a';
            else if (kb.IsKeyDown(Keys.S) && oldKb.IsKeyUp(Keys.S))
                return 's';
            else if (kb.IsKeyDown(Keys.D) && oldKb.IsKeyUp(Keys.D))
                return 'd';
            else if (kb.IsKeyDown(Keys.F) && oldKb.IsKeyUp(Keys.F))
                return 'f';
            else if (kb.IsKeyDown(Keys.G) && oldKb.IsKeyUp(Keys.G))
                return 'g';
            else if (kb.IsKeyDown(Keys.H) && oldKb.IsKeyUp(Keys.H))
                return 'h';
            else if (kb.IsKeyDown(Keys.J) && oldKb.IsKeyUp(Keys.J))
                return 'j';
            else if (kb.IsKeyDown(Keys.K) && oldKb.IsKeyUp(Keys.K))
                return 'k';
            else if (kb.IsKeyDown(Keys.L) && oldKb.IsKeyUp(Keys.L))
                return 'l';
            // Third Row
            else if (kb.IsKeyDown(Keys.Z) && oldKb.IsKeyUp(Keys.Z))
                return 'z';
            else if (kb.IsKeyDown(Keys.X) && oldKb.IsKeyUp(Keys.X))
                return 'x';
            else if (kb.IsKeyDown(Keys.C) && oldKb.IsKeyUp(Keys.C))
                return 'c';
            else if (kb.IsKeyDown(Keys.V) && oldKb.IsKeyUp(Keys.V))
                return 'v';
            else if (kb.IsKeyDown(Keys.B) && oldKb.IsKeyUp(Keys.B))
                return 'b';
            else if (kb.IsKeyDown(Keys.N) && oldKb.IsKeyUp(Keys.N))
                return 'n';
            else if (kb.IsKeyDown(Keys.M) && oldKb.IsKeyUp(Keys.M))
                return 'm';

            return ' ';
        }
    }
}
