using System;


namespace Minesweeper
{
    /// <summary>
    /// This class defines the building blocks for the Grid class. Only
    /// Grid and Tile objects can directly access and modify Tile objecs.
    /// </summary>
    sealed class Tile
    {
        /*  

             INTERFACE METHODS:

             - Change tile state based on enum State.
             - Display tile (based on its state).
             - Get and Set int number.
             - Only Get bool hasBomb.

        */

        //-------------------------------------------------------------------------fields

        //PUBLIC:

        /// <summary>
        /// SetStateTo( ) argument.
        /// </summary>
        public enum States { covered, uncovered, flagged }
        public readonly bool hasBomb;

        //PRIVATE:

        private int number;
        private States state;
        private static readonly Random rnd = new Random();

        private const double CHANCE_OF_HAVING_BOMB = .2;

        //-------------------------------------------------------------------------properties

        /// <summary>
        /// Number of bombs surrounding the tile.
        /// </summary>
        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                if (value < 0) return;
                number = value;
            }
        }



        //-------------------------------------------------------------------------public methods

        /// <summary>
        /// Creates a Tile object. Whether it has a bomb or not is determined randomly.
        /// </summary>
        public Tile()
        {
            state = States.covered;
            number = 0;
            hasBomb = rnd.NextDouble() < CHANCE_OF_HAVING_BOMB;
        }

        public void Display()
        {
            switch (state)
            {
                case States.covered:
                    DisplayWith(" ", ConsoleColor.White, ConsoleColor.Black);
                    break;

                case States.uncovered:
                    if (hasBomb)                  
                        DisplayWith("X", ConsoleColor.Red, ConsoleColor.Black, false);  
                    
                    else if(number == 0)                  
                        DisplayWith(" ", ConsoleColor.Gray, ConsoleColor.Black, false);
                    
                    else
                        DisplayWith(number.ToString(), ConsoleColor.Gray, ConsoleColor.Black, false);

                    break;

                case States.flagged:
                    DisplayWith("F", ConsoleColor.White, ConsoleColor.DarkRed);
                    break;

                default:
                    Console.WriteLine("Tile Error: Landed on Defaut case (not intended) in Tile.cs/Display().");
                    break;
            }
        }

        public bool SetStateTo(States newState)
        {
            if(state == States.uncovered) return false;
            state = newState;
            return true;
        }


        //-------------------------------------------------------------------------private methods

        private void DisplayWith(string centerCharacter, ConsoleColor backColor, ConsoleColor foreColor, bool hasBrackets = true)
        {
            Console.BackgroundColor = backColor;
            Console.ForegroundColor = foreColor;
            if (hasBrackets)
            {
                Console.Write("[");
                Console.Write(centerCharacter);
                Console.Write("]");
            }
            else
            {
                Console.Write($" {centerCharacter} ");
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
