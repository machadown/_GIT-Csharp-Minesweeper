using System;

namespace Minesweeper
{
    class Translator
    { 

        public Translator() {}

        /// <summary>
        /// Returns an array of integers that correspond to the position of letters in the alphabet starting at 0.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int[] GetNumbersFrom(string input)
        {
            string upperInput = input.ToUpper();
            int[] numbers = new int[input.Length];

            for(int i = 0; i < input.Length; i++)
            {
                numbers[i] = upperInput[i] - 65;
            }

            return numbers;
        }
    }
}
