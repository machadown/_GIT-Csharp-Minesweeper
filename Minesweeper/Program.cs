using System;
using System.Threading;

namespace Minesweeper
{
    class Program
    {
        static Random r = new Random();

        static void Main()
        {
            Grid grid = new Grid(26, 26);
            grid.Display();

            Thread.Sleep(1000);

            for(int i = 0; i < 26; i++)
            {
                for(int j = 0; j < 26; j++)
                {
                    if(r.NextDouble() < .5)
                    {
                        grid.SetTileState(i, j, Tile.States.uncovered);
                    }
                }
            }

            

            Console.Clear();

            grid.Display();
        }
    

    }
}
