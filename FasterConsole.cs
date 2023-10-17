using ConsoleBros;
using System;
using System.Text;


class FasterConsole
{
    static Encoding encoding = Encoding.ASCII; // Use the unicode table
    public static void Write(StringBuilder sb)
    {
        IntPtr hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
        COORD bufferSize = new COORD((short)Console.WindowWidth, (short)(sb.Length / Console.WindowWidth + 1));
        COORD bufferCoord = new COORD(0, 0);
        SMALL_RECT writeRegion = new SMALL_RECT(0, 0, (short)(Console.WindowWidth - 1), (short)(sb.Length / Console.WindowWidth));
        CHAR_INFO[] consoleBuffer = new CHAR_INFO[bufferSize.X * bufferSize.Y];

        // Convert the entire StringBuilder to a byte array
        byte[] bytes = encoding.GetBytes(sb.ToString());

        // Fill the console buffer with data
        int index = 0;

        for (int y = 0; y < bufferSize.Y; y++)
        {
            for (int x = 0; x < bufferSize.X; x++)
            {
                if (index < bytes.Length)
                {
                    consoleBuffer[y * bufferSize.X + x].AsciiChar = bytes[index];
                    consoleBuffer[y * bufferSize.X + x].Attributes = 7;
                    index++;
                }
            }
        }

        // Set the console buffer size and write the console buffer to the screen
        SetConsoleScreenBufferSize(hConsole, bufferSize);
        WriteConsoleOutput(hConsole, consoleBuffer, bufferSize, bufferCoord, ref writeRegion);
    }

    private const int STD_OUTPUT_HANDLE = -11;

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct COORD
    {
        public short X;
        public short Y;

        public COORD(short x, short y)
        {
            X = x;
            Y = y;
        }
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
    public struct CHAR_INFO
    {
        [System.Runtime.InteropServices.FieldOffset(0)]
        public byte AsciiChar;
        [System.Runtime.InteropServices.FieldOffset(2)]
        public UInt16 Attributes;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct SMALL_RECT
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;

        public SMALL_RECT(short left, short top, short right, short bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }

    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr GetStdHandle(int nStdHandle);

    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    static extern bool WriteConsoleOutput(IntPtr hConsoleOutput, CHAR_INFO[] lpBuffer,
       COORD dwBufferSize, COORD dwBufferCoord, ref SMALL_RECT lpWriteRegion);

    [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
    static extern bool SetConsoleScreenBufferSize(IntPtr hConsoleOutput, COORD dwSize);
}