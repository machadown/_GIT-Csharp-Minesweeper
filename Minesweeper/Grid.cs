using System;

namespace Minesweeper
{
    class Grid
    {
        private readonly int xLength;
        private readonly int yLength;

        private const string OFFSET = "              ";

        bool[,] wereChecked;

        public Tile[,] Tiles { get; }

        public Grid(int xLength, int yLength)
        {
            this.xLength = xLength;
            this.yLength = yLength;
            Tiles = new Tile[yLength, xLength];
            for(int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    Tiles[y, x] = new Tile();
                }
            }

            wereChecked = new bool[yLength, xLength];

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
                    Tiles[y, x].Display();
                }

                Console.WriteLine();
            }
        }

        public bool SetTileState(int x, int y, Tile.States state)
        {
            if(state == Tile.States.uncovered && Tiles[y, x].Number == 0 && !Tiles[y, x].hasBomb)
            {
                UncoverSurroudings(y, x);
                return true;
            }

            if (Tiles[y, x].hasBomb)
            {
                SetOffMines();
                return true;
            } 

            return Tiles[y, x].SetStateTo(state);
        }

        //--------------------------------------------------------------------------private methods

        private void SetOffMines()
        {
            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    if (Tiles[y, x].hasBomb)
                    {
                        Tiles[y, x].SetStateTo(Tile.States.uncovered);
                    }
                }
            }
        }

        private void SetupTilesNumbers()
        {
            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    Tiles[y, x].Number = GetNumberOfSurroundingBombs(y, x);
                }
            }
        }

        private int GetNumberOfSurroundingBombs(int y, int x)
        {
            int bombCount = 0;

            //Remember to use a try-catch block alternative instead.
            try { if (Tiles[y + 1, x + 1].hasBomb) bombCount++; } catch (IndexOutOfRangeException) { }
            try { if (Tiles[y + 1, x].hasBomb) bombCount++; } catch (IndexOutOfRangeException) { }
            try { if (Tiles[y + 1, x - 1].hasBomb) bombCount++; } catch (IndexOutOfRangeException) { }
            try { if (Tiles[y, x + 1].hasBomb) bombCount++; } catch (IndexOutOfRangeException) { }
            try { if (Tiles[y, x - 1].hasBomb) bombCount++; } catch (IndexOutOfRangeException) { }
            try { if (Tiles[y - 1, x + 1].hasBomb) bombCount++; } catch (IndexOutOfRangeException) { }
            try { if (Tiles[y - 1, x].hasBomb) bombCount++; } catch (IndexOutOfRangeException) { }
            try { if (Tiles[y - 1, x - 1].hasBomb) bombCount++; } catch (IndexOutOfRangeException) { }

            return bombCount;
        }

        private void UncoverSurroudings(int y, int x)
        {
            Tiles[y, x].SetStateTo(Tile.States.uncovered);
            wereChecked[y, x] = true;

            UncoverThisDirection(1, 0, y, x);
            UncoverThisDirection(0, 1, y, x);
            UncoverThisDirection(-1, 0, y, x);
            UncoverThisDirection(0, -1, y, x);
            UncoverThisDirection(1, 1, y, x);
            UncoverThisDirection(-1, -1, y, x);
            UncoverThisDirection(-1, 1, y, x);
            UncoverThisDirection(1, -1, y, x);

            MoveThisMethodsPosition(1, 1, y, x);
            MoveThisMethodsPosition(-1, 1, y, x);
            MoveThisMethodsPosition(-1, -1, y, x);
            MoveThisMethodsPosition(1, -1, y, x);
            MoveThisMethodsPosition(1, 0, y, x);
            MoveThisMethodsPosition(-1, 0, y, x);
            MoveThisMethodsPosition(0, -1, y, x);
            MoveThisMethodsPosition(0, 1, y, x);
        }

        private void UncoverThisDirection(int yDir, int xDir, int y, int x)
        {
            try
            {
                if (!Tiles[y + yDir, x + xDir].hasBomb)
                    Tiles[y + yDir, x + xDir].SetStateTo(Tile.States.uncovered);
            }
            catch (IndexOutOfRangeException) { }
        }

        private void MoveThisMethodsPosition(int yDir, int xDir, int y, int x)
        {
            try
            {
                if (wereChecked[y + yDir, x + xDir] || Tiles[y + yDir, x + xDir].Number != 0) return;
                if (!Tiles[y + yDir, x + xDir].hasBomb)
                {
                    UncoverSurroudings(y + yDir, x + xDir);
                }
            }
            catch (IndexOutOfRangeException) { }
        }

    }
}
