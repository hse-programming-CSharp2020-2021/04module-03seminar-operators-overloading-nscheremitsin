using System;

/*
Источник: https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/operator-overloading

Fraction - упрощенная структура, представляющая рациональное число.
Необходимо перегрузить операции:
+ (бинарный)
- (бинарный)
*
/ (в случае деления на 0, выбрасывать DivideByZeroException)

Тестирование приложения выполняется путем запуска разных наборов тестов, например,
на вход поступает две строки, содержацие числители и знаменатели двух дробей, разделенные /, соответственно.
1/3
1/6
Программа должна вывести на экран сумму, разность, произведение и частное двух дробей, соответственно,
с использованием перегруженных операторов (при необходимости, сокращать дроби):
1/2
1/6
1/18
2

Обратите внимание, если дробь имеет знаменатель 1, то он уничтожается (2/1 => 2). Если дробь в числителе имеет 0, то 
знаменатель также уничтожается (0/3 => 0).
Никаких дополнительных символов выводиться не должно.

Код метода Main можно подвергнуть изменениям, но вывод меняться не должен.
*/

public struct Fraction
{
    private int numerator;
    private int denomenator;


    public Fraction(int numerator, int denomenator)
    {
        if (denomenator == 0)
        {
            throw new ArgumentException();
        }
        this.numerator = numerator;
        this.denomenator = denomenator;
    }


    public static Fraction operator +(Fraction a, Fraction b)
    {
        Fraction r = new Fraction()
        {
            numerator = a.numerator * b.denomenator + b.numerator * a.denomenator,
            denomenator = a.denomenator * b.denomenator
        };
        return Simplify(r);
    }


    public static Fraction operator -(Fraction a, Fraction b)
    {
        Fraction r = new Fraction()
        {
            numerator = a.numerator * b.denomenator - b.numerator * a.denomenator,
            denomenator = a.denomenator * b.denomenator
        };
        return Simplify(r);
    }


    public static Fraction operator *(Fraction a, Fraction b)
    {
        Fraction r = new Fraction()
        {
            numerator = a.numerator * b.numerator,
            denomenator = a.denomenator * b.denomenator
        };
        return Simplify(r);
    }


    public static Fraction operator /(Fraction a, Fraction b)
    {
        if (b.numerator == 0)
        {
            throw new DivideByZeroException();
        }
        Fraction r = new Fraction()
        {
            numerator = a.numerator * b.denomenator,
            denomenator = a.denomenator * b.numerator
        };

        if (r.denomenator < 0)
        {
            r.numerator *= -1;
            r.denomenator *= -1;
        }
        return Simplify(r);
    }


    private static Fraction Simplify(Fraction r)
    {
        if (r.numerator == 0)
        {
            return new Fraction() { numerator = 0, denomenator = 1 };
        }
        for (int i = 2; i <= r.denomenator; i++)
        {
            while (r.numerator % i == 0 && r.denomenator % i == 0)
            {
                r.numerator /= i;
                r.denomenator /= i;
            }
        }
        return r;
    }


    public static Fraction Parse(string input)
    {
        string[] args = input.Split('/');

        Fraction rational = new Fraction()
        {
            numerator = int.Parse(args[0]),
            denomenator = 1
        };

        if (args.Length == 2)
        {
            rational.denomenator = int.Parse(args[1]);
        }

        return rational;
    }


    public override string ToString()
    {
        if (denomenator == 1 || numerator == 0)
        {
            return numerator.ToString();
        }
        return numerator + "/" + denomenator;
    }
}


public static class OperatorOverloading
{
    public static void Main()
    {
        try
        {
            var args = Console.ReadLine().Split('/');
            var a = new Fraction(int.Parse(args[0]), int.Parse(args[1]));
            args = Console.ReadLine().Split('/');
            var b = new Fraction(int.Parse(args[0]), int.Parse(args[1]));
            Console.WriteLine(a + b);
            Console.WriteLine(a - b);
            Console.WriteLine(a * b);
            Console.WriteLine(a / b);
        }
        catch (ArgumentException)
        {
            Console.WriteLine("error");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("zero");
        }
    }
}
