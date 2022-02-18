using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            Console.Title = "Training Calculator";

            //Console.WriteLine("Hello World!\n");
            //Console.BackgroundColor = ConsoleColor.Green;
            //Console.WriteLine("Hello World!\n");
            //Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Hello World! My brilliant idea. Version: -1\n");

            Random rnd = new Random();
            int a, b; // c, d, e... ets
            int res;
            int count = 0;
            int level = 1;
            char oper = '+';
            char operation1 = '+';
            char operation2 = '-';
            char operation3 = '*';
            char operation4 = '/';

            // https://docs.microsoft.com/ru-ru/dotnet/api/system.console.readkey?view=netframework-4.7.2&f1url=%3FappId%3DDev16IDEF1%26l%3DRU-RU%26k%3Dk(System.Console.ReadKey)
            ConsoleKeyInfo cki;
            Console.CursorTop = 20;
            do
            {
                a = rnd.Next(1, 99);
                b = rnd.Next(1, 99);
                switch (oper)
                {
                    case '+':res = a + b;
                        break;
                    default:
                }
                Console.CursorTop = 5;

                cki = Console.ReadKey(true);
                if (cki.Key >= ConsoleKey.D0 && cki.Key <= ConsoleKey.D9)
                {
                    Console.Beep();
                    Console.Write(((int)cki.Key - 48).ToString());
                    Console.CursorLeft = 0;
                }
                else
                {
                    Console.CursorTop = 20;
                    Console.Write(" --- You pressed ");
                    if ((cki.Modifiers & ConsoleModifiers.Alt) != 0) Console.Write("ALT+");
                    if ((cki.Modifiers & ConsoleModifiers.Shift) != 0) Console.Write("SHIFT+");
                    if ((cki.Modifiers & ConsoleModifiers.Control) != 0) Console.Write("CTL+");
                    //Console.Write(cki.Key.ToString());
                    Console.CursorLeft = 0;
                }
            } while (cki.Key != ConsoleKey.Escape && cki.Key != ConsoleKey.Q);
        }
    }
}
