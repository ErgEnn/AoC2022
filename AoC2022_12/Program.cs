using AoC.Util;

var exampleInput =
    """
    Sabqponm
    abcryxxl
    accszExk
    acctuvwj
    abdefghi
    """;

var realInput = File.ReadAllText("input.txt");

Console.WriteLine(Solve1(realInput));
Console.WriteLine(Solve2(realInput));

Map<HeightTile> Parse(string input)
{
    return Map<HeightTile>.From<HeightTile>(input.Lines(),tuple => new HeightTile()
    {
        X = tuple.x,
        Y = tuple.y,
        Data = tuple.data,
        Map = tuple.map
    });
}

string Solve1(string input)
{
    Map<HeightTile> map = Parse(input);
    var (start, end) = map.GetTiles().FirstMultisearch(tile => tile.Data == 'S', tile => tile.Data == 'E');

    var path = map.Pathfind(start, end);

    PrintMap(map,path);
    return (path.Count-1).ToString();
}

void PrintMap(Map<HeightTile> map, IEnumerable<HeightTile> path)
{

    map.PrintMap(tile =>
    {
        if (path.Contains(tile))
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        else
            Console.BackgroundColor = ConsoleColor.Black;

        Console.Write(tile.Data);
    });
    Console.BackgroundColor = ConsoleColor.Black;
}


string Solve2(string input)
{
    Map<HeightTile> map = Parse(input);
    var (start, end) = map.GetTiles().FirstMultisearch(tile => tile.Data == 'S', tile => tile.Data == 'E');
    var path = map.Pathfind(start,end);
    foreach (var altStart in map.GetTiles().Where(tile => tile.Data == 'a'))
    {
        var tempPath = map.Pathfind(altStart, end);
        if (tempPath != null && tempPath.Count < path.Count)
            path = tempPath;
    }

    PrintMap(map, path);
    return (path.Count - 1).ToString();
}

class HeightTile : Map<HeightTile>.Tile
{
    private string heights = "abcdefghijklmnopqrstuvwxyz";
    public int GetDataAsInt()
    {
        if (Data == 'S')
            return 0;
        if (Data == 'E')
            return 25;
        return heights.IndexOf(Data);
    }

    public override IEnumerable<HeightTile> GetNextTiles()
    {
        return Iter4Surrounding()
            .Select(tuple => tuple.tile)
            .Where(tile => tile != null)
            .Where(tile => GetDataAsInt() + 1 >= tile.GetDataAsInt());
    }
}