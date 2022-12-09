using AoC.Util;

namespace AoC2022_05
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetAnswer1());
            Console.WriteLine(GetAnswer2());
        }

        public static string GetAnswer1()
        {
            return GetTopmostCrates(File.ReadAllLines("input.txt"));
        }

        public static string GetAnswer2()
        {
            return GetTopmostCratesWithMultiMove(File.ReadAllLines("input.txt"));
        }

        public static List<Stack<char>> GetInitialStacks(string[] input, out int firstActionRow)
        {
            var stacks = new List<Stack<char>>();
            int stackNumberRow = 0;
            while (!char.IsDigit(input[stackNumberRow][1]))
                stackNumberRow++;
            int colCount = (input[stackNumberRow].Length - 2) / 4 + 1;
            for (int colIndex = 0; colIndex < colCount; colIndex++)
            {
                int colCoord = 1 + colIndex * 4;
                stacks.Add(new Stack<char>());
                for (int rowIndex = stackNumberRow - 1; rowIndex >= 0; rowIndex--)
                {
                    var crateId = input[rowIndex][colCoord];
                    if (char.IsSeparator(crateId))
                    {
                        break;
                    }
                    stacks[colIndex].Push(crateId);
                }
            }
            firstActionRow = stackNumberRow + 2;
            return stacks;
        }

        public static string GetTopmostCrates(string[] input)
        {
            var stacks = GetInitialStacks(input, out var firstActionRow);

            for (int actionRow = firstActionRow; actionRow < input.Length; actionRow++)
            {
                var (_, amount, _, from, _, to) = input[actionRow].Deconstruct<string, int, string, int, string, int>();

                for (int i = 0; i < amount; i++)
                {
                    stacks[to - 1].Push(stacks[from - 1].Pop());
                }
            }
            return string.Join("", stacks.Select(stack => stack.First()));
        }

        public static string GetTopmostCratesWithMultiMove(string[] input)
        {
            var stacks = GetInitialStacks(input, out var firstActionRow);

            for (int actionRow = firstActionRow; actionRow < input.Length; actionRow++)
            {
                var (_, amount, _, from, _, to) = input[actionRow].Deconstruct<string, int, string, int, string, int>();
                var buffer = new char[amount];
                for (int i = 0; i < amount; i++)
                {
                    buffer[i] = stacks[from - 1].Pop();
                }

                for (int i = buffer.Length - 1; i >= 0; i--)
                {
                    stacks[to - 1].Push(buffer[i]);
                }
            }
            return string.Join("", stacks.Select(stack => stack.Peek()));
        }
    }
}