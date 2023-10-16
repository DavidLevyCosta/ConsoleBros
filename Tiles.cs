namespace ConsoleBros
{
    public class Tiles
    {
        public int x = 2000;
        public int y;
        char[,] tiles;
        Elevation current_elevation;

        public Tiles()
        {
            current_elevation = Elevation.Surface;
            tiles = SpriteHandling.ReadSprite("tiles.txt");
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
            char[,] full_canva = new char[Program.SCREEN_HEIGHT, Program.SCREEN_WIDTH];

            if (current_elevation == Elevation.Surface)
            {
                top_start = (tiles.GetLength(0) / 2) - Program.SCREEN_HEIGHT;
                if (x >= 0) left_start = x;
                else if (x > tiles.GetLength(0) - Program.SCREEN_WIDTH) left_start = tiles.GetLength(0) - Program.SCREEN_WIDTH;
                else left_start = 0;

                for (int i = 0; i < full_canva.GetLength(0); i++)
                {
                    for (int j = 0; j < full_canva.GetLength(1); j++)
                    {
                        full_canva[i, j] = tiles[i + top_start, j + left_start];
                    }
                }

            }
            return full_canva;
        }

    }
}
