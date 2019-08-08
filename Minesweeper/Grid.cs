using System;

namespace Minesweeper
{
    class Grid
    {
        public Tile[,] tiles;
        private readonly int xLength;
        private readonly int yLength;

        private const string OFFSET = "              ";

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

            SetupTilesNumbers();
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

        //--------------------------------------------------------------------------private methods

        private void SetupTilesNumbers()
        {
            for(int x = 0; x < xLength; x++)
            {
                for(int y = 0; y < yLength; y++)
                {
                    tiles[y, x].Number = GetNumberOfSurroundingBombs(y, x);
                }
            }
        }

        private int GetNumberOfSurroundingBombs(int y, int x)
        {
            int bombCount = 0;

            //Remember to use a try-catch block alternative instead.
            try { if (tiles[y + 1, x + 1].hasBomb) bombCount++; } catch (IndexOutOfRangeException) { }
            try { if (tiles[y + 1, x].hasBomb) bombCount++; } catch (IndexOutOfRangeException) { }
            try { if (tiles[y + 1, x - 1].hasBomb) bombCount++; } catch (IndexOutOfRangeException) { }
            try { if (tiles[y, x + 1].hasBomb) bombCount++; } catch (IndexOutOfRangeException) { }
            try { if (tiles[y, x - 1].hasBomb) bombCount++; } catch (IndexOutOfRangeException) { }
            try { if (tiles[y - 1, x + 1].hasBomb) bombCount++; } catch (IndexOutOfRangeException) { }
            try { if (tiles[y - 1, x].hasBomb) bombCount++; } catch (IndexOutOfRangeException) { }
            try { if (tiles[y - 1, x - 1].hasBomb) bombCount++; } catch (IndexOutOfRangeException) { }

            return bombCount;
        }

    }
}
