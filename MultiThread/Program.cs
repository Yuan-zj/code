using System;
using System.Globalization;
using System.Runtime.Serialization;
using AwaitAsyncLibrary;

namespace MultiThread
{
    class Program
    {
        /*
        static void Main(string[] args)
        {
            //string OpId = "002323";
            //string ticks = DateTime.Now.Ticks.ToString();
            //Random r = new Random();
            //string RandomStr = "";
            //string pattern = "abcdefghijklmnopqrstuvwxyz";
            //pattern += pattern.ToUpperInvariant();
            //pattern += "1234567890";
            //Console.WriteLine(pattern);
            //for (int i = 0; i < 17; i++)
            //{
            //    RandomStr += pattern[r.Next(pattern.Length)];
            //}
            //string token = $"{OpId}{ticks.Substring(0, 9)}{RandomStr}";
            //Console.WriteLine(ticks.Substring(0, 9));
            //Console.WriteLine(RandomStr);
            //Console.WriteLine(token);


            //double x = double.Parse("1.234", CultureInfo.InvariantCulture);
            //Console.WriteLine(x);

            //NumberFormatInfo f = new NumberFormatInfo();
            //f.CurrencySymbol = "$$";
            //Console.WriteLine(3.ToString("c7", f));

            //double dblValue = -12445.6789;
            //Console.WriteLine(dblValue.ToString("N", CultureInfo.InvariantCulture));
            //// Displays -12,445.68
            //Console.WriteLine(dblValue.ToString("N1",
            //                  CultureInfo.CreateSpecificCulture("sv-SE")));
            //// Displays -12 445,7

            //int intValue = 123456789;
            //Console.WriteLine(intValue.ToString("N1", CultureInfo.InvariantCulture));
            //// Displays 123,456,789.0


            double number = .2468013;
            Console.WriteLine(number.ToString("P", CultureInfo.InvariantCulture));
            // Displays 24.68 %
            Console.WriteLine(number.ToString("P",
                              CultureInfo.CreateSpecificCulture("hr-HR")));
            // Displays 24,68%
            Console.WriteLine(number.ToString("P1", CultureInfo.InvariantCulture));
            // Displays 24.7 %


        }
        */
        public static void Main()
        {
            try
            {
                {
                    new AwaitAsyncClassNew().Show();
                }
            }
            catch (Exception e)
            {

                throw;
            }
            Console.ReadKey();
        }
    }
}

