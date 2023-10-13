using System.Timers;

namespace ConsoleBros
{
    internal class Engine
    {
        private static System.Timers.Timer frame_timer; // cria o timer de frames
        private CanvaManager canva_manager;
        public Player player;
        private string screen; // tela que receberá a informação do canva
        private char[,] mario; // o array 2d que receberá um sprite do mario

        public Engine(int fps, int canva_width, int canva_height) // cria o motor com informação de tamanho do canva e a quantidade de frames por segundo
        {
            canva_manager = new CanvaManager(canva_width, canva_height);
            player = new Player();
            frame_timer = new System.Timers.Timer(1000 / fps);
            frame_timer.Elapsed += FrameUpdate;
        }

        public void Start() // inicia o canva (desnulifica os index) e a atualizão dos frames
        {
            canva_manager.StartCanva(); // LEMBRAR DE VIR TROCAR ISSO DEPOIS <-
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
            mario = player.WalkingAnimation();
            player.Draw(mario, canva_manager.canva); // coloca o mario no canva
            screen = canva_manager.CreateCanvaDraw(); // concatena todo o canva para desenhar tudo de uma vez (string buffer)
        }

        public void Draw()
        {
            Console.SetCursorPosition(0, 0); // volta o cursor para o canto da tela para fazer o redraw
            Console.WriteLine(screen);
        }



        public static void KeepItRunning() // mantém o console aberto (tipo Console.ReadKey só que sem esperar algum input para não causar lag)
        {
            while (true) Thread.Sleep(1000);
        }

    }
}
