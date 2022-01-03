using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using static Graphs1.ScreenElement;

namespace Graphs1
{
    public class Screen
    {
        public static Screen ActiveScreen { get; set; }
        public static string FeedBack { get; private set; }


        public Button SelectedButton { get; private set; }
        private int _selectedButtonIndex = 0;

        public ScreenElement[] Elements { get; private set; }
        public Button[] ActiveButtons { get; private set; }

        internal static void Report(string message) => FeedBack = message;

        private void JumpTo(Screen screen)
        {
            ActiveScreen = screen;
            Console.Clear();
        }
        private bool Control()
        {
            var ck = CatchKey();

            bool result = false;

            int hiPos = ActiveButtons.Length - 1;

            if ((ck == ConsoleKey.W || ck == ConsoleKey.UpArrow) && _selectedButtonIndex > 0)
            {
                _selectedButtonIndex--;
                Report("//");
                result = true;
            }

            if ((ck == ConsoleKey.S || ck == ConsoleKey.DownArrow) && _selectedButtonIndex < hiPos)
            {
                _selectedButtonIndex++;
                Report("//");
                result = true;
            }


            if (ck == ConsoleKey.Enter)
            {
                try
                {
                    Report("Успешно!");
                    SelectedButton.Action();
                }
                catch (Exception ex)
                {
                    Report(ex.Message);
                }
                result = true;
            }

            return result;
        }
        private void CheckActiveButtons()
        {
            ActiveButtons = Elements
                .OfType<ScreenElement.Button>()
                .Where(b => b.IsAccessible)
                .ToArray();
            try
            {
                SelectedButton = ActiveButtons[_selectedButtonIndex];
            }
            catch
            {
                _selectedButtonIndex = 0;
                SelectedButton = ActiveButtons[_selectedButtonIndex];
            }
        }
        private void Show()
        {
            Console.SetCursorPosition(0, 0);
            foreach (var element in Elements)
            {
                element.Display();
            }
        }
        private void Work()
        {
            if (Control())
            {
                CheckActiveButtons();
                Show();
            }
        }

        public static void Setup()
        {
            Console.CursorVisible = false;
            Console.WindowHeight = 50;

            ActiveScreen = MainMenu;
            ActiveScreen.Show();
            ActiveScreen.CheckActiveButtons();
            ActiveScreen.SelectedButton = ActiveScreen.ActiveButtons[0];

            while (true) 
            { 
                ActiveScreen.Work(); 
            }
        }

        public static ConsoleKey CatchKey() => Console.ReadKey(true).Key;
        public static T SelectValue<T>(string query, T[] values, Func<T, string> displayMethod)
        {
            int t = Console.CursorTop;
            int l = Console.CursorLeft;
            
            int pos = 0;

            int hiPos = values.Length - 1;

            ConsoleKey ck;

            do
            {
                Console.SetCursorPosition(l, t);


                Console.Write($"{query}: ");
                
                Console.ForegroundColor = pos < hiPos ? ConsoleColor.Gray : ConsoleColor.DarkGray;
                Console.Write("▲ ");
                Console.ResetColor();

                Console.Write(displayMethod(values[pos]));

                Console.ForegroundColor = pos > 0 ? ConsoleColor.Gray : ConsoleColor.DarkGray;
                Console.WriteLine(" ▼");
                Console.ResetColor();

                ck = CatchKey();

                if ((ck == ConsoleKey.W || ck == ConsoleKey.UpArrow) && pos > 0)
                    pos--;

                if ((ck == ConsoleKey.S || ck == ConsoleKey.DownArrow) && pos < hiPos)
                    pos++;
            }
            while (ck != ConsoleKey.Enter);

            Console.SetCursorPosition(l, t);

            Console.WriteLine(new string(' ', 120));

            return values[pos];
        }

        public static T ReadValue<T>(string query, Func<string, T> converter, Predicate<T> predicate)
        {
            int t = Console.CursorTop;
            int l = Console.CursorLeft;

            string input = string.Empty;

            T result = default;

            bool isCorrect = false;

            do
            {
                Console.SetCursorPosition(l, t);


                Console.Write($"{query}: ");

                input = Console.ReadLine();

                try
                {
                    result = converter(input);
                    isCorrect = true;
                }
                finally
                {
                    
                }
            }
            while (!isCorrect || !predicate(result));

            Console.SetCursorPosition(l, t);

            Console.WriteLine(new string(' ', input.Length + query.Length + 2));

            return result;
        }


        private static Screen MainMenu = new Screen()
        {
            Elements = new ScreenElement[]
            {
                new TextBlock("ИМПОРТ"),

                TextBlock.Indent,

                new Button("Из CSV", () =>
                {
                    if (File.Exists("Graph.csv"))
                    {
                        Program.ActiveGraph = Graph.FromCSV(File.ReadAllLines("Graph.csv"), '\t');
                        Report("Успешно!");
                    }
                    else
                        Report("Файл Graph.csv не найден!");
                }, () => true),

                new Button("Из JSON", () =>
                {
                    if (File.Exists("Graph.json"))
                    {
                        Program.ActiveGraph = Graph.FromJSON(File.ReadAllText("Graph.json"));
                        Report("Успешно!");
                    }
                    else
                        Report("Файл Graph.json не найден!");
                }, () => true),

                TextBlock.Indent,

                TextBlock.Indent,

                new TextBlock("ЭКСПОРТ"),

                TextBlock.Indent,

                new Button("В CSV", () =>
                {
                    Program.ActiveGraph.SaveCSV("Graph1.csv", '\t');
                    Report("Graph1.csv сохранен!");

                }, () => Program.ActiveGraph != null),

                new Button("В JSON", () =>
                {
                    Program.ActiveGraph.SaveJSON("Graph1.json");
                    Report("Graph1.json сохранен!");

                }, () => Program.ActiveGraph != null),

                TextBlock.Indent,

                TextBlock.Indent,

                new Button("Открыть папку", () =>
                {
                    Process.Start("explorer", Environment.CurrentDirectory);
                }, () => true),

                new Button("Статус графа", () =>
                {
                    Report($"Информация о графе: " +
                        $"Вершин: {Program.ActiveGraph.Vertexes.Count}, " +
                        $"Ребер: {Program.ActiveGraph.Edges.Count}, " +
                        $"Взвешенный? {Program.ActiveGraph.IsWeighted}, " +
                        $"Трансортная сеть? {Program.ActiveGraph.IsTransport}, " +
                        $"Ориентированный? {Program.ActiveGraph.IsOriented}");
                }, () => Program.ActiveGraph != null),

                TextBlock.Indent,

                TextBlock.Indent,

                new TextBlock("РЕДАКТИРОВАНИЕ"),

                TextBlock.Indent,

                new Button("Добавить вершину", () =>
                {
                    Program.ActiveGraph.AddVertex();
                }, () => Program.ActiveGraph != null),

                new Button("Удалить вершину", () =>
                {
                    int vertex = SelectValue("Введите номер вершины", Program.ActiveGraph.Vertexes.Select(v => v.Number).ToArray(), v => v.ToString());
                    Program.ActiveGraph.RemoveVertex(vertex);
                }, () => Program.ActiveGraph != null && Program.ActiveGraph.Vertexes.Any()),

                new Button("Добавить ребро", () =>
                {
                    int vertex1 = SelectValue("Введите номер первой вершины", Program.ActiveGraph.Vertexes
                        .Select(v => v.Number)
                        .ToArray(), v => v.ToString());
                    int vertex2 = SelectValue("Введите номер второй вершины", Program.ActiveGraph.Vertexes
                        .Where(v => v.Edges.Find(e => e.Destination == vertex1) == null)
                        .Select(v => v.Number)
                        .ToArray(), v => v.ToString());
                    int weight = ReadValue("Введите вес", s => Convert.ToInt32(s), w => w >= 0);
                    int capacity = ReadValue("Введите пропускную способность", s => Convert.ToInt32(s), w => w >= 0);
                    Program.ActiveGraph.AddEdge(vertex1, vertex2, weight);
                }, () => Program.ActiveGraph != null),

                new Button("Удалить ребро", () =>
                {
                    int vertex1 = SelectValue("Введите номер первой вершины", Program.ActiveGraph.Vertexes
                        .Select(v => v.Number)
                        .ToArray(), v => v.ToString());
                    int vertex2 = SelectValue("Введите номер второй вершины", Program.ActiveGraph.Vertexes
                        .Find(v => v.Number == vertex1).Edges
                        .Select(e => e.Destination)
                        .ToArray(), v => v.ToString());
                    try
                    {
                        Program.ActiveGraph.RemoveEdge(vertex1, vertex2);
                    }
                    catch (Exception ex)
                    {
                        Report(ex.Message);
                    }
                }, () => Program.ActiveGraph != null && Program.ActiveGraph.Edges.Any()),

                TextBlock.Indent,

                TextBlock.Indent,

                new TextBlock("АЛГОРИТМЫ"),

                TextBlock.Indent,

                new Button("Обход в ширину", () =>
                {
                    Algos.BreadthTraversal(Program.ActiveGraph);
                }, () => Program.ActiveGraph != null),

                new Button("Обход в глубину", () =>
                {
                    Algos.DepthTraversal(Program.ActiveGraph);
                }, () => Program.ActiveGraph != null),

                new Button("Транспортная сеть", () =>
                {
                    Vertex source = SelectValue("Введите номер первой вершины", Program.ActiveGraph.Vertexes
                        .ToArray(), v => v.Number.ToString());
                    Vertex sink = SelectValue("Введите номер второй вершины", Program.ActiveGraph.Vertexes
                        .ToArray(), v => v.Number.ToString());
                    Algos.FordFulkerson(new GraphVisualizationData(Program.ActiveGraph), source, sink);
                }, () => Program.ActiveGraph != null && Program.ActiveGraph.IsTransport),

                TextBlock.Indent,

                TextBlock.Indent,

                new TextBlock("ВИЗУАЛИЗАЦИЯ"),

                TextBlock.Indent,

                new Button("Сохранить состояние", () =>
                {
                    Visualizer.SaveStatus(Program.ActiveGraph);
                }, () => Program.ActiveGraph != null),

                new Button("Начать запись в GIF", () =>
                {
                    Visualizer.StartTrace();
                }, () => Program.ActiveGraph != null),

                new Button("Добавить состояние в GIF", () =>
                {
                    Visualizer.AddFrame(new GraphVisualizationData(Program.ActiveGraph));
                }, () => Program.ActiveGraph != null),

                new Button("Сохранить GIF", () =>
                {
                    Visualizer.SaveGif("Graph");
                }, () => Program.ActiveGraph != null && Visualizer.IsTracing),

                TextBlock.Indent,

                new Button("Выйти", () => Environment.Exit(0), () => true),

                new FeedBackElement()
            }
        };
    }

    public abstract class ScreenElement
    {
        public string DisplayName;
        public abstract void Display();

        public class TextBlock : ScreenElement
        {
            public TextBlock(string content)
            {
                DisplayName = content;
            }

            public static TextBlock Indent => new TextBlock(String.Empty);

            public override void Display() => Console.WriteLine(DisplayName);
        }

        public class Button : ScreenElement
        {
            private Func<bool> accessibilityCheck;

            public bool IsAccessible => accessibilityCheck();
            public bool IsSelected => Screen.ActiveScreen.SelectedButton == this;
            public Action Action;

            public Button(string content, Action action, Func<bool> access)
            {
                DisplayName = content;
                Action = action;
                accessibilityCheck = access;
            }

            public override void Display()
            {
                if (IsSelected) Console.ForegroundColor = ConsoleColor.Cyan;
                else if (!IsAccessible) Console.ForegroundColor = ConsoleColor.DarkGray;
                else Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(DisplayName);
                Console.ResetColor();
            }
        }

        public class FeedBackElement : ScreenElement
        {
            private static string indent = new string(' ', 120);
            public override void Display()
            {
                DisplayName = Screen.FeedBack;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"--> {DisplayName}{indent}");
                Console.ResetColor();
            }
        }
    }
}