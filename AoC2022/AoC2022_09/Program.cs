using AoC.Util;

var exampleInput =
    """
    R 4
    U 4
    L 3
    D 1
    R 4
    D 1
    L 5
    R 2
    """;

var exampleInput2 =
    """
    R 5
    U 8
    L 8
    D 3
    R 17
    D 10
    L 25
    U 20
    """;

var realInput = File.ReadAllText("input.txt");

Console.WriteLine(Solve1(realInput));
Console.WriteLine(Solve2(realInput));

string Solve1(string input)
{
    var t_visited = new HashSet<(int x, int y)>();
    var x = 0;
    var y = 0;
    var tx = 0;
    var ty = 0;
    t_visited.Add((x, y));
    foreach (var line in input.Lines())
    {
        var parts = line.Split(' ') switch
        {
            [var direction, var amount] => (direction, amount:int.Parse(amount))
        };
        for (int i = 0; i < parts.amount; i++)
        {
            switch (parts.direction)
            {
                case "R":
                    x++;
                    break;
                case "U":
                    y--;
                    break;
                case "L":
                    x--;
                    break;
                case "D":
                    y++;
                    break;
            }

            if (x - tx > 1)
            {
                tx += 1;

                if (y - ty > 0)
                {
                    ty += 1;
                }

                if (y - ty < 0)
                {
                    ty -= 1;
                }
            }

            if (x - tx < -1)
            {
                tx -= 1;

                if (y - ty > 0)
                {
                    ty += 1;
                }

                if (y - ty < 0)
                {
                    ty -= 1;
                }
            }

            if (y - ty > 1)
            {
                ty += 1;

                if (x - tx > 0)
                {
                    tx += 1;
                }

                if (x - tx < 0)
                {
                    tx -= 1;
                }
            }

            if (y - ty < -1)
            {
                ty -= 1;

                if (x - tx > 0)
                {
                    tx += 1;
                }

                if (x - tx < 0)
                {
                    tx -= 1;
                }
            }

            t_visited.Add((tx, ty));
            //Console.WriteLine($"({tx},{ty})");
        }
        
    }

    return t_visited.Count().ToString();
}

string Solve2(string input)
{
    var t_visited = new HashSet<(int x, int y)>();
    var xh = 0;
    var yh = 0;
    var knots = new (int x, int y)[9];
    t_visited.Add((0, 0));
    foreach (var line in input.Lines())
    {
        var parts = line.Split(' ') switch
        {
            [var direction, var amount] => (direction, amount: int.Parse(amount))
        };
        for (int i = 0; i < parts.amount; i++)
        {
            switch (parts.direction)
            {
                case "R":
                    xh++;
                    break;
                case "U":
                    yh--;
                    break;
                case "L":
                    xh--;
                    break;
                case "D":
                    yh++;
                    break;
            }

            for (int knotIndex = 0; knotIndex < knots.Length; knotIndex++)
            {
                int x,y;
                if (knotIndex == 0)
                {
                    x = xh;
                    y = yh;
                }
                else
                {
                    x = knots[knotIndex - 1].x;
                    y = knots[knotIndex - 1].y;
                }
                
                if (x - knots[knotIndex].x > 1)
                {
                    knots[knotIndex].x += 1;

                    if (y - knots[knotIndex].y > 0)
                    {
                        knots[knotIndex].y += 1;
                    }

                    if (y - knots[knotIndex].y < 0)
                    {
                        knots[knotIndex].y -= 1;
                    }
                }

                if (x - knots[knotIndex].x < -1)
                {
                    knots[knotIndex].x -= 1;

                    if (y - knots[knotIndex].y > 0)
                    {
                        knots[knotIndex].y += 1;
                    }

                    if (y - knots[knotIndex].y < 0)
                    {
                        knots[knotIndex].y -= 1;
                    }
                }

                if (y - knots[knotIndex].y > 1)
                {
                    knots[knotIndex].y += 1;

                    if (x - knots[knotIndex].x > 0)
                    {
                        knots[knotIndex].x += 1;
                    }

                    if (x - knots[knotIndex].x < 0)
                    {
                        knots[knotIndex].x -= 1;
                    }
                }

                if (y - knots[knotIndex].y < -1)
                {
                    knots[knotIndex].y -= 1;

                    if (x - knots[knotIndex].x > 0)
                    {
                        knots[knotIndex].x += 1;
                    }

                    if (x - knots[knotIndex].x < 0)
                    {
                        knots[knotIndex].x -= 1;
                    }
                }
            }

            t_visited.Add((knots[8].x, knots[8].y));
            //Console.WriteLine($"({tx},{ty})");
        }

    }

    return t_visited.Count().ToString();
}



