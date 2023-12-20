using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace Day_03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] data = File.ReadAllLines("input.txt");
            Console.WriteLine($"Parte 1: {Part1(data)}");
            Console.WriteLine($"Parte 2: {Part2(data)}");
        }


        static int Part1(string[] lines)
        {
            int sum = 0, posStart = 0, posEnd, down = 0, end = 0;

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

                    if (!nextChar.IsDigit() || right + 1 >= currentLine.Length)
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
                            end = 1 + posEnd - posStart;

                            end =  posStart + end < previousLine.Length ? end : previousLine.Length - posStart;

                            if (previousLine.Substring(posStart, end).Any(p => p.IsSymbol()))
                            {
                                Console.WriteLine(temp);
                                sum += int.Parse(temp);
                                temp = "";
                            }
                        }

                        if (temp.Length > 0 && down + 1 < lines.Length)
                        {
                            nextLine = lines[down + 1];
                            end = 2 + temp.Length;

                            end = posStart + end < nextLine.Length ? end : nextLine.Length - posStart;

                            if (nextLine.Substring(posStart, end).Any(p => p.IsSymbol()))
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

        static int Part2(string[] lines)
        {
            int down = 0, right = -1, count = 0, aux = 0;
            string currentLine = null!, auxLine = null!, tempA = "", tempB = "";
            bool ant = false;
            
            char c = ' ';

            while (down < lines.Length)
            {
                right++;

                if (right == 0)
                { 
                    currentLine = lines[down];
                }

                if (right < currentLine.Length)
                {
                    c = currentLine[right];

                    if (c.IsSymbol())
                    {
                        if (right > 0 && currentLine[right - 1].IsDigit()) 
                        { 
                            aux++;
                            tempA = ExtactNumber(currentLine, right - 1, false);
                        }

                        if (right < currentLine.Length - 1 && currentLine[right + 1].IsDigit()) 
                        { 
                            aux++;

                            if (aux == 1)
                            {
                                tempA = ExtactNumber(currentLine, right + 1, true);
                            }
                            else if(aux == 2)
                            {
                                tempB = ExtactNumber(currentLine, right + 1, true);
                            }

                        }

                        if (aux < 2 && down > 0)
                        {
                            auxLine = lines[down - 1];

                            if (right > 0 && auxLine[right - 1].IsDigit()) 
                            { 
                                aux++; ant = true;
                                FindNumber(right - 1, aux, auxLine, ref tempA, ref tempB, true);
                            }

                            if (!ant && right < auxLine.Length && auxLine[right].IsDigit()) 
                            { 
                                aux++; ant = true;
                                FindNumber(right, aux, auxLine, ref tempA, ref tempB, null);
                            }
                            else { ant = false; }

                            if (!ant && aux < 2 && !auxLine[right].IsDigit() && right + 1 < auxLine.Length && auxLine[right + 1].IsDigit()) 
                            { 
                                aux++; ant = true;
                                FindNumber(right+1, aux, auxLine, ref tempA, ref tempB, false);
                            }
                            
                        }

                        ant = false;

                        if (aux < 2 && down + 1 < lines.Length)
                        {
                            auxLine = lines[down + 1];

                            if (right > 0 && auxLine[right - 1].IsDigit()) 
                            { 
                                aux++; ant = true;
                                FindNumber(right - 1, aux, auxLine, ref tempA, ref tempB, true);
                            }

                            if (!ant && right < auxLine.Length && auxLine[right].IsDigit()) 
                            { 
                                aux++; ant = true;
                                FindNumber(right, aux, auxLine, ref tempA, ref tempB, null);
                            }
                            else { ant = false; }

                            if (!ant && aux < 2 && !auxLine[right].IsDigit() && right + 1 < auxLine.Length && auxLine[right + 1].IsDigit()) 
                            { 
                                aux++; ant = true;
                                FindNumber(right + 1, aux, auxLine, ref tempA, ref tempB, false);
                            }
                        }
                    }
                }

                if (aux >= 2) 
                {
                    Console.WriteLine(tempA + "*" + tempB);
                    count += int.Parse(tempA) * int.Parse(tempB); 
                }
                
                aux = 0;
                ant = false;
                auxLine = string.Empty;
                tempA = string.Empty;
                tempB = string.Empty;

                if (right == currentLine.Length - 1)
                {
                    right = -1;
                    down++;
                }
            }

            return count;
        }

        private static void FindNumber(int right, int aux, string auxLine, ref string tempA, ref string tempB, bool? leftFist)
        {
            if (aux == 1 && tempA.Length == 0)
            {
                tempA = ExtactNumber(auxLine, right, leftFist);
            }
            else if (aux == 1 && tempA.Length > 0)
            {
                tempB = ExtactNumber(auxLine, right, leftFist);
            }

            if (aux == 2)
            {
                if (tempA.Length == 0) { tempA = ExtactNumber(auxLine, right, leftFist); }
                tempB = ExtactNumber(auxLine, right, leftFist);
            }
        }

        private static string ExtactNumber(string currentLine, int right, bool? leftFist)
        {
            string temp = "";
            if (currentLine[right].IsDigit()) { temp = $"{currentLine[right]}"; }
            int i = 0;
            if (leftFist == true)
            {
                i = right - 1;

                while (i >= 0 && currentLine[i].IsDigit())
                {
                    temp = $"{currentLine[i]}{temp}";
                    i--;
                }

                i = right + 1;
                if (currentLine[right].IsDigit() || temp.Length == 0)
                {
                    while (i < currentLine.Length && currentLine[i].IsDigit())
                    {
                        temp = $"{temp}{currentLine[i]}";
                        i++;
                    }
                }
            }
            else if(leftFist ==  false)
            {
                i = right + 1;
                if (currentLine[right].IsDigit() || temp.Length == 0)
                {
                    while (i < currentLine.Length && currentLine[i].IsDigit())
                    {
                        temp = $"{temp}{currentLine[i]}";
                        i++;
                    }
                }

                i = right - 1;

                while (i >= 0 && currentLine[i].IsDigit())
                {
                    temp = $"{currentLine[i]}{temp}";
                    i--;
                }

            }
            else
            {
                i = right - 1;

                while (i >= 0 && currentLine[i].IsDigit())
                {
                    temp = $"{currentLine[i]}{temp}";
                    i--;
                }
                i = right + 1;
                while (i < currentLine.Length && currentLine[i].IsDigit())
                {
                    temp = $"{temp}{currentLine[i]}";
                    i++;
                }

            }



            return temp;
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
