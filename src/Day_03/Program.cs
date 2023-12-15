using System;
using System.Runtime.CompilerServices;

namespace Day_03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] data = File.ReadAllLines("input.txt");
            Console.WriteLine($"{Sum(data)}");
        }

       
        static int Sum(string[] lines)
        {
            int sum = 0, posStart = 0, posEnd, down = 0;

            string previousLine = string.Empty;
            string currentLine = lines[down];
            string nextLine = string.Empty;
            string temp = string.Empty;

            char previousChar = ' ', nextChar = ' ', tempChar;

            for (int right = 0; right < currentLine.Length; right++)
            {
                var c = currentLine[right];

                if (c.IsDigit())
                {
                    temp = $"{temp}{c}";

                    if (temp.Length == 1)
                    {
                        if (right - 1 >= 0)
                        {
                            posStart = right - 1;
                        }
                        else
                        {
                            posStart = right;
                        }
                        previousChar = currentLine[posStart];
                    }

                    if (right + 1 < currentLine.Length)
                    {
                        posEnd = right + 1;
                    }
                    else
                    {
                        posEnd = right;
                    }

                    nextChar = currentLine[posEnd];

                    if (!nextChar.IsDigit() || posEnd + 1 >= currentLine.Length)
                    {
                        if (previousChar.IsSymbol())
                        {
                            Console.WriteLine(temp);
                            sum += int.Parse(temp);
                            temp = "";
                        }
                        else if (nextChar.IsSymbol())
                        {
                            Console.WriteLine(temp);
                            sum += int.Parse(temp);
                            temp = "";
                        }

                        if (temp.Length > 0 && down > 0)
                        {
                            previousLine = lines[down - 1];

                            if (previousLine.Substring(posStart, 1 + posEnd - posStart).Any(p => p.IsSymbol()))
                            {
                                Console.WriteLine(temp);
                                sum += int.Parse(temp);
                                temp = "";
                            }
                        }

                        if (temp.Length > 0 && down + 1 < lines.Length)
                        {
                            nextLine = lines[down + 1];

                            if (nextLine.Substring(posStart, 1 + posEnd - posStart).Any(p => p.IsSymbol()))
                            {
                                Console.WriteLine(temp);
                                sum += int.Parse(temp);
                                temp = "";
                            }
                        }

                        if (!nextChar.IsDigit() || posEnd == currentLine.Length)
                        {
                            temp = "";
                        }
                    }

                    
                }

                if (right + 1 == currentLine.Length)
                {
                    right = -1;
                    down++;
                    temp = "";

                    if (down == lines.Length)
                    {
                        return sum;
                    }
                    
                    currentLine = lines[down];

                }
            }

            return sum;
        }
    }

    public static class ExtensionMethods
    {
        public static bool IsSymbol(this char c)
        {
            if (c == null || c == ' ')
                return false;

            return !char.IsDigit(c)  &&  c != '.';
        }

        public static bool IsDigit(this char c)
        {
            if (c == null || c == ' ')
                return false;

            return char.IsDigit(c);
        }
    }
}
