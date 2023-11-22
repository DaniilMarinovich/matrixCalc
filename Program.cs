using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Что должна уметь делать программа с матрицами :
///   √ 1. Создавать матрицы N слобцов и M строк
///   √`1. Заполнять данную матрицу
///   √ 2. Транспонировать матрицу A в матрицу A`
///   √ 4. Складывать матрицы A и B
///   √ 5. Отнимать матрицы A и B
///   √ 6. Умножать матрицы A и B
///   √ 7. Умножать матрицу A и Е (единичная матрица)
///   √ 8. Умножение матрицы А на число n
///   √ 9. Возводить матрицу А в степень n
///   √ 10. Искать определитель матрицы
///   √ 11. Создать обратную матрицу
///   √ 12. Поиск минора
///   √ 13. Поиск алгебраического дополнения
///   - 14. Определение ранга матрицы
/// </summary>
static class Programm
{
    public static Dictionary<string, Matritzi> MatritziNames = new Dictionary<string, Matritzi>();
    public static void Main(string[] args)
    {
        StartSession();
        SaveMatritzi();
    }

    // Save matritzi in file
    public static void SaveMatritzi()
    {
        Console.WriteLine("Введите имя файла для сохранения : ");
        string way = @"D:\";
        string name = Console.ReadLine();
        way = way + name + ".txt";
        foreach (var matritza in MatritziNames)
        {
            File.AppendAllText(way, matritza.Key + " " + Convert.ToString(matritza.Value.size.Length) + " " + Convert.ToString(matritza.Value.size[0].Length) + "\n");
            for (int i = 0; i < matritza.Value.size.Length; ++i)
            {
                string line = "";
                for (int j = 0; j < matritza.Value.size[0].Length; j++)
                {
                    line += Convert.ToString(matritza.Value.size[i][j]) + " ";
                }
                File.AppendAllText(way, line + "\n");
            }
        }
        Console.WriteLine("Успешно сохранено");
        Console.WriteLine();
    }


    // Load matritzi from file
    public static void LoadMatritzi()
    {
        Console.WriteLine("Введите имя файла для загрузки сохранения (если таковой существует) : ");
        string name = Console.ReadLine();
        string way = @"D:\";
        way = way + name + ".txt";

        if (File.Exists(way))
        {
            MatritziNames.Clear();
            string[] input = File.ReadAllLines(way);
            int k = 0;
            while (k != input.Length)
            {
                string[] inp = input[k].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var matritza = new Matritzi();
                ++k;

                if (inp[1] == inp[2])
                {
                    matritza.size = new double[int.Parse(inp[1])][];
                    matritza.minor = new double[int.Parse(inp[1])][];
                    matritza.alAddition = new double[int.Parse(inp[1])][];
                    for (int i = 0; i < int.Parse(inp[1]); i++)
                    {
                        matritza.size[i] = new double[int.Parse(inp[2])];
                        matritza.minor[i] = new double[int.Parse(inp[2])];
                        matritza.alAddition[i] = new double[int.Parse(inp[2])];
                    }

                    for (int j = 0; j < int.Parse(inp[1]); j++)
                    {
                        string[] nums = input[k].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int h = 0; h < int.Parse(inp[2]); h++)
                        {
                            matritza.size[j][h] = double.Parse(nums[h]);
                        }
                        ++k;
                    }

                    matritza.determinant = DetermMatritzaA(matritza);
                    alAddMatritzaA(matritza);
                    MinoriMatritzaA(matritza);

                }
                else
                {
                    matritza.size = new double[int.Parse(inp[1])][];

                    for (int i = 0; i < int.Parse(inp[1]); i++)
                    {
                        matritza.size[i] = new double[int.Parse(inp[2])];
                    }

                    for (int j = 0; j < int.Parse(inp[1]); j++)
                    {
                        string[] nums = input[k].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int h = 0; h < int.Parse(inp[2]); h++)
                        {
                            matritza.size[j][h] = double.Parse(nums[h]);
                        }
                        ++k;
                    }
                }
                MatritziNames.Add(inp[0], matritza);
            }
            Console.WriteLine("Сохранение успешно загружено");
        }
        else
        {
            Console.WriteLine("Не существует такого сохранения.");
        }
        Console.WriteLine();
    }

    public static void StartSession()
    {
        Console.WriteLine("Вас приветствует меню выбора операций : ");
        Console.WriteLine("1. Просмотреть список имеющихся матриц");
        Console.WriteLine("2. Создать матрицу N слобцов и M строк");
        Console.WriteLine("3. Транспонировать матрицу A в матрицу A`");
        Console.WriteLine("4. Складывать матрицы A и B");
        Console.WriteLine("5. Отнимать матрицы A и B");
        Console.WriteLine("6. Умножать матрицы A и B");
        Console.WriteLine("7. Умножение матрицы А на число n");
        Console.WriteLine("8. Возводить матрицу А в степень n");
        Console.WriteLine("9. Создать обратную матрицу (только для матриц NxN)");
        Console.WriteLine("10. Просмотр миноров (только для матриц NxN)");
        Console.WriteLine("11. Просмотр алгебраических дополнений (только для матриц NxN)");
        Console.WriteLine("12. Просмотр матрицы");
        Console.WriteLine("13. Просмотр определителя матрицы (только для матриц NxN)");
        Console.WriteLine("14. Определение ранга матрицы (В разработке)");
        Console.WriteLine("15. Загрузка имеющихся сохранений");
        Console.WriteLine("16. Сохранение имеющихся матриц");
        Console.Write("Введите номер операции или END для завершения сессии");
        Console.WriteLine();
        Console.WriteLine();
        string numberOfOperation = Console.ReadLine();
        switch (numberOfOperation)
        {
            case "1":
                WriteAllMatritzies();
                StartSession();
                break;
            case "2":
                CreateNewMatritza();
                StartSession();
                break;
            case "3":
                if (MatritziNames.Count > 0)
                {
                    AddMatritzi(TranspositionMatritza(Choise()));
                }
                else
                {
                    Console.WriteLine("У вас нет ни одной матрицы");
                    Console.WriteLine();
                }
                StartSession();
                break;
            case "4":
                if (MatritziNames.Count > 0)
                {
                    AddMatritzi(SumMatritzaAAndB(Choise(), Choise()));
                }
                else
                {
                    Console.WriteLine("У вас нет ни одной матрицы");
                    Console.WriteLine();
                }
                StartSession();
                break;
            case "5":
                if (MatritziNames.Count > 0)
                {
                    AddMatritzi(DedMatritzaAAndB(Choise(), Choise()));
                }
                else
                {
                    Console.WriteLine("У вас нет ни одной матрицы");
                    Console.WriteLine();
                }
                StartSession();
                break;
            case "6":
                if (MatritziNames.Count > 0)
                {
                    AddMatritzi(MultiplyMatritzaAAndB(Choise(), Choise()));
                }
                else
                {
                    Console.WriteLine("У вас нет ни одной матрицы");
                    Console.WriteLine();
                }
                StartSession();
                break;
            case "7":
                if (MatritziNames.Count > 0)
                {
                    Console.WriteLine("Введите число-множитель для матрицы");
                    double n = double.Parse(Console.ReadLine());
                    AddMatritzi(MultiplyMatritzaAAndN(Choise(), n));
                }
                else
                {
                    Console.WriteLine("У вас нет ни одной матрицы");
                    Console.WriteLine();
                }
                StartSession();
                break;
            case "8":
                if (MatritziNames.Count > 0)
                {
                    Console.WriteLine("Введите степень для возведения матрицы в эту степень");
                    int k = int.Parse(Console.ReadLine());
                    AddMatritzi(ExpoMatritzaAAndN(Choise(), k));
                }
                else
                {
                    Console.WriteLine("У вас нет ни одной матрицы");
                    Console.WriteLine();
                }
                StartSession();
                break;
            case "9":
                if (MatritziNames.Count > 0)
                {
                    var matritza = Choise();
                    if (matritza.size[0].Length == matritza.size.Length)
                    {
                        if (matritza.determinant != 0)
                        {
                            AddMatritzi(InvMatritzaA(matritza));
                        }
                        else Console.WriteLine("Определитель равен 0,обратной матрицы не существует");
                    }
                    else Console.WriteLine("Матрица не квадратичная");

                }
                else
                {
                    Console.WriteLine("У вас нет ни одной матрицы");
                    Console.WriteLine();
                }
                StartSession();
                break;
            case "10":
                if (MatritziNames.Count > 0)
                {
                    WriteMinors(Choise());
                }
                else
                {
                    Console.WriteLine("У вас нет ни одной матрицы");
                    Console.WriteLine();
                }
                StartSession();
                break;
            case "11":
                if (MatritziNames.Count > 0)
                {
                    WriteAlAdds(Choise());
                }
                else
                {
                    Console.WriteLine("У вас нет ни одной матрицы");
                    Console.WriteLine();
                }
                StartSession();
                break;
            case "12":
                if (MatritziNames.Count > 0)
                {
                    WriteMatritza(Choise());
                }
                else
                {
                    Console.WriteLine("У вас нет ни одной матрицы");
                    Console.WriteLine();
                }
                StartSession();
                break;
            case "13":
                if (MatritziNames.Count > 0)
                {
                    WriteDet(Choise());
                }
                else
                {
                    Console.WriteLine("У вас нет ни одной матрицы");
                    Console.WriteLine();
                }
                StartSession();
                break;
            case "14":
                Console.WriteLine("Данная операция находится в разработке");
                StartSession();
                break;
            case "15":
                LoadMatritzi();
                StartSession();
                break;
            case "16":
                SaveMatritzi();
                StartSession();
                break;
            case "END":
                break;
            default:
                Console.WriteLine("Нет такой операции");
                Console.WriteLine();
                StartSession();
                break;
        }
    }

    // write matritza 
    public static void WriteMatritza(Matritzi matritza)
    {
        for (int i = 0; i < matritza.size.Length; i++)
        {
            for (int j = 0; j < matritza.size[0].Length; j++)
            {
                Console.Write($"{matritza.size[i][j]} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    // write minors 
    public static void WriteMinors(Matritzi matritza)
    {
        if (matritza.size.Length == matritza.size[0].Length)
        {
            for (int i = 0; i < matritza.size.Length; i++)
            {
                for (int j = 0; j < matritza.size.Length; j++)
                {
                    Console.Write($"{matritza.minor[i][j]} ");
                }
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Для матрицы не осуществлялся поиск миноров");
            Console.WriteLine("(Возможно была добавлена обратная матрица или она не является квадратичной)");
        }
        Console.WriteLine();
    }

    // write AlAdd 
    public static void WriteAlAdds(Matritzi matritza)
    {
        if (matritza.size.Length == matritza.size[0].Length)
        {
            for (int i = 0; i < matritza.size.Length; i++)
            {
                for (int j = 0; j < matritza.size.Length; j++)
                {
                    Console.Write($"{matritza.alAddition[i][j]} ");
                }
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Для матрицы не осуществлялся поиск алгебраических дополнений");
            Console.WriteLine("(Возможно была добавлена обратная матрица или она не является квадратичной)");
        }
        Console.WriteLine();
    }

    // write Det
    public static void WriteDet(Matritzi matritza)
    {
        if (matritza.size.Length == matritza.size[0].Length)
        {
            Console.WriteLine($"Определитель матрицы равен : {matritza.determinant}");
        }
        else
        {
            Console.WriteLine("Для матрицы не осуществлялся поиск определителя");
            Console.WriteLine("(Возможно была добавлена обратная матрица или она не является квадратичной)");
        }
        Console.WriteLine();
    }

    // add matrirza
    public static void AddMatritzi(Matritzi NewMatritza)
    {
        if (NewMatritza.size != null)
        {
            Console.WriteLine("Желаете добавить матрицу в список матриц? y/n");
            Console.WriteLine();
            string ans = Console.ReadLine();
            if (ans == "y")
            {
                Console.WriteLine("Введите название матрицы\n");
                string input = Console.ReadLine();
                MatritziNames.Add(input, NewMatritza);
            }
            else if (ans == "n") ;
            else
            {
                Console.WriteLine("Неправильный ввод");
                AddMatritzi(NewMatritza);
            }
            Console.WriteLine();
        }
    }

    // choose your matritza
    public static Matritzi Choise()
    {
        WriteAllMatritzies();
        string name;
        Console.WriteLine($"Введите имя матрицы : ");
        name = Console.ReadLine();
        Console.WriteLine();
        if (MatritziNames.ContainsKey(name))
        {
            return MatritziNames[name];
        }
        else
        {
            Console.WriteLine("Такой матрицы нет в списке");
            return Choise();
        }

    }

    // write All names of created matritzies
    public static void WriteAllMatritzies()
    {

        if (MatritziNames.Count > 0)
        {
            Console.WriteLine("Список имеющихся матриц :");
            int i = 1;
            foreach (var matritza in MatritziNames)
            {
                Console.WriteLine($"{i}. {matritza.Key}");
                ++i;
            }
        }
        else Console.WriteLine("У вас нет ни одной матрицы");
        Console.WriteLine();
    }

    // Create new matritza and add in database
    public static void CreateNewMatritza()
    {
        Console.WriteLine("Введите название матрицы\n");
        string input = Console.ReadLine();

        Console.WriteLine("Введите количество строк матрицы\n");
        int n = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите количество столбцов матрицы\n");
        int m = int.Parse(Console.ReadLine());

        var matritza = new Matritzi();
        matritza.size = new double[n][];
        matritza.minor = new double[n][];
        matritza.alAddition = new double[n][];
        for (int i = 0; i < n; i++)
        {
            matritza.size[i] = new double[m];
            matritza.minor[i] = new double[m];
            matritza.alAddition[i] = new double[m];
        }

        for (int i = 0; i < n; i++)
        {
            Console.Write($"Введите строку A({i + 1}) : ");
            string[] inputt = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < m; j++)
            {
                try
                {
                    matritza.size[i][j] = double.Parse(inputt[j]);
                }
                catch (Exception)
                {
                    Console.WriteLine("Вы ввели неправильное значение");
                    --i;
                    matritza.size[i] = new double[n];
                    throw;
                }

            }
        }

        if (matritza.size.Length > 1)
        {
            if (matritza.size.Length == matritza.size[0].Length)
            {
                matritza.determinant = DetermMatritzaA(matritza);
                alAddMatritzaA(matritza);
                MinoriMatritzaA(matritza);
            }
        }
        else
        {
            matritza.minor[0][0] = matritza.size[0][0];
            matritza.alAddition[0][0] = matritza.size[0][0];
            matritza.determinant = matritza.size[0][0];
        }

        MatritziNames.Add(input, matritza);
        Console.WriteLine();
    }

    // Transposes matritza
    public static Matritzi TranspositionMatritza(Matritzi matritzaA)
    {
        var matritzaB = new Matritzi();
        matritzaB.size = new double[matritzaA.size[0].Length][];
        for (int i = 0; i < matritzaA.size[0].Length; i++)
        {
            matritzaB.size[i] = new double[matritzaA.size.Length];
        }

        for (int i = 0; i < matritzaA.size[0].Length; ++i)
        {
            for (int j = 0; j < matritzaA.size.Length; ++j)
            {
                matritzaB.size[i][j] = matritzaA.size[j][i];
            }
        }
        return matritzaB;
    }

    // sum matritza A and B
    public static Matritzi SumMatritzaAAndB(Matritzi matritzaA, Matritzi matritzaB)
    {
        var matritzaC = new Matritzi();
        if (matritzaA.size.Length != matritzaB.size.Length)
        {
            Console.WriteLine("Матрицы имеют различное число строк");
            return matritzaC;
        }
        if (matritzaA.size[0].Length != matritzaB.size[0].Length)
        {
            Console.WriteLine("Матрицы имеют различное число столбцов");
            return matritzaC;
        }

        matritzaC.size = new double[matritzaA.size.Length][];
        for (int i = 0; i < matritzaA.size.Length; i++)
        {
            matritzaC.size[i] = new double[matritzaA.size[0].Length];
        }


        for (int i = 0; i < matritzaA.size.Length; ++i)
        {
            for (int j = 0; j < matritzaA.size[0].Length; ++j)
            {
                matritzaC.size[i][j] = matritzaA.size[i][j] + matritzaB.size[i][j];
            }
        }

        return matritzaC;
    }

    // ded matritza A and B
    public static Matritzi DedMatritzaAAndB(Matritzi matritzaA, Matritzi matritzaB)
    {
        var matritzaC = new Matritzi();
        if (matritzaA.size.Length != matritzaB.size.Length)
        {
            Console.WriteLine("Матрицы имеют различное число строк");
            return matritzaC;
        }
        if (matritzaA.size[0].Length != matritzaB.size[0].Length)
        {
            Console.WriteLine("Матрицы имеют различное число столбцов");
            return matritzaC;
        }

        matritzaC.size = new double[matritzaA.size.Length][];
        for (int i = 0; i < matritzaA.size.Length; i++)
        {
            matritzaC.size[i] = new double[matritzaA.size[0].Length];
        }

        for (int i = 0; i < matritzaA.size.Length; ++i)
        {
            for (int j = 0; j < matritzaA.size[0].Length; ++j)
            {
                matritzaC.size[i][j] = matritzaA.size[i][j] - matritzaB.size[i][j];
            }
        }

        return matritzaC;
    }

    // multiply matritza A and B
    public static Matritzi MultiplyMatritzaAAndB(Matritzi matritzaA, Matritzi matritzaB)
    {
        var matritzaC = new Matritzi();
        if (matritzaA.size[0].Length != matritzaB.size.Length)
        {
            Console.WriteLine("Матрица A не имеет одинаковое число столбцов с числом строк матрицы B");
            return matritzaC;
        }

        matritzaC.size = new double[matritzaA.size.Length][];

        for (int i = 0; i < matritzaA.size.Length; i++)
        {
            matritzaC.size[i] = new double[matritzaB.size[0].Length];
        }

        for (int i = 0; i < matritzaA.size.Length; i++)
        {
            for (int j = 0; j < matritzaB.size[0].Length; j++)
            {
                for (int k = 0; k < matritzaB.size.Length; k++)
                {
                    matritzaC.size[i][j] += matritzaA.size[i][k] * matritzaB.size[k][j];
                }
            }
        }

        return matritzaC;
    }

    // multiply matritza A and n
    public static Matritzi MultiplyMatritzaAAndN(Matritzi matritzaA, double n)
    {
        var matritzaC = new Matritzi();

        matritzaC.size = new double[matritzaA.size.Length][];
        for (int i = 0; i < matritzaA.size.Length; i++)
        {
            matritzaC.size[i] = new double[matritzaA.size[0].Length];
        }

        for (int i = 0; i < matritzaA.size.Length; ++i)
        {
            for (int j = 0; j < matritzaA.size[0].Length; ++j)
            {
                matritzaC.size[i][j] = matritzaA.size[i][j] * n;
            }
        }

        return matritzaC;
    }

    // exponentiation matritza A and n
    public static Matritzi ExpoMatritzaAAndN(Matritzi matritzaA, int n)
    {
        var matritzaC = new Matritzi();

        matritzaC.size = new double[matritzaA.size.Length][];
        for (int i = 0; i < matritzaA.size.Length; i++)
        {
            matritzaC.size[i] = new double[matritzaA.size[0].Length];
        }

        for (int i = 0; i < matritzaA.size.Length; ++i)
        {
            for (int j = 0; j < matritzaA.size[0].Length; ++j)
            {
                matritzaC.size[i][j] = matritzaA.size[i][j];

                for (int k = 0; k < n - 1; ++k)
                {
                    matritzaC.size[i][j] = matritzaC.size[i][j] * matritzaA.size[i][j];
                }
            }
        }

        return matritzaC;
    }

    // determinant matritzi A 
    public static double DetermMatritzaA(Matritzi matritzaA)
    {
        if (matritzaA.size[0].Length != matritzaA.size.Length)
        {
            Console.WriteLine("Матрица не является квадратичной для поиска опредлителя");
            return double.MinValue;
        }

        if (matritzaA.size.Length == 2)
        {
            double n = matritzaA.size[0][0] * matritzaA.size[1][1] - matritzaA.size[0][1] * matritzaA.size[1][0];
            return n;
        }
        else if (matritzaA.size.Length == 1)
        {
            return matritzaA.size[0][0];
        }
        else
        {
            var matritzaC = new Matritzi();
            matritzaC.size = new double[matritzaA.size.Length - 1][];
            for (int i = 0; i < matritzaA.size.Length - 1; i++)
            {
                matritzaC.size[i] = new double[matritzaA.size.Length - 1];
            }

            double det = 0;
            int a, b;

            for (int j = 0; j < matritzaA.size.Length; j++)
            {
                a = 0;
                for (int k = 1; k < matritzaA.size.Length; k++)
                {
                    b = 0;
                    for (int s = 0; s < matritzaA.size.Length; s++)
                    {
                        if (s != j)
                        {
                            matritzaC.size[a][b] = matritzaA.size[k][s];
                            ++b;
                        }
                    }
                    ++a;
                }
                det += Math.Pow(-1, (double)j + 2) * matritzaA.size[0][j] * DetermMatritzaA(matritzaC);
            }
            return det;
        }
    }

    // minori matritzi A 
    public static void MinoriMatritzaA(Matritzi matritzaA)
    {
        if (matritzaA.size[0].Length != matritzaA.size.Length)
        {
            Console.WriteLine("Матрица не является квадратичной для поиска миноров");
            return;
        }

        var matritzaC = new Matritzi();
        matritzaC.size = new double[matritzaA.size.Length - 1][];
        for (int i = 0; i < matritzaA.size.Length - 1; i++)
        {
            matritzaC.size[i] = new double[matritzaA.size.Length - 1];
        }

        int a, b;
        for (int i = 0; i < matritzaA.size.Length; i++)
        {
            for (int j = 0; j < matritzaA.size.Length; j++)
            {
                a = 0;
                for (int k = 0; k < matritzaA.size.Length; k++)
                {
                    b = 0;
                    for (int h = 0; h < matritzaA.size.Length; h++)
                    {
                        if (k != i && h != j)
                        {
                            matritzaC.size[a][b] = matritzaA.size[k][h];
                            ++b;
                        }
                    }

                    if (k != i)
                    {
                        ++a;
                    }
                }
                matritzaA.minor[i][j] = Math.Abs(DetermMatritzaA(matritzaC));
            }
        }
    }

    // alAddition matritzi A 
    public static void alAddMatritzaA(Matritzi matritzaA)
    {
        if (matritzaA.size[0].Length != matritzaA.size.Length)
        {
            Console.WriteLine("Матрица не является квадратичной для поиска алгебраических дополнений");
            return;
        }

        var matritzaC = new Matritzi();
        matritzaC.size = new double[matritzaA.size.Length - 1][];
        for (int i = 0; i < matritzaA.size.Length - 1; i++)
        {
            matritzaC.size[i] = new double[matritzaA.size.Length - 1];
        }

        int a, b;
        for (int i = 0; i < matritzaA.size.Length; i++)
        {
            for (int j = 0; j < matritzaA.size.Length; j++)
            {
                a = 0;
                for (int k = 0; k < matritzaA.size.Length; k++)
                {
                    b = 0;
                    for (int h = 0; h < matritzaA.size.Length; h++)
                    {
                        if (k != i && h != j)
                        {
                            matritzaC.size[a][b] = matritzaA.size[k][h];
                            ++b;
                        }
                    }

                    if (k != i)
                    {
                        ++a;
                    }
                }
                matritzaA.alAddition[i][j] = Math.Pow(-1, (double)i + j) * DetermMatritzaA(matritzaC);
            }
        }
    }

    // inverse matritzi A 
    public static Matritzi InvMatritzaA(Matritzi matritzaA)
    {
        var matritzaC = new Matritzi();
        matritzaC.size = new double[matritzaA.size.Length][];
        for (int i = 0; i < matritzaA.size.Length; i++)
        {
            matritzaC.size[i] = new double[matritzaA.size[0].Length];
        }

        for (int i = 0; i < matritzaA.size.Length; i++)
        {
            for (int j = 0; j < matritzaA.size.Length; j++)
            {
                matritzaC.size[i][j] = matritzaA.alAddition[i][j];
            }
        }
        return MultiplyMatritzaAAndN(TranspositionMatritza(matritzaC), 1 / matritzaA.determinant);
    }
}

struct Matritzi
{
    public double[][] size;
    public double[][] minor;
    public double[][] alAddition;
    public double determinant;
}