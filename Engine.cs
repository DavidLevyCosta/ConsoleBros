using System.Diagnostics;
using System.Text;
using System.Timers;
using Windows.Security.EnterpriseData;

namespace ConsoleBros
{
    internal class Engine
    {
        private int fps;
        Thread DrawThread;
        Thread UpdateThread;
        private CanvaManager canva_manager;
        public Player player;
        private Input input;
        private StringBuilder? screen; // tela que receberá a informação do canva
        public Engine(int fps, int canva_width, int canva_height) // cria o motor com informação de tamanho do canva e a quantidade de frames por segundo
        {
            this.fps = fps;
            player = new Player(0, 0);
            input = new Input();
            canva_manager = new CanvaManager(player, canva_width, canva_height);
            DrawThread = new Thread(Draw);
            DrawThread.IsBackground = true;
            UpdateThread = new Thread(Update);
            UpdateThread.IsBackground = false;
        }

        public void Start() // inicia o canva (desnulifica os index) e a atualizão dos frames
        {
            CanvaManager.StartCanva(canva_manager.backBuffer); // LEMBRAR DE VIR TROCAR ISSO DEPOIS <-
            DrawThread.Start();
            UpdateThread.Start();
        }

        public void Update()
        {
            while (true)
            {
                Move();
                screen = canva_manager.CreateCanvaDraw(); // concatena todo o canva para desenhar tudo de uma vez (string buffer)
                Thread.Sleep(1000 / fps);
            }

        }

        public void Draw()
        {
            while (true)
            {
                Console.SetCursorPosition(0, 0); // volta o cursor para o canto da tela para fazer o redraw
                if (screen != null) FasterConsole.Write(screen);
                Thread.Sleep(1000 / fps);
            }
        }

        //movement


        internal void Move()
        {
            // Log player details
            Debug.WriteLine($"ace: {player.acceleration:F2} r_pos: {player.render_position:F2} X: {player.X}");

            // Read input keys
            input.ReadKeys();

            // Handle idle state
            if (input.IsIdle)
            {
                HandleIdleState();
            }

            // Handle right key press
            if (input.IsRightKeyPressed)
            {
                HandleRightKeyPress();
            }

            // Handle left key press
            if (input.IsLeftKeyPressed)
            {
                HandleLeftKeyPress();
            }

            // Update player position
            player.render_position += player.acceleration;
            player.X = (int)Math.Floor(player.render_position);
        }

        private void HandleIdleState()
        {
            if (player.acceleration == 0)
                player.animation_state = Player.AnimationState.Idle;

            if (player.acceleration > 0)
                player.acceleration = Math.Max(0, player.acceleration - (Player.subpixel * player.friction));

            if (player.acceleration < 0)
                player.acceleration = Math.Min(0, player.acceleration + (Player.subpixel * player.friction));
        }

        private void HandleRightKeyPress()
        {
            player.orientation_right = true;

            if (player.acceleration < 0)
            {
                player.animation_state = Player.AnimationState.Drifting;
                player.friction = 4;
            }

            if (player.acceleration > 0)
            {
                player.animation_state = Player.AnimationState.WalkingRight;
                player.friction = 1;
            }

            player.acceleration = Math.Min(2, player.acceleration + (Player.subpixel * player.friction));
        }

        private void HandleLeftKeyPress()
        {
            player.orientation_right = false;

            if (player.acceleration > 0)
            {
                player.animation_state = Player.AnimationState.Drifting;
                player.friction = 4;
            }

            if (player.acceleration < 0)
            {
                player.animation_state = Player.AnimationState.WalkingLeft;
                player.friction = 1;
            }

            player.acceleration = Math.Max(-2, player.acceleration - (Player.subpixel * player.friction));
        }

    }
}
