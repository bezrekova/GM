using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigMom
{
    //1 task
    class Program
    {

        public static void Main(string[] args)
        {
            int[,] arr = new int[2, 3];
            int[,] arrNew;
            FillArray(arr);
            Console.WriteLine("Old array: ");
            PrintArray(arr);

            TranspondArray(arr, out arrNew);
            Console.WriteLine("New Array:");
            PrintArray(arrNew);
            Console.ReadKey();
        }

        //fill array with random values
        public static void FillArray(int[,] array)
        {
            Random rand = new Random();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = rand.Next(1, 10);

                }
            }

        }

        private static void PrintArray(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {

                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j] + " ");

                }
                Console.WriteLine();
            }
        }

        private static void TranspondArray(int[,] array, out int[,] arrNew)
        {
            int row = array.GetLength(0);//rows num of old array
            int col = array.GetLength(1);//col num of old array
            arrNew = new int[col, row];//transponded array
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    arrNew[j, i] = array[i, j];

                }
            }
        }
    }
}
