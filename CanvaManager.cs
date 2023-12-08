using System.Runtime.CompilerServices;
using System.Text;

namespace ConsoleBros
{
    internal class CanvaManager
    {
        public char[,] canva;
        public char[,] frontBuffer;
        public char[,] middleBuffer;
        public char[,] backBuffer;
        public ushort crop_x = 16;
        public ushort crop_y = 16;
        internal Player player;
        internal Background background;
        internal Tiles tiles;
        internal char alfa;
        char[,]? mario;
        char[,]? screen_drawing;

        public CanvaManager(Player player, int width, int height)
        {

            this.player = player;
            alfa = NesPalette.ASCIIColors[0];
            background = new Background();
            tiles = new Tiles();
            frontBuffer = new char[width, height];
            middleBuffer = new char[width, height];
            backBuffer = new char[width, height];


            // inicia os buffers
            FullCanvaDraw();
            SwapBuffers();
            FullCanvaDraw();
            SwapBuffers();
            FullCanvaDraw();

        }

        public static void StartCanva(char[,] canva) // desnulifica o canva
        {
            for (int i = 0; i < canva.GetLength(0); i++)
            {
                for (int j = 0; j < canva.GetLength(1); j++)
                {
                    canva[i, j] = '.';
                }
            }
        }

        public void FullCanvaDraw()
        {
            mario = player.Animation();
            screen_drawing = tiles.Draw(player.Draw(background.Draw(), mario));

            for (int i = 0; i < backBuffer.GetLength(0); i++)
            {
                for (int j = 0; j < backBuffer.GetLength(1); j++)
                {
                    //backBuffer[i, j] = layer1[i, j] != alfa ? layer1[i, j] : layer0[i, j];
                    backBuffer[i, j] = screen_drawing[i + crop_y, j + crop_x];
                }
            }
        }

        public StringBuilder CreateCanvaDraw() // concatena todo o canva
        {
            StringBuilder sb = new StringBuilder();
            FullCanvaDraw();
            SwapBuffers();

            for (int i = 0; i < frontBuffer.GetLength(0); i++)
            {
                for (int j = 0; j < frontBuffer.GetLength(1); j++)
                {
                    sb.Append(NesPalette.ColorMap[frontBuffer[i, j]]);
                    sb.Append(NesPalette.ColorMap[frontBuffer[i, j]]);
                }
                sb.Append("\n");
            }
            return sb;
            
        }

        internal void SwapBuffers()
        {
            var temp = frontBuffer;
            frontBuffer = middleBuffer;
            middleBuffer = backBuffer;
            backBuffer = temp;
        }

    }
}
