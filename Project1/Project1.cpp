// Project1.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include <Windows.h>

int main()
{
    HANDLE hnd = GetStdHandle(STD_OUTPUT_HANDLE);
    CONSOLE_CURSOR_INFO     cursorInfo;
    GetConsoleCursorInfo(hnd, &cursorInfo);
    //cursorInfo.bVisible = false;
    //SetConsoleCursorInfo(hnd, &cursorInfo);
    SetConsoleTextAttribute(hnd, 0xc);
    SetConsoleOutputCP(1251);

    CONSOLE_FONT_INFOEX cfi_old;
    cfi_old.cbSize = sizeof(cfi_old);
    GetCurrentConsoleFontEx(hnd, FALSE, &cfi_old);

    CONSOLE_FONT_INFOEX cfi;
    cfi.cbSize = sizeof(cfi);
    cfi.nFont = 0;
    cfi.dwFontSize.X = 0;                   // Width of each character in the font
    cfi.dwFontSize.Y = 22;                  // Height
    cfi.FontFamily = FF_DONTCARE;
    cfi.FontWeight = FW_NORMAL;
    wcscpy_s(cfi.FaceName, L"Consolas"); // Choose your font
    SetCurrentConsoleFontEx(hnd, FALSE, &cfi);

    std::cout << "Hello World!\n";

    // https://stackoverflow.com/questions/4053837/colorizing-text-in-the-console-with-c
    //for (int k = 1; k < 255; k++)
    //{
    //    // pick the colorattribute k you want
    //    SetConsoleTextAttribute(hnd, k);
    //    std::cout << k << " I want to be nice today!" << std::endl;
    //}
    
    HWND wnd = GetConsoleWindow();

    // https://docs.microsoft.com/en-us/windows/win32/winmsg/using-messages-and-message-queues
    MSG msg;
    BOOL bRet;
    while ((bRet = GetMessage(&msg, GetConsoleWindow(), 0, 0)) != 0)
    {
        if (msg.message == VK_ESCAPE)
        {
            break;
        }
        if (msg.wParam == WM_KEYDOWN)
        {
            break;
        }
        //switch (msg.message)
        //{
        //case WM_KEYDOWN:
        //}
        if (bRet == -1)
        {
            // handle the error and possibly exit
        }
        else
        {
            TranslateMessage(&msg);
            DispatchMessage(&msg);
        }
    } 

    int c;
    do
    {
        c = getchar();
//        if (c == 'z') break;
        int x = c;
    }while (c != 'z');

    printf("\n\n\tКонец обработки.\n\n");

    Sleep(1500);

    SetCurrentConsoleFontEx(hnd, FALSE, &cfi_old);
}
