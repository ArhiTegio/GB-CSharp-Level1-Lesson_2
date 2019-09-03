using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _4_AccessRight
{
    class Program
    {
        static void Main(string[] args)
        {
            var arrayEng_NumSymbol = new HashSet<char>()
            {
                'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m',
                'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M',
                '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
            };
            var ex = new Extension();
            var q = new Questions();
            WriteLine("С# - Уровень 1. Задание 2.4");
            WriteLine("Кузнецов");
            WriteLine("Реализовать метод проверки логина и пароля. На вход метода подается логин и пароль. " +
                "На выходе истина, если прошел авторизацию, и ложь, если не прошел (Логин: root, Password: GeekBrains). " +
                "Используя метод проверки логина и пароля, написать программу: пользователь вводит логин и пароль, программа пропускает его дальше или не пропускает. " +
                "С помощью цикла do while ограничить ввод пароля тремя попытками.");

            var rightLogin = "root";
            var rightPassword = -169741140;

            WriteLine("Ваш пароль и логин для раскрытия потаенных замыслов программиста C#:");
            var login = "";
            var password = "";
            var step = 1;
            do
            {
                login = q.Question<string>("Введите логин?", arrayEng_NumSymbol, true);
                password = q.Question<string>("Введите пароль?", arrayEng_NumSymbol, false);

                if (login == rightLogin &&
                    password.GetHashCode() == rightPassword)
                {
                    WriteLine("Потаенные замыслы программиста...");
                    ex.Print("НА ЭТОМ МЕСТЕ МОЖЕТ БЫТЬ ВАША РЕКЛАМА!", PositionForRow.Center, WindowHeight/2);
                    ex.Print("Обращайтесь на сайте https://geekbrains.ru/", PositionForRow.Center, WindowHeight / 2 + 1);
                    ex.Print("Торопитесь! Предложение огрониченно и возможно из-за ваших конкурентов.", PositionForRow.Center, WindowHeight / 2 + 2);
                    break;
                }
                WriteLine("Неверный логин или пароль.");
                step++;
                if (step > 2)
                {
                    WriteLine("В доступе отказано.");
                    break;
                }
            } while (true);

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
