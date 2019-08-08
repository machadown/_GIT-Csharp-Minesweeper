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
                Console.WriteLine("INVALID INPUT!");
                Thread.Sleep(1000);
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
            while (true)
            {
                Display();
                ClickPrompt();

            }
        }

        private static void Display()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("                                             ***** MINESWEEPER *****");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            grid.Display();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        }

        private static void ClickPrompt()
        {
            Console.Write(">>> Select tile: ");
            int[] coordenates = translator.GetNumbersFrom(Console.ReadLine());

        }

    }
}
