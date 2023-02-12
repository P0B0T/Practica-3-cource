using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionLibrary
{
    public class CustomExceptions // класс для хранения исключений, нужных для корректной работы метода SumOfLargestNumbers
    {
        public class NumbersEqualException : Exception // создание кастомного исключения
        {
            public NumbersEqualException(string message) : base(message)
            {
            }
        }

        public class FirsAndSecondEqualException : Exception
        {
            public FirsAndSecondEqualException(string message) : base(message)
            {
            }
        }

        public class FirsAndThirdEqualException : Exception
        {
            public FirsAndThirdEqualException(string message) : base(message)
            {
            }
        }

        public class SecondAndThirdEqualException : Exception
        {
            public SecondAndThirdEqualException(string message) : base(message)
            {
            }
        }
    }
}
