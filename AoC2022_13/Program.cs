using System.Text.Json;
using System.Text.Json.Nodes;
using AoC.Util;

var exampleInput =
    """
    [1,1,3,1,1]
    [1,1,5,1,1]

    [[1],[2,3,4]]
    [[1],4]

    [9]
    [[8,7,6]]

    [[4,4],4,4]
    [[4,4],4,4,4]

    [7,7,7,7]
    [7,7,7]

    []
    [3]

    [[[]]]
    [[]]

    [1,[2,[3,[4,[5,6,7]]]],8,9]
    [1,[2,[3,[4,[5,6,0]]]],8,9]
    """;

var realInput = File.ReadAllText("input.txt");

Console.WriteLine(Solve1(exampleInput));
Console.WriteLine(Solve2(exampleInput));

string Solve1(string input)
{
    int i = 0;
    int sum = 0;
    foreach (var (leftStr,rightStr) in input.Lines().PairsOfLines())
    {
        Console.WriteLine("next pair");
        i++;
        
    }

    return null;
}

bool IsInCorrectOrder(string leftStr, string rightStr)
{
    var leftEnumerator = (JsonNode.Parse(leftStr) as JsonArray).GetEnumerator();
    var rightEnumerator = (JsonNode.Parse(rightStr) as JsonArray).GetEnumerator();
    while (leftEnumerator.MoveNext() && rightEnumerator.MoveNext())
    {
        var left = leftEnumerator.Current.GetValue<JsonElement>();
        var right = rightEnumerator.Current.GetValue<JsonElement>();

    }
}

bool IsInCorrectOrder(JsonElement left, JsonElement right)
{
    if (left.ValueKind == JsonValueKind.Number && right.ValueKind == JsonValueKind.Number)
    {
        return left.GetInt32() < right.GetInt32();
    }

    if (left.ValueKind == JsonValueKind.Array && right.ValueKind == JsonValueKind.Array)
    {
        int i = 0;
        while (true)
        {
            if(i >= left.GetArrayLength())
                if (i >= right.GetArrayLength())
                    return true;
                else
                    return false;
            if(i >= right.GetArrayLength())
                if (i < left.GetArrayLength())
                    return false;
            if (!IsInCorrectOrder(left[i], right[i]))

            i++;
        }
    }

    if (left.ValueKind == JsonValueKind.Array && right.ValueKind == JsonValueKind.Number)
    {

    }

    if (left.ValueKind == JsonValueKind.Number && right.ValueKind == JsonValueKind.Array)
    {

    }
}

string Solve2(string input)
{
    return null;
}