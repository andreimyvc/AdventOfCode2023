
namespace Day_02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] data = File.ReadAllLines("day02.1.txt");
            var gameList = Parse(data);
            int? sum = gameList.Sum(p => p.Id);

            Console.WriteLine($"What is the sum of the IDs of those games?: {sum}");
            Console.ReadKey();
        }

        private static List<Game> Parse(string[] data)
        {
            //Game 1: 2 red, 2 green; 6 red, 3 green; 2 red, 1 green, 2 blue; 1 red

            var gameList = new List<Game>();

            foreach (var item in data.Select(p => p.Split(':'))) 
            {
                var game = new Game
                {
                    Id = int.Parse(item[0].Replace("Game ", ""))
                };

                bool success = true;
                foreach (var sub in item[1].Split(';'))
                {
                    var cubos = sub
                        .Split(',')
                        .Select(p => p
                            .Split(' ', StringSplitOptions.RemoveEmptyEntries))
                        .Select(p => new { amount = int.Parse(p[0]), color = p[1] });

                    game.Red = cubos.Where(p => p.color == "red").Sum(p => p.amount);
                    game.Green = cubos.Where(p => p.color == "green").Sum(p => p.amount);
                    game.Blue = cubos.Where(p => p.color == "blue").Sum(p => p.amount);

                    if (game.Red > 12 || game.Green > 13 || game.Blue > 14)
                    {
                        success =  false;
                    }
                }

                if(success)
                {
                    gameList.Add(game);
                }

            }

            return gameList;
        }
    }

    class Game
    {
        public int Id { get; set; }
        public int  Green { get; set; }
        public int Blue { get; set; }
        public int Red { get; set; }
    }
}
