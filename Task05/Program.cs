using System;
using System.Globalization;

/*
Источник: https://metanit.com/

Класс Dollar представляет сумму в долларах, а Euro - сумму в евро.

Определите операторы преобразования от типа Dollar в Euro и наоборот.
Допустим, 1 евро стоит 1,14 долларов. При этом один оператор должен подразумевать явное,
и один - неявное преобразование. Обработайте ситуации с отрицательными аргументами
(в этом случае должен быть выброшен ArgumentException).

Тестирование приложения выполняется путем запуска разных наборов тестов, например,
на вход поступает две строки - количество долларов и количество евро.
10
100
Программа должна вывести на экран количество евро и долларов, соответственно,
с использованием перегруженных операторов (округлять до 2 знаков после запятой):
8,77
114,00

Никаких дополнительных символов выводиться не должно.

Код метода Main можно подвергнуть изменениям, но вывод меняться не должен.
*/

namespace Task05
{
    abstract class Currency
    {
        protected const decimal OneEuroToDollar = 1.14M;

        private decimal sum;

        public decimal Sum
        {
            get => sum;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException();
                }
                sum = value;
            }
        }

        public override string ToString() => string.Format("{0:f2}", Sum);
    }


    class Dollar : Currency
    {
        public static implicit operator Dollar(Euro euro) => new Dollar() { Sum = euro.Sum * OneEuroToDollar };
    }


    class Euro : Currency
    {
        public static explicit operator Euro(Dollar dollar) => new Euro() { Sum = dollar.Sum / OneEuroToDollar };
    }


    class MainClass
    {
        public static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = new CultureInfo("ru-Ru");
            try
            {
                Console.WriteLine((Euro)new Dollar() { Sum = decimal.Parse(Console.ReadLine()) });
                Console.WriteLine((Dollar)new Euro() { Sum = decimal.Parse(Console.ReadLine()) });
            }
            catch (ArgumentException)
            {
                Console.WriteLine("error");
            }
        }
    }
}
