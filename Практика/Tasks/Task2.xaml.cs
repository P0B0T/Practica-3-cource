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
using SolutionLibrary;

namespace Практика
{
    /// <summary>
    /// Логика взаимодействия для Task2.xaml
    /// </summary>
    public partial class Task2 : Window
    {
        public Task2()
        {
            InitializeComponent();
            // изменение высоты нужно для корректно отображения элементов окна
            Height += 30; // высота + 30
        }

        private void CloseTask_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnClosing(CancelEventArgs e) // метод для отслеживания закрытия окна
        {
            // очищаем поля ввода
            InputFirstNumber.Clear();
            InputSecondNumber.Clear();
            InputThirdNumber.Clear();

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
            MessageBox.Show("Ввести три целых числа. Найти сумму двух наибольших из них.");
        }

        // при изменении ввода, очищаем результат

        private void InputNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            Sum.Clear();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            // проверяем ввод, если введены не целые числа, то выводим сообщение о некорректном вводе, очищаем поле ввода и передаём на него фокус, останавливаем программу (для каждого числа отдельно)
            if (!int.TryParse(InputFirstNumber.Text, out int firstNumber))
            {
                MessageBox.Show("Введите целое число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                InputFirstNumber.Clear();
                InputFirstNumber.Focus();
                return;
            }
            if (!int.TryParse(InputSecondNumber.Text, out int secondNumber))
            {
                MessageBox.Show("Введите целое число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                InputSecondNumber.Clear();
                InputSecondNumber.Focus();
                return;
            }
            if (!int.TryParse(InputThirdNumber.Text, out int thirdNumber))
            {
                MessageBox.Show("Введите целое число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                InputThirdNumber.Clear();
                InputThirdNumber.Focus();
                return;
            }

            try
            {
                Sum.Text = Solutions.SumOfLargestNumbers(firstNumber, secondNumber, thirdNumber).ToString(); // выполняем метод из библиотеки SolutionLibrary, переводим вывод метода в строку и выводим
            }
            // "отлавливаем" специальные исключения, выводим сообщение об ошибке, очищаем некорретновведённые поля ввода
            catch (CustomExceptions.NumbersEqualException)
            {
                MessageBox.Show("Все три числа равны.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                InputFirstNumber.Clear();
                InputFirstNumber.Focus();
                InputSecondNumber.Clear();
                InputThirdNumber.Clear();

            }
            catch(CustomExceptions.FirsAndThirdEqualException)
            {
                MessageBox.Show("Первое и третье число равны.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                InputFirstNumber.Clear();
                InputFirstNumber.Focus();
                InputThirdNumber.Clear();
            }
            catch(CustomExceptions.FirsAndSecondEqualException)
            {
                MessageBox.Show("Первое и второе число равны.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                InputFirstNumber.Clear();
                InputFirstNumber.Focus();
                InputSecondNumber.Clear();
            }
            catch (CustomExceptions.SecondAndThirdEqualException)
            {
                MessageBox.Show("Второе и третье число равны.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                InputSecondNumber.Clear();
                InputSecondNumber.Focus();
                InputThirdNumber.Clear();
            }
        }
    }
}
