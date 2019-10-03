using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace CenterTwoPoint
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var arrayNumForOnlyNum = new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', };
            WriteLine("С# - Уровень 1. Задание 2.8 - секретное (по заданию преподавателя на вебенаре 1)");
            WriteLine("Кузнецов");
            WriteLine("*Написать программу деления на два отрезка каждого из трех отрезков. \n\r" +
                "Нажмите любую клавишу, когда вам будет нужно для остановки процесса деления линий");

            var ex = new Extension();
            var q = new Questions();
            var mass = new Queue<Line<double>>();
            var t = int.Parse(q.Question<int>("Какая задержка в миллисекундах между нахождениями линий вам нужна?", arrayNumForOnlyNum));
            mass.Enqueue(new Line<double>() { p1 = new PointN<double>() { X = 0, Y = 0 }, p2 = new PointN<double> { X = 20, Y = 10 } });
            mass.Enqueue(new Line<double>() { p1 = new PointN<double>() { X = 30, Y = 0 }, p2 = new PointN<double> { X = 0, Y = 10 } });            
            mass.Enqueue(new Line<double>() { p1 = new PointN<double>() { X = 30, Y = 0 }, p2 = new PointN<double> { X = 0, Y = 10 } });
            var list = new List<Line<double>>();

            var step = 0;
            foreach(var e in BreakLineForCenter(mass))
            {
                step++;
                WriteLine($"Линия {step} с точкой 1: х1 = {e.p1.X}, y1 = {e.p1.Y} и точка 2: x2 = {e.p2.X}, y2 = {e.p2.Y} ");
                Thread.Sleep(t);
                WriteLine($"Линия {step} с точкой 1: х1 = {e.p1.X}, y1 = {e.p1.Y} и точка 2: x2 = {e.p2.X}, y2 = {e.p2.Y} ");
                Thread.Sleep(t);
                if (KeyAvailable) break;
            }

            WriteLine($"Процесс вами остановлен!");
            ex.Pause();
        }

        public static IEnumerable<Line<double>> BreakLineForCenter(Queue<Line<double>> listLine)
        {
            while (listLine.Count > 0)
            {
                var n = listLine.Dequeue();
                    var p = new PointN<double>() { X = (n.p1.X + n.p2.X) / 2, Y = (n.p1.Y + n.p2.Y) / 2 };
                
                listLine.Enqueue(new Line<double>() { p1 = new PointN<double>() { X = p.X, Y = p.Y }, p2 = new PointN<double>() { X = n.p1.X, Y = n.p1.Y } });
                listLine.Enqueue(new Line<double>() { p1 = new PointN<double>() { X = n.p2.X, Y = n.p2.Y }, p2 = new PointN<double>() { X = p.X, Y = p.Y } });

                yield return n;
            }
        }
    }

    public class PointN<T> where T : struct
    {
        T x = new T();
        T y = new T();

        public T X { get => x; set => x = value; }
        public T Y { get => y; set => y = value; }
    }

    public class Line<T> where T : struct
    {
        public PointN<T> p1 = new PointN<T>();
        public PointN<T> p2 = new PointN<T>();
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
