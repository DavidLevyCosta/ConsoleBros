using System.Drawing;
using System.Drawing.Imaging;

namespace ConsoleBros
{
    public static class NesPalette // basicamente é uma coletânea contendo as cores e caracters
    {
        public static Color[] RGBColors { get; set; }
        public static char[] ASCIIColors { get; set; }
        public static char[] ColorTag { get; set; }
        public static Dictionary<char, char> ColorMap { get; set; }

        static NesPalette()
        {
            RGBColors = new Color[]
            {
                Color.FromArgb(124,124,124), // 0 - Cinza
                Color.FromArgb(0,0,252), // 1 - Azul
                Color.FromArgb(0, 0, 188), // 2 - AzulEscuro
                Color.FromArgb(68, 40, 188), // 3 - Roxo
                Color.FromArgb(148, 0, 132), // 4 - RoxoEscuro
                Color.FromArgb(168, 0, 32), // 5 - VermelhoEscuro
                Color.FromArgb(168, 16, 0), // 6 - VermelhoAlaranjado
                Color.FromArgb(136, 20, 0), // 7 - LaranjaEscuro
                Color.FromArgb(80, 48, 0), // 8 - Marrom
                Color.FromArgb(0, 120, 0), // 9 - VerdeEscuro
                Color.FromArgb(0, 104, 0), // 10 - VerdeAzuladoEscuro
                Color.FromArgb(0, 88, 0), // 11 - VerdeAzuladoMaisEscuro
                Color.FromArgb(0, 64, 88), // 12 - AzulPetróleo
                Color.FromArgb(0, 0, 0), // 13 - Preto
                Color.FromArgb(188, 188, 188), // 14 - CinzaClaro
                Color.FromArgb(0, 120, 248), // 15 - AzulClaro
                Color.FromArgb(0, 88, 248), // 16 - AzulMédio
                Color.FromArgb(104, 68, 252), // 17 - RoxoClaro
                Color.FromArgb(216, 0, 204), // 18 - Rosa
                Color.FromArgb(228, 0, 88), // 19 - VermelhoClaro
                Color.FromArgb(248, 56, 0), // 20 - LaranjaClaro
                Color.FromArgb(228, 92, 16), // 21 - LaranjaAmarelado
                Color.FromArgb(172, 124, 0), // 22 - AmareloEscuro
                Color.FromArgb(0, 184, 0), // 23 - VerdeClaro
                Color.FromArgb(0, 168, 0), // 24 - VerdeMédio
                Color.FromArgb(0, 168, 68), // 25 - VerdeAzuladoClaro
                Color.FromArgb(0, 136, 136), // 26 - AzulPetróleoClaro
                Color.FromArgb(248, 248, 248) // 27 - Branco
            };

            ColorTag = new char[]
            {
                '.',
                'A',
                'B',
                'C',
                'D',
                'E',
                'F',
                'G',
                'H',
                'I',
                'J',
                'K',
                'L',
                'M',
                'N',
                'O',
                'P',
                'Q',
                'R',
                'S',
                'T',
                'U',
                'V',
                'W',
                'X',
                'Y',
                'Z',
                '1',
                '2'

            };
            
            ASCIIColors = new char[]
            {
                'A', // 0 - Cinza
                '›', // 1 - Azul
                '‹', // 2 - AzulEscuro
                '@', // 3 - Roxo
                '0', // 4 - RoxoEscuro
                'Z', // 5 - VermelhoEscuro
                '.', // 6 - VermelhoAlaranjado
                'M', // 7 - LaranjaEscuro
                '^', // 8 - Marrom
                'O', // 9 - VerdeEscuro
                '.', // 10 - VerdeAzuladoEscuro
                '¦', // 11 - VerdeAzuladoMaisEscuro
                'D', // 12 - AzulPetróleo
                '.', // 13 - Preto
                'H', // 14 - CinzaClaro
                ' ', // 15 - AzulClaro
                '-', // 16 - AzulMédio
                ' ', // 17 - RoxoClaro
                '/', // 18 - Rosa
                '$', // 19 - VermelhoClaro
                '¨', // 20 - LaranjaClaro
                '#', // 21 - LaranjaAmarelado
                '%', // 22 - AmareloEscuro
                'V', // 23 - VerdeClaro
                'M', // 24 - VerdeMédio
                ' ', // 25 - VerdeAzuladoClaro
                ',', // 26 - AzulPetróleoClaro
                '*', // 27 - Branco
                '.' // 28 - Cinza
            };

            ColorMap = new Dictionary<char, char>();
            for (int i = 0; i < ColorTag.Length; i++)
            {
                ColorMap[ColorTag[i]] = ASCIIColors[i];
            }

        }

    }

}

