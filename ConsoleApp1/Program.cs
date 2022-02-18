using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        // наверное, что-то - как-то нужно будет запоминать.
        // Импорт функции GetPrivateProfileInt (для чтения значений) из библиотеки kernel32.dll
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileInt")]
        private static extern uint GetPrivateInt(string section, string key, int def, string path);

        // Импорт функции GetPrivateProfileString (для чтения значений) из библиотеки kernel32.dll
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        private static extern int GetPrivateString(string section, string key, string def, StringBuilder buffer, int size, string path);

        // Импорт функции WritePrivateProfileString (для записи значений) из библиотеки kernel32.dll
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
        private static extern int WritePrivateString(string section, string key, string str, string path);


        static void Main(string[] args)
        {
            Console.Title = "Training Calculator";

            Console.Clear();

            //Console.BackgroundColor = ConsoleColor.Green;
            //Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Hello World! My brilliant idea. Version: -1\n");

            string location = System.Reflection.Assembly.GetEntryAssembly().Location;
            string IniFile = location.Replace(".exe", ".ini");
            StringBuilder buff = new StringBuilder(260);

            int level;
            level = (int)GetPrivateInt("SECTION", "LEVEL", 1, IniFile);
            //GetPrivateString("SECTION", "WELL", "", buff, 260, IniFile);

            Random rnd = new Random();
            int a, b; // c, d, e... ets
            int res;
            int count = 0;

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
                    //default:
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

            WritePrivateString("SECTION", "LEVEL", level.ToString(), IniFile);
        }
    }
}
