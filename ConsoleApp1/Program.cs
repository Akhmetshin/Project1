using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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

        //public static int saveBufferWidth;
        //public static int saveBufferHeight;
        //public static int saveWindowHeight;
        //public static int saveWindowWidth;
        //public static bool saveCursorVisible; <- оставил эти комментарии как следы моих усилий по управлению окном и шрифтами. много времени потерял. проще через ярлык

        static void Main(string[] args)
        {
            //Console.Title = "Training Calculator";
            Console.Title = "CalcTrain";

            //saveBufferWidth = Console.BufferWidth;
            //saveBufferHeight = Console.BufferHeight;
            //saveWindowHeight = Console.WindowHeight;
            //saveWindowWidth = Console.WindowWidth;
            //saveCursorVisible = Console.CursorVisible;

            //// Set the smallest possible window size before setting the buffer size.
            //Console.SetWindowSize(1, 1);
            //Console.SetBufferSize(60, 20);
            //Console.SetWindowSize(60, 20);

            //ConsoleHelper.SetCurrentFont("Consolas", 20); <- установка шрифта. лучше через ярлык. файл ConsoleHelper.cs удалил

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Hello World! My brilliant idea. Version: -1");
            Console.CursorTop = 0;
            Console.CursorLeft = 16;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("brilliant");
            Console.ForegroundColor = ConsoleColor.Red;

            //Console.CursorTop = 15;
            //Console.CursorLeft = 0;
            //Console.ForegroundColor = ConsoleColor.DarkMagenta;
            //Console.Write("?? UWP, Android, DirectX, OpenGL, iMac ??\n");
            //Console.ForegroundColor = ConsoleColor.Red;

            string location = System.Reflection.Assembly.GetEntryAssembly().Location;
            string IniFile = location.Replace(".exe", ".ini");

            int level;
            level = (int)GetPrivateInt("SECTION", "LEVEL", 1, IniFile);
            uint beep= GetPrivateInt("SECTION", "BEEP", 1, IniFile); // <- для хакеров)

            Random rnd = new Random();
            int a, b; // c, d, e... ets
            int aMin, aMax, bMin, bMax;
            int res;
            double resD = 1;
            int count = 0;

            //char[] oper = { '+', '-', '*', '/' };
            int operMax;
            int indexOper;
            int cursorLeft;

            ConsoleKeyInfo cki;
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

                if (indexOper == 4)
                {
                    a = rnd.Next(50, 101);
                    b = rnd.Next(2, 15);
                }
                //a = 2; b = 100; indexOper = 4;
                //a = 1000; b = 900; indexOper = 4;
                if ((indexOper == 2 || indexOper == 4) && b > a) (b, a) = (a, b); // потому что версия -1.
                if (indexOper == 3)
                {
                    a = rnd.Next(10, 21);
                    b = rnd.Next(2, 15);
                }

                Console.CursorTop = 3;
                Console.CursorLeft = 0;
                Console.Write("                                 ");
                Console.CursorTop = 3;
                Console.CursorLeft = 0;

                switch (indexOper)
                {
                    case 1: res = a + b; Console.Write("{0} + {1} = ", a, b); break;
                    case 2: res = a - b; Console.Write("{0} - {1} = ", a, b); break;
                    case 3: res = a * b; Console.Write("{0} * {1} = ", a, b); break;
                    case 4: resD= (double)a / b; Console.Write("{0} / {1} = ", a, b); break;
                    // надо переходить на польскую нотацию, добавлять переменные и скобки.
                    default: break;
                }

                cursorLeft = Console.CursorLeft;
                string buff;
                int len = 0;
                if (indexOper != 4)
                {
                    buff = res.ToString();
                    len = buff.Length;
                }
                else
                {
                    buff = resD.ToString("F");
                    len = buff.IndexOf('.');
                }
                char key;

                for (int i = 0; i < len; i++)
                {
                    Console.CursorTop = 3;
                    Console.CursorVisible = true;
                    cki = Console.ReadKey(true);
 
                    if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.Q) { flagBreak = true; break; }
                    if (cki.Key == ConsoleKey.Tab) { count--; break; }

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
                        Console.CursorTop = 12;
                        Console.CursorVisible = false;
                        Console.Write("                                               ");
                        Console.CursorLeft = 0;
                        // https://docs.microsoft.com/ru-ru/dotnet/api/system.console.readkey?view=netframework-4.7.2&f1url=%3FappId%3DDev16IDEF1%26l%3DRU-RU%26k%3Dk(System.Console.ReadKey)
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

                Console.CursorVisible = false;

                if (!flagBreak)
                {
                    count++;
                    if (indexOper == 4 && len < buff.Length)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("{0} (ok)", buff.Substring(len));
                        Console.ForegroundColor = ConsoleColor.Red;
                        Thread.Sleep(500);
                    }
                    if (count >= 10) { level++; count = 0; }

                    Console.CursorLeft = 0;
                    Console.CursorTop = 12;
                    Console.Write("                                               ");
                    Console.CursorLeft = 0;
                    Console.CursorTop = 5;
                    Console.Write("                                               ");
                    Console.CursorLeft = 0;
                    Console.CursorTop = 5;
                    if (level > 10)
                    {
                        level = 10;
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write("level: max    count = {0}", count, level);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.Write("level: {1}    count = {0}", count, level);
                    }
                }

                Thread.Sleep(500);

            } while (!flagBreak);

            WritePrivateString("SECTION", "LEVEL", level.ToString(), IniFile);

            //Console.Clear();
            //Console.SetWindowSize(1, 1);
            //Console.SetBufferSize(saveBufferWidth, saveBufferHeight);
            //Console.SetWindowSize(saveWindowWidth, saveWindowHeight);
            //Console.CursorVisible = saveCursorVisible;
        }
    }
}

/*
 * и добавить таймер
 */