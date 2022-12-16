string exampleData =
    """
    1000
    2000
    3000

    4000

    5000
    6000

    7000
    8000
    9000

    10000
    """;
string[] dataLines = exampleData.Split(Environment.NewLine);
int bestElfCalories = 0;
int currentElfCalories = 0;
for (int i = 0; i < dataLines.Length; i++)
{
    if (dataLines[i] == "")
    {
        if (currentElfCalories > bestElfCalories)
        {
            bestElfCalories = currentElfCalories;
        }

        currentElfCalories = 0;
    }
    else
    {
        currentElfCalories += int.Parse(dataLines[i]);
    }
}
Console.WriteLine(bestElfCalories);