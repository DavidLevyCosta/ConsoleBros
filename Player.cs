using System;


namespace ConsoleBros
{
    static class Player
    {
        const int SPRITES_IN_X = 21;
        const int SPRITES_IN_Y = 2;

        public const int SPRITE_FRAME_WIDTH = 16;
        public static int SPRITE_FRAME_HEIGHT = 32;

        public static short layer = 1;
        static char[,] sprite = SpriteHandling.ReadSprite("mario_sprite.txt");
        public static List<char[,]> sprite_frame = new List<char[,]>();

        public static void SliceFrames()
        {
            int x_start = 0;
            int y_start = 0;
            char[,] frame_data;

            for (int frame = 0; frame < SPRITES_IN_X * SPRITES_IN_Y; frame++)
            {
                frame_data = new char[SPRITE_FRAME_HEIGHT, SPRITE_FRAME_WIDTH];

                for (int i = 0; i < frame_data.GetLength(0); i++)
                {
                    for (int j = 0; j < frame_data.GetLength(1); j++)
                    {
                        frame_data[i, j] = sprite[i + y_start, j + x_start] ;
                        
                    }
                }
                sprite_frame.Add(frame_data);

                if (frame == 20)
                {
                    y_start += SPRITE_FRAME_HEIGHT; // Mini Mario
                    SPRITE_FRAME_HEIGHT = 16;
                    x_start = 0; 
                } else x_start += SPRITE_FRAME_WIDTH;

            }
        }

        public static char[,] InvokePlayerSprite(int sprite_num)
        {
            return sprite_frame[sprite_num];
        }

        public static void SetPosition(this char[,] sprite, char[,] canva, int y, int x)
        {
            for (int i = 0; i < sprite.GetLength(0); i++)
            {
                for(int j = 0; j < sprite.GetLength(1); j++)
                {
                    if (x + sprite.GetLength(1) > canva.GetLength(1)) x = canva.GetLength(1) - sprite.GetLength(1); // evita sair das bordas
                    if (y + sprite.GetLength(0) > canva.GetLength(0)) y = canva.GetLength(0) - sprite.GetLength(0); // evita sair das bordas
                    canva[i + y, j + x] = sprite[i, j];
                }
            }
        }
        

        
    }
}
