namespace Day_04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] data = File.ReadAllLines("input.txt");
            Console.WriteLine($"Part 01:{Part1(data)}");
            Console.WriteLine($"Part 02:{Part2(data.Skip(0).ToArray())}");
            Console.WriteLine(lista.Count);
            lista.Order().ToList().ForEach(x => Console.WriteLine(x));
        }

        static int Part1(string[] data)
        {
            int sum = 0, temp = 0;
            foreach (string line in data) 
            {
                var arr = line.Split('|');
                var winner = arr[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var games = arr[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var n in games)
                {
                    if (winner.Contains(n))
                    {
                        if (temp == 0) { temp = 1; }
                        else { temp *= 2; }
                    }
                }
                sum += temp;
                temp = 0;
            }

            return sum;
        }

        static List<string> lista = new List<string>();
        static int Part2Old(string[] data)
        {
            lista.AddRange(data);
            int sum = 0, temp = 0;
            string[] tempLine = default!;
            for (int i = 0; i < data.Length; i++)
            {
                var line = data[i];
                var arr = line.Split('|');
                var winner = arr[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var games = arr[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var n in games)
                {
                    if (winner.Contains(n))
                    {
                        temp++;
                    }
                }

                sum += temp;

                if (temp > 0)
                {
                    tempLine = data.Skip(i + 1).Take(temp).ToArray();

                    if (tempLine.Length > 0)
                    {
                        sum += Part2(tempLine);
                    }

                    temp = 0;
                }

            }            

            return sum;
        }

        static int Part2(string[] data)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();

            for (int i = 0; i < data.Length; i++)
            {
                var line = data[i];
                var arr = line.Split('|');
                var winner = arr[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var games = arr[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                
                int temp = 0;

                foreach (var b in games)
                {
                    if (winner.Contains(b))
                    {
                        temp++;
                    }
                }

                int n = 0;
                int key = 0;
                int y = 0;

                int next = 0;

                while (y < temp)
                {
                    key = i + y + 1;

                    if (n == 0)
                    {
                        result.TryGetValue(key, out n);
                        n += 1;
                    }

                    result.TryGetValue(key + 1, out next);

                    result[key + 1] = next + n;

                    y++;
                }
            }

            return result.Sum(p => p.Value) + data.Length;
        }
    }
}