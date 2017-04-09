using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigMom3Task
{
    class Program
    {
        static void Main(string[] args)
        {
            //!!!Use comma separator for input exp.:right: 123,4  wrong: 123.5 !!!
            double number = Convert.ToDouble(Console.ReadLine());
            PrintInWords(number);
            Console.ReadKey();
        }

        private static string[] tens = {
    "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",  "ten",
    "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private static string[] dozens = { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        public static void PrintInWords(double num)
        {
            int dollars = Convert.ToInt32(num);
            int cents = Math.Abs((int)((num - dollars) * (double)100));//Abs to make cents positive
            if (cents >0) { 
            Console.WriteLine("{0} {1} {2} {3}", ConvertToText(dollars),"dollars", ConvertToText(cents),"cents");
            }
            else
            {
                Console.WriteLine("{0} {1}", ConvertToText(dollars), "dollars");
            }

            }
        
        private static string ConvertToText(int num)
        {
            string result = " ";

            if (num == 0)
            {
                result = "zero";
            }
            if (num / 1000000000 > 0)//billion
            {
                result += ConvertToText(num / 1000000000) + " billion ";
                num = num % 1000000000;
            }
            if (num / 1000000 > 0)//million
            {
                result += ConvertToText(num / 1000000) + "million ";
                num = num % 1000000;
            }
            if (num / 1000 > 0)//thousand
            {
                result += ConvertToText(num / 1000) + " thousand ";
                num = num % 1000;
            }
            if (num / 100 > 0)//hundred
            {
                if (num / 100 == 1)//if 1 hundred
                {
                    result += ConvertToText(num / 100) + " hundred ";
                }
                else
                {
                    result += ConvertToText(num / 100) + " hundreds ";
                }

                num = num % 100;
            }
            if (num > 0)
            {//less than 100
                if (result != " ")
                    result += "and ";

                if (num / 10 < 2)
                {
                    result += tens[num];

                }
                else
                {
                    result += dozens[num / 10];
                    if (num % 10 > 0)
                    {
                        result += "-"+ tens[num % 10];
                    }
                }
            }
            return result;
        }


    }
}

