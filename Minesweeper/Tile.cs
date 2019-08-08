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

        //PRIVATE:

        private int number;
        private readonly bool hasBomb;
        private States state;
        private static readonly Random rnd = new Random();

        private const double CHANCE_OF_HAVING_BOMB = .5;

        //-------------------------------------------------------------------------properties

        /// <summary>
        /// True if the Tile object has a hidden bomb.
        /// </summary>
        public bool HasBomb { get; }

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
                        DisplayWith(number.ToString(), ConsoleColor.White, ConsoleColor.Black, false);

                    break;

                case States.flagged:
                    DisplayWith("F", ConsoleColor.White, ConsoleColor.Black);
                    break;

                default:
                    Console.WriteLine("Tile Error: Landed on Defaut case (not intended).");
                    break;
            }
        }

        public void SetStateTo(States newState)
        {
            state = newState;
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
