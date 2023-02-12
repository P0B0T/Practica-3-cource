using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Libmas;
using Microsoft.Win32;
using SolutionLibrary;

namespace Практика
{
    /// <summary>
    /// Логика взаимодействия для Task4.xaml
    /// </summary>
    public partial class Task4 : Window
    {
        int[,] matrix = null; // объявляем глобальный целочисленную матрицу

        int min, max, rows, columns; // объявляем глобальные переменные (минимальное значение матрицы, максимальное, количество строк и столбцов)

        public Task4()
        {
            InitializeComponent();
        }

        private void CloseTask_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnClosing(CancelEventArgs e) // метод для отслеживания закрытия окна
        {
            ClearInput(); // очищаем поля ввода с помощью метода

            if (this.IsActive == false) { } // если окно не активно, то выполняется такой же метод в главном окне
            else // иначе
            {
                e.Cancel = true; // отменяем закрытие
                Hide(); // скрываем окно
                Owner.Focus(); // передаём фокус на владельца
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Дана целочисленная матрица размера M * N. Найти номер последнего из ее столбцов, содержащих равное количество положительных и отрицательных элементов (нулевые элементы матрицы не учитываются). Если таких столбцов нет, то вывести 0.");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfg = new SaveFileDialog();
            sfg.Filter = "Текстовые файлы | *.txt";
            sfg.FilterIndex = 1;

            if (matrix == null) MessageBox.Show("Матрица не может быть пустой!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); // проверяем, заполнена ли матрица, если нет, то выводим сообщение

            else if (sfg.ShowDialog() == true) // иначе, если пользователь выбрал "Сохранить", то 
            {
                LibraryMas.SaveMatrix(matrix, sfg.FileName, min, max, rows, columns); // с помощью метода из библиотеки Libmas сохраняем матрицу
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog pfg = new OpenFileDialog();
            pfg.Filter = "Текстовые файлы | *.txt";
            pfg.FilterIndex = 1;

            if (pfg.ShowDialog() == true) // если пользователь выбрал "Открыть", то 
            {
                LibraryMas.UploadMatrix(ref matrix, pfg.FileName, ref min, ref max, ref rows, ref columns); // с помощью метода из библиотеки Libmas открываем матрицу
                // и выводим её данные на экран
                Min.Text = min.ToString(); 
                Max.Text = max.ToString();
                Rows.Text = rows.ToString();
                Columns.Text = columns.ToString();
                Matrix.ItemsSource = VisualArray.ToDataTable(matrix).DefaultView;
            }
        }

        private void CreateMatrix_Click(object sender, RoutedEventArgs e)
        {
            NumberColumn.Clear(); // очищаем результат 

            // проверяем ввод, если введены не целые числа, то выводим сообщение о некорректном вводе, очищаем поле ввода и передаём на него фокус, останавливаем программу (для каждого числа отдельно)
            if (!int.TryParse(Min.Text, out min))
            {
                MessageBox.Show("Введите целое число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Min.Clear();
                Min.Focus();
                return;
            }
            if (!int.TryParse(Max.Text, out max))
            {
                MessageBox.Show("Введите целое число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Max.Clear();
                Max.Focus();
                return;
            }
            if (!int.TryParse(Rows.Text, out rows))
            {
                MessageBox.Show("Введите целое число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Rows.Clear();
                Rows.Focus();
                return;
            }
            if (!int.TryParse(Columns.Text, out columns))
            {
                MessageBox.Show("Введите целое число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Columns.Clear();
                Columns.Focus();
                return;
            }

            if (!(min > max)) // если минимальное значение массива не превышает максимальное , то
            {
                matrix = new int[rows, columns]; // расширяем матрицу

                LibraryMas.FillMatrix(ref matrix, min, max); // с помощью метода из библиотеки Libmas заполняем матрицу 
                Matrix.ItemsSource = VisualArray.ToDataTable(matrix).DefaultView; // и выводим её с помощью класса VisualArray (из библиотеки Libmas)
            }
            else // иначе выводим сообщение о некорректном вводе, очищаем поле ввода и передаём на него фокус
            {
                MessageBox.Show("Минимальное значение не может быть больше максимального.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); 
                Min.Clear();
                Min.Focus();
            } 
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (matrix == null) MessageBox.Show("Матрица не может быть пустой!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); // проверяем, заполнена ли матрица, если нет, то выводим сообщение

            else // иначе
            {
                NumberColumn.Text = Solutions.NLastColumnWithSameNumberOfPosAndNegElements(matrix).ToString(); // с помощью метода из библиотеки SolutionLibrary получаем номер столбца, переводим в строку и выводим
            }
        }

        private void ClearInput() // метод для очистки полей ввода
        {
            Min.Clear();
            Max.Clear();
            Rows.Clear();
            Columns.Clear();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            NumberColumn.Clear();
            ClearInput();
            Matrix.ItemsSource = null;
            matrix = null;
        }

        private void Matrix_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                matrix[e.Row.GetIndex(), e.Column.DisplayIndex] = Convert.ToInt32(((TextBox)e.EditingElement).Text); // определяем выбранный столбец, определяем строку, записываем значение в матрицу

                // находим минимальный и максимальный элементы матрицы для записи их в соответсвующие поля
                int maxElement = int.MinValue,
                    minElement = int.MaxValue;

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (matrix[i, j] < minElement)
                        {
                            minElement = matrix[i, j];
                        }

                        if (matrix[i, j] > maxElement)
                        {
                            maxElement = matrix[i, j];
                        }
                    }
                }

                Min.Text = minElement.ToString();
                Max.Text = maxElement.ToString();
            }
            catch
            {
                MessageBox.Show("Введите целое число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); // выводим сообщение о некорректном вводе
            }
        }

        private void ClearResult_Click(object sender, RoutedEventArgs e)
        {
            NumberColumn.Clear();
        }

        private void ClearInput_Click(object sender, RoutedEventArgs e)
        {
            ClearInput();
        }

        private void Matrix_TextChanged(object sender, TextChangedEventArgs e) // при изменении ввода, очищаем результат
        {
            NumberColumn.Clear();
        }
    }
}
