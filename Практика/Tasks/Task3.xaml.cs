using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для Task3.xaml
    /// </summary>
    public partial class Task3 : Window
    {
        int[] array = null; // объявляем глобальный целочисленный массив

        int min, max, arrayLenght; // объявляем глобальные переменные (минимальное значение массива, максимальное, длина массива)

        public Task3()
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

        private void ClearInput() // метод для очистки полей ввода
        {
            Min.Clear();
            Max.Clear();
            ValueMin.Clear();
            ValueMax.Clear();
            ArrayLenght.Clear();
        }

        private void ClearResult() // метод для очистки полей с результатом
        {
            CountItems.Clear();
            Items.ItemsSource = null;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearResult();
            ClearInput();
            Array.ItemsSource = null;
            array = null;
        }

        private void ClearResult_Click(object sender, RoutedEventArgs e)
        {
            ClearResult();
        }

        private void ClearInput_Click(object sender, RoutedEventArgs e)
        {
            ClearInput();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Проверить, имеется ли в одномерном массиве хотя бы один элемент, попадающий в интервал [a; b].");
        }

        // при изменении ввода, очищаем результат

        private void Array_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClearResult();
        }
        private void Value_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClearResult();
        }

        private void CreateArray_Click(object sender, RoutedEventArgs e)
        {
            ClearResult(); // очищаем результат с помощью метода

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
            if (!int.TryParse(ArrayLenght.Text, out arrayLenght))
            {
                MessageBox.Show("Введите целое число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                ArrayLenght.Clear();
                ArrayLenght.Focus();
                return;
            }

            if(!(min > max)) // если минимальное значение массива не превышает максимальное , то
            {
                array = new int[arrayLenght]; // расширяем массив

                LibraryMas.FillArray(ref array, min, max); // с помощью метода из библиотеки LibraryMas заполняем массив 
                Array.ItemsSource = VisualArray.ToDataTable(array).DefaultView; // и выводим его с помощью класса VisualArray (из библиотеки Libmas)
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
            ClearResult(); // очищаем результат с помощью метода

            // проверяем ввод, если введены не целые числа, то выводим сообщение о некорректном вводе (для каждого числа отдельно)
            if (!int.TryParse(ValueMin.Text, out int valueMin))
            {
                MessageBox.Show("Введите целое число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                ValueMin.Clear();
                ValueMin.Focus();
                return;
            }
            if (!int.TryParse(ValueMax.Text, out int valueMax))
            {
                MessageBox.Show("Введите целое число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                ValueMax.Clear();
                ValueMax.Focus();
                return;
            }
            if (!(valueMin > valueMax)) // если минимальное значение интервала не превышает максимальное , то
            {
                if (array == null) MessageBox.Show("Массив не может быть пустым!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); // проверяем, заполнен ли массив, если нет, то выводим сообщение

                else // иначе
                {
                    Items.ItemsSource = VisualArray.ToDataTable(Solutions.CountElementsInInterval(array, valueMin, valueMax, out int count)).DefaultView; // с помощью метода из библиотеки SolutionLibrary получаем массив элементов, входящих в интервал, и с помощью класса VisualArray (из библиотеки LibraryMas)
                    CountItems.Text = count.ToString(); // также получаем количество этих элементов, переводим в строку и выводим
                }
            }
            else // иначе выводим сообщение о некорректном вводе и очищаем поле ввода
            {
                MessageBox.Show("Минимальное значение не может быть больше максимального.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                ValueMin.Clear();
                ValueMin.Focus();
            }
        }

        private void Array_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e) // событие на ручное добавление значения в массив
        {
            try
            {
                array[e.Column.DisplayIndex] = Convert.ToInt32(((TextBox)e.EditingElement).Text); // определяем выбранный столбец, записываем значение в массив

                // находим минимальный и максимальный элементы миссива для записи их в соответсвующие поля
                int maxElement = int.MinValue,
                    minElement = int.MaxValue;

                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] < minElement)
                    {
                        minElement = array[i];
                    }

                    if (array[i] > maxElement)
                    {
                        maxElement = array[i];
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfg = new SaveFileDialog();
            sfg.Filter = "Текстовые файлы | *.txt";
            sfg.FilterIndex = 1;

            if (array == null) MessageBox.Show("Массив не может быть пустым!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); // проверяем, заполнен ли массив, если нет, то выводим сообщение

            else if (sfg.ShowDialog() == true) // иначе, если пользователь выбрал "Сохранить", то 
            {
                LibraryMas.SaveArray(array, sfg.FileName, min, max, arrayLenght); // с помощью метода из библиотеки Libmas сохраняем массив 
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog pfg = new OpenFileDialog();
            pfg.Filter = "Текстовые файлы | *.txt";
            pfg.FilterIndex = 1;

            if (pfg.ShowDialog() == true) // если пользователь выбрал "Открыть", то 
            {
                LibraryMas.UploadArray(ref array, pfg.FileName, ref min, ref max, ref arrayLenght); // с помощью метода из библиотеки Libmas открываем массив 
                // и выводим его данные на экран
                Min.Text = min.ToString();
                Max.Text = max.ToString();
                ArrayLenght.Text = arrayLenght.ToString();
                Array.ItemsSource = VisualArray.ToDataTable(array).DefaultView;
            }
        }
    }
}
