using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Numerics;
using System.Timers;
using Windows.Perception.People;
using Windows.Security.EnterpriseData;
using static ConsoleBros.SpriteHandling;

namespace ConsoleBros
{
    public class Engine
    {
        public static int Level_X;
        public static int Level_Y;
        private int fps;
        Thread DrawThread;
        Thread UpdateThread;
        private CanvaManager canva_manager;
        public Player player;
        private Input input;
        private Tiles tile;
        private StringBuilder? screen; // tela que receberá a informação do canva
        Rectangle mario;
        List<Tile> tiles_on_screen;
        List<Tile> tiles_data;

        public Engine(int fps, int canva_width, int canva_height) // cria o motor com informação de tamanho do canva e a quantidade de frames por segundo
        {
            Level_X = 0;
            Level_Y = 0;
            this.fps = fps;
            player = new Player();
            input = new Input();
            tile = new Tiles();
            canva_manager = new CanvaManager(player, canva_width, canva_height);
            DrawThread = new Thread(Draw);
            DrawThread.IsBackground = true;
            UpdateThread = new Thread(Update);
            tiles_data = tile.tiles_on_screen;
            tiles_on_screen = new List<Tile>();
            UpdateThread.IsBackground = false;
        }

        public void Start() // inicia o canva (desnulifica os index) e a atualizão dos frames
        {
            tiles_on_screen = tile.GetTilesOnScreen(tiles_data);
            CanvaManager.StartCanva(canva_manager.backBuffer); // LEMBRAR DE VIR TROCAR ISSO DEPOIS <-
            DrawThread.Start();
            UpdateThread.Start();
        }

        public void Update()
        {
            while (true)
            {
                tiles_on_screen = tile.GetTilesOnScreen(tiles_data);
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


        short sign = 1;
        const float gravity = 1.5f;



        internal void Move()
        {
            // Log player details
            //Debug.WriteLine($"ace: {player.y_acceleration:F2} mario_bottom: {player.Y + 31} Y: {player.Y} onColision: {OnColision_Y()} state: {player.current_state}");
            //Debug.WriteLine($"ace: {player.y_acceleration:F2} r_pos: {player.render_y_position:F2} Y: {player.Y} onColision: {OnColision_Y()} state: {player.current_state} mario_b: {player.Y + 31}");

            //Update AABB


            // Read input keys
            input.ReadKeys();

            // Handle idle state
            if (input.IsIdle)
                HandleIdleState();

            // Handle right key press
            if (input.IsRightKeyPressed && !input.IsLeftKeyPressed)
                HandleRightKeyPress();

            // Handle left key press
            if (input.IsLeftKeyPressed && !input.IsRightKeyPressed)
                HandleLeftKeyPress();

            // Update player position
            UpdatePlayerPosition();

            // Apply gravity when airborne
            //if (player.current_state == Player.State.Airborne)
            //    player.y_acceleration = Math.Min(10, player.y_acceleration + gravity + (Player.subpixel * 4));

            // Jump when grounded and jump key is pressed
            //if (player.current_state == Player.State.Grounded && input.IsJumpKeyPressed)
            //    Jump();
        }

        private void UpdatePlayerPosition()
        {
            if (OnColision_X()) player.x_acceleration = 0;
            else player.render_x_position += player.x_acceleration;

            player.X = (int)Math.Floor(player.render_x_position);

            
        }

        private void Jump()
        {
            player.current_state = Player.State.Airborne;
            player.y_acceleration = -15;
        }

        private void HandleIdleState()
        {
            if (player.x_acceleration == 0)
                player.animation_state = Player.AnimationState.Idle;

            if (player.x_acceleration > 0)
                player.x_acceleration = Math.Max(0, player.x_acceleration - (Player.subpixel * player.friction));

            if (player.x_acceleration < 0)
                player.x_acceleration = Math.Min(0, player.x_acceleration + (Player.subpixel * player.friction));
        }

        private void HandleRightKeyPress()
        {

            player.orientation_right = true;

            if (player.x_acceleration < 0)
            {
                player.animation_state = Player.AnimationState.Drifting;
                player.friction = 4;
            }

            if (player.x_acceleration > 0)
            {
                player.animation_state = Player.AnimationState.WalkingRight;
                player.friction = 1;
            }
            player.x_acceleration = Math.Min(2, player.x_acceleration + (Player.subpixel * player.friction));
        }

        private void HandleLeftKeyPress()
        {
            player.orientation_right = false;

            if (player.x_acceleration > 0)
            {
                player.animation_state = Player.AnimationState.Drifting;
                player.friction = 4;
            }

            if (player.x_acceleration < 0)
            {
                player.animation_state = Player.AnimationState.WalkingLeft;
                player.friction = 1;
            }

            player.x_acceleration = Math.Max(-2, player.x_acceleration - (Player.subpixel * player.friction));
        }

        internal bool OnColision_X()
        {
            bool left_border_colision = player.X <= (0 + canva_manager.crop_x) + (int)player.x_acceleration * sign && input.IsLeftKeyPressed;
            if (left_border_colision) return true;
            else return false;
        }

        internal bool OnColision_Y()
        {

            for (int T = 0; T < tiles_on_screen.Count; T++)
            {
                //iterar com os blocos na tela
            }
            
            return false;
        }

    }
}
