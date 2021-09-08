using System;
using System.Collections.Generic;
using System.Text;

namespace TestingDuringClass
{
    class Game
    {
        /// <summary>
        /// Printing the numbers in an array. 
        /// You must put your array and the number of indices within that array
        /// into the function when you call it.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arraySize"></param>
        void Numbers(int[] array, int arraySize)
        {
            Console.Write("[");

            //Gets each number in order from the array...
            for (int i = 0; i < arraySize; i++)
            {
                //...and prints each number
                Console.Write(" " + array[i] + " ");
            }

            Console.Write("]\n");
        }


        public void Run()
        {
            //Calling the Numbers function and give it an array and the number
            //of indices within that array
            Numbers(new int[] { 1, 2, 3 }, 3);
            Numbers(new int[] { 8, 9, 123, 1, 23, 1 }, 6);
        }
    }
}
