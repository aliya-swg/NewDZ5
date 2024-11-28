using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabTumakov5
{
    internal class Program
    {
        // Упражнение 6.1 Написать программу, которая вычисляет число гласных и согласных букв в файле.Имя файла передавать как аргумент в функцию Main.Содержимое текстового файла заносится в массив символов. Количество гласных и согласных букв определяется проходом по массиву.Предусмотреть метод, входным параметром которого является массив символов. Метод вычисляет количество гласных и согласных букв.
        public static void CountVowelsConsonants(string filePath, out int vowelCount, out int consonantCount)
        {
            vowelCount = 0;
            consonantCount = 0;

            try
            {
                // Читаем файл в массив символов
                List<char> chars = File.ReadAllText(filePath).ToLowerInvariant().ToList();


                // Считаем гласные и согласные
                foreach (char c in chars)
                {
                    if (IsVowel(c))
                    {
                        vowelCount++;
                    }
                    else if (char.IsLetter(c)) // Проверяем, является ли символ буквой
                    {
                        consonantCount++;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Файл {filePath} не найден.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }

        // Проверка на гласную букву
        static bool IsVowel(char c)
        {
            return "aeiou".Contains(c);
        }
        // Упражнение 6.2 Написать программу, реализующую умножению двух матриц, заданных в виде двумерного массива.В программе предусмотреть два метода: метод печати матрицы, метод умножения матриц(на вход две матрицы, возвращаемое значение – матрица).
        // Метод печати матрицы
        public static void PrintMatrix(LinkedList<LinkedList<int>> matrix)
        {
            foreach (var row in matrix)
            {
                foreach (var item in row)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
            }
        }

        // Метод умножения матриц 
        public static LinkedList<LinkedList<int>> MultiplyMatrices(LinkedList<LinkedList<int>> matrix1, LinkedList<LinkedList<int>> matrix2)
        {
            // Проверка совместимости матриц
            if (matrix1.First.Value.Count != matrix2.Count)
            {
                throw new ArgumentException("Матрицы несовместимы для умножения.");
            }

            int rows1 = matrix1.Count;
            int cols1 = matrix1.First.Value.Count;
            int cols2 = matrix2.First.Value.Count;

            // Создание результирующей матрицы
            LinkedList<LinkedList<int>> resultMatrix = new LinkedList<LinkedList<int>>();
            for (int i = 0; i < rows1; i++)
            {
                LinkedList<int> row = new LinkedList<int>();
                for (int j = 0; j < cols2; j++)
                {
                    row.AddLast(0);
                }
                resultMatrix.AddLast(row);
            }

            // Умножение матриц (используем итераторы)
            LinkedListNode<LinkedList<int>> resultRowNode = resultMatrix.First;
            LinkedListNode<LinkedList<int>> row1Node = matrix1.First;

            while (row1Node != null)
            {
                LinkedList<int> row1 = row1Node.Value;
                LinkedListNode<LinkedList<int>> col2Node = matrix2.First;
                LinkedListNode<int> resultColNode = resultRowNode.Value.First;

                while (col2Node != null)
                {
                    int sum = 0;
                    IEnumerator<int> enumeratorRow1 = row1.GetEnumerator();
                    IEnumerator<int> enumeratorCol2 = col2Node.Value.GetEnumerator();

                    while (enumeratorRow1.MoveNext() && enumeratorCol2.MoveNext())
                    {
                        sum += enumeratorRow1.Current * enumeratorCol2.Current;
                    }
                    resultColNode.Value = sum;
                    resultColNode = resultColNode.Next;
                    col2Node = col2Node.Next;
                }
                row1Node = row1Node.Next;
                resultRowNode = resultRowNode.Next;
            }

            return resultMatrix;
        }
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Использование: DZ5 <имя_файла>");
                return;
            }

            string filePath = args[0]; // Получаем имя файла из аргументов командной строки
            Task1(filePath);
            Task2();
            Task3();

        }
        static void Task1(string filePath) // Добавили параметр filePath
        {
            int vowelCount, consonantCount;

            CountVowelsConsonants(filePath, out vowelCount, out consonantCount);

            if (vowelCount >= 0 && consonantCount >= 0)
            {
                Console.WriteLine($"В файле '{filePath}':");
                Console.WriteLine($"  Количество гласных букв: {vowelCount}");
                Console.WriteLine($"  Количество согласных букв: {consonantCount}");
            }
        }
        static void Task2()
        {
            // Пример для умножения матриц (LinkedList)
            LinkedList<LinkedList<int>> matrixA = new LinkedList<LinkedList<int>>();
            matrixA.AddLast(new LinkedList<int>(new int[] { 1, 2 }));
            matrixA.AddLast(new LinkedList<int>(new int[] { 3, 4 }));

            LinkedList<LinkedList<int>> matrixB = new LinkedList<LinkedList<int>>();
            matrixB.AddLast(new LinkedList<int>(new int[] { 5, 6 }));
            matrixB.AddLast(new LinkedList<int>(new int[] { 7, 8 }));

            Console.WriteLine("\nМатрица A:");
            PrintMatrix(matrixA);
            Console.WriteLine("\nМатрица B:");
            PrintMatrix(matrixB);

            try
            {
                LinkedList<LinkedList<int>> result = MultiplyMatrices(matrixA, matrixB);
                Console.WriteLine("\nРезультат умножения:");
                PrintMatrix(result);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }


        }
        //Упражнение 6.3 Написать программу, вычисляющую среднюю температуру за год. Создать двумерный рандомный массив temperature[12, 30], в котором будет храниться температура для каждого дня месяца(предполагается, что в каждом месяце 30 дней). Сгенерировать значения температур случайным образом.Для каждого месяца распечатать среднюю температуру.Для этого написать метод, который по массиву temperature [12, 30] для каждого месяца вычисляет среднюю температуру в нем, и в качестве результата возвращает массив средних температур. Полученный массив средних температур отсортировать по возрастанию.


        // Массив названий месяцев
        private static readonly string[] Months = {
        "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
        "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
    };
        static void Task3()
        {
            // Создаем словарь с температурами для каждого месяца
            Dictionary<string, int[]> monthlyTemperatures = GenerateMonthlyTemperatures();

            // Вычисляем средние температуры для каждого месяца
            double[] averageMonthlyTemperatures = CalculateAverageMonthlyTemperatures(monthlyTemperatures);

            // Выводим средние температуры для каждого месяца
            Console.WriteLine("Средние температуры по месяцам:");
            for (int i = 0; i < averageMonthlyTemperatures.Length; i++)
            {
                Console.WriteLine($"{Months[i]}: {averageMonthlyTemperatures[i]:F2} °C");
            }

            // Сортируем средние температуры по возрастанию
            Array.Sort(averageMonthlyTemperatures);

            // Выводим отсортированные средние температуры
            Console.WriteLine("\nОтсортированные средние температуры по месяцам:");
            foreach (double temp in averageMonthlyTemperatures)
            {
                Console.WriteLine($"{temp:F2} °C");
            }

            // Вычисляем среднюю годовую температуру
            double yearlyAverageTemperature = averageMonthlyTemperatures.Average();
            Console.WriteLine($"\nСреднегодовая температура: {yearlyAverageTemperature:F2} °C");
        }
        static Dictionary<string, int[]> GenerateMonthlyTemperatures()
        {
            Random random = new Random();
            Dictionary<string, int[]> temperatures = new Dictionary<string, int[]>();
            for (int i = 0; i < Months.Length; i++)
            {
                int[] monthTemps = new int[30];
                for (int j = 0; j < 30; j++)
                {
                    monthTemps[j] = random.Next(-20, 40); // Температуры от -20 до 39 градусов
                }
                temperatures.Add(Months[i], monthTemps);
            }
            return temperatures;
        }

        // Вычисляет средние температуры для каждого месяца и возвращает массив средних температур
        static double[] CalculateAverageMonthlyTemperatures(Dictionary<string, int[]> monthlyTemperatures)
        {
            double[] averageTemps = new double[Months.Length];
            for (int i = 0; i < Months.Length; i++)
            {
                averageTemps[i] = monthlyTemperatures[Months[i]].Average();
            }
            return averageTemps;
        }
    }

}
