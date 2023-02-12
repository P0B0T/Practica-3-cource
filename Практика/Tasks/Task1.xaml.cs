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
    /// Логика взаимодействия для Task1.xaml
    /// </summary>
    public partial class Task1 : Window
    {
        public Task1()
        {
            InitializeComponent();
            // изменение высоты и ширины нужно для корректно отображения элементов окна
            Height += 30; // высота + 30
            Width += 16; // ширина + 16
        }

        private void CloseTask_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnClosing(CancelEventArgs e) // метод для отслеживания закрытия окна
        {
            Input.Clear(); // очищаем поле ввода

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
            MessageBox.Show("Ввести трехзначное число. Определить: является ли сумма его цифр двузначным числом.");
        }

        private void Input_TextChanged(object sender, TextChangedEventArgs e) // при изменении ввода, очищаем результат
        {
            Result.Clear();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if(!int.TryParse(Input.Text, out int input)) // проверяем ввод, если введено не целое число, то 
            {
                MessageBox.Show("Введите целое число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); // выводим сообщение о некорректном вводе
                Input.Clear(); // очищаем поле ввода
                Input.Focus(); // передаём на него фокус
                return; // останавливаем выполнение кода
            }

            if (input > 99 && input < 1000) // если число трёхзначное и положительное, то
            {
                if (Solutions.SumDigitsIsTwoDigitNumber(input) == true) Result.Text = "Да"; // выполняем метод из библиотеки SolutionLibrary, при значении true выводим "Да" 
                else Result.Text = "Нет"; // иначе выводим "Нет"
            }
            else if (input < -99 && input > -1000) // если число трёхзначное и отрицательное, то 
            {
                if (Solutions.SumDigitsIsTwoDigitNumber(input * -1) == true) Result.Text = "Да"; // умнажаем его на -1, для перевода из отрицательного в положительное, остальное также как выше
                else Result.Text = "Нет";
            }
            else // иначе
            {
                MessageBox.Show("Число должно быть трехзначным", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); // выводим сообщение о некорректном вводе
                Input.Clear(); // очищаем поле ввода
                Input.Focus(); // передаём на него фокус
            }
        }
    }
}
