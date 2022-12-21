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
Console.WriteLine($"Answer 2: {Solve2(exampleInput)}");

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
        switch (valOrOp[5])
        {
                case '+':
                    return valOrOp.Split(" + ") switch
                    {
                        [string mon1, string mon2] => GetMonkeyVal(monkeys, mon1) + GetMonkeyVal(monkeys, mon2)
                    };
                case '-':
                return valOrOp.Split(" - ") switch
                {
                    [string mon1, string mon2] => GetMonkeyVal(monkeys, mon1) - GetMonkeyVal(monkeys, mon2)
                };
            case '*':
                return valOrOp.Split(" * ") switch
                {
                    [string mon1, string mon2] => GetMonkeyVal(monkeys, mon1) * GetMonkeyVal(monkeys, mon2)
                };
            case '/':
                return valOrOp.Split(" / ") switch
                {
                    [string mon1, string mon2] => GetMonkeyVal(monkeys, mon1) / GetMonkeyVal(monkeys, mon2)
                };
        }
    }
    return long.Parse(valOrOp);
}

long GetMonkeyValAdv(IDictionary<string,string> monkeys, IDictionary<string,string> reverseMonkeys, string monkey)
{
    var valOrOp = monkeys[monkey];
    if (monkey == "humn")
        return GetMonkeyVal(reverseMonkeys, monkey);
    if (valOrOp.Length == 11)
    {
        var oper = valOrOp[5];
        var (subMonke1, subMonke2) = valOrOp.Deconstruct<string, string>($" {oper} ");
        switch (oper)
        {
            case '+':
                reverseMonkeys.Add(subMonke1, $"{monkey} - {subMonke2}");
                reverseMonkeys.Add(subMonke2, $"{monkey} - {subMonke1}");
                return GetMonkeyValAdv(monkeys, reverseMonkeys, subMonke1) +
                       GetMonkeyValAdv(monkeys, reverseMonkeys, subMonke2);
            case '-':
                reverseMonkeys.Add(subMonke1, $"{monkey} + {subMonke2}");
                reverseMonkeys.Add(subMonke2, $"{subMonke1} - {monkey}");
                return GetMonkeyValAdv(monkeys, reverseMonkeys, subMonke1) -
                       GetMonkeyValAdv(monkeys, reverseMonkeys, subMonke2);
            case '*':
                reverseMonkeys.Add(subMonke1, $"{monkey} / {subMonke2}");
                reverseMonkeys.Add(subMonke2, $"{monkey} / {subMonke1}");
                return GetMonkeyValAdv(monkeys, reverseMonkeys, subMonke1) *
                       GetMonkeyValAdv(monkeys, reverseMonkeys, subMonke2);
            case '/':
                reverseMonkeys.Add(subMonke1, $"{monkey} * {subMonke2}");
                reverseMonkeys.Add(subMonke2, $"{monkey} / {subMonke1}");
                return GetMonkeyValAdv(monkeys, reverseMonkeys, subMonke1) /
                       GetMonkeyValAdv(monkeys, reverseMonkeys, subMonke2);
        }
    }
    reverseMonkeys.Add(monkey,valOrOp);
    return long.Parse(valOrOp);
}

string Solve2(string input)
{
    var monkeys = new Dictionary<string, string>();
    var reverseMonkeys = new Dictionary<string, string>();
    foreach (var line in input.Lines())
    {
        var (monkey, valOrOp) = line.Deconstruct<string, string>(":");
        monkeys.Add(monkey, valOrOp.Trim());
    }
    var side1 = GetMonkeyVal(monkeys, "sjmn");//ngpl
    reverseMonkeys.Add("pppw",side1.ToString()); //pvgq
    var side2 = GetMonkeyValAdv(monkeys,reverseMonkeys, "pppw");//pvgq

    return null;
}