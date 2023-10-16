using System;
using System.Runtime.InteropServices;
using System.Threading;

public class Input
{
    [DllImport("user32.dll")]
    public static extern short GetAsyncKeyState(ConsoleKey vKey);

    private Thread inputThread;
    private bool wasRightKeyPressed;
    private bool wasLeftKeyPressed;

    public bool IsRightKeyPressed { get; private set; }
    public bool IsLeftKeyPressed { get; private set; }
    public bool IsJumpKeyPressed { get; private set; }
    public bool IsRunKeyPressed { get; private set; }
    public bool IsIdle { get; private set; }

    public Input()
    {
        inputThread = new Thread(ReadKeys);
        inputThread.IsBackground = false;
        inputThread.Start();
    }

    private void ReadKeys()
    {
        while (true)
        {
            bool isRightKeyPressed = IsKeyDown(ConsoleKey.RightArrow);
            bool isLeftKeyPressed = IsKeyDown(ConsoleKey.LeftArrow);

            if (isRightKeyPressed && !wasRightKeyPressed)
            {
                // A tecla direita foi pressionada
                IsRightKeyPressed = true;
            }
            else if (!isRightKeyPressed && wasRightKeyPressed)
            {
                // A tecla direita foi liberada
                IsRightKeyPressed = false;
            }

            if (isLeftKeyPressed && !wasLeftKeyPressed)
            {
                // A tecla esquerda foi pressionada
                IsLeftKeyPressed = true;
            }
            else if (!isLeftKeyPressed && wasLeftKeyPressed)
            {
                // A tecla esquerda foi liberada
                IsLeftKeyPressed = false;
            }

            wasRightKeyPressed = isRightKeyPressed;
            wasLeftKeyPressed = isLeftKeyPressed;

            IsIdle = !IsRightKeyPressed && !IsLeftKeyPressed;
        }
    }

    private bool IsKeyDown(ConsoleKey key)
    {
        return (GetAsyncKeyState(key) < 0);
    }
}