using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadachi

{
    // Задача 2. Студенты
    struct Student
    {
        public string lastName;
        public string firstName;
        public int birthYear;
        public string exam;
        public int score;

        public void Print()
        {
            Console.WriteLine($"{lastName} {firstName}");
            Console.WriteLine($"Год рождения - {birthYear}");
            Console.WriteLine($"Экзамен - {exam}");
            Console.WriteLine($"Итоговые баллы - {score}\n");
        }
    }

    // Задача 3. Бабушки, больницы, болезни...
    public class Babushka
    {
        public string Name { get; set; }
        public byte Age { get; set; }
        public List<string> Illnesses { get; set; } = new List<string>();
        public Dictionary<string, string> Medications { get; set; } = new Dictionary<string, string>();
    }

    public class Hospital
    {
        public string Name { get; set; }
        public List<string> TreatableIllnesses { get; set; } = new List<string>();
        public int Capacity { get; set; }
        public Queue<Babushka> Patients { get; set; } = new Queue<Babushka>();

        public int PatientCount => Patients.Count;
        public double OccupancyPercentage => (double)PatientCount / Capacity * 100;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Task1();
            Task2();
            Task3();
            Task4();

        }
        static void Task1()
        {
        }
        static void Task2()
        {
            string[] paths = Directory.GetCurrentDirectory().Split('\\');
            string path = String.Empty;

            for (int i = 0; i < paths.Length - 3; i++)
            {
                path += paths[i] + "/";
            }


            path += "resourses/Студенты.txt";

            string[] readAllText = File.ReadAllLines(path);
            Dictionary<string, Student> students = new Dictionary<string, Student>();
            foreach (string line in readAllText)
            {
                Student student = new Student();
                student.firstName = line.Split(',')[0];
                student.lastName = line.Split(',')[1];
                student.birthYear = int.Parse(line.Split(',')[2]);
                student.exam = line.Split(',')[3];
                student.score = int.Parse(line.Split(',')[4]);

                students[student.lastName] = student;

            }
            bool flag = true;
            do
            {
                Console.WriteLine("Ввести нового студента - 'новый студент' (без кавычек)");
                Console.WriteLine("Удалить студента - 'удалить' (без кавычек)");
                Console.WriteLine("Сортироавть список студентов - 'сортировать' (без кавычек)");
                Console.WriteLine("Выйти - 'выход' (без кавычек)");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "выход":
                        flag = false;
                        break;
                    case "новый студент":
                        NewStudent(students);
                        break;
                    case "удалить":
                        DelStudent(students);
                        break;
                    case "сортировать":
                        SortStud(students);
                        break;
                    default:
                        Console.WriteLine("Неправильный ввод, попробуйте ещё раз");
                        break;
                }
            }
            while (flag);
        }

        static int EnterPosNumber()
        {
            bool flag = true;
            int number;
            do
            {
                bool isNumber = int.TryParse(Console.ReadLine(), out number);
                if (isNumber)
                {
                    flag = false;
                }
                else if (number <= 0)
                {
                    Console.WriteLine("Ну это прям хорошо меня вынесло. Но нет");
                }
                else
                {
                    Console.WriteLine("Неверный ввод - необходимо ввести целое число");
                }
            }
            while (flag);

            return number;
        }

        static int EnterBirthYear()
        {
            bool flag = true;
            int number;
            do
            {
                Console.WriteLine("Введите год рождения:");
                bool isNumber = int.TryParse(Console.ReadLine(), out number);
                if (isNumber)
                {
                    flag = false;
                }
                else if (number <= 0)
                {
                    Console.WriteLine("Ну ок");
                    flag = false;
                }
                else
                {
                    Console.WriteLine("Неверный ввод - необходимо ввести целое число");
                }
            }
            while (flag);

            return number;
        }

        static void NewStudent(Dictionary<string, Student> studs)
        {
            Console.WriteLine("Введите имя");
            string firstName = Console.ReadLine();
            Console.WriteLine("Введите фамилию");
            string lastName = Console.ReadLine();

            int birthYear = EnterBirthYear();
            Console.WriteLine("Введите экзамен");
            string exam = Console.ReadLine();
            Console.WriteLine("Введите итоговую сумму баллов");
            int scores = EnterPosNumber();

            Student student = new Student();
            student.firstName = firstName;
            student.lastName = lastName;
            student.birthYear = birthYear;
            student.exam = exam;
            student.score = scores;

            studs[student.lastName] = student;
        }

        static void DelStudent(Dictionary<string, Student> studs)
        {
            Console.WriteLine("Введите фамилию");
            string lastName = Console.ReadLine();
            studs.Remove(lastName);

            Console.WriteLine($"Удаление прошло благополучно");
        }

        static void SortStud(Dictionary<string, Student> studs)
        {
            foreach (var stud in studs.OrderBy(stud => stud.Value.score))
            {
                stud.Value.Print();
            }
        }

        static void Task3()
        {
            // Список бабушек(очередь)
            Queue<Babushka> babushkas = new Queue<Babushka>();

            // Список больниц (стек)
            Stack<Hospital> hospitals = new Stack<Hospital>();

            // Добавление больниц 
            hospitals.Push(new Hospital { Name = "Больница №1", TreatableIllnesses = new List<string> { "Язва", "Бессоница", "Простуда" }, Capacity = 5 });
            hospitals.Push(new Hospital { Name = "Больница №2", TreatableIllnesses = new List<string> { "Диабет", "Глухота", "Сердечная недостаточность" }, Capacity = 3 });


            // Ввод бабушек с клавиатуры
            Console.WriteLine("Введите количество бабушек:");
            int babushkaCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < babushkaCount; i++)
            {
                Console.WriteLine($"\nБабушка №{i + 1}:");
                Babushka babushka = new Babushka();
                Console.Write("Имя: ");
                babushka.Name = Console.ReadLine();
                Console.Write("Возраст: ");
                babushka.Age = byte.Parse(Console.ReadLine());

                Console.WriteLine("Болезни (введите 'стоп' для завершения ввода):");
                string illness;
                do
                {
                    Console.Write("Болезнь: ");
                    illness = Console.ReadLine();
                    if (illness.ToLower() != "стоп")
                    {
                        babushka.Illnesses.Add(illness);
                        Console.Write("Лекарство: ");
                        babushka.Medications[illness] = Console.ReadLine();
                    }
                } while (illness.ToLower() != "стоп");
                babushkas.Enqueue(babushka);
            }

            // Распределение бабушек по больницам
            while (babushkas.Count > 0)
            {
                Babushka babushka = babushkas.Dequeue();
                AssignHospital(babushka, hospitals);
            }

            // Вывод информации о бабушках и больницах
            PrintBabushkas(babushkas); // Уже пустая очередь, но можно использовать для другого вывода
            PrintHospitals(hospitals);
        }

        static void AssignHospital(Babushka babushka, Stack<Hospital> hospitals)
        {
            if (babushka.Illnesses.Count == 0)
            {
                AssignToFirstAvailable(babushka, hospitals);
                return;
            }

            foreach (Hospital hospital in hospitals.Reverse()) //Проверка с конца стека
            {
                int treatableCount = babushka.Illnesses.Count(illness => hospital.TreatableIllnesses.Contains(illness));
                if (treatableCount >= (double)babushka.Illnesses.Count * 0.5 && hospital.PatientCount < hospital.Capacity)
                {
                    hospital.Patients.Enqueue(babushka);
                    Console.WriteLine($"{babushka.Name} направлен(а) в {hospital.Name}.");
                    return;
                }
            }
            Console.WriteLine($"{babushka.Name} осталась на улице плакать.");
        }

        static void AssignToFirstAvailable(Babushka babushka, Stack<Hospital> hospitals)
        {
            foreach (Hospital hospital in hospitals.Reverse())
            {
                if (hospital.PatientCount < hospital.Capacity)
                {
                    hospital.Patients.Enqueue(babushka);
                    Console.WriteLine($"{babushka.Name} направлен(а) в {hospital.Name}.");
                    return;
                }
            }
            Console.WriteLine($"{babushka.Name} осталась на улице плакать (нет мест).");
        }

        static void PrintBabushkas(Queue<Babushka> babushkas)
        {
            Console.WriteLine("\nСписок бабушек:");
            foreach (Babushka babushka in babushkas)
            {
                Console.WriteLine($"Имя: {babushka.Name}, Возраст: {babushka.Age}, Болезни: {string.Join(", ", babushka.Illnesses)}");
            }
        }

        static void PrintHospitals(Stack<Hospital> hospitals)
        {
            Console.WriteLine("\nСписок больниц:");
            foreach (Hospital hospital in hospitals)
            {
                Console.WriteLine($"\nНазвание: {hospital.Name}, Вместимость: {hospital.Capacity}, Лечимые болезни: {string.Join(", ", hospital.TreatableIllnesses)}, " +
                                  $"Количество пациентов: {hospital.PatientCount}, Заполненность: {hospital.OccupancyPercentage:F1}%");
            }

        }
        // Задача 4. Написать метод для обхода графа в глубину или ширину - вывести на экран кратчайший путь.
        static void Task4()
        {
            // Граф (словарь: вершина -> список смежных вершин)
            var graph = new Dictionary<string, List<string>>()
            {
                {"A", new List<string>(){"B", "C"}},
                {"B", new List<string>(){"A", "D", "E"}},
                {"C", new List<string>(){"A", "F"}},
                {"D", new List<string>(){"B"}},
                {"E", new List<string>(){"B", "F"}},
                {"F", new List<string>(){"C", "E"}}
            };

            string startNode = "A";
            string endNode = "F";

            Console.WriteLine("Обход в глубину (DFS):");
            if (TryFindPath(graph, startNode, endNode, out var dfsPath, SearchType.DFS))
            {
                PrintPath(dfsPath);
            }
            else
            {
                Console.WriteLine("Путь не найден.");
            }


            Console.WriteLine("\nОбход в ширину (BFS):");
            if (TryFindPath(graph, startNode, endNode, out var bfsPath, SearchType.BFS))
            {
                PrintPath(bfsPath);
            }
            else
            {
                Console.WriteLine("Путь не найден.");
            }

        }


        enum SearchType { DFS, BFS }
        static bool TryFindPath(Dictionary<string, List<string>> graph, string start, string end, out List<string> path, SearchType searchType)
        {
            path = new List<string>();
            var visited = new HashSet<string>();

            if (searchType == SearchType.DFS)
            {
                var stack = new Stack<string>();
                stack.Push(start);

                while (stack.Count > 0)
                {
                    string node = stack.Pop();
                    if (!visited.Contains(node))
                    {
                        visited.Add(node);
                        path.Add(node);

                        if (node == end) return true;

                        // Используем TryGetValue
                        if (graph.TryGetValue(node, out var neighbors))
                        {
                            foreach (string neighbor in neighbors.Where(n => !visited.Contains(n)))
                            {
                                stack.Push(neighbor);
                            }
                        }
                    }
                }
            }
            else // BFS
            {
                var queue = new Queue<List<string>>();
                queue.Enqueue(new List<string> { start });

                while (queue.Count > 0)
                {
                    path = queue.Dequeue();
                    string lastNode = path.Last();
                    visited.Add(lastNode);

                    if (lastNode == end) return true;

                    //  Используем TryGetValue
                    if (graph.TryGetValue(lastNode, out var neighbors))
                    {
                        foreach (string neighbor in neighbors.Where(n => !visited.Contains(n)))
                        {
                            var newPath = new List<string>(path) { neighbor };
                            queue.Enqueue(newPath);
                        }
                    }
                }
            }
            return false;
        }

        static void PrintPath(List<string> path)
        {
            Console.WriteLine(string.Join(" -> ", path));
        }

    }

}

