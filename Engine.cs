using System.Diagnostics;
using System.Text;
using System.Timers;

namespace ConsoleBros
{
    internal class Engine
    {
        private static System.Timers.Timer frame_timer; // cria o timer de frames
        private CanvaManager canva_manager;
        public Player player;
        private Input input;
        private StringBuilder screen; // tela que receberá a informação do canva
        public Engine(int fps, int canva_width, int canva_height) // cria o motor com informação de tamanho do canva e a quantidade de frames por segundo
        {
            player = new Player(20, 20);
            input = new Input();
            canva_manager = new CanvaManager(player, canva_width, canva_height);
            frame_timer = new System.Timers.Timer(1000 / fps);
            frame_timer.Elapsed += FrameUpdate;
        }

        public void Start() // inicia o canva (desnulifica os index) e a atualizão dos frames
        {
            CanvaManager.StartCanva(canva_manager.canva); // LEMBRAR DE VIR TROCAR ISSO DEPOIS <-
            frame_timer.Enabled = true; // inicia o frame timer
            frame_timer.AutoReset = true; // faz ele resetar quando acabar o timer
        }


        internal void FrameUpdate(Object source, ElapsedEventArgs e) // a cada frame
        {
            Update(); // auto explicativo
            Draw(); // desenha o canva no console
        }

        public void Update()
        {
            Move();
            screen = canva_manager.CreateCanvaDraw(); // concatena todo o canva para desenhar tudo de uma vez (string buffer)
        }

        public void Draw()
        {
            Debug.WriteLine(" X: " + player.X + " ace: " + player.h_acceleration);
            Console.SetCursorPosition(0, 0); // volta o cursor para o canto da tela para fazer o redraw
            FasterConsole.Write(screen);
        }

        //movement

        double subpixel = 0.0625;


        internal void Move()
        {

            if (input.IsIdle)
            {
                player.h_acceleration = Math.Max(0, player.h_acceleration - subpixel);
            }
            if (input.IsRightKeyPressed)
            {
                player.h_acceleration = Math.Min(3, player.h_acceleration + subpixel);
            }

            player.X += (int)player.h_acceleration;
        }

    }
}
