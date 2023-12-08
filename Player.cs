using System.Numerics;
using ConsoleBros;
using System.Runtime.CompilerServices;

namespace ConsoleBros
{
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ushort width { get; }
        public ushort height { get; }
        public Vector2 vector { get; set; }
        public bool orientation_right { get; set; }
        public short max_speed { get; set; }
        public bool big_mario { get; set; }
        public short friction;
        private int mario_animation_frame;

        public const double subpixel = 0.0625;
        public double x_acceleration = 0;
        public double y_acceleration = 0;
        public double render_x_position = 0;
        public double render_y_position = 0;
        private int animationCounter;
        public State current_state;

        public List<char[,]> sprite;

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
        public enum State
        {
            Grounded,
            Airborne
        }

        

        public Player() {
            width = 16;
            height = 32;
            Y = 32;
            X = 16;
            render_x_position = 16;
            sprite = SpriteHandling.SliceSprite(21, 2, 16, 32, "../../../Sprites/assets/mario_sprite.txt", 16);
            animationCounter = 0;
            friction = 1;
            orientation_right = true;
            mario_animation_frame = 0;
            current_state = State.Grounded;
        }


        // mover o método para SpriteHandling depois



        public char[,] Draw(char[,] canva, char[,] sprite) // joga um sprite pro canva
        {

            for (int i = 0; i < sprite.GetLength(0); i++)
            {
                for (int j = 0; j < sprite.GetLength(1); j++)
                {
                    if (sprite[i, j] != '.')
                    {
                        if (orientation_right)
                        {
                            canva[i + Y, j + X] = sprite[i, j];
                        }
                        else
                        {
                            canva[i + Y, X + sprite.GetLength(1) - 1 - j] = sprite[i, j];
                        }
                    }
                    else continue;
                }
            }
            return canva;
        }

        // animation

        public char[,] Animation()
        {
            if (current_state == State.Grounded)
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
            } else
            {
                return JumpAnimation();
            }

        }

        public char[,] WalkingAnimation()  // cicla os frames para a animação de andar/correr do mario
        {
            // Ajusta a velocidade da animação com base na aceleração
            int animationSpeed = (int)(5 - Math.Abs(x_acceleration));

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

            return sprite[mario_animation_frame];
        }

        public char[,] DriftAnimation()
        {
            return sprite[4];
        }

        public char[,] IdleAnimation()
        {
            return sprite[0];
        }

        public char[,] JumpAnimation()
        {
            return sprite[5];
        }


    }
}
