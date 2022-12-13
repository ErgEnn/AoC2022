using System.Text;
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

Console.WriteLine(Solve1(realInput));
Console.WriteLine(Solve2(realInput));

AoCList Parse(string input,out int len)
{
    var root = new AoCList();
    var sb = new StringBuilder();
    for (int i = 1; i < input.Length; i++)
    {
        var c = input[i];
        if (c == '[')
        {
            root.Items.Add(Parse(input.Substring(i), out var length));
            i += length;
            continue;
        }

        if (c == ']')
        {
            if (sb.Length > 0)
            {
                root.Items.Add(new AoCValue() {Value = sb.ToString().ToInt()});
                sb.Clear();
            }

            len = i;
            return root;
        }

        if (c == ',')
        {
            if (sb.Length > 0)
            {
                root.Items.Add(new AoCValue() { Value = sb.ToString().ToInt() });
                sb.Clear();
            }
            continue;
        }

        sb.Append(c);

    }

    len = 0;
    return root;

}

string Solve1(string input)
{
    int i = 0;
    int sum = 0;
    foreach (var (leftStr,rightStr) in input.Lines().PairsOfLines())
    {
        i++;
        var left = Parse(leftStr, out _);
        var right = Parse(rightStr, out _);

        var comp = left.CompareTo(right);
        Console.WriteLine($"{i}: {comp > 0}");
        if (comp > 0)
            sum += i;
    }

    return sum.ToString();
}

string Solve2(string input)
{
    var ordered = input
        .Lines()
        .Where(s => !s.IsNullOrWhitespace())
        .Concat("[[2]]", "[[6]]")
        .Select(s => Parse(s, out _))
        .OrderByDescending(item => item, new AoCComparer())
        .Select(item => item.ToString())
        .ToList();
    foreach (var (i, line) in ordered.Indexed())
    {
        Console.WriteLine($"{i+1}:{line}");
    }
    var a = ordered.IndexOf("[[2]]")+1;
    var b = ordered.IndexOf("[[6]]")+1;
    return (a*b).ToString();
}

class AoCComparer : IComparer<AoCList>
{
    public int Compare(AoCList? left, AoCList? right)
    {
        return left.CompareTo(right);
    }
}

class AoCList : IComparable<AoCList>, IComparable<AoCValue>, IComparable<object>
{
    public IList<object> Items { get; } = new List<object>();

    public int CompareTo(AoCList? right)
    {
        var leftIter = Items.GetEnumerator();
        var rightIter = right.Items.GetEnumerator();
        while (true)
        {
            leftIter.MoveNext();
            rightIter.MoveNext();
            if (leftIter.Current == null || rightIter.Current == null)
                break;
            var cmp = (leftIter.Current as IComparable<object>).CompareTo(rightIter.Current);
            if (cmp != 0)
                return cmp;
        }
        if(leftIter.Current == null)
            if (rightIter.Current == null)
                return 0;
            else
                return 1;
        return -1;
    }

    public int CompareTo(AoCValue? right)
    {
        var temp = new AoCList();
        temp.Items.Add(right);
        return CompareTo(temp);
    }

    public int CompareTo(object? right)
    {
        if (right is AoCList list)
            return CompareTo(list);
        if (right is AoCValue val)
            return CompareTo(val);
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return $"[{string.Join(",", Items)}]";
    }
}

class AoCValue : IComparable<AoCList>, IComparable<AoCValue>, IComparable<object>
{
    public int Value { get; set; }

    public int CompareTo(AoCList? right)
    {
        return Math.Clamp(0 - right.CompareTo(this),-1,1);
    }

    public int CompareTo(AoCValue? right)
    {
        return Math.Clamp(right.Value - Value, -1, 1);
    }

    public int CompareTo(object? right)
    {
        if (right is AoCList list)
            return CompareTo(list);
        if (right is AoCValue val)
            return CompareTo(val);
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}