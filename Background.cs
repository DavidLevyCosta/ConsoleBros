namespace ConsoleBros
{
    public class Background
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char[,] background;
        Elevation current_elevation;

        public Background ()
        {
            X = 0; //apenas teste
            current_elevation = Elevation.Surface;
            background = SpriteHandling.ReadSprite("../../../Sprites/assets/background.txt");
        }

        enum Elevation
        {
            Surface = 0,
            Underground = 1
        }

        public char[,] Draw()
        {
            int top_start;
            int left_start;
            char[,] full_canva = new char[Program.SCREEN_HEIGHT + 32, Program.SCREEN_WIDTH + 32];

            if (current_elevation == Elevation.Surface)
            {
                top_start = (background.GetLength(0) / 2) - Program.SCREEN_HEIGHT - 16;
                if (X >= 0) left_start = X;
                else if (X > background.GetLength(0) - Program.SCREEN_WIDTH) left_start = background.GetLength(0) - Program.SCREEN_WIDTH;
                else left_start = 0;
                for (int i = 0; i < full_canva.GetLength(0); i++)
                {
                    for (int j = 0; j < full_canva.GetLength(1); j++)
                    {
                        full_canva[i, j] = background[i + top_start, j + left_start];
                    }
                }

            }
            return full_canva;
        }

    }
}
