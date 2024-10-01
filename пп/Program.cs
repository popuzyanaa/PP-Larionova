//ЛАБИРИНТ//
//using System;

//class Program
//{
//    static void Main()
//    {
//        // Определяем лабиринт (1 - стена, пробел - путь, '.' - сокровище)
//        char[,] maze = {
//            { '1', '1', '1', '1', '1', '1', '1', '1' },
//            { '1', ' ', '$', '1', ' ', ' ', ' ', '1' },
//            { '1', ' ', '1', '1', ' ', '1', '1', '1' },
//            { '1', '$', ' ', ' ', ' ', ' ', ' ', '1' },
//            { '1', ' ', '1', '1', '1', '1', '$', '1' },
//            { '1', '$', ' ', ' ', '$', '1', ' ', ' ' },
//            { '1', ' ', '1', '1', ' ', '1', ' ', '1' },
//            { '1', '1', '1', '1', '1', '1', '1', '1' }
//        };

//        // Начальная позиция 'O'
//        int posX = 1;
//        int posY = 1;
//        maze[posY, posX] = 'O';

//        // Целевая позиция (выход из лабиринта)
//        int exitX = maze.GetLength(1) - 1; // Предположим, что выход находится на последнем столбце
//        int exitY = maze.GetLength(0) - 3; // и предпоследней строке

//        int treasuresCollected = 0;

//        while (true)
//        {
//            // Выводим лабиринт и счет сокровищ
//            PrintMaze(maze);
//            Console.WriteLine($"Собранные сокровища: {treasuresCollected}");

//            // Проверка на достижение выхода
//            if (posX == exitX && posY == exitY)
//            {
//                Console.WriteLine("Поздравляю! Вы нашли выход");
//                Console.WriteLine($"Общее количество собранных сокровищ: {treasuresCollected}");
//                break;
//            }

//            // Ввод пользователя
//            Console.Write("Введите направление (w/a/s/ d - вверх/влево/ вниз/вправо, q - выход).: ");
//            char direction = Console.ReadKey().KeyChar;
//            Console.WriteLine();

//            // Выход из программы
//            if (direction == 'q') break;

//            // Перемещение
//            Move(ref posY, ref posX, direction, maze, ref treasuresCollected);
//        }
//    }

//    static void PrintMaze(char[,] maze)
//    {
//        Console.Clear();
//        for (int y = 0; y < maze.GetLength(0); y++)
//        {
//            for (int x = 0; x < maze.GetLength(1); x++)
//            {
//                Console.Write(maze[y, x]);
//            }
//            Console.WriteLine();
//        }
//    }

//    static void Move(ref int posY, ref int posX, char direction, char[,] maze, ref int treasuresCollected)
//    {
//        // Очистка текущей позиции
//        maze[posY, posX] = ' ';

//        // Определение новой позиции
//        int newY = posY;
//        int newX = posX;

//        switch (direction)
//        {
//            case 'w': // вверх
//                newY--;
//                break;
//            case 's': // вниз
//                newY++;
//                break;
//            case 'a': // влево
//                newX--;
//                break;
//            case 'd': // вправо
//                newX++;
//                break;
//            default:
//                Console.WriteLine("Неверное направление. Используйте \"w\", \"a\", \"s\", \"d\" или \"q\".");
//                // Вернуться без обновления позиции
//                maze[posY, posX] = 'O'; // Восстановить предыдущую позицию
//                return;
//        }

//        // Проверка границ и стен
//        if (newY >= 0 && newY < maze.GetLength(0) && newX >= 0 && newX < maze.GetLength(1))
//        {
//            if (maze[newY, newX] == ' ')
//            {
//                posY = newY;
//                posX = newX;
//            }
//            else if (maze[newY, newX] == '$')
//            {
//                posY = newY;
//                posX = newX;
//                treasuresCollected++;
//            }
//        }

//        // Установка нового положения
//        maze[posY, posX] = 'O';
//    }
//}



//НОНОГРАММ//

using System;
using System.Collections.Generic;
using System.Linq;

namespace NonogramConsole
{
    class Program
    {
        private const int GridSize = 5; // Размер сетки
        private static char[,] solution;
        private static char[,] userSolution;
        private static bool[,] activeCells;
        private static int lives = 3; // Количество жизней

        static void Main(string[] args)
        {
            InitializeGame();
            PlayGame();
        }

        private static void InitializeGame()
        {
            solution = new char[GridSize, GridSize];
            userSolution = new char[GridSize, GridSize];
            activeCells = new bool[GridSize, GridSize]; // Для хранения активных ячеек

            GenerateSolution();
            // Инициализация пользовательского решения пустыми значениями
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    userSolution[i, j] = '.';
                    activeCells[i, j] = solution[i, j] != '.'; // Активные ячейки определяются наличием 'X'
                }
            }
        }

        private static void GenerateSolution()
        {
            // Заданный рисунок в виде двумерного массива
            solution = new char[GridSize, GridSize]
            {
                { 'X', '.', '.', '.', 'X' },
                { '.', 'X', '.', 'X', '.' },
                { '.', '.', 'X', '.', '.' },
                { '.', 'X', '.', 'X', '.' },
                { 'X', '.', '.', '.', 'X' }
            };
        }

        private static void PlayGame()
        {
            bool isGameOver = false;

            while (!isGameOver)
            {
                DisplayGrid();
                Console.WriteLine($"Оставшиеся жизни: {lives}");
                Console.WriteLine("Введите координаты (row col) или 'exit' для выхода:");

                var input = Console.ReadLine();

                if (input?.ToLower() == "exit")
                {
                    isGameOver = true;
                    continue;
                }

                var parts = input?.Split();
                if (parts.Length != 2 || !int.TryParse(parts[0], out int row) || !int.TryParse(parts[1], out int col))
                {
                    Console.WriteLine("Неверный формат. Пожалуйста, введите два числа.");
                    continue;
                }

                if (row < 0 || row >= GridSize || col < 0 || col >= GridSize)
                {
                    Console.WriteLine($"Координаты вне диапазона. Пожалуйста, введите значения от 0 до {GridSize - 1}.");
                    continue;
                }

                //if (!activeCells[row, col])
                //{
                //    Console.WriteLine("Ошибка: Введите координаты только в пределах рисунка.");
                //    continue;
                //}

                // Изменение состояния ячейки
                userSolution[row, col] = userSolution[row, col] == '.' ? 'X' : '.';

                // Проверка решения и обновление жизней
                bool isCorrect = CheckSolution();
                if (isCorrect)
                {
                    lives--;
                    if (lives <= 0)
                    {
                        Console.Clear();
                        DisplayGrid();
                        Console.WriteLine("К сожалению, у вас закончились жизни. Игра окончена.");
                        isGameOver = true;
                    }
                }
            }
        }

        private static void DisplayGrid()
        {
            Console.Clear();

            // Получение подсказок для строк и столбцов
            var rowHints = GetRowHints();
            var colHints = GetColHints();

            int maxHintLength = colHints.Max(hint => FormatHint(hint).Length);

            // Печать заголовка
            Console.Write(""); // Пробел перед заголовками

            // Печать подсказок в виде вертикального столбика
            for (int row = 0; row < maxHintLength; row++)
            {
                Console.Write("       "); // Пробел перед подсказками
                for (int col = 0; col < GridSize; col++)
                {
                    string hint = FormatHint(colHints[col]);
                    // Печать символа из строки подсказки в текущем ряду или пробела
                    if (row < hint.Length)
                    {
                        Console.Write($"{hint[row],-4}"); // Печать символа из строки подсказки
                    }
                    else
                    {
                        Console.Write("    "); // Печать пробела, если строка подсказки короче
                    }
                }
                Console.WriteLine(); // Переход на новую строку
            }

            // Печать сетки и подсказок для строк
            for (int row = 0; row < GridSize; row++)
            {
                Console.Write($"{FormatHint(rowHints[row]),-4} "); // Печать подсказки для строки
                for (int col = 0; col < GridSize; col++)
                {
                    Console.Write($"  {userSolution[row, col]} ");
                }
                Console.WriteLine();
            }
        }

        // Метод для форматирования подсказок
        private static string FormatHint(List<int> hints)
        {
            if (hints.Count == 0)
            {
                return "";
            }
            return string.Join("", hints);
        }

        // Метод для получения подсказок по строкам
        private static List<List<int>> GetRowHints()
        {
            var rowHints = new List<List<int>>(GridSize);
            for (int row = 0; row < GridSize; row++)
            {
                rowHints.Add(GetHints(GetLine(solution, row, true)));
            }
            return rowHints;
        }

        // Метод для получения подсказок по столбцам
        private static List<List<int>> GetColHints()
        {
            var colHints = new List<List<int>>(GridSize);
            for (int col = 0; col < GridSize; col++)
            {
                colHints.Add(GetHints(GetLine(solution, col, false)));
            }
            return colHints;
        }

        // Метод для получения линии из сетки (строка или столбец)
        private static char[] GetLine(char[,] grid, int index, bool isRow)
        {
            var line = new char[GridSize];
            for (int i = 0; i < GridSize; i++)
            {
                line[i] = isRow ? grid[index, i] : grid[i, index];
            }
            return line;
        }

        // Метод для получения подсказок
        private static List<int> GetHints(char[] line)
        {
            var hints = new List<int>();
            int count = 0;

            foreach (var cell in line)
            {
                if (cell == 'X')
                {
                    count++;
                }
                else
                {
                    if (count > 0)
                    {
                        hints.Add(count);
                        count = 0;
                    }
                }
            }
            if (count > 0)
            {
                hints.Add(count);
            }

            return hints;
        }

        private static bool CheckSolution()
        {
            bool isCorrect = true;

            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    if (userSolution[row, col] != solution[row, col])
                    {
                        isCorrect = false;
                        break;
                    }
                }
                if (!isCorrect) break;
            }

            if (isCorrect)
            {
                Console.Clear();
                DisplayGrid();
                Console.WriteLine("Поздравляю! Вы решили нанограмм!");
                Environment.Exit(0);
            }

            return isCorrect;
        }
    }
}