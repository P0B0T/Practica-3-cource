using System;
using System.Data;
using System.IO;

namespace Libmas
{
    public class LibraryMas
    {
        /// <summary>
        /// Заполнять одномерный целочисленный массив
        /// </summary>
        /// <param name="array">Одномерный целочисленный массив</param>
        /// <param name="min">Минимальное значение случайного числа</param>
        /// <param name="max">Максимальное значение случайного числа</param>
        public static void FillArray(ref int[] array, int min, int max)
        {
            Random rnd = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next(min, max);
            }
        }

        /// <summary>
        /// Заполнять двумерный целочисленный массив
        /// </summary>
        /// <param name="matrix">Двумерный целочисленный массив</param>
        /// <param name="min">Минимальное значение случайного числа</param>
        /// <param name="max">Максимальное значение случайного числа</param>
        public static void FillMatrix(ref int[,] matrix, int min, int max)
        {
            Random rnd = new Random();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = rnd.Next(min, max);
                }
            }
        }

        /// <summary>
        /// Сохранять одномерный целочисленный массив
        /// </summary>
        /// <param name="array">Одномерный целочисленный массив</param>
        /// <param name="path">Путь к файлу</param>
        /// <param name="min">Минимальное значение случайного числа</param>
        /// <param name="max">Максимальное значение случайного числа</param>
        /// <param name="arraylenght">Размер одномерного целочисленного массива</param>
        public static void SaveArray(int[] array, string path, int min, int max, int arraylenght)
        {
            using(StreamWriter file = new StreamWriter(path))
            {
                file.WriteLine(array.Length);

                for (int i = 0; i < array.Length; i++)
                {
                    file.WriteLine(array[i]);
                }

                file.WriteLine(min);
                file.WriteLine(max);
                file.WriteLine(arraylenght);

                file.Close();
            }
        }

        /// <summary>
        /// Сохранять двумерный целочисленный массив
        /// </summary>
        /// <param name="matrix">Двумерный целочисленный массив</param>
        /// <param name="path">Путь к файлу</param>
        /// <param name="min">Минимальное значение случайного числа</param>
        /// <param name="max">Максимальное значение случайного числа</param>
        /// <param name="rows">Количество строк</param>
        /// <param name="columns">Количсетво столбцов</param>
        public static void SaveMatrix(int[,] matrix, string path, int min, int max, int rows, int columns)
        {
            using(StreamWriter file = new StreamWriter(path))
            {
                file.WriteLine(matrix.GetLength(0));
                file.WriteLine(matrix.GetLength(1));

                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        file.WriteLine(matrix[i, j]);
                    }
                }

                file.WriteLine(min);
                file.WriteLine(max);
                file.WriteLine(rows);
                file.WriteLine(columns);

                file.Close();
            }
        }

        /// <summary>
        /// Загружать одномерный целочисленный массив
        /// </summary>
        /// <param name="array">Одномерный целочисленный массив</param>
        /// <param name="path">Путь к файлу</param>
        /// <param name="min">Минимальное значение случайного числа</param>
        /// <param name="max">Максимальное значение случайного числа</param>
        /// <param name="arraylenght">Размер одномерного целочисленного массива</param>
        public static void UploadArray(ref int[] array, string path, ref int min, ref int max, ref int arraylenght)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                int.TryParse(reader.ReadLine(), out int lenght);

                array = new int[lenght];

                for (int i = 0; i < array.Length; i++)
                {
                    int.TryParse(reader.ReadLine(), out int value);
                    array[i] = value;
                }

                int.TryParse(reader.ReadLine(), out min);
                int.TryParse(reader.ReadLine(), out max);
                int.TryParse(reader.ReadLine(), out arraylenght);

                reader.Close();
            }
        }

        /// <summary>
        /// Загружать двумерный целочисленный массив
        /// </summary>
        /// <param name="matrix">Двумерный целочисленный массив</param>
        /// <param name="path">Путь к файлу</param>
        /// <param name="min">Минимальное значение случайного числа</param>
        /// <param name="max">Максимальное значение случайного числа</param>
        /// <param name="rows">Количество строк</param>
        /// <param name="columns">Количсетво столбцов</param>
        public static void UploadMatrix(ref int[,] matrix, string path, ref int min, ref int max, ref int rows, ref int columns)
        {
            using(StreamReader reader = new StreamReader(path))
            {
                int.TryParse(reader.ReadLine(), out int row);
                int.TryParse(reader.ReadLine(), out int column);

                matrix = new int[row, column];

                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        int.TryParse(reader.ReadLine(), out int value);
                        matrix[i, j] = value;
                    }
                }

                int.TryParse(reader.ReadLine(), out min);
                int.TryParse(reader.ReadLine(), out max);
                int.TryParse(reader.ReadLine(), out rows);
                int.TryParse(reader.ReadLine(), out columns);

                reader.Close();
            }  
        }
    }

    //Класс для связывания массива с элементом DataGrid
    //для визуализации данных 
    public static class VisualArray
    {
        //Метод для одномерного массива
        public static DataTable ToDataTable<T>(this T[] array)
        {
            var res = new DataTable();
            for (int i = 0; i < array.Length; i++)
            {
                res.Columns.Add("col" + (i + 1), typeof(T));
            }
            var row = res.NewRow();
            for (int i = 0; i < array.Length; i++)
            {
                row[i] = array[i];
            }
            res.Rows.Add(row);
            return res;
        }

        //Метод для двухмерного массива
        public static DataTable ToDataTable<T>(this T[,] matrix)
        {
            var res = new DataTable();
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                res.Columns.Add("col" + (i + 1), typeof(T));
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var row = res.NewRow();

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    row[j] = matrix[i, j];
                }

                res.Rows.Add(row);
            }

            return res;
        }
    }
}
