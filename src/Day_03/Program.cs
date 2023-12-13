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
            int sum = 0, posStart = 0, posEnd;

            string previousLine = string.Empty;
            string currentLine = string.Empty;
            string nextLine = string.Empty;
            char tempChar;

            for (int top = 0; top < lines.Length; top++) 
            {
                currentLine = lines[top];

                string temp = "";

                for (int left = 0; left < currentLine.Length; left++)
                {
                    var c = currentLine[left];

                    if (char.IsDigit(c))
                    {
                        temp = $"{temp}{c}";
                    }
                    
                    if(temp.Length > 0 && (left >= currentLine.Length - 1 || !char.IsDigit(currentLine[left + 1])))
                    {
                        //LEFT
                        if (left < currentLine.Length - 1)
                        {
                            if (left - 1 >= 0)
                            {
                                tempChar = currentLine[left - 1];

                                if (tempChar != '.' && !char.IsDigit(tempChar))
                                {
                                    sum += int.Parse(temp);
                                    Console.WriteLine(temp);
                                    temp = string.Empty;
                                    continue;
                                }
                            }                            
                        }

                        //RIGHT
                        if (left - temp.Length - 1 >= 0)
                        {
                            if (left + 1 < currentLine.Length)
                            {
                                tempChar = currentLine[left + 1];

                                if (tempChar != '.' && !char.IsDigit(tempChar))
                                {
                                    sum += int.Parse(temp);
                                    Console.WriteLine(temp);
                                    temp = string.Empty;
                                    continue;
                                }
                            }                            
                        }

                        //UP
                        if (top > 0)
                        {
                            posStart = left - temp.Length - 1;
                            posEnd = left;

                            posStart = posStart >= 0 ? posStart : 0;
                            posEnd = posEnd < previousLine.Length ? posEnd : posEnd - 1;

                            if (previousLine.Substring(posStart, 1 + posEnd - posStart).Any(p => !char.IsDigit(p) && p != '.'))
                            {
                                sum += int.Parse(temp);
                                Console.WriteLine(temp);
                                temp = string.Empty;
                                continue;
                            }
                        }

                        //DOWN
                        if (top < lines.Length - 1)
                        {
                            nextLine = lines[top + 1];

                            posStart = left - temp.Length - 1;
                            posEnd = left;

                            posStart = posStart >= 0 ? posStart : 0;
                            posEnd = posEnd < nextLine.Length ? posEnd : posEnd - 1;

                            if (nextLine.Substring(posStart, 1 + posEnd - posStart).Any(p => !char.IsDigit(p) && p != '.'))
                            {
                                sum += int.Parse(temp);
                                Console.WriteLine(temp);
                                temp = string.Empty;
                                continue;
                            }

                        }

                        temp = string.Empty;
                    }
                }

                previousLine = currentLine;
            }

            return sum;
        }
    }
}
