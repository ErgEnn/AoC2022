using AoC.Util;

var exampleInput =
    """
    Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
    Valve BB has flow rate=13; tunnels lead to valves CC, AA
    Valve CC has flow rate=2; tunnels lead to valves DD, BB
    Valve DD has flow rate=20; tunnels lead to valves CC, AA, EE
    Valve EE has flow rate=3; tunnels lead to valves FF, DD
    Valve FF has flow rate=0; tunnels lead to valves EE, GG
    Valve GG has flow rate=0; tunnels lead to valves FF, HH
    Valve HH has flow rate=22; tunnel leads to valve GG
    Valve II has flow rate=0; tunnels lead to valves AA, JJ
    Valve JJ has flow rate=21; tunnel leads to valve II
    """;

var realInput = File.ReadAllText("input.txt");
var valves = new Dictionary<string, Valve>();

Console.WriteLine($"Answer 1: {Solve1(exampleInput)}");
Console.WriteLine($"Answer 2: {Solve2(exampleInput)}");


string Solve1(string input)
{
    
    foreach (var line in input.Lines())
    {
        var valve =
            line.Deconstruct<string, int, string[]>($"Valve {null} has flow rate={0}; tunnels? leads? to valves? {", "}");
        valves.Add(valve.s1,new Valve(){Id = valve.s1, FlowRate = valve.s2, DirectChildrenIds = valve.s3});
    }

    foreach (var (_, valve) in valves)
    {
        RecursiveTraverse(valve,valve, 1);
    }



    return null;
}

void RecursiveTraverse(Valve startValve, Valve currentValve, int distance)
{
    foreach (var child in valves.ForKeys(currentValve.DirectChildrenIds))
    {
        if(child == startValve) continue;
        if(child == currentValve) continue;
        if (startValve.Distances.TryGetValue(child, out var existingDistance))
        {
            if (existingDistance > distance)
            {
                startValve.Distances[child] = distance;
            }
            else
            {
                continue;
            }
        }
        else
        {
            startValve.Distances.Add(child, distance);
        }
        RecursiveTraverse(startValve,child,distance+1);
    }
}

string Solve2(string input)
{
    return null;
}


class Valve
{
    public string Id { get; init; }
    public string[] DirectChildrenIds { get; init; }
    public int FlowRate { get; init; }
    public Dictionary<Valve, int> Distances { get; } = new();

}