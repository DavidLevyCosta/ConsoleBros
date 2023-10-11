using ConsoleBros;

namespace ConsoleBros
{
    internal class Program
    {
        const int SCREEN_WIDTH = 156 * 2;
        const int SCREEN_HEIGHT = 140;


        static Engine engine = new Engine(24, SCREEN_HEIGHT, SCREEN_WIDTH);
        static void Main(string[] args)
        {

            Player.SliceFrames();
            Console.CursorVisible = false;
            Console.BufferWidth = SCREEN_WIDTH;
            Console.BufferHeight = SCREEN_HEIGHT;
            ConsoleFont.SetSize(3);
            Console.SetWindowSize(SCREEN_WIDTH + 100, SCREEN_HEIGHT + 100);

            engine.Start();
            Engine.KeepItRunning();

        }
    }
}