
using System;
using System.Text;

namespace day3
{
    public class Map
    {
        private MapTile[,] _map;

        public Map(string[] lines)
        {
            this.ParseMap(lines);
        }

        private void ParseMap(string[] lines)
        {
            this._map = new MapTile[lines[0].Length, lines.Length];
            for (int y = 0; y < lines.Length; y++)
            {
                string line = (string)lines[y];
                for (int x = 0; x < line.Length; x++)
                {
                    MapTile tile = (MapTile)line[x];
                    this._map.SetValue(tile, x, y);
                }
            }
        }

        private int Width => this._map?.GetUpperBound(0) + 1 ?? -1;

        public int Height => this._map?.GetUpperBound(1) + 1 ?? -1;

        public MapTile GetTile(int posX, int posY) => this._map[posX % this.Width, posY];

        public void SetTile(MapTile tile, int posX, int posY) => this._map[posX % this.Width, posY] = tile;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    builder.Append(Convert.ToChar(this._map.GetValue(x, y)));
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }

    }
}