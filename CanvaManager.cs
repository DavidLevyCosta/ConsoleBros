using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBros
{
    internal class CanvaManager
    {
        public char[,] canva;
        public CanvaManager(int width, int height)
        {
            canva = new char[width, height];
        }

        public void StartCanva() // desnulifica o canva
        {
            for (int i = 0; i < canva.GetLength(0); i++)
            {
                for (int j = 0; j < canva.GetLength(1); j++)
                {
                    canva[i, j] = '.';
                }
            }
        }

        public string CreateCanvaDraw() // concatena todo o canva
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < canva.GetLength(0); i++)
            {
                for (int j = 0; j < canva.GetLength(1); j++)
                {
                    for (int k = 0; k < NesPalette.ColorTag.Length; k++) {
                        if (k == 0) sb.Append(' ');                             //transparencia, preciso mudar para receber a 'cor' de menor prioridade de layer
                        else if (canva[i, j] == NesPalette.ColorTag[k])
                        {
                            sb.Append(NesPalette.ASCIIColors[k]);
                        }
                    }
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }



    }
}
