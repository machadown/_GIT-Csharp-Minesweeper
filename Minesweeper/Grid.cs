using System;

namespace Minesweeper
{
    class Grid
    {
        private Tile[,] tiles;
        private int xLength;
        private int yLength;

        private const string OFFSET = "          ";

        public Grid(int xLength, int yLength)
        {
            this.xLength = xLength;
            this.yLength = yLength;
            tiles = new Tile[yLength, xLength];
            for(int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    tiles[y, x] = new Tile();
                }
            }
        }

        public void Display()
        {
            Console.Write(OFFSET);

            //prints row of letters.
            Console.Write("   ");
            for (int x = 0; x < xLength; x++)
            {
                Console.Write($" {(char)(x + 'A')} ");
            }
            Console.WriteLine();

            for(int y = 0; y < yLength; y++)
            {
                Console.Write(OFFSET);

                //prints column of letters.
                Console.Write($" {(char)(y + 'A')} ");

                for (int x = 0; x < xLength; x++)
                {
                    tiles[y, x].Display();
                }

                Console.WriteLine();
            }
        }

        public void SetTileState(int x, int y, Tile.States state)
        {
            tiles[y, x].SetStateTo(state);
        }
       

    }
}
