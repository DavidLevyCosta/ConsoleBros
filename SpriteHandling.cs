using System;
using System.Drawing;
using System.Text;

namespace ConsoleBros
{
    public static class SpriteHandling
    {

        public static char[,] ConvertSpriteToChar(string png_path) // lê o png contendo o sprite e faz o mapeamento de pixels, e depois converte ele para a ColorTag (o png precisa ser com a paleta de cores do NES)
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

                return sprite;

            }
            
        }

        public static List<char[,]> SliceSprite(ushort sprite_num_x, ushort sprite_num_y, ushort width, ushort height, string path, ushort other_height) // divide o spritesheet do mario em varios sprites
        {
            List<char[,]> sprite_list = new List<char[,]>();
            char[,] sprite = ReadSprite(path);
            char[,] frame_data;
            ushort x_start = 0;
            ushort y_start = 0;

            for (int frame = 0; frame < sprite_num_x * sprite_num_y; frame++) // é o loop que define o index do sprite dividido na coletanea
            {
                frame_data = new char[height, width]; // é o sprite que vai ser adicionado a coletanea, é definido aqui para ter altura dinamica (para abrigar sprites de tamanho diferente)

                for (int i = 0; i < frame_data.GetLength(0); i++)       // esse loop e o de baixo é o iter do sprite dividido
                {
                    for (int j = 0; j < frame_data.GetLength(1); j++)
                    {

                        frame_data[i, j] = sprite[i + y_start, j + x_start];

                    }
                }
                sprite_list.Add(frame_data); // adiciona o sprite completo para a coletanea de sprites divididos


                if (sprite_num_y != 1 && frame == sprite_num_x - 1) // quando chegar ao final da primeira linha, diminui a altura do sprite para pegar o mini mario
                {
                    y_start += height; // Mini Mario
                    height = other_height;
                    x_start = 0;
                }
                else x_start += width; // no final do de cada divisão, passa para o próximo sprite



            }
            return sprite_list;
        }

        public static List<char[,]> SliceSprite(ushort sprite_num_x, ushort sprite_num_y, ushort width, ushort height, string path) // divide o spritesheet do mario em varios sprites
        {
            List<char[,]> sprite_list = new List<char[,]>();
            char[,] sprite = ReadSprite(path);
            char[,] frame_data;
            ushort x_start = 0;
            ushort y_start = 0;

            for (int frame = 0; frame < sprite_num_x * sprite_num_y; frame++) // é o loop que define o index do sprite dividido na coletanea
            {
                frame_data = new char[height, width]; // é o sprite que vai ser adicionado a coletanea, é definido aqui para ter altura dinamica (para abrigar sprites de tamanho diferente)

                for (int i = 0; i < frame_data.GetLength(0); i++)       // esse loop e o de baixo é o iter do sprite dividido
                {
                    for (int j = 0; j < frame_data.GetLength(1); j++)
                    {

                        frame_data[i, j] = sprite[i + y_start, j + x_start];

                    }
                }
                sprite_list.Add(frame_data); // adiciona o sprite completo para a coletanea de sprites divididos
                if (sprite_num_y != 1 && frame == sprite_num_x - 1) // quando chegar ao final da primeira linha, diminui a altura do sprite para pegar o mini mario
                {
                    y_start += height;
                    x_start = 0;
                }
                else x_start += width; // no final do de cada divisão, passa para o próximo sprite


            }
            return sprite_list;
        }

        public static void ConvertSprite(string path, string name) // converte o sprite para um arquivo txt
        {
            char[,] sprite = ConvertSpriteToChar(path);
            using (StreamWriter writer = new StreamWriter(name))
            {
                for (int i = 0; i < sprite.GetLength(0); i++)
                {
                    for (int j = 0; j < sprite.GetLength(1); j++)
                    {
                        writer.Write(sprite[i, j]);
                    }
                    writer.WriteLine();
                }
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


        // tile shit

        public struct Tile
        {
            public int type_id;
            public Rectangle rectangle;
        }


        public static List<Tile> ReadTileMap()
        {
            List<Tile> tiles = new List<Tile>();

            Color[] TileMap = NesPalette.TileMap;

            using (Bitmap bitmap = new Bitmap("../../../Sprites/tile_map.png"))
            {
                for (int y = 0; y < bitmap.Height; y += 16)
                {
                    for (int x = 0; x < bitmap.Width; x += 16)
                    {
                        Color pixelColor = bitmap.GetPixel(x, y);
                        for (int i = 0; i < TileMap.Length; i++)
                        {
                            if (pixelColor == TileMap[i])
                            {
                                int width = 16;
                                int height = 16;

                                if (i == 4 || i == 6 || i == 7) // Cano Trigger, Cabo Bandeira, Ponta Bandeira
                                {
                                    width = Math.Min(bitmap.Width - x, width);
                                    height = Math.Min(bitmap.Height - y, height);
                                }

                                tiles.Add(new Tile { type_id = i, rectangle = new Rectangle(x, y, width, height) });
                                break;
                            }
                        }
                    }
                }
            }

            return tiles;
        }

        public static void WriteTilesToFile(List<Tile> tiles, string filePath)
        {
            using (StreamWriter file = new StreamWriter(filePath))
            {
                foreach (Tile tile in tiles)
                {
                    file.WriteLine($"type_id={tile.type_id}/Rectangle_x={tile.rectangle.X}/Rectangle_y={tile.rectangle.Y}/Rectangle_width={tile.rectangle.Width}/Rectangle_height={tile.rectangle.Height}");
                }
            }
        }

        public static List<Tile> ReadTilesFromFile(string filePath)
        {
            List<Tile> tiles = new List<Tile>();

            using (StreamReader file = new StreamReader(filePath))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string[] parts = line.Split('/');
                    int type_id = int.Parse(parts[0].Split('=')[1]);
                    int x = int.Parse(parts[1].Split('=')[1]);
                    int y = int.Parse(parts[2].Split('=')[1]);
                    int width = int.Parse(parts[3].Split('=')[1]);
                    int height = int.Parse(parts[4].Split('=')[1]);

                    tiles.Add(new Tile { type_id = type_id, rectangle = new System.Drawing.Rectangle(x, y, width, height) });
                }
            }

            return tiles;
        }
    }
}
