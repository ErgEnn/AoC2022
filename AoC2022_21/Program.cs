using AoC.Util;

var exampleInput =
    """
    root: pppw + sjmn
    dbpl: 5
    cczh: sllz + lgvd
    zczc: 2
    ptdq: humn - dvpt
    dvpt: 3
    lfqf: 4
    humn: 5
    ljgn: 2
    sjmn: drzm * dbpl
    sllz: 4
    pppw: cczh / lfqf
    lgvd: ljgn * ptdq
    drzm: hmdt - zczc
    hmdt: 32
    """;

var realInput = File.ReadAllText("input.txt");


Console.WriteLine($"Answer 1: {Solve1(realInput)}");
Console.WriteLine($"Answer 2: {Solve2(realInput)}");

string Solve1(string input)
{
    var monkeys = new Dictionary<string, string>();
    foreach (var line in input.Lines())
    {
        var (monkey, valOrOp) = line.Deconstruct<string, string>(":");
        monkeys.Add(monkey, valOrOp.Trim());
    }


    return GetMonkeyVal(monkeys, "root").ToString();
}

long GetMonkeyVal(IDictionary<string,string> monkeys, string monkey)
{
    var valOrOp = monkeys[monkey];
    if (valOrOp.Length == 11)
    {
        long val = 0;
        switch (valOrOp[5])
        {
            case '+':
                val= valOrOp.Split(" + ") switch
                {
                    [string mon1, string mon2] => GetMonkeyVal(monkeys, mon1) + GetMonkeyVal(monkeys, mon2)
                };
                break;
            case '-':
                val = valOrOp.Split(" - ") switch
                {
                    [string mon1, string mon2] => GetMonkeyVal(monkeys, mon1) - GetMonkeyVal(monkeys, mon2)
                };
                break;
            case '*':
                val = valOrOp.Split(" * ") switch
                {
                    [string mon1, string mon2] => GetMonkeyVal(monkeys, mon1) * GetMonkeyVal(monkeys, mon2)
                };
                break;
            case '/':
                val = valOrOp.Split(" / ") switch
                {
                    [string mon1, string mon2] => GetMonkeyVal(monkeys, mon1) / GetMonkeyVal(monkeys, mon2)
                };
                break;
        }
        monkeys[monkey] = val.ToString();
        return val;
    }
    return long.Parse(valOrOp);
}

long GetMonkeyValAlt(IDictionary<string,HashSet<string>> monkeys, string monkey, Func<HashSet<string>,string> strategy)
{
    var ops = monkeys[monkey];
    var valOrOp = strategy(ops);
    //Console.WriteLine($"{monkey} = {valOrOp} ;alt = {string.Join(";", ops.Where(s => s!=valOrOp))}");
    if (valOrOp.Length == 11)
    {
        long val = 0;
        switch (valOrOp[5])
        {
            case '+':
                val= valOrOp.Split(" + ") switch
                {
                    [string mon1, string mon2] => GetMonkeyValAlt(monkeys, mon1, set => set.First(s => !s.Contains(monkey))) + GetMonkeyValAlt(monkeys, mon2, set => set.First(s => !s.Contains(monkey)))
                };
                break;
            case '-':
                val = valOrOp.Split(" - ") switch
                {
                    [string mon1, string mon2] => GetMonkeyValAlt(monkeys, mon1, set => set.First(s => !s.Contains(monkey))) - GetMonkeyValAlt(monkeys, mon2, set => set.First(s => !s.Contains(monkey)))
                };
                break;
            case '*':
                val = valOrOp.Split(" * ") switch
                {
                    [string mon1, string mon2] => GetMonkeyValAlt(monkeys, mon1, set => set.First(s => !s.Contains(monkey))) * GetMonkeyValAlt(monkeys, mon2, set => set.First(s => !s.Contains(monkey)))
                };
                break;
            case '/':
                val = valOrOp.Split(" / ") switch
                {
                    [string mon1, string mon2] => GetMonkeyValAlt(monkeys, mon1, set => set.First(s => !s.Contains(monkey))) / GetMonkeyValAlt(monkeys, mon2, set => set.First(s => !s.Contains(monkey)))
                };
                break;
        }
        monkeys[monkey] = new HashSet<string>(){ val.ToString()};
        return val;
    }
    return long.Parse(valOrOp);
}

string Solve2(string input)
{
    var monkeys = new Dictionary<string, string>();
    var reverseMonkeys = new Dictionary<string, HashSet<string>>();
    foreach (var line in input.Lines())
    {
        var (monkey, valOrOp) = line.Deconstruct<string, string>(":");
        valOrOp = valOrOp.Trim();
        monkeys.Add(monkey, valOrOp);
        if (valOrOp.Length == 11)
        {
            var oper = valOrOp[5];
            var (subMonke1, subMonke2) = valOrOp.Deconstruct<string, string>($" {oper} ");
            reverseMonkeys.AddToSetUnderKey(monkey, valOrOp);
            switch (oper)
            {
                case '+':
                    reverseMonkeys.AddToSetUnderKey(subMonke1, $"{monkey} - {subMonke2}");
                    reverseMonkeys.AddToSetUnderKey(subMonke2, $"{monkey} - {subMonke1}");
                    break;
                case '-':
                    reverseMonkeys.AddToSetUnderKey(subMonke1, $"{monkey} + {subMonke2}");
                    reverseMonkeys.AddToSetUnderKey(subMonke2, $"{subMonke1} - {monkey}");
                    break;
                case '*':
                    reverseMonkeys.AddToSetUnderKey(subMonke1, $"{monkey} / {subMonke2}");
                    reverseMonkeys.AddToSetUnderKey(subMonke2, $"{monkey} / {subMonke1}");
                    break;
                case '/':
                    reverseMonkeys.AddToSetUnderKey(subMonke1, $"{monkey} * {subMonke2}");
                    reverseMonkeys.AddToSetUnderKey(subMonke2, $"{subMonke1} / {monkey}");
                    break;
            }
        }
        else
        {
            reverseMonkeys.AddToSetUnderKey(monkey, valOrOp);
        }
    }

    var (side1_monkey, side2_monkey) = monkeys["root"].Deconstruct<string, string>(" + ");

    var side1 = GetMonkeyVal(monkeys, side1_monkey);
    var side2 = GetMonkeyVal(monkeys, side2_monkey);

    reverseMonkeys[side1_monkey] = new HashSet<string>(){ side2.ToString() }; 
    reverseMonkeys[side2_monkey] = new HashSet<string>(){ side1.ToString() }; 
    var humn = GetMonkeyValAlt(reverseMonkeys, "humn", set => set.First());

    return humn.ToString();
}