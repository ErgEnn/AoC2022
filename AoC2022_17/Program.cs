var exampleInput =
    """
    >>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>
    """;

var realInput = File.ReadAllText("input.txt");

Console.WriteLine($"Answer 1: {Solve1(exampleInput)}");
Console.WriteLine($"Answer 2: {Solve2(exampleInput)}");

string Solve1(string input)
{
    var jets = input.Split("");
    var highestRock = 0;
    return null;
}

string Solve2(string input)
{
    return null;
}