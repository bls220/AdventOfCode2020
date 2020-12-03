using System;

namespace day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines("input.txt");
            Map map = new Map(lines);
            int numTrees = CalculateTreeEncounters(map, 1, 1);
            numTrees *= CalculateTreeEncounters(map, 3, 1);
            numTrees *= CalculateTreeEncounters(map, 5, 1);
            numTrees *= CalculateTreeEncounters(map, 7, 1);
            numTrees *= CalculateTreeEncounters(map, 1, 2);
            Console.WriteLine($"Number of trees hit: {numTrees}");
        }

        private static int CalculateTreeEncounters(Map map, int stepX, int stepY)
        {
            int numTrees = 0;
            int maxHeight = map.Height;
            int x = 0;
            for (int y = 0; y < maxHeight; y += stepY)
            {
                if (map.GetTile(x, y) == MapTile.Tree)
                {
                    numTrees++;
                }
                x += stepX;
            }
            return numTrees;
        }
    }
}
