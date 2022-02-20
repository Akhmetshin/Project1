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
            //StringBuilder buff = new StringBuilder(260);

            int level;
            level = (int)GetPrivateInt("SECTION", "LEVEL", 1, IniFile);
            //GetPrivateString("SECTION", "WELL", "", buff, 260, IniFile);
            uint beep= GetPrivateInt("SECTION", "BEEP", 1, IniFile);

            Random rnd = new Random();
            int a, b; // c, d, e... ets
            int aMin, aMax, bMin, bMax;
            int res;
            int count = 0;

            char[] oper = { '+', '-', '*', '/' };
            int operMax;
            int indexOper;
            int cursorLeft;

            // https://docs.microsoft.com/ru-ru/dotnet/api/system.console.readkey?view=netframework-4.7.2&f1url=%3FappId%3DDev16IDEF1%26l%3DRU-RU%26k%3Dk(System.Console.ReadKey)
            ConsoleKeyInfo cki;
            Console.CursorTop = 20;
            aMin = 1; aMax = 4; bMin = 1; bMax = 4;
            operMax = 1;
            res = 0;
            bool flagBreak = false;
            do
            {
                switch (level)
                {
                    case 1: aMin = 1;  aMax = 4;  bMin = 1;  bMax = 4;  operMax = 2; break;
                    case 2: aMin = 1;  aMax = 11; bMin = 1;  bMax = 11; operMax = 2; break;
                    case 3: aMin = 1;  aMax = 21; bMin = 1;  bMax = 21; operMax = 2; break;
                    case 4: aMin = 10; aMax = 51; bMin = 10; bMax = 51; operMax = 2; break;
                    case 5: aMin = 1;  aMax = 11; bMin = 1;  bMax = 11; operMax = 3; break;
                    case 6: aMin = 10; aMax = 51; bMin = 10; bMax = 51; operMax = 3; break;
                    case 7: aMin = 5;  aMax = 11; bMin = 5;  bMax = 11; operMax = 4; break;
                    case 8: aMin = 10; aMax = 51; bMin = 10; bMax = 51; operMax = 4; break;
                    case 9: aMin = 10; aMax = 21; bMin = 10; bMax = 21; operMax = 5; break;
                    case 10:aMin = 50; aMax = 101;bMin = 50; bMax = 101;operMax = 5; break;
                    default: break;
                }
                a = rnd.Next(aMin, aMax);
                b = rnd.Next(bMin, bMax);
                indexOper=rnd.Next(1,operMax);

                Console.CursorTop = 5;
                Console.CursorLeft = 0;
                Console.Write("                                 ");
                Console.CursorTop = 5;
                Console.CursorLeft = 0;

                switch (indexOper)
                {
                    case 1: res = a + b; Console.Write("{0} + {1} = ", a, b); break;
                    case 2: res = a - b; Console.Write("{0} - {1} = ", a, b); break;
                    case 3: res = a * b; Console.Write("{0} * {1} = ", a, b); break;
                    case 4: res = a / b; Console.Write("{0} / {1} = ", a, b); break;
                    // надо переходить на польскую нотацию, добавлять переменные и скобки.
                    default: break;
                }

                cursorLeft = Console.CursorLeft;
                string buff = res.ToString();
                int len = buff.Length;
                char key;

                for (int i = 0; i < len; i++)
                {
                    Console.CursorTop = 5;
                    cki = Console.ReadKey(true);
 
                    if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.Q) { flagBreak = true; break; }
                    
                    key = ' ';
                    if (cki.Key >= ConsoleKey.D0 && cki.Key <= ConsoleKey.D9) { key = (char)cki.Key; }
                    if (cki.Key >= ConsoleKey.NumPad0 && cki.Key <= ConsoleKey.NumPad9) { key = (char)(cki.Key - 48); }

                    if (key >= '0' && key <= '9')
                    {
                        Console.Write(key);
                        if (key == buff[i])
                        {
                            cursorLeft = Console.CursorLeft;
                            if (beep > 0) Console.Beep();
                        }
                        else
                        {
                            Console.CursorLeft = cursorLeft;
                            if (beep > 0) Console.Beep(1500, 150);
                            i--;
                        }
                    }
                    else
                    {
                        Console.CursorLeft = 0;
                        Console.CursorTop = 20;
                        Console.Write("                                               ");
                        Console.CursorLeft = 0;
                        Console.Write(" --- You pressed ");
                        if ((cki.Modifiers & ConsoleModifiers.Alt) != 0) Console.Write("ALT+");
                        if ((cki.Modifiers & ConsoleModifiers.Shift) != 0) Console.Write("SHIFT+");
                        if ((cki.Modifiers & ConsoleModifiers.Control) != 0) Console.Write("CTL+");
                        Console.Write(cki.Key.ToString());
                        Console.CursorLeft = cursorLeft;
                        if (beep > 0) Console.Beep(2000, 70);
                        i--;
                    }
                }
                if (!flagBreak) count++;

                Console.CursorLeft = 0;
                Console.CursorTop = 20;
                Console.Write("                                               ");
                //Console.CursorLeft = 0;
                //Console.CursorTop = 5;
                //Console.Write("                                               ");
            } while (!flagBreak);

            WritePrivateString("SECTION", "LEVEL", level.ToString(), IniFile);
        }
    }
}
