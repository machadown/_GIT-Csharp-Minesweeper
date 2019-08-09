using System;
using System.Threading;

namespace Minesweeper
{
    static class MainClass
    {
        private static Grid grid;
        private static readonly Translator translator = new Translator();
        private static int width;
        private static int height;
        private static int[] playerInput;
        private static bool isNotOver = true;

        static void Main()
        {
            FirstPrompt();
            Setup();
            MainLoop();
        }

        private static void FirstPrompt()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("                                               ***** MINESWEEPER *****");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Welcome to Minesweeper!\n");
            try
            {
                Console.Write(">>> Enter field width (max 26): ");
                width = int.Parse(Console.ReadLine());

                Console.Write(">>> Enter field height (max 26): ");
                height = int.Parse(Console.ReadLine());

            }
            catch (FormatException)
            {
                ErrorMessage();
                FirstPrompt();
            }
        }

        private static void Setup()
        {
            Console.WindowHeight = 40;
            Console.WindowWidth = 120;
            grid = new Grid(width, height);
        }

        private static void MainLoop()
        {
            while (isNotOver)
            {
                Display();
                ClickPrompt();
                Compute();
            }
        }

        private static void Display()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("                                             ***** MINESWEEPER *****");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            grid.Display();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        }

        private static void ClickPrompt()
        {
            Console.Write(">>> Select tile: ");
            try
            {
                playerInput = translator.GetNumbersFrom(Console.ReadLine());
                if (playerInput.Length > 3 || playerInput.Length < 2) { ErrorMessage(); MainLoop(); }
            }
            catch (FormatException)
            {
                ErrorMessage();
                MainLoop();
            }
        }

        private static void Compute()
        {
            int x;
            int y;
            int state;

            if (playerInput.Length == 3)
            {
                state = playerInput[0];
                x = playerInput[1];
                y = playerInput[2];
            }
            else
            {
                state = 0;
                x = playerInput[0];
                y = playerInput[1];
            }

            if (!grid.SetTileState(x, y, ToState(state)))
            {
                ErrorMessage();
                MainLoop();
            }

            if (grid.Tiles[y, x].hasBomb) EndGame();
        }

        private static void ErrorMessage()
        {
            Console.WriteLine("INVALID INPUT!");
            Thread.Sleep(1000);
        }

        private static Tile.States ToState(int state)
        {
            switch (state)
            {
                case 5: // F
                    return Tile.States.flagged;
                case 20: // U
                    return Tile.States.covered;
                default:
                    return Tile.States.uncovered;
            }
        }

        private static void EndGame()
        {
            isNotOver = false;
            grid.SetOffMines();
            Display();
            Console.WriteLine("GAME OVER");
        }

    }
}
