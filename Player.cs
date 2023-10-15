﻿namespace ConsoleBros
{
    public class Player
    {
        public short layer = 1;
        public int x = 0;
        public int y = 0;

        public static List<char[,]> sprite_index = new List<char[,]>();
        private int mario_animation_frame = 0;


        public static void SliceFrames() // divide o spritesheet do mario em varios sprites
        {
            char[,] mario_spritesheet = SpriteHandling.ReadSprite("mario_sprite.txt");
            const int SPRITES_IN_X = 21;
            const int SPRITES_IN_Y = 2;
            int sprite_frame_width = 16;
            int sprite_frame_height = 32;
            int x_start = 0;
            int y_start = 0;
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
            if (x + sprite.GetLength(1) > full_canva.GetLength(1)) x = full_canva.GetLength(1) - sprite.GetLength(1); // evita sair das bordas
            if (y + sprite.GetLength(0) > full_canva.GetLength(0)) y = full_canva.GetLength(0) - sprite.GetLength(0); // evita sair das bordas

            for (int i = 0; i < sprite.GetLength(0); i++)
            {
                for (int j = 0; j < sprite.GetLength(1); j++)
                {
                    full_canva[i + y, j + x] = sprite[i, j];
                }
            }
            return full_canva;
        }

        // animation
        
        enum Animation
        {
            walking_start = 3,
            walking_end = 2,
        }
        
        public char[,] WalkingAnimation()  // cicla os frames para a animação de andar/correr do mario
        {
            if (mario_animation_frame >= (int)Animation.walking_end) mario_animation_frame--;
            else mario_animation_frame = (int)Animation.walking_start;
            return sprite_index[mario_animation_frame];
        }



    }
}
