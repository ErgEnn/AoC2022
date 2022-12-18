using AoC.Util;

var exampleInput =
    """
    2,2,2
    1,2,2
    3,2,2
    2,1,2
    2,3,2
    2,2,1
    2,2,3
    2,2,4
    2,2,6
    1,2,5
    3,2,5
    2,1,5
    2,3,5
    """;

var realInput = File.ReadAllText("input.txt");

Console.WriteLine($"Answer 1: {Solve1(realInput)}");
Console.WriteLine($"Answer 2: {Solve2(realInput)}");

string Solve1(string input)
{
    var map = new HashSet<(int x, int y, int z)>();
    foreach (var line in input.Lines())
    {
        var cube = line.Deconstruct<int, int, int>(separator: ',');
        map.Add(cube);
    }

    var exposedSidesCount = 0;
    var translationVectors = new[] {(1, 0, 0), (-1, 0, 0), (0, 1, 0), (0, -1, 0), (0, 0, 1), (0, 0, -1)};
    foreach (var cube in map)
    {
        foreach (var vector in translationVectors)
        {
            if (!map.Contains(cube.Add(vector)))
            {
                exposedSidesCount++;
            }
        }
    }

    return exposedSidesCount.ToString();
}

string Solve2(string input)
{
    var dropletCubeMap = new HashSet<(int x, int y, int z)>();
    foreach (var line in input.Lines())
    {
        var cube = line.Deconstruct<int, int, int>(separator: ',');
        dropletCubeMap.Add(cube);
    }

    var start = (dropletCubeMap.MinBy(c => c.x).x-1, dropletCubeMap.MinBy(c => c.y).y-1, dropletCubeMap.MinBy(c => c.z).z-1);
    var end = (dropletCubeMap.MaxBy(c => c.x).x+1, dropletCubeMap.MaxBy(c => c.y).y+1, dropletCubeMap.MaxBy(c => c.z).z+1);
    var exposedSidesCount = 0;
    var checkVectors = new[] { (1, 0, 0), (-1, 0, 0), (0, 1, 0), (0, -1, 0), (0, 0, 1), (0, 0, -1) };
    var expansionVectors = new[] { (1, 0, 0), (-1, 0, 0), (0, 1, 0), (0, -1, 0), (0, 0, 1), (0, 0, -1) };
    var queue = new Queue<(int,int,int)>();
    var checkedCoords = new HashSet<(int,int,int)>();
    queue.Enqueue(start);
    
    while (queue.Count > 0)
    {
        var currentCoords = queue.Dequeue();
        checkedCoords.Add(currentCoords);
        foreach (var expansionVector in expansionVectors)
        {
            var newCoords = currentCoords.Add(expansionVector);
            if(newCoords.AnyGreaterThan(end) || newCoords.AnyLessThan(start))
                continue;
            if(checkedCoords.Contains(newCoords))
                continue;
            if(queue.Contains(newCoords))
                continue;
            if (dropletCubeMap.Contains(newCoords))
                exposedSidesCount++;
            else
                queue.Enqueue(newCoords);
        }
    }
    
    return exposedSidesCount.ToString();
}