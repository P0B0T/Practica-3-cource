using System.Collections.Generic;
using static SolutionLibrary.CustomExceptions;

namespace SolutionLibrary
{
    public class Solutions
    {
        /// <summary>
        /// Метод для проверки суммы цифр числа
        /// </summary>
        /// <param name="number">трёхзначное число</param>
        /// <returns></returns>
        public static bool SumDigitsIsTwoDigitNumber(int number)
        {
            int helpNumber = number / 10; // вспомогательная переменная
            int firstNumber = helpNumber / 10; // первая цифра
            int secondNumber = helpNumber % 10; // вторая цифра
            int thirdNumber = number % 10; // третья цифра

            int sum = firstNumber + secondNumber + thirdNumber;

            if (sum > 9) return true; // если сумма цифр числа - двузначное число, то true (трёхзначное получиться не может)
            else return false; // иначе false
        }

        /// <summary>
        /// Метод для вычисления суммы двух наибольших целых чисел из трёх
        /// </summary>
        /// <param name="firstNumber">Первое число</param>
        /// <param name="secondNumber">Второе число</param>
        /// <param name="thirdNumber">Третье число</param>
        /// <returns></returns>
        /// Кастомные ошибки, нужные для корректной работы программы
        /// <exception cref="NumbersEqualException">Все числа равны</exception>
        /// <exception cref="FirsAndThirdEqualException">Первое и третье число равны</exception>
        /// <exception cref="FirsAndSecondEqualException">Первое и второе число равны</exception>
        /// <exception cref="SecondAndThirdEqualException">Второе и третье число равны</exception>
        public static int SumOfLargestNumbers(int firstNumber, int secondNumber, int thirdNumber)
        {
            if ((firstNumber == secondNumber) && (firstNumber == thirdNumber)) throw new NumbersEqualException("Все числа равны.");
            if (firstNumber == thirdNumber) throw new FirsAndThirdEqualException("Первое и третье число равны.");
            if (firstNumber == secondNumber) throw new FirsAndSecondEqualException("Первое и второе число равны.");
            if (thirdNumber == secondNumber) throw new SecondAndThirdEqualException("Второе и третье число равны.");

            if ((firstNumber < secondNumber) && (firstNumber < thirdNumber)) return secondNumber + thirdNumber;

            if ((secondNumber < firstNumber) && (secondNumber < thirdNumber)) return firstNumber + thirdNumber;

            if ((thirdNumber < firstNumber) && (thirdNumber < secondNumber)) return firstNumber + secondNumber;
                
            return 0; // вспомогательный return (на вычисления не влияет, но без него ошибка)
        }

        /// <summary>
        /// Метод для нахождения количества элементов, входящих в интервал, и записи их в массив 
        /// </summary>
        /// <param name="array">Целочисленный массив</param>
        /// <param name="min">Минимальное значение интервала</param>
        /// <param name="max">Максимальное значение интервала</param>
        /// <param name="count">Количество элементов, входящих в интервал</param>
        /// <returns></returns>
        public static int[] CountElementsInInterval(int[] array, int min, int max, out int count)
        {
            List<int> result = new List<int>(); // целочисленная коллекция

            count = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] >= min && array[i] <= max) // если элемент массива входит в интервал, то 
                {
                    result.Add(array[i]); // добавляем его в коллекцию
                    count++; // и увеличиваем счётчик на 1
                }
            }

            int[] resultArray = new int[result.Count]; // массив для записи значений из коллекции

            for (int i = 0; i < result.Count; i++) // переносим элементы коллекции в массив
            {
                resultArray[i] = result[i];
            }

            return resultArray;
        }

        /// <summary>
        /// Метод для нахождения номера последнего столбца матрицы, в котором одинаковое количество положительных и отрицательных элементов
        /// </summary>
        /// <param name="matrix">Целочисленная матрица</param>
        /// <returns></returns>
        public static int NLastColumnWithSameNumberOfPosAndNegElements(int[,] matrix)
        {
            for (int j = matrix.GetLength(1) - 1; j >= 0; j--) // просматриваем матрицу по столбцам с конца
            {
                int positive = 0, negative = 0;

                for (int i = 0; i < matrix.GetLength(0); i++) // просматриваем матрицу по строкам с начала
                {
                    if (matrix[i, j] > 0) positive++; // если элемент матрицы больше 0, то количсетво положительных увеличиваем на 1
                    if (matrix[i, j] < 0) negative++; // если элемент матрицы меньше 0, то количсетво отрицательных увеличиваем на 1
                }

                if (positive == negative) return j + 1; // если количество положительных и отрицательных равно, то возвращаем номер столбца (+ 1 для удобства пользователей, т.к отч1т идёт с 0)
            }
            return 0; // иначе выводим 0
        }
    }
}
