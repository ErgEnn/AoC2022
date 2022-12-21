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

Console.WriteLine($"Answer 1: {Solve1(realInput)}");
Console.WriteLine($"Answer 2: {Solve2(realInput)}");

string Solve1(string input)
{
    var values = input.Lines().Select(line => line.ToLong()).ToArray();
    var positions = Enumerable.Range(0,values.Length).ToArray();
    
    Mix(values, positions);

    //Console.WriteLine(positions.ToFormattedString());
    //var orderedValues = values
    //    .Indexed()
    //    .Join(positions.Indexed(), tuple => tuple.i, tuple => tuple.i,
    //        (valueTuple, posTuple) => (val: valueTuple.value, pos: posTuple.value))
    //    .OrderBy(tuple => tuple.pos)
    //    .Select(tuple => tuple.val)
    //    .ToArray();
    //Console.WriteLine(orderedValues.ToFormattedString());

    var at1k = ValueAtPos(1000, values,positions);
    var at2k = ValueAtPos(2000, values,positions);
    var at3k = ValueAtPos(3000, values,positions);
    
    return (at1k+at2k+at3k).ToString();
}

void Mix(long[] values, int[] positions)
{
    var highestPos = positions.Length - 1;
    for (int indexOfVal = 0; indexOfVal < values.Length; indexOfVal++)
    {
        var val = values[indexOfVal];

        if (val == 0)
            continue;

        var newPos = (positions[indexOfVal] + val) % highestPos;
        newPos = newPos < 0 ? newPos + highestPos : newPos;

        var positionsToDecr = positions
            .Indexed()
            .Where(tuple => tuple.value >= positions[indexOfVal] && tuple.i != indexOfVal);

        foreach (var valueTuple in positionsToDecr)
        {
            positions[valueTuple.i] -= 1;
        }

        var positionsToUpdate = positions
            .Indexed()
            .Where(tuple => tuple.value >= newPos && tuple.i != indexOfVal)
            .OrderBy(tuple => tuple.value)
            .ToArray();

        for (int i = 0; i < positionsToUpdate.Length; i++)
        {
            positions[positionsToUpdate[i].i] = (int) newPos + 1 + i;
        }

        positions[indexOfVal] = (int) newPos;

    }
}

long ValueAtPos(int pos, long[] values, int[] positions)
{
    var indexOfZero = values.Indexed().First(tuple => tuple.value == 0).i;
    var posOfZero = positions[indexOfZero];
    var shift = pos % positions.Length;
    var posOfVal = (posOfZero + shift) % (positions.Length);
    var valAtPos = values[positions.Indexed().First(tuple => tuple.value == posOfVal).i];
    return valAtPos;
}

string Solve2(string input)
{
    var decryptionKey = 811589153;
    var values = input.Lines().Select(line => line.ToLong()).Select(i => i * decryptionKey).ToArray();
    var positions = Enumerable.Range(0, values.Length).ToArray();


    for (int i = 0; i < 10; i++)
    {
        Mix(values,positions);
    }

    //Console.WriteLine(positions.ToFormattedString());
    //var orderedValues = values
    //    .Indexed()
    //    .Join(positions.Indexed(), tuple => tuple.i, tuple => tuple.i,
    //        (valueTuple, posTuple) => (val: valueTuple.value, pos: posTuple.value))
    //    .OrderBy(tuple => tuple.pos)
    //    .Select(tuple => tuple.val)
    //    .ToArray();
    //Console.WriteLine(orderedValues.ToFormattedString());

    var at1k = ValueAtPos(1000, values, positions);
    var at2k = ValueAtPos(2000, values, positions);
    var at3k = ValueAtPos(3000, values, positions);

    return (at1k + at2k + at3k).ToString();
}