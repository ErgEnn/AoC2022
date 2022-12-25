using System.ComponentModel;
using AoC.Util;

var exampleInput =
    """
    1=-0-2
    12111
    2=0=
    21
    2=01
    111
    20012
    112
    1=-1=
    1-12
    12
    1=
    122
    """;

var realInput = File.ReadAllText("input.txt");

Console.WriteLine($"Answer 1: {Solve1(realInput)}");
Console.WriteLine($"Answer 2: {Solve2(exampleInput)}");

string Solve1(string input)
{
    var dec = input.Lines().Select(s => ToDec(s)).Sum();
    Console.WriteLine(dec);
    return ToSnafu(dec);
}

long ToDec(string snafu)
{
    var val = 0L;
    for (int i = 0; i < snafu.Length; i++)
    {
        val += (long)Math.Pow(5, i) * snafu[snafu.Length-1-i] switch
        {
            '2' => 2,
            '1' => 1,
            '0' => 0,
            '-' => -1,
            '=' => -2
        };
    }
    return val;
}

string ToSnafu(long dec)
{
    var root = new Number {Value = (int) (dec % 5)};
    var current = root;
    dec /= 5;
    while (dec > 0)
    {
        current.AddNextDigit(new Number {Value = (int) (dec % 5)});
        current = current.NextDigit;
        dec /= 5;
    }
    
    return root.Traverse().Reverse().Select(n => n.ToString()).Join("");
}

string Solve2(string input)
{
    return null;
}

class Number
{
    public int Value { get; set; }

    public Number? NextDigit { get; private set; }

    public void AddNextDigit(Number number)
    {
        if (Value > 2)
            number.Value++;
        NextDigit = number;
    }

    public IEnumerable<Number> Traverse()
    {
        yield return this;
        foreach (var number in NextDigit?.Traverse() ?? Enumerable.Empty<Number>())
        {
            yield return number;
        }
    }

    public override string ToString()
    {
        return (Value switch
        {
            0 => '0',
            1 => '1',
            2 => '2',
            3 => '=',
            4 => '-'
        }).ToString();
    }
}



