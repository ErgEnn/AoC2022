using System.Diagnostics;
using AoC.Util;

var exampleInput =
    """
    Sensor at x=2, y=18: closest beacon is at x=-2, y=15
    Sensor at x=9, y=16: closest beacon is at x=10, y=16
    Sensor at x=13, y=2: closest beacon is at x=15, y=3
    Sensor at x=12, y=14: closest beacon is at x=10, y=16
    Sensor at x=10, y=20: closest beacon is at x=10, y=16
    Sensor at x=14, y=17: closest beacon is at x=10, y=16
    Sensor at x=8, y=7: closest beacon is at x=2, y=10
    Sensor at x=2, y=0: closest beacon is at x=2, y=10
    Sensor at x=0, y=11: closest beacon is at x=2, y=10
    Sensor at x=20, y=14: closest beacon is at x=25, y=17
    Sensor at x=17, y=20: closest beacon is at x=21, y=22
    Sensor at x=16, y=7: closest beacon is at x=15, y=3
    Sensor at x=14, y=3: closest beacon is at x=15, y=3
    Sensor at x=20, y=1: closest beacon is at x=15, y=3
    """;

var realInput = File.ReadAllText("input.txt");

//Console.WriteLine($"Answer 1: {Solve1(realInput)}");
Console.WriteLine($"Answer 2: {Solve2(realInput)}");

int Dist(int x1, int y1, int x2, int y2) => Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
bool IsWithinDist(int x1, int y1, int dist, int x2, int y2) => Dist(x1, y1, x2, y2) <= dist;

string Solve1(string input)
{
    var sensors = new HashSet<(int x, int y, int dist)>();
    var beacons = new HashSet<(int x, int y)>();
    var covered = new HashSet<(int x, int y)>();
    foreach (var line in input.Lines())
    {
        (int sx, int sy, int bx, int by) =
            line.Deconstruct<int, int, int, int>(
                $"Sensor at x={null}, y={null}: closest beacon is at x={null}, y={null}");
        var dist = Dist(sx,sy,bx,by);
        sensors.Add((sx,sy,dist));
        beacons.Add((bx,by));
    }

    var answer_y = 2_000_000;

    foreach (var sensor in sensors)
    {
        for (int x = sensor.x - sensor.dist; x <= sensor.x + sensor.dist; x++)
        {
            if (IsWithinDist(sensor.x, sensor.y, sensor.dist, x, answer_y))
                covered.Add((x, answer_y));
        }
    }

    return (covered.Count(coord => coord.y == answer_y)-beacons.Count(coord => coord.y == answer_y)).ToString();
}


void PrintMap(params (char symbol ,HashSet<(int x, int y)> items)[] symbols)
{
    var min_x = symbols.SelectMany(s => s.items).MinBy(tuple => tuple.x).x;
    var max_x = symbols.SelectMany(s => s.items).MaxBy(tuple => tuple.x).x;
    var min_y = symbols.SelectMany(s => s.items).MinBy(tuple => tuple.y).y;
    var max_y = symbols.SelectMany(s => s.items).MaxBy(tuple => tuple.y).y;
    for (int y = min_y; y <= max_y; y++)
    {
        Console.Write($"{y.ToString().PadLeft(Math.Max(max_y.ToString().Length, min_y.ToString().Length))}");
        for (int x = min_x; x <= max_x; x++)
        {
            foreach ((char symbol, HashSet<(int x, int y)> items) tuple in symbols)
            {
                if (tuple.items.Contains((x, y)))
                {
                    Console.Write(tuple.symbol);
                    goto next;
                }
            }
            Console.Write(".");
            next: ;
        }
        Console.WriteLine();
    }

}

string Solve2(string input)
{
    var sensors = new HashSet<(int x, int y, int dist)>();
    
    var max = 4_000_000;
    var sw = new Stopwatch();
    sw.Start();
    foreach (var line in input.Lines())
    {
        (int sx, int sy, int bx, int by) =
            line.Deconstruct<int, int, int, int>(
                $"Sensor at x={null}, y={null}: closest beacon is at x={null}, y={null}");
        var dist = Dist(sx, sy, bx, by);
        sensors.Add((sx, sy, dist));
    }
    sw.Stop();
    Console.WriteLine($"Parse: {sw.ElapsedMilliseconds} ms");
    sw.Restart();
    var coords_original = Original(sensors, max);
    sw.Stop();
    Console.WriteLine($"Original: {sw.ElapsedMilliseconds} ms");
    sw.Restart();
    var coords_quick = Quick(sensors, max);
    sw.Stop();
    Console.WriteLine($"Quick: {sw.ElapsedMilliseconds} ms");
    if (coords_quick != coords_original)
        throw new Exception();
    var coords = coords_original;
    return ((long)coords.x*4_000_000+coords.y).ToString();
}

(int x, int y) Quick(HashSet<(int x, int y, int dist)> sensors, int max)
{
    var bb_tl = Vertex.FromTuple((0, 0));
    var bb_br = Vertex.FromTuple((max, max));
    var bb = Rect.FromCorners(bb_tl, bb_br);
    var linesInBB = new List<Line>();
    foreach (var (sx, sy, dist) in sensors)
    {
        // q3/\q0
        // q2\/q1
        
        var v0 = Vertex.FromTuple((sx, sy - dist - 1));
        var v1 = Vertex.FromTuple((sx + dist + 1, sy));
        var v2 = Vertex.FromTuple((sx, sy + dist + 1));
        var v3 = Vertex.FromTuple((sx - dist - 1, sy));
        var q0 = Line.FromPoints(v0, v1);
        var q1 = Line.FromPoints(v1, v2);
        var q2 = Line.FromPoints(v2, v3);
        var q3 = Line.FromPoints(v3, v0);
        var lines = new Line[] {q0, q1, q2, q3};
        foreach (var line in lines)
        {
            if (bb.Intersect(line) is Line l)
            {
                linesInBB.Add(l);
            }
        }
    }

    var intersections = new List<Vertex>();
    foreach (var l1 in linesInBB)
    {
        foreach (var l2 in linesInBB)
        {
            if(l1 == l2)
                continue;
            if (l1.Intersect(l2) is Vertex v)
            {
                intersections.Add(v);
            }
        }
    }

    var coords = (0, 0);
    foreach (var intersection in intersections)
    {
        foreach (var sensor in sensors)
        {
            if (IsWithinDist(sensor.x, sensor.y, sensor.dist, intersection.X, intersection.Y))
                goto next;
        }
        coords = (intersection.X, intersection.Y);
        break;
        next: ;
    }

    return coords;
}

(int x, int y) Original(HashSet<(int x, int y, int dist)> sensors, int max)
{
    var sw = new Stopwatch();
    var boundary = new HashSet<(int x, int y)>();
    sw.Restart();
    foreach (var (sx, sy, dist) in sensors)
    {
        for (int step = 0; step < dist + 1; step++)
        {
            var q0 = (sx + step, sy - dist - 1 + step);
            if (q0.IsIn(0, max)) boundary.Add(q0);
            var q1 = (sx + dist + 1 - step, sy + step);
            if (q1.IsIn(0, max)) boundary.Add(q1);
            var q2 = (sx - step, sy + dist + 1 - step);
            if (q2.IsIn(0, max)) boundary.Add(q2);
            var q3 = (sx + dist + 1 - step, sy - step);
            if (q3.IsIn(0, max)) boundary.Add(q3);
        }
    }
    sw.Stop();
    Console.WriteLine($"Find boundary: {sw.ElapsedMilliseconds} ms");
    sw.Restart();
    var coords = (x: 0, y: 0);
    foreach (var coord in boundary)
    {
        if (coord.x > max || coord.x < 0 || coord.y > max || coord.y < 0)
            continue;
        foreach (var sensor in sensors)
            if (IsWithinDist(sensor.x, sensor.y, sensor.dist, coord.x, coord.y))
                goto end;

        coords = coord;
        break;
        end:;
    }
    sw.Stop();
    Console.WriteLine($"Find point: {sw.ElapsedMilliseconds} ms");
    return coords;
}