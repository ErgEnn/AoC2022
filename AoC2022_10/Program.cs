using System.Text;
using AoC.Util;

var exampleInput =
    """
    addx 15
    addx -11
    addx 6
    addx -3
    addx 5
    addx -1
    addx -8
    addx 13
    addx 4
    noop
    addx -1
    addx 5
    addx -1
    addx 5
    addx -1
    addx 5
    addx -1
    addx 5
    addx -1
    addx -35
    addx 1
    addx 24
    addx -19
    addx 1
    addx 16
    addx -11
    noop
    noop
    addx 21
    addx -15
    noop
    noop
    addx -3
    addx 9
    addx 1
    addx -3
    addx 8
    addx 1
    addx 5
    noop
    noop
    noop
    noop
    noop
    addx -36
    noop
    addx 1
    addx 7
    noop
    noop
    noop
    addx 2
    addx 6
    noop
    noop
    noop
    noop
    noop
    addx 1
    noop
    noop
    addx 7
    addx 1
    noop
    addx -13
    addx 13
    addx 7
    noop
    addx 1
    addx -33
    noop
    noop
    noop
    addx 2
    noop
    noop
    noop
    addx 8
    noop
    addx -1
    addx 2
    addx 1
    noop
    addx 17
    addx -9
    addx 1
    addx 1
    addx -3
    addx 11
    noop
    noop
    addx 1
    noop
    addx 1
    noop
    noop
    addx -13
    addx -19
    addx 1
    addx 3
    addx 26
    addx -30
    addx 12
    addx -1
    addx 3
    addx 1
    noop
    noop
    noop
    addx -9
    addx 18
    addx 1
    addx 2
    noop
    noop
    addx 9
    noop
    noop
    noop
    addx -1
    addx 2
    addx -37
    addx 1
    addx 3
    noop
    addx 15
    addx -21
    addx 22
    addx -6
    addx 1
    noop
    addx 2
    addx 1
    noop
    addx -10
    noop
    noop
    addx 20
    addx 1
    addx 2
    addx 2
    addx -6
    addx -11
    noop
    noop
    noop
    """;

var realInput = File.ReadAllText("input.txt");

Console.WriteLine(Solve1(realInput));
Console.WriteLine(Solve2(realInput));

string Solve1(string input)
{
    var x = 1;
    var cycle = 0;
    var poi = new[] {20, 60, 100, 140, 180, 220};
    var strength = 0;

    int addx(int cnt)
    {
        cycle++;
        if (poi.Contains(cycle))
        {
            strength += cycle * x;
            Console.WriteLine($"{cycle}: {cycle * x}");
        }

        cycle++;
        if (poi.Contains(cycle))
        {
            strength += cycle * x;
            Console.WriteLine($"{cycle}: {cycle * x}");
        }

        x+=cnt;
        return 0;
    }

    int noop()
    {
        cycle++;
        if (poi.Contains(cycle))
        {
            strength += cycle * x;
            Console.WriteLine($"{cycle}: {cycle * x}");
        }

        return 0;
    }

    foreach (var line in input.Lines())
    {
        var _ = line.Split(' ') switch
        {
            [_] => noop(),
            [_, var cnt] => addx(cnt.ToInt())
        };
    }

    return strength.ToString();
}

string Solve2(string input)
{
    var x = 1;
    var cycle = 0;
    var sb = new List<string>();

    int addx(int cnt)
    {
        draw();
        cycle++;
        draw();
        cycle++;
        x += cnt;
        return 0;
    }

    int noop()
    {
        draw();
        cycle++;
        return 0;
    }

    void draw()
    {
        var cursor = cycle % 40;
        if (cursor == x || cursor == x - 1 || cursor == x + 1)
            sb.Add("#");
        else
            sb.Add(".");
    }

    foreach (var line in input.Lines())
    {
        var _ = line.Split(' ') switch
        {
            [_] => noop(),
            [_, var cnt] => addx(cnt.ToInt())
        };
    }

    var output = new StringBuilder();
    for (int i = 0; i < 6; i++)
    {
        output.AppendLine(string.Join("", sb.Skip(i*40).Take(40)));
    }

    return output.ToString();
}