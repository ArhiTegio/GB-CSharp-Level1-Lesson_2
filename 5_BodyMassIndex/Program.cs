using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _5_BodyMassIndex
{
    class Program
    {
        static void Main(string[] args)
        {
            var arrayNumForOnlyNum = new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', };
            var ex = new Extension();
            var q = new Questions();
            WriteLine("С# - Уровень 1. Задание 2.5");
            WriteLine("Кузнецов");
            WriteLine("а) Написать программу, которая запрашивает массу и рост человека, вычисляет его индекс массы и сообщает, нужно ли человеку похудеть, набрать вес или всё в норме.\n\r"+
                "б) * Рассчитать, на сколько кг похудеть или сколько кг набрать для нормализации веса.");
            var height_cm = int.Parse(q.Question<int>("Какой у вас рост в см?", arrayNumForOnlyNum, true));
            var mass_kg = int.Parse(q.Question<int>("Какой у вас вес в кг?", arrayNumForOnlyNum, true));
            var bmi = mass_kg / ((height_cm / 100.0) * (height_cm / 100.0));
            WriteLine("Ваш индекс массы тела (ИМТ) составляет {0:F2} кг/м2", bmi);
            if (bmi < 18.5)
            {
                WriteLine("Рекомендуется набрать {0:F2} кг", (18.5 - bmi) * ((height_cm / 100.0) * (height_cm / 100.0)));
            } else if (bmi > 24.99)
            {
                WriteLine("Рекомендуется cбросить {0:F2} кг", (bmi - 24.99) * ((height_cm / 100.0) * (height_cm / 100.0)));
            }
            else
            {
                WriteLine("У вас все в норме. Так держать.");
            }

            ex.Pause();
        }
    }

    class Questions
    {
        public string FirstUpper(string text) => text.Substring(0, 1).ToUpper() + (text.Length > 1 ? text.Substring(1) : "");

        public string Question<T>(string text, HashSet<char> arraySym, bool show)
        {
            Console.WriteLine(text);
            var textAnswer = new StringBuilder();
            while (true)
            {
                var symbol = Console.ReadKey(true);
                if (arraySym.Contains(symbol.KeyChar))
                {
                    textAnswer.Append(symbol.KeyChar.ToString());
                    if (show)
                        Console.Write(symbol.KeyChar.ToString());
                    else
                        Console.Write('*');
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
