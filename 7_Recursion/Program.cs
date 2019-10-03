using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _7_Recursion
{
    class Program
    {
        static void Main(string[] args)
        {
            var arrayNumForOnlyNum = new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', };
            var a = new Answer();
            var ex = new Extension();
            var q = new Questions();
            WriteLine("С# - Уровень 1. Задание 2.7");
            WriteLine("Кузнецов");
            WriteLine("a) Разработать рекурсивный метод, который выводит на экран числа от a до b(a<b).\r\n" +
                      "б) * Разработать рекурсивный метод, который считает сумму чисел от a до b.");

            var t1 = int.Parse(q.Question<int>("Введите число a?", arrayNumForOnlyNum, true));
            var t2 = int.Parse(q.Question<int>("Введите число b?", arrayNumForOnlyNum, true));
            a.RecursionA(Math.Min(t1, t2), Math.Max(t1, t2));
            var sum = a.RecursionB(Math.Min(t1, t2), Math.Max(t1, t2));

            WriteLine($"Сумма рекурсии является {sum}");
            ex.Pause();
        }
    }

    class Answer
    {
        public void RecursionA(int min, int max)
        {
            WriteLine(min);
            if (min < max) RecursionA(min + 1, max);
        }

        public int RecursionB(int min, int max)
        {
            var value = 0;
            if (min < max)            
                value += RecursionB(min + 1, max);            
            return min + value;
        }
    }

    class Questions
    {
        public string FirstUpper(string text) => text.Substring(0, 1).ToUpper() + (text.Length > 1 ? text.Substring(1) : "");

        public string Question<T>(string text, HashSet<char> arraySym, bool show)
        {
            WriteLine(text);
            var textAnswer = new StringBuilder();
            while (true)
            {
                var symbol = ReadKey(true);
                if (arraySym.Contains(symbol.KeyChar))
                {
                    textAnswer.Append(symbol.KeyChar.ToString());
                    if (show)
                        Write(symbol.KeyChar.ToString());
                    else
                        Write('*');
                }

                if (symbol.Key == ConsoleKey.Backspace && textAnswer.Length > 0)
                {
                    textAnswer.Remove(textAnswer.Length - 1, 1);
                    Write(symbol.KeyChar.ToString());
                    Write(" ");
                    Write(symbol.KeyChar.ToString());
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
