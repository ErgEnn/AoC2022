﻿var input = File.ReadAllText("input.txt");
for (int i = 0; i < input.Length; i++)
{
    var slice = input.Substring(i,14);
    if (slice.Distinct().Count() == 14)
    {
        Console.WriteLine(i+14);
        break;
    }
}
