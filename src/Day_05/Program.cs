using System.Diagnostics;

namespace Day_05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string[] data = File.ReadAllLines("input.txt");
            Console.WriteLine($"Parte 1: {Part1(data)}");
            Console.WriteLine($"Parte 2: {Part2(data)}");
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed.ToString());  
            Console.ReadLine();
        }


        static long Part1(string[] data)
        {
            long util = 0, result = 0, temp = 0;
            IEnumerable<long> seeds = GetSeeds(data);
            var maps = GetMaps(data.Skip(2).ToArray());

            foreach (var seed in seeds)
            {
                util = seed;

                foreach (var map in maps)
                {
                    temp = 0;

                    foreach (var item in map)
                    {
                        if (util >= item[1] && util <= (item[1] + item[2] - 1))
                        {
                            temp = util - item[1] + item[0];
                            break;
                        }
                    }

                    if (temp != 0) { util = temp; }
                }

                if (result == 0 || util < result)
                {
                    result = util;
                }
            }

            return result;
        }
        static long Part2(string[] data)
        {
            long util = 0, result = 0, temp = 0;
            long[] seeds = GetSeeds(data);
            var maps = GetMaps(data.Skip(2).ToArray());

            for (int i = 1; i < seeds.Count(); i += 2)
            {
                for (long y = 0; y < seeds[i]; y++)
                {
                    util = seeds[i-1] + y;

                    foreach (var map in maps)
                    {
                        temp = 0;

                        foreach (var item in map)
                        {
                            if (util >= item[1] && util <= (item[1] + item[2] - 1))
                            {
                                temp = util - item[1] + item[0]; 
                                break;
                            }
                        }

                        if (temp != 0) { util = temp; }
                    }

                    if (result == 0 || util < result)
                    {
                        result = util;
                    }
                }
            }

            return result;
        }

        private static List<List<long[]>> GetMaps(string[] data)
        {
            List<List<long[]>> result = [];
            string mapsName = "seed-to-soil map:soil-to-fertilizer map:fertilizer-to-water map:water-to-light map:light-to-temperature map:temperature-to-humidity map:humidity-to-location map";

            string sdata = string.Join("\n", data);

            mapsName
                .Split(':', StringSplitOptions.RemoveEmptyEntries).ToList()
                .ForEach(x => 
                {
                    sdata = sdata.Replace(x, "");
                });

            var maps = sdata.Split(':', StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var map in maps)
            {
                List<long[]> ints = new List<long[]>();

                foreach (var line in map.Split("\n", StringSplitOptions.RemoveEmptyEntries))
                {
                    var x = line
                            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                            .Select(p => long.Parse(p))
                            .ToArray();

                    ints.Add(x);
                }

                result.Add(ints);
            }


            return result;
        }

        private static long[] GetSeeds(string[] data)
        {
            return data[0]
                .Replace("seeds: ", "")
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(p => long.Parse(p))
                .ToArray();
        }
    }
}
