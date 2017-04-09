using System;

namespace BigMom2Task
{
    class Program
    {

        private static int[,] source = null;

        static void Main(string[] args)
        {
            int frame_width, frame_height;
            Console.Write("Write frame_height: ");
            frame_height = Convert.ToInt32(Console.ReadLine());
            Console.Write("Write frame_width: ");
            frame_width = Convert.ToInt32(Console.ReadLine());

            source = new int[frame_height, frame_width];
            int[,] destination = null;

            //1)fill frame with random values
            Console.WriteLine("Source array:");
            FillArrayWithRandomValues(source);
            PrintArray(source);
            Console.WriteLine("***************************************************");

            //2)fill pic array from console:
            FillDestnArrayFromConsole(ref destination);
            Console.WriteLine("***************************************************");
            Console.WriteLine("Destination array:");
            PrintArray(destination);
            Console.WriteLine("***************************************************");
            //3)move pic to upper left frame corner

            for (int i = 0; i < destination.GetLength(0); i++)
            {
                for (int j = 0; j < destination.GetLength(1); j++)
                {
                    source[i, j] = destination[i, j];

                }
            }
            Console.WriteLine("Congrats! Destination array was successfully moved: ");
            PrintArray(source);
            Console.ReadKey();

        }

        private static void FillDestnArrayFromConsole(ref int[,] pic)
        {

            int pic_y, pic_x, pic_width, pic_height;

            Console.Write("Write pic_x:");
            pic_x = Convert.ToInt32(Console.ReadLine());
            Console.Write("Write pic_y: ");
            pic_y = Convert.ToInt32(Console.ReadLine());

            Console.Write("Write pic_width: ");
            pic_width = Convert.ToInt32(Console.ReadLine());

            Console.Write("Write pic_height: ");
            pic_height = Convert.ToInt32(Console.ReadLine());

            //check input numbers
            if (pic_x >= source.GetLength(1))
            {
                Console.WriteLine("Choose smaller x indent");
                FillDestnArrayFromConsole(ref pic);
            }
            else if (pic_y >= source.GetLength(0))
            {
                Console.WriteLine("Choose smaller y indent");
                FillDestnArrayFromConsole(ref pic);
            }
            else if (source.GetLength(1) - pic_width <= 0 | pic_width == 0)
            {
                Console.WriteLine("Choose other pic_width");
                FillDestnArrayFromConsole(ref pic);
            }
            else if (source.GetLength(0) - pic_height <= 0 | pic_height == 0)
            {
                Console.WriteLine("Choose other pic_height");
                FillDestnArrayFromConsole(ref pic);
            }
            else
            {//if it is all right
                CreatePicArrayDueToCoordinates(out pic, pic_y, pic_x, pic_width, pic_height);
            }

        }

        private static void CreatePicArrayDueToCoordinates(out int[,] pic, int pic_y, int pic_x, int pic_width, int pic_height)
        {
            pic = new int[pic_height, pic_width];

            for (int i = 0, t = pic_y; i < pic_height; t++, i++)
            {
                for (int j = 0, m = pic_x; j < pic_width; m++, j++)
                {
                    pic[i, j] = source[t, m];
                }
            }
        }//method

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

        public static void FillArrayWithRandomValues(int[,] array)
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
    }
}
