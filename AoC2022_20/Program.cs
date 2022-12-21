using AoC.Util;

var exampleInput =
    """
    1
    2
    -3
    3
    -2
    0
    4
    """;

var realInput = File.ReadAllText("input.txt");

Console.WriteLine($"Answer 1: {Solve1(exampleInput)}");
Console.WriteLine($"Answer 2: {Solve2(exampleInput)}");

string Solve1(string input)
{
    var vals = input.Lines().Select(line => line.ToInt()).ToArray();
    var positions = Enumerable.Range(0,vals.Length).ToArray();
    for (int indexOfVal = 0; indexOfVal < vals.Length; indexOfVal++)
    {
        Console.WriteLine(positions.ToFormattedString());
        var val = vals[indexOfVal];
        if(val == 0)
            continue;
        var newPosOfVal = (positions[indexOfVal] + val + (vals.Length-1)) % (vals.Length-1);
        newPosOfVal = newPosOfVal == 0 ? (vals.Length - 1) : newPosOfVal;

        var previousPoses = positions
            .Select((posOfVal, indexOfPos) => (indexOfPos, pos: posOfVal))
            .Where(tuple => tuple.pos <= newPosOfVal && tuple.indexOfPos != indexOfVal)
            .OrderBy(tuple => tuple.pos)
            .ToArray()
            .Indexed()
            .ToArray();
        foreach (var (index,indexInPos) in previousPoses)
        {
            positions[indexInPos.indexOfPos] = index;
        }
        positions[indexOfVal] = newPosOfVal;

    }
    Console.WriteLine(positions.ToFormattedString());
    return null;
}

string Solve2(string input)
{
    return null;
}