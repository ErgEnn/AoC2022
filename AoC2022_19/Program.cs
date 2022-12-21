using AoC.Util;

var exampleInput =
    """
    Blueprint 1: Each ore robot costs 4 ore. Each clay robot costs 2 ore. Each obsidian robot costs 3 ore and 14 clay. Each geode robot costs 2 ore and 7 obsidian.
    Blueprint 2: Each ore robot costs 2 ore. Each clay robot costs 3 ore. Each obsidian robot costs 3 ore and 8 clay. Each geode robot costs 3 ore and 12 obsidian.
    """;

var realInput = File.ReadAllText("input.txt");

Console.WriteLine($"Answer 1: {Solve1(exampleInput)}");
Console.WriteLine($"Answer 2: {Solve2(exampleInput)}");

string Solve1(string input)
{
    foreach (var (i,line) in input.Lines().Indexed())
    {
        var (oresPerOre,oresPerClay,oresPerObsidian,clayPerObsidian,oresPerGeode,obsidianPerGeode) = line.Deconstruct<int,int,int,int,int,int>(
            $"Blueprint \\d+: Each ore robot costs {1} ore. Each clay robot costs {2} ore. Each obsidian robot costs {3} ore and {4} clay. Each geode robot costs {5} ore and {6} obsidian.");
        var totalOrePerGeode = oresPerGeode + obsidianPerGeode * (oresPerObsidian + clayPerObsidian * oresPerClay);
        Console.WriteLine(totalOrePerGeode);
    }

    return null;
}

string Solve2(string input)
{
    return null;
}