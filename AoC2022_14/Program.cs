using AoC.Util;
using System.Collections.Generic;

var exampleInput =
    """
    498,4 -> 498,6 -> 496,6
    503,4 -> 502,4 -> 502,9 -> 494,9
    """;

var realInput = File.ReadAllText("input.txt");

Console.WriteLine(Solve1(realInput));
Console.WriteLine(Solve2(realInput));

HashSet<(int x, int y)> ParseRocks(string input)
{
    HashSet<(int x, int y)> items = new HashSet<(int x, int y)>();
    foreach (var line in input.Lines())
    {
        var vertices = line
            .Split(" -> ")
            .Select(s => s.Deconstruct<int, int>(","));
        foreach (var (startVertex, endVertex) in vertices.Pairwise())
        {
            foreach (var x in startVertex.s1.StepTowards(endVertex.s1))
            {
                items.Add((x, startVertex.s2));
            }
            foreach (var y in startVertex.s2.StepTowards(endVertex.s2))
            {
                items.Add((startVertex.s1, y));
            }
        }
    }
    return items;
}

string Solve1(string input)
{
    var items = ParseRocks(input);

    var max_y = items.MaxBy(tuple => tuple.y).y;
    var sandCount = 0;
    while (true)
    {
        var sandPos = (x:500,y:0);
        while (true)
        {
            var potentialSandPos = sandPos with {y = sandPos.y+1};
            if (potentialSandPos.y > max_y)
            {
                sandPos = potentialSandPos;
                break;
            }

            if (!items.Contains(potentialSandPos))
            {
                sandPos = potentialSandPos;
                continue;
            }
            potentialSandPos = (x:sandPos.x-1,y:sandPos.y+1);
            if (!items.Contains(potentialSandPos))
            {
                sandPos = potentialSandPos;
                continue;
            }
            potentialSandPos = (x:sandPos.x+1,y:sandPos.y+1);
            if (!items.Contains(potentialSandPos))
            {
                sandPos = potentialSandPos;
                continue;
            }
            break;
        }

        if (sandPos.y > max_y)
            break;
        items.Add(sandPos);
        sandCount++;
    }
    PrintMap(items);
    return sandCount.ToString();
}

void PrintMap(HashSet<(int x, int y)> items)
{
    var min_x = items.MinBy(tuple => tuple.x).x;
    var max_x = items.MaxBy(tuple => tuple.x).x;
    var min_y = items.MinBy(tuple => tuple.y).y;
    var max_y = items.MaxBy(tuple => tuple.y).y;
    for (int y = min_y; y <= max_y; y++)
    {
        for (int x = min_x; x <= max_x; x++)
        {
            if(items.Contains((x,y)))
                Console.Write("#");
            else
                Console.Write(".");
        }
        Console.WriteLine();
    }
    
}

string Solve2(string input)
{
    var items = ParseRocks(input);

    var min_x = items.MinBy(tuple => tuple.x).x;
    var max_x = items.MaxBy(tuple => tuple.x).x;
    var max_y = items.MaxBy(tuple => tuple.y).y;

    for (int x = min_x-min_x; x < max_x*2; x++)
    {
        items.Add((x, max_y + 2));
    }


    var sandCount = 0;
    while (true)
    {
        var sandPos = (x: 500, y: 0);
        while (true)
        {
            var potentialSandPos = sandPos with { y = sandPos.y + 1 };
            if (!items.Contains(potentialSandPos))
            {
                sandPos = potentialSandPos;
                continue;
            }
            potentialSandPos = (x: sandPos.x - 1, y: sandPos.y + 1);
            if (!items.Contains(potentialSandPos))
            {
                sandPos = potentialSandPos;
                continue;
            }
            potentialSandPos = (x: sandPos.x + 1, y: sandPos.y + 1);
            if (!items.Contains(potentialSandPos))
            {
                sandPos = potentialSandPos;
                continue;
            }
            break;
        }

        if (sandPos == (x: 500, y: 0))
            break;
        items.Add(sandPos);
        sandCount++;
    }

    return sandCount.ToString();
}