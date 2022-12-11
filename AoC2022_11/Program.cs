using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using AoC.Util;

var exampleInput =
    """
    Monkey 0:
      Starting items: 79, 98
      Operation: new = old * 19
      Test: divisible by 23
        If true: throw to monkey 2
        If false: throw to monkey 3

    Monkey 1:
      Starting items: 54, 65, 75, 74
      Operation: new = old + 6
      Test: divisible by 19
        If true: throw to monkey 2
        If false: throw to monkey 0

    Monkey 2:
      Starting items: 79, 60, 97
      Operation: new = old * old
      Test: divisible by 13
        If true: throw to monkey 1
        If false: throw to monkey 3

    Monkey 3:
      Starting items: 74
      Operation: new = old + 3
      Test: divisible by 17
        If true: throw to monkey 0
        If false: throw to monkey 1
    """;

var realInput = File.ReadAllText("input.txt");

Console.WriteLine(Solve1(realInput));
Console.WriteLine(Solve2(realInput));

IEnumerable<Monkey> ParseInput(IEnumerable<string> lines)
{
    var enumerator = lines.GetEnumerator();
    while (enumerator.MoveNext() && enumerator.MoveNext())
    {
        Monkey monkeyBuffer = new Monkey();
        var line = enumerator.Current;
        
        var items = line
            .Split(':')[1] //" 79, 98"
            .Replace(" ","") //"79,98"
            .Split(',') //["79","98"]
            .Select(s => (long)s.ToInt()) //[79,98]
            .ToArray();
        monkeyBuffer.Items = new Queue<long>(items);

        enumerator.MoveNext();
        line = enumerator.Current;

        var operationStr = line.Split(':')[1].Replace(" ", "");
        if (operationStr.Contains('*'))
        {
            var multiplierStr = operationStr.Split('*')[1];
            if (multiplierStr == "old") monkeyBuffer.Operation = i => i * i;
            else
            {
                var multiplier = multiplierStr.ToInt();
                monkeyBuffer.Operation = i => i * multiplier;
            }
        }
        else
        {
            var additive = operationStr.Split('+')[1].ToInt();
            monkeyBuffer.Operation = i => i + additive;
        }

        enumerator.MoveNext();
        line = enumerator.Current;

        var divisor = line.Split(' ').Last().ToInt();
        monkeyBuffer.TestThreshold = divisor;

        enumerator.MoveNext();
        line = enumerator.Current;

        var monkeyIndexIfTrue = line.Split(' ').Last().ToInt();
        monkeyBuffer.MonkeyIndexIfTestTrue = monkeyIndexIfTrue;

        enumerator.MoveNext();
        line = enumerator.Current;

        var monkeyIndexIfFalse = line.Split(' ').Last().ToInt();
        monkeyBuffer.MonkeyIndexIfTestFalse = monkeyIndexIfFalse;

        enumerator.MoveNext();

        yield return monkeyBuffer;
    }
    enumerator.Dispose();
}

string Solve1(string input)
{
    var monkeys = ParseInput(input.Lines()).ToArray();

    Console.WriteLine($"Before round {1}");
    for (int i = 0; i < monkeys.Length; i++) 
        Console.WriteLine($"Monkey {i}: {monkeys[i]}");

    for (int round = 1; round <= 20; round++)
    {
        foreach (var monkey in monkeys)
        {
            while (monkey.Items.TryDequeue(out var worryLvl))
            {
                worryLvl = monkey.Operation(worryLvl);
                worryLvl = (long) Math.Floor((decimal) worryLvl / 3);
                if (worryLvl % monkey.TestThreshold == 0)
                    monkeys[monkey.MonkeyIndexIfTestTrue].Items.Enqueue(worryLvl);
                else
                    monkeys[monkey.MonkeyIndexIfTestFalse].Items.Enqueue(worryLvl);

                monkey.ItemsInspected++;
            }
        }

        Console.WriteLine($"After round {round}");
        for (int i = 0; i < monkeys.Length; i++) Console.WriteLine($"Monkey {i}: {monkeys[i]}");

    }

    var result = monkeys.OrderByDescending(monkey => monkey.ItemsInspected).Take(2).Select(monkey => monkey.ItemsInspected).Multiply();
    return result.ToString();
}

string Solve2(string input)
{
    var monkeys = ParseInput(input.Lines()).ToArray();

    Console.WriteLine($"Before round {1}");
    for (int i = 0; i < monkeys.Length; i++)
        Console.WriteLine($"Monkey {i}: {monkeys[i]}");

    var modulus = monkeys.Select(monkey => monkey.TestThreshold).Multiply();

    for (int round = 1; round <= 10_000; round++)
    {
        foreach (var monkey in monkeys)
        {
            while (monkey.Items.TryDequeue(out var worryLvl))
            {
                worryLvl = monkey.Operation(worryLvl);
                worryLvl %= modulus;
                if (worryLvl % monkey.TestThreshold == 0)
                    monkeys[monkey.MonkeyIndexIfTestTrue].Items.Enqueue(worryLvl);
                else
                    monkeys[monkey.MonkeyIndexIfTestFalse].Items.Enqueue(worryLvl);

                monkey.ItemsInspected++;
            }
        }

        Console.WriteLine($"After round {round}");
        for (int i = 0; i < monkeys.Length; i++) Console.WriteLine($"Monkey {i}: {monkeys[i].ItemsInspected}");
        
    }

    var result = monkeys.OrderByDescending(monkey => monkey.ItemsInspected).Take(2).Select(monkey => monkey.ItemsInspected).Multiply();
    return result.ToString();
}

class Monkey
{
    public Queue<long> Items { get; set; }
    public Func<long, long> Operation { get; set; }
    public long TestThreshold { get; set; }
    public int MonkeyIndexIfTestTrue { get; set; }
    public int MonkeyIndexIfTestFalse { get; set; }
    public long ItemsInspected = 0;

    public override string ToString()
    {
        return string.Join(", ",Items);
    }
}