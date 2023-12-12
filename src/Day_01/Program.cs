
namespace Day_01
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string[] data = File.ReadAllLines("day01.1.txt");
            int sum = Calibrate(data);

            Console.WriteLine($"Calibration : {sum}");
            Console.ReadKey();
        }

        private static int Calibrate(string[] data)
        {
            return data.Select(p => p.ToArray().Where(c => char.IsDigit(c)))
                .Sum(p => int.Parse(p.FirstOrDefault() + "" + p.LastOrDefault()));
        }
    }
}
