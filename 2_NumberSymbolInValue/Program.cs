using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _2_NumberSymbolInValue
{
    class Program
    {
        static void Main(string[] args)
        {
            var ex = new Extension();
            var arrayNumForOnlyNum = new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', };
            var q = new Questions();
            WriteLine("С# - Уровень 1. Задание 2.2");
            WriteLine("Кузнецов");
            WriteLine("Написать метод подсчета количества цифр числа.");
            var t = int.Parse(q.Question<int>("Введите значение: ", arrayNumForOnlyNum));

            ex.Print(($"В значении количество символов равно: {NumberSymbolInValue(t)}"), PositionForRow.Center, WindowHeight / 2);
            ex.Pause(2000);
        }

        public static int NumberSymbolInValue<T>(T x) where T : struct => x.ToString().Length;
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
