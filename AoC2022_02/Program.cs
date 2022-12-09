namespace AoC2022_02
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetAnswer1());
        }

        public static int GetAnswer1()
        {
            return GetScores(File.ReadAllLines("input.txt")).Sum();
        }

        public static int GetAnswer2()
        {
            return GetScoresByStrategy(File.ReadAllLines("input.txt")).Sum();
        }

        public static IEnumerable<int> GetScores(IEnumerable<string> lines)
        {
            var lookup = new Dictionary<string, int>()
            {
                {"AX", 1 + 3},
                {"AY", 2 + 6},
                {"AZ", 3 + 0},
                {"BX", 1 + 0},
                {"BY", 2 + 3},
                {"BZ", 3 + 6},
                {"CX", 1 + 6},
                {"CY", 2 + 0},
                {"CZ", 3 + 3},
            };
            foreach (var line in lines)
            {
                yield return lookup[line.Trim().Replace(" ", "")];
            }
        }

        public static IEnumerable<int> GetScoresByStrategy(IEnumerable<string> lines)
        {
            var lookup = new Dictionary<string, int>()
            {
                // X lose
                // Y draw
                // Z win
                {"AX", 3 + 0},
                {"AY", 1 + 3},
                {"AZ", 2 + 6},
                {"BX", 1 + 0},
                {"BY", 2 + 3},
                {"BZ", 3 + 6},
                {"CX", 2 + 0},
                {"CY", 3 + 3},
                {"CZ", 1 + 6},
            };

            foreach (var line in lines)
            {
                yield return lookup[line.Trim().Replace(" ", "")];
            }
        }
    }
}