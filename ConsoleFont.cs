using System.Runtime.InteropServices;

// basicamente essa classe me ajuda a definir o tamanho da fonte dentro do código (não faço ideia de como funciona)
public class ConsoleFont
{
    private const int STD_OUTPUT_HANDLE = -11;
    private static IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal unsafe struct CONSOLE_FONT_INFO_EX
    {
        internal uint cbSize;
        internal uint nFont;
        internal COORD dwFontSize;
        internal int FontFamily;
        internal int FontWeight;
        fixed char FaceName[32];
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct COORD
    {
        internal short X;
        internal short Y;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    static extern bool SetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool bMaximumWindow, ref CONSOLE_FONT_INFO_EX ConsoleCurrentFontEx);

    public static void SetSize(int size)
    {
        CONSOLE_FONT_INFO_EX cfi = new CONSOLE_FONT_INFO_EX();
        cfi.cbSize = (uint)Marshal.SizeOf(cfi);
        cfi.nFont = 0;
        cfi.dwFontSize.X = 0;
        cfi.dwFontSize.Y = (short)size;
        cfi.FontFamily = 0;
        cfi.FontWeight = 400;

        IntPtr hnd = GetStdHandle(STD_OUTPUT_HANDLE);
        if (hnd != INVALID_HANDLE_VALUE)
        {
            SetCurrentConsoleFontEx(hnd, false, ref cfi);
        }
    }
}