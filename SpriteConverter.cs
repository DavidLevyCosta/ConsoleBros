using System;
using System.IO;

namespace ConsoleBros
{
    internal class SpriteConverter
    {
        public static char[,] mario_sprite = SpriteHandling.ConvertSprite("../../../Sprites/mario_std.png");

        public static void ConvertSprite()
        {
            using (StreamWriter writer = new StreamWriter("mario_sprite.txt"))
            {
                for (int i = 0; i < mario_sprite.GetLength(0); i++)
                {
                    for (int j = 0; j < mario_sprite.GetLength(1); j++)
                    {
                        writer.Write(mario_sprite[i, j]);
                    }
                    writer.WriteLine();
                }
            }
        }
    }
}
