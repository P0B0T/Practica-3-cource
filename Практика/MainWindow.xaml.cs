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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Практика
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Task1 task1 = new Task1(); // окно с заданием 1
        Task2 task2 = new Task2(); // окно с заданием 2
        Task3 task3 = new Task3(); // окно с заданием 3
        Task4 task4 = new Task4(); // окно с заданием 4

        private void About_programm_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Разработчик: Касаткин Олег Андреевич ИСП-31\nПрограмма создана для выполнения задания на практику.");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnClosing(CancelEventArgs e) // метод для отслеживания закрытия окна
        {
            MessageBoxResult result;
            result = MessageBox.Show("Закрыть программу?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes); // диалоговое окно для закрытия программы
            if (result == MessageBoxResult.Yes) // если пользователь выбрал "Да", то закрываются все окна
            {
                task1.Close();
                task2.Close();
                task3.Close();
                task4.Close();
            }
            else e.Cancel = true; // если "Нет", то закрытие отменяется
        }

        private void OpenTask1_Click(object sender, RoutedEventArgs e) // Click для открытия окна 1
        {
            task1.Owner = this; // установка владельца для окна, это нужно для корректной работы программы

            if (task1.WindowState == WindowState.Minimized) task1.WindowState = WindowState.Normal; // если окно свёрнуто, то разворачиваем

            if (task1.IsVisible == true) task1.Focus(); // если окно видимо (находится на экране), то передаём на него фокус

            else task1.Show(); // иначе выводим окно на экран
        }

        // для кода ниже, всё аналогично

        private void OpenTask2_Click(object sender, RoutedEventArgs e) // Click для открытия окна 
        {
            task2.Owner = this;

            if (task2.WindowState== WindowState.Minimized) task2.WindowState = WindowState.Normal;

            if (task2.IsVisible == true) task2.Focus();

            else task2.Show();
        }

        private void OpenTask3_Click(object sender, RoutedEventArgs e) // Click для открытия окна 
        {
            task3.Owner = this; 

            if (task3.WindowState == WindowState.Minimized) task3.WindowState = WindowState.Normal;

            if (task3.IsVisible == true) task3.Focus();

            else task3.Show();
        }

        private void OpenTask4_Click(object sender, RoutedEventArgs e) // Click для открытия окна 
        {
            task4.Owner = this;

            if (task4.WindowState == WindowState.Minimized) task4.WindowState = WindowState.Normal;

            if (task4.IsVisible == true) task4.Focus();

            else task4.Show();
        }
    }
}
