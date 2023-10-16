using System.Runtime.CompilerServices;
using System.Text;

namespace ConsoleBros
{
    internal class CanvaManager
    {
        public char[,] canva;
        internal Player player;
        internal Background background;
        internal Tiles tiles;
        public CanvaManager(Player player, int width, int height)
        {
            this.player = player;
            background = new Background();
            tiles = new Tiles();
            canva = new char[width, height];
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
            char[,] mario = player.WalkingAnimation();
            char[,] layer0 = background.Draw();
            char[,] layer1 = player.Draw(mario);
            char[,] layer2 = tiles.Draw();

            char[,] complete_canva = new char[Program.SCREEN_HEIGHT, Program.SCREEN_WIDTH];
            char alfa = NesPalette.ColorTag[0];

            for (int i = 0; i < complete_canva.GetLength(0); i++)
            {
                for (int j = 0; j < complete_canva.GetLength(1); j++)
                {
                    if (layer1[i, j] == alfa && layer2[i, j] == alfa) complete_canva[i, j] = layer0[i, j];
                    if (layer1[i, j] != alfa && layer2[i, j] == alfa) complete_canva[i, j] = layer1[i, j];
                    if (layer2[i, j] != alfa) complete_canva[i, j] = layer2[i, j];
                }
            }
            canva = complete_canva;
        }

        public StringBuilder CreateCanvaDraw() // concatena todo o canva
        {
            FullCanvaDraw();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < canva.GetLength(0); i++)
            {
                for (int j = 0; j < canva.GetLength(1); j++)
                {
                    for (int k = 0; k < NesPalette.ColorTag.Length; k++)
                    {
                        if (canva[i, j] == NesPalette.ColorTag[k])
                        {
                            sb.Append(NesPalette.ASCIIColors[k]);
                            sb.Append(NesPalette.ASCIIColors[k]);
                        }
                    }
                }
                sb.Append("\n");
            }
            return sb;
        }



    }
}
