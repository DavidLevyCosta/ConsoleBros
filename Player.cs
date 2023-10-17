namespace ConsoleBros
{
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool orientation_right { get; set; }
        public short max_speed { get; set; }

        public short friction;
        private int mario_animation_frame;

        public const double subpixel = 0.0625;
        public double acceleration = 0;
        public double render_position = 0;
        private int animationCounter;

        private const int SPRITES_IN_X = 21;
        private const int SPRITES_IN_Y = 2;
        private static int sprite_frame_width = 16;
        private static int sprite_frame_height = 32;
        private static int x_start = 0;
        private static int y_start = 0;
        private static char[,] mario_spritesheet = SpriteHandling.ReadSprite("../../../Sprites/assets/mario_sprite.txt");

        public AnimationState animation_state = new AnimationState();

        public enum AnimationState
        {
            Idle,
            WalkingRight,
            WalkingLeft,
            RunningRight,
            RunningLeft,
            Drifting
        }

        public Player(int X, int Y) {
            this.X = X;
            this.Y = Y;
            animationCounter = 0;
            friction = 1;
            orientation_right = true;
            mario_animation_frame = 0;
        }

        public static List<char[,]> sprite_index = new List<char[,]>();

        public static void SliceFrames() // divide o spritesheet do mario em varios sprites
        {

            char[,] frame_data;

            for (int frame = 0; frame < SPRITES_IN_X * SPRITES_IN_Y; frame++) // é o loop que define o index do sprite dividido na coletanea
            {
                frame_data = new char[sprite_frame_height, sprite_frame_width]; // é o sprite que vai ser adicionado a coletanea, é definido aqui para ter altura dinamica (para abrigar sprites de tamanho diferente)

                for (int i = 0; i < frame_data.GetLength(0); i++)       // esse loop e o de baixo é o iter do sprite dividido
                {
                    for (int j = 0; j < frame_data.GetLength(1); j++)
                    {

                        frame_data[i, j] = mario_spritesheet[i + y_start, j + x_start];

                    }
                }
                sprite_index.Add(frame_data); // adiciona o sprite completo para a coletanea de sprites divididos

                if (frame == 20) // quando chegar ao final da primeira linha, diminui a altura do sprite para pegar o mini mario
                {
                    y_start += sprite_frame_height; // Mini Mario
                    sprite_frame_height = 16;
                    x_start = 0;
                }
                else x_start += sprite_frame_width; // no final do de cada divisão, passa para o próximo sprite

            }
        }


        public char[,] Draw(char[,] sprite) // joga um sprite pro canva
        {
            char[,] full_canva = new char[Program.SCREEN_HEIGHT, Program.SCREEN_WIDTH];
            CanvaManager.StartCanva(full_canva);
            if (X + sprite.GetLength(1) > full_canva.GetLength(1)) X = full_canva.GetLength(1) - sprite.GetLength(1); // evita sair das bordas
            if (X < 0) X = 0;
            if (Y + sprite.GetLength(0) > full_canva.GetLength(0)) Y = full_canva.GetLength(0) - sprite.GetLength(0); // evita sair das bordas

            for (int i = 0; i < sprite.GetLength(0); i++)
            {
                for (int j = 0; j < sprite.GetLength(1); j++)
                {
                    if (orientation_right)
                    {
                        full_canva[i + Y, j + X] = sprite[i, j];
                    }
                    else
                    {
                        full_canva[i + Y, X + sprite.GetLength(1) - 1 - j] = sprite[i, j];
                    }
                }
            }
            return full_canva;
        }

        // animation

        public char[,] Animation()
        {

            switch (animation_state)
            {
                case AnimationState.WalkingRight or AnimationState.WalkingLeft:
                    return WalkingAnimation();
                case AnimationState.Drifting:
                    return DriftAnimation();
                default:
                    return IdleAnimation();
            }
        }

        public char[,] WalkingAnimation()  // cicla os frames para a animação de andar/correr do mario
        {
            // Ajusta a velocidade da animação com base na aceleração
            int animationSpeed = (int)(5 - Math.Abs(acceleration));

            // Incrementa o contador de animação
            animationCounter++;

            // Muda o frame da animação quando o contador atinge o limite
            if (animationCounter >= animationSpeed)
            {
                if (mario_animation_frame >= 2) mario_animation_frame--;
                else mario_animation_frame = 3;

                // Reseta o contador de animação
                animationCounter = 0;
            }

            return sprite_index[mario_animation_frame];
        }

        public char[,] DriftAnimation()
        {
            return sprite_index[4];
        }

        public char[,] IdleAnimation()
        {
            return sprite_index[0];
        }



    }
}
