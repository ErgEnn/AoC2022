var input = File.ReadAllLines("input.txt");

IEnumerable<(string,string,string)> TakeThree(IEnumerable<string> values)
{
    var enumer = values.GetEnumerator();
    while (enumer.MoveNext())
    {
        yield return (enumer.Current, enumer.MoveNext() ? enumer.Current : null, enumer.MoveNext() ? enumer.Current : null);
    }
}

var priorities = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
var total = 0;
foreach(var lines in TakeThree(input))
{
    var intersection = lines.Item1.Intersect(lines.Item2).Intersect(lines.Item3);
    if (intersection.Any())
    {
        total += intersection.Select(i => priorities.IndexOf(i)+1).Sum();
    }
}
Console.WriteLine(total);
