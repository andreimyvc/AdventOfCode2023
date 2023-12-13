
namespace Day_01
{
    internal class Program
    {
        static Dictionary<string, int> Numbers = new Dictionary<string, int>()
        {
            { "0", 0},
            { "1", 1},
            { "2", 2},
            { "3", 3},
            { "4", 4},
            { "5", 5},
            { "6", 6},
            { "7", 7},
            { "8", 8},
            { "9", 9},
            { "zero", 0},
            { "one", 1},
            { "two", 2},
            { "three", 3},
            { "four", 4},
            { "five", 5},
            { "six", 6},
            { "seven", 7},
            { "eight", 8},
            { "nine", 9},
        };
        static void Main(string[] args)
        {
            string[] data = File.ReadAllLines("input.txt");
            int? sum = Calibrate2(data);

            Console.WriteLine($"Calibration : {sum}");
            Console.ReadKey();
        }

        private static int Calibrate(string[] data)
        {
            return data.Select(p => p.ToArray().Where(c => char.IsDigit(c)))
                .Sum(p => int.Parse(p.FirstOrDefault() + "" + p.LastOrDefault()));
        }

        private static int? Calibrate2(string[] data) 
        {
            int? firstIndex, firstNumber, lastIndex, lastNumber, firstTemp, lastTemp;

            int? sum = 0;

            foreach (string line in data) 
            {
                firstIndex = null;
                firstNumber = 0;
                lastIndex = 0;
                lastNumber = 0;

                foreach (string snumber in Numbers.Keys)
                {
                    firstTemp = line.IndexOf(snumber);
                    lastTemp = line.LastIndexOf(snumber);

                    if (firstTemp >= 0 && (firstTemp < firstIndex || firstIndex == null))
                    { 
                        firstIndex = firstTemp;
                        firstNumber = Numbers[snumber];
                    }

                    if (lastTemp >= lastIndex)
                    { 
                        lastIndex = lastTemp;
                        lastNumber = Numbers[snumber];
                    }
                }

                sum += (firstNumber * 10) + lastNumber;
            }

            return sum;
        }
    }
}
