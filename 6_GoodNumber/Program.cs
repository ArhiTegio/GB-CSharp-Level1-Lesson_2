using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace _6_GoodNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            var answer = new Answer();
            var ex = new Extension();
            var q = new Questions();
            WriteLine("С# - Уровень 1. Задание 2.6");
            WriteLine("Кузнецов");
            WriteLine("*Написать программу подсчета количества «хороших» чисел в диапазоне от 1 до 1 000 000 000. " +
                "«Хорошим» называется число, которое делится на сумму своих цифр. " +
                "Реализовать подсчёт времени выполнения программы, используя структуру DateTime.");

            var time = new Stopwatch();
            time.Start();
            time.Restart();

            var num = answer.GoodNumber(1000000);

            time.Stop();
            WriteLine($"Найдено {num.Length} хороших значений и при этом затрачено {time.ElapsedMilliseconds} миллисекунд");
            ex.Pause(3000);
        }
    }

    class Answer
    {
        public Func<int, int[]> GoodNumber = step => Enumerable.Range(1, step).ToArray()
                                              .Select(x => Tuple.Create(x.ToString().Select(y => int.Parse(y.ToString()))
                                                                                    .ToArray() 
                                                                                    .Sum(), x))
                                              .Where(x => x.Item2 % x.Item1 == 0)
                                              .Select(t => t.Item1) 
                                              .ToArray();
    }

    class Questions
    {
        public string FirstUpper(string text) => text.Substring(0, 1).ToUpper() + (text.Length > 1 ? text.Substring(1) : "");

        public string Question<T>(string text, HashSet<char> arraySym)
        {
            Console.WriteLine(text);
            var textAnswer = new StringBuilder();
            while (true)
            {
                var symbol = Console.ReadKey(true);
                if (arraySym.Contains(symbol.KeyChar))
                {
                    textAnswer.Append(symbol.KeyChar.ToString());
                    Console.Write(symbol.KeyChar.ToString());
                }

                if (symbol.Key == ConsoleKey.Backspace && textAnswer.Length > 0)
                {
                    textAnswer.Remove(textAnswer.Length - 1, 1);
                    Console.Write(symbol.KeyChar.ToString());
                    Console.Write(" ");
                    Console.Write(symbol.KeyChar.ToString());
                }

                if (typeof(T) == typeof(string))
                {
                    if (symbol.Key == ConsoleKey.Enter && textAnswer.Length > 0)
                        break;
                }
                else
                    if (symbol.Key == ConsoleKey.Enter &&
                        double.TryParse(textAnswer.ToString()
                            .Replace(".", ","),
                            out var number))
                    break;
            }
            Console.WriteLine("");
            return textAnswer.ToString();
        }
    }

    public class Extension
    {
        public void Print(string text, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }

        public void Print(string text, PositionForRow position, int y)
        {
            if (position == PositionForRow.Center)
            {
                var n = (WindowWidth - text.Length) / 2;
                if (n >= 0)
                    Console.SetCursorPosition(n, y);
                else
                    Console.SetCursorPosition(0, y);
                Console.Write(text);
            }

            if (position == PositionForRow.LeftEdge)
            {
                Console.SetCursorPosition(0, y);
                Console.Write(text);
            }

            if (position == PositionForRow.RightEdge)
            {
                var n = (WindowWidth - text.Length);
                if (n >= 0)
                    Console.SetCursorPosition(n, y);
                else
                    Console.SetCursorPosition(0, y);
                Console.Write(text);
            }
        }

        public void Pause(int millisec) => System.Threading.Thread.Sleep(millisec);
        public void Pause() => ReadKey(true);

    }

    public enum PositionForRow
    {
        Center,
        LeftEdge,
        RightEdge
    }
}
