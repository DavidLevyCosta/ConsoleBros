using System;
using System.Drawing;
using System.Text;

namespace ConsoleBros
{
    internal static class SpriteHandling
    {

        public static char[,] ConvertSprite(string png_path)
        {
            char[,] sprite;

            using (Bitmap bitmap = new Bitmap(png_path))
            {
                int sprite_sheet_h = bitmap.Height;
                int sprite_sheet_w = bitmap.Width;

                sprite = new char[sprite_sheet_h, sprite_sheet_w];
                
                for (int i = 0; i < sprite_sheet_h; i++)
                {
                    for (int j = 0; j < sprite_sheet_w; j++)
                    {

                        if (bitmap.GetPixel(j, i).A == 0)
                        {
                            sprite[i, j] = NesPalette.ColorTag[0];
                        }
                        else
                        {
                            for (int k = 0; k < 28; k++)
                            {
                                if (bitmap.GetPixel(j, i) == NesPalette.RGBColors[k])
                                {
                                    sprite[i, j] = NesPalette.ColorTag[k];
                                    break;
                                }
                            }
                        }
                    }
                }

                /*
                for (int i = 0; i < sprite_sheet_h; i++)
                {
                    for (int j = 0; j < sprite_sheet_w; j++)
                    {
                        if (bitmap.GetPixel(j, i).A == 0) sprite[i, j] = "[000]";
                        else sprite[i, j] = "["+bitmap.GetPixel(j, i).R.ToString()+"]";
                    }
                }*/

                return sprite;

            }
            
        }

        public static char[,] ReadSprite(string path)
        {
            // Create a list to hold the lines of the file
            List<string> lines = new List<string>();

            // Read the file line by line
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            // Determine the dimensions of the array
            int rows = lines.Count;
            int cols = lines[0].Length;

            // Create a 2D char array
            char[,] array = new char[rows, cols];

            // Fill the array with the characters from the file
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = lines[i][j];
                }
            }

            return array;
        }
    }
}
