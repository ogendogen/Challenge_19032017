using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace _16032017_medium
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Podaj regex'a");
            string input = Console.ReadLine();
            StringBuilder output = new StringBuilder();
            StringBuilder group = new StringBuilder();
            Random rand = new Random();
            bool is_group = false; // czy grupa ?

            // wersja bez grup
            int tmp = 0;
            for (int i=0; i<input.Length; i++)
            {
                // wykrywanie grup
                if (input[i] == '[')
                {
                    is_group = true;
                    continue;
                }
                else if (input[i] == ']')
                {
                    is_group = false;
                    if (i + 1 < input.Length) // coś jeszcze jest za grupą
                    {
                        if (input[i + 1] == '+')
                        {
                            tmp = rand.Next(1, 5);
                            for (int k = 0; k < tmp; k++) output.Append(group.ToString());
                        }
                        else if (input[i + 1] == '*')
                        {
                            tmp = rand.Next(0, 5);
                            for (int k = 0; k < tmp; k++) output.Append(group.ToString());
                        }
                        else output.Append(group.ToString());
                    }
                    else output.Append(group.ToString()); // nic nie ma za grupą
                    group.Clear();
                    continue;
                }


                // parsowanie
                if ((input[i] >= 'a' && input[i] <= 'z') || (input[i] >= 'A' && input[i] <= 'Z') || (input[i] >= '0' && input[i] <= '9'))
                {
                    if (i + 1 == input.Length)
                    {
                        if (is_group) group.Append(input[i]); else output.Append(input[i]);
                        continue;
                    }

                    if (input[i+1] != '+' && input[i+1] != '*')
                    {
                        if (is_group) group.Append(input[i]); else output.Append(input[i]);
                        continue;
                    }
                    else if (input[i+1] == '+')
                    {
                        tmp = rand.Next(1, 5);
                        for (int k = 0; k < tmp; k++) if (is_group) group.Append(input[i]); else output.Append(input[i]);
                    }
                    else if (input[i+1] == '*')
                    {
                        tmp = rand.Next(0, 5);
                        for (int k = 0; k < tmp; k++) if (is_group) group.Append(input[i]); else output.Append(input[i]);
                    }
                }
                else if (input[i] == '.')
                {
                    char sign = (char)rand.Next(33, 126); // zakres czytelnych znaków
                    if ((i + 1 == input.Length) || (input[i+1] != '+' && input[i+1] != '*')) // każdy inny literał, razem z kropką (bez uwzględnienia grup)
                    {
                        if (is_group) group.Append(input[i]); else output.Append(input[i]);
                        continue;
                    }
                    else if (input[i+1] == '+')// + lub *
                    {
                        tmp = rand.Next(1, 5);
                        for (int k = 0; k < tmp; k++) if (is_group) group.Append(input[i]); else output.Append(input[i]);
                    }
                    else if (input[i+1] == '*')
                    {
                        tmp = rand.Next(0, 5);
                        for (int k = 0; k < tmp; k++) if (is_group) group.Append(input[i]); else output.Append(input[i]);
                    }
                }
                else if (input[i] == '-')
                {
                    int lower_bound = (int)input[i - 1];
                    int higher_bound = (int)input[i + 1];
                    group.Remove(group.Length - 1, 1); // usuwamy literę dodaną poprzednio, przed myślnikiem
                    char sign = (char)rand.Next(lower_bound, higher_bound);
                    if (input[i+2] == '+')
                    {
                        tmp = rand.Next(1, 5);
                        for (int k = 0; k < tmp; k++) group.Append(sign);
                    }
                    else if (input[i+2] == '*')
                    {
                        tmp = rand.Next(0, 5);
                        for (int k = 0; k < tmp; k++) group.Append(sign);
                    }
                    else group.Append(sign);
                    if (i + 1 < input.Length) i++;
                    Debug.WriteLine(sign);
                }
            }
            Console.Write("Przykładowy string pasujący do wzorca: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(output.ToString());
            Console.ReadLine();
        }
    }
}
