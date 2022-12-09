namespace AoC.Util;

public class Map
{
    public static Map From(string[] lines,int startIndex = 0, int step = 1)
    {
        var map = new Map();
        for (int y = 0; y < lines.Length; y++)
        {
            map._tiles.Add(new List<Tile>());
            for (int x = 0; x < lines[y].Length; x++)
            {
                map._tiles[y].Add(new Tile(){X = x, Y = y, Map = map});
            }
        }
        return map;
    }

    private IList<IList<Tile>> _tiles = new List<IList<Tile>>();

    public Tile At(int x, int y)
    {
        return _tiles[y][x];
    }

    public class Tile
    {
        public Map Map { get; init; }
        public int X { get; init; }
        public int Y { get; init; }

        public Tile Up()
        {
            return Map.At(X, Y-1);
        }
        public Tile Down()
        {
            return Map.At(X, Y+1);
        }
        public Tile Left()
        {
            return Map.At(X-1, Y);
        }
        public Tile Right()
        {
            return Map.At(X+1, Y);
        }

        
    }
}