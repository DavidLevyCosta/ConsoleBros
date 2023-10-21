using System.Drawing;
using static ConsoleBros.SpriteHandling;

namespace ConsoleBros
{
    public class Tiles
    {
        public int X { get; set; }
        public int y { get; set; }
        Background background;
        public List<Tile> tile_data;
        List<char[,]> tile_sprite;
        Elevation current_elevation;

        public Tiles()
        {
            background = new Background();
            tile_sprite = SliceSprite(11, 1, 16, 16, "../../../Sprites/assets/tiles_sprite.txt");
            tile_data = ReadTilesFromFile("../../../Sprites/assets/tile_map.txt");
            current_elevation = Elevation.Surface;
        }

        enum Elevation
        {
            Surface = 0,
            Underground = 1
        }

        public List<Tile> GetTilesOnScreen(List<Tile> tiles)
        {
            Rectangle viewport = new Rectangle
            {
                X = background.X,
                Y = background.Y,
                Width = Program.SCREEN_WIDTH + 32, //pra não ficar cortado desenha dois tile para frente
                Height = Program.SCREEN_HEIGHT + 16  //estavam afundados 16px pra baixo. está resolvido, mas de uma forma bem mé...
            };
            List<Tile> tiles_on_screen = new List<Tile>();
            

            for (int T = 0; T < tiles.Count; T++)
            {
                Tile tile = tiles[T];
                if (IsTileOnScreen(tile.rectangle, viewport))
                {
                    tiles_on_screen.Add(tile);
                }
            }

            return tiles_on_screen;
        }


        public char[,] Draw(char[,] canva)
        {
            List<Tile> tiles_on_screen = GetTilesOnScreen(tile_data);
            char[,] sprite;
            ushort x_in_screen;
            ushort y_in_screen;
            for (int T = 0; T < tiles_on_screen.Count; T++)
            {
                sprite = tile_sprite[tiles_on_screen[T].type_id];
                x_in_screen = (ushort)(tiles_on_screen[T].rectangle.X - background.X);
                y_in_screen = (ushort)(tiles_on_screen[T].rectangle.Y - background.Y - 16); //estavam afundados 16px pra baixo. está resolvido, mas de uma forma bem mé...



                for (int i = 0; i < sprite.GetLength(0); i++)
                {
                    for (int j = 0; j < sprite.GetLength(1); j++)
                    {
                        canva[y_in_screen + i, x_in_screen + j] = sprite[i, j];
                    }
                }
            }
            return canva;
        }


        internal bool IsTileOnScreen(System.Drawing.Rectangle tile, Rectangle viewport)
        {
            // Verifica se o tile está à direita da borda esquerda da viewport
            bool rightOfLeftBound = tile.Left >= viewport.Left;
            // Verifica se o tile está à esquerda da borda direita da viewport
            bool leftOfRightBound = tile.Right <= viewport.Right;
            // Verifica se o tile está abaixo da borda superior da viewport
            bool belowTopBound = tile.Top >= viewport.Top;
            // Verifica se o tile está acima da borda inferior da viewport
            bool aboveBottomBound = tile.Bottom <= viewport.Bottom;

            // Se todas as condições forem verdadeiras, o tile está na tela
            return rightOfLeftBound && leftOfRightBound && belowTopBound && aboveBottomBound;
        }
    }
}
