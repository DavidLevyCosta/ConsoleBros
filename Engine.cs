using System;
using System.Threading;
using System.Timers;

namespace ConsoleBros
{
    internal class Engine
    {
        private static System.Timers.Timer timer;
        private CanvaManager canva_manager;
        private string screen;
        private int walking_frame = 3;
        private char[,] player;
        private int current_frame = 0;
        public Engine(int fps, int canva_width, int canva_height)
        {
            canva_manager = new CanvaManager(canva_width, canva_height);
            timer = new System.Timers.Timer(1000 / fps);
            timer.Elapsed += FrameUpdate;
        }

        public void Start()
        {
            canva_manager.StartCanva();
            timer.Enabled = true;
            timer.AutoReset = true;
        }


        internal void FrameUpdate(Object source, ElapsedEventArgs e)
        {
            Update();
            Draw();
        }

        public void Update()
        {

            UpdateAnimation();

            player.SetPosition(canva_manager.canva, 0, 0);
            screen = canva_manager.CreateCanvaDraw();

            current_frame++;
        }

        public void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(screen);
        }

        public void UpdateAnimation()
        {
            if (walking_frame > 1) walking_frame--;
            else walking_frame = 3;
            player = Player.InvokePlayerSprite(walking_frame);

        }

        public static void KeepItRunning()
        {
            while (true) Thread.Sleep(1000);
        }
    }
}
