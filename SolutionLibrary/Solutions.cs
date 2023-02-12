using System.Collections.Generic;
using static SolutionLibrary.CustomExceptions;

namespace SolutionLibrary
{
    public class Solutions
    {
        /// <summary>
        /// ����� ��� �������� ����� ���� �����
        /// </summary>
        /// <param name="number">���������� �����</param>
        /// <returns></returns>
        public static bool SumDigitsIsTwoDigitNumber(int number)
        {
            int helpNumber = number / 10; // ��������������� ����������
            int firstNumber = helpNumber / 10; // ������ �����
            int secondNumber = helpNumber % 10; // ������ �����
            int thirdNumber = number % 10; // ������ �����

            int sum = firstNumber + secondNumber + thirdNumber;

            if (sum > 9) return true; // ���� ����� ���� ����� - ���������� �����, �� true (���������� ���������� �� �����)
            else return false; // ����� false
        }

        /// <summary>
        /// ����� ��� ���������� ����� ���� ���������� ����� ����� �� ���
        /// </summary>
        /// <param name="firstNumber">������ �����</param>
        /// <param name="secondNumber">������ �����</param>
        /// <param name="thirdNumber">������ �����</param>
        /// <returns></returns>
        /// ��������� ������, ������ ��� ���������� ������ ���������
        /// <exception cref="NumbersEqualException">��� ����� �����</exception>
        /// <exception cref="FirsAndThirdEqualException">������ � ������ ����� �����</exception>
        /// <exception cref="FirsAndSecondEqualException">������ � ������ ����� �����</exception>
        /// <exception cref="SecondAndThirdEqualException">������ � ������ ����� �����</exception>
        public static int SumOfLargestNumbers(int firstNumber, int secondNumber, int thirdNumber)
        {
            if ((firstNumber == secondNumber) && (firstNumber == thirdNumber)) throw new NumbersEqualException("��� ����� �����.");
            if (firstNumber == thirdNumber) throw new FirsAndThirdEqualException("������ � ������ ����� �����.");
            if (firstNumber == secondNumber) throw new FirsAndSecondEqualException("������ � ������ ����� �����.");
            if (thirdNumber == secondNumber) throw new SecondAndThirdEqualException("������ � ������ ����� �����.");

            if ((firstNumber < secondNumber) && (firstNumber < thirdNumber)) return secondNumber + thirdNumber;

            if ((secondNumber < firstNumber) && (secondNumber < thirdNumber)) return firstNumber + thirdNumber;

            if ((thirdNumber < firstNumber) && (thirdNumber < secondNumber)) return firstNumber + secondNumber;
                
            return 0; // ��������������� return (�� ���������� �� ������, �� ��� ���� ������)
        }

        /// <summary>
        /// ����� ��� ���������� ���������� ���������, �������� � ��������, � ������ �� � ������ 
        /// </summary>
        /// <param name="array">������������� ������</param>
        /// <param name="min">����������� �������� ���������</param>
        /// <param name="max">������������ �������� ���������</param>
        /// <param name="count">���������� ���������, �������� � ��������</param>
        /// <returns></returns>
        public static int[] CountElementsInInterval(int[] array, int min, int max, out int count)
        {
            List<int> result = new List<int>(); // ������������� ���������

            count = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] >= min && array[i] <= max) // ���� ������� ������� ������ � ��������, �� 
                {
                    result.Add(array[i]); // ��������� ��� � ���������
                    count++; // � ����������� ������� �� 1
                }
            }

            int[] resultArray = new int[result.Count]; // ������ ��� ������ �������� �� ���������

            for (int i = 0; i < result.Count; i++) // ��������� �������� ��������� � ������
            {
                resultArray[i] = result[i];
            }

            return resultArray;
        }

        /// <summary>
        /// ����� ��� ���������� ������ ���������� ������� �������, � ������� ���������� ���������� ������������� � ������������� ���������
        /// </summary>
        /// <param name="matrix">������������� �������</param>
        /// <returns></returns>
        public static int NLastColumnWithSameNumberOfPosAndNegElements(int[,] matrix)
        {
            for (int j = matrix.GetLength(1) - 1; j >= 0; j--) // ������������� ������� �� �������� � �����
            {
                int positive = 0, negative = 0;

                for (int i = 0; i < matrix.GetLength(0); i++) // ������������� ������� �� ������� � ������
                {
                    if (matrix[i, j] > 0) positive++; // ���� ������� ������� ������ 0, �� ���������� ������������� ����������� �� 1
                    if (matrix[i, j] < 0) negative++; // ���� ������� ������� ������ 0, �� ���������� ������������� ����������� �� 1
                }

                if (positive == negative) return j + 1; // ���� ���������� ������������� � ������������� �����, �� ���������� ����� ������� (+ 1 ��� �������� �������������, �.� ���1� ��� � 0)
            }
            return 0; // ����� ������� 0
        }
    }
}
