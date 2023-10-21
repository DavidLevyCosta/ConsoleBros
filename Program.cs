using System.Diagnostics;

namespace ConsoleBros
{
    internal class Program
    {
        public const int SCREEN_WIDTH = 180 * 2;
        public const int SCREEN_HEIGHT = 224;

        static Engine engine = new Engine(60, SCREEN_HEIGHT, SCREEN_WIDTH);
        static void Main(string[] args)
        {
            Start();
            engine.Start();
            
        }

        internal static void Start()
        {
            //SpriteHandling.WriteTilesToFile(SpriteHandling.ReadTileMap(), "../../../Sprites/assets/tile_map.txt");
            ConsoleFont.SetSize(2);
            Console.SetWindowSize(SCREEN_WIDTH * 2, SCREEN_HEIGHT);
            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;
            Console.CursorVisible = false;
            Console.SetWindowSize((Program.SCREEN_WIDTH * 2) + 1, Program.SCREEN_HEIGHT);

            Thread.Sleep(1000);
        }
    }
}