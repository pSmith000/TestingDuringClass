using System;
using System.Collections.Generic;
using System.Text;

namespace TestingDuringClass
{
    class Game
    {

        string value1;
        float number;
        float number1;
        float number2;
        float number3;
        float number4;
        float number5;

        /// <summary>
        /// Printing the numbers in an array. 
        /// You must put your array and the number of indices within that array
        /// into the function when you call it.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arraySize"></param>
        void PrintNumbers(int[] array, int arraySize)
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

        float NumReturn(string description)
        {

            int invalidInput = 0;
            float answer = 0;

            while (!(invalidInput == 1))
            {
                Console.Write(description);

                value1 = Console.ReadLine();

                if (float.TryParse(value1, out number))
                {
                    invalidInput = 1;
                    answer = number;
                }
                else
                {
                    invalidInput = 0;
                    Console.WriteLine("invalid input");
                    Console.WriteLine("press ENTER to continue");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            return answer;
        }

        void NumArray(string description)
        {
            float smallestNum;
            float largestNum;

            Console.WriteLine(description);

            number1 = NumReturn("First Number: ");

            number2 = NumReturn("Second Number: ");

            number3 = NumReturn("Third Number: ");

            number4 = NumReturn("Fourth Number: ");

            number5 = NumReturn("Fifth Number: ");

            float[] newArray = new float[5] {number1, number2, number3, number4, number5};

            smallestNum = number1;
            largestNum = number1;

            foreach (int i in newArray)
            {
                if (i < smallestNum)
                {
                    smallestNum = i;
                }
                if (i > largestNum)
                {
                    largestNum = i;
                }
            }

            Console.WriteLine("\nThe smallest number in your array is " + smallestNum);
            Console.WriteLine("The largest number in your array is " + largestNum);


        }


        public void Run()
        {
            //Calling the Numbers function and give it an array and the number
            //of indices within that array
            NumArray("Enter 5 numbers");
        }
    }
}
