namespace AoC2022_01
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetAnswer1());
            Console.WriteLine(GetAnswer2());
        }

        public static int GetAnswer1()
        {
            return GetCalories(File.ReadAllLines("input.txt")).Max();
        }

        public static int GetAnswer2()
        {
            return GetCalories(File.ReadAllLines("input.txt")).OrderByDescending(i => i).Take(3).Sum();
        }

        public static IEnumerable<int> GetCalories(IEnumerable<string> lines)
        {
            var buffer = 0;
            var reset = false;
            foreach (var line in lines)
            {
                if (reset)
                {
                    reset = false;
                    buffer = 0;
                }
                if (string.IsNullOrEmpty(line))
                {
                    reset = true;
                    yield return buffer;
                }
                else
                {
                    buffer += int.Parse(line.Trim());
                }
            }

            yield return buffer;
        }
    }
}