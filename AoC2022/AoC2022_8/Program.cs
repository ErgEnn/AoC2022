var input2 =
    """
    30373
    25512
    65332
    33549
    35390
    """.Split(Environment.NewLine);

var input = File.ReadAllText("input.txt").Split(Environment.NewLine);

var visibles = 0;
var scenicScore = 0;
for (int y = 1; y < input.Length-1; y++)
{
    for (int x = 1; x < input[0].Length-1; x++)
    {
        var tree = int.Parse(input[y].Substring(x, 1));
        var top = 0;
        for (int offset = 1; offset <= y; offset++)
        {
            var h = int.Parse(input[y - offset].Substring(x, 1));
            if (h >= tree)
            {
                top++;
                break;
            }
            if (h < tree)
            {
                top++;
            }
        }

        var bottom = 0;
        for (int offset = 1; offset <= input.Length - 1 - y; offset++)
        {
            var h = int.Parse(input[y + offset].Substring(x, 1));
            if (h >= tree)
            {
                bottom++;
                break;
            }
            if (h < tree)
            {
                bottom++;
            }
        }

        var left = 0;
        for (int offset = 1; offset <= x; offset++)
        {
            var h = int.Parse(input[y].Substring(x - offset, 1));
            if (h >= tree)
            {
                left++;
                break;
            }
            if (h < tree)
            {
                left++;
            }
        }

        var right = 0;
        for (int offset = 1; offset <= input[0].Length - 1 - x; offset++)
        {
            var h = int.Parse(input[y].Substring(x + offset, 1));
            if (h >= tree)
            {
                right++;
                break;
            }
            if (h < tree)
            {
                right++;
            }
        }

        //Console.WriteLine($" {top}");
        //Console.WriteLine($"{left}{tree}{right}");
        //Console.WriteLine($" {bottom} ");
        //Console.WriteLine();
        scenicScore = Math.Max(scenicScore, top * bottom * left * right);
    }
}
Console.WriteLine(scenicScore);
//Console.WriteLine(visibles);
//visibles += input[0].Length * 2 + (input.Length - 2) * 2;
//Console.WriteLine(visibles);