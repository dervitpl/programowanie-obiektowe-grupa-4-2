using System;

namespace lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = Person.OfName("Adam");
            Console.WriteLine(person.Name == null ? "NULL" : "PERSON");
            Money? money = Money.Of(13, Currency.USD) ?? Money.Of(1, Currency.PLN);
            Console.WriteLine(money.Value);
            Money result = money * 4;
            Console.WriteLine(result.Value);



        }

    }
    class Person
    {
        private string _Name;

        private Person(string name)
        {
            _Name = name;
        }
        public static Person OfName(String name)
        {
            if (name != null && name.Length >= 2)
            {
                return new Person(name);
            }
            else
            {
                throw new ArgumentOutOfRangeException("imie zbyt krotkie");
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (value != null && value.Length >= 2)
                {
                    _Name = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("imie zbyt krotkie");
                }
            }
        }
    }
    public enum Currency
    {
        PLN = 1,
        USD = 2,
        EUR = 3
    }
    public class Money
    {
        private readonly decimal _value;
        private readonly Currency _currency;
        private Money(decimal value, Currency currency)
        {
            _value = value;
            _currency = currency;
        }

    }
    public static Money? Of(decimal value, Currency currency)
    {
        return value < 0 ? null : new Money(value, currency);
    }
    public static Money? OfWithException(decimal value, Currency currency)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        else
        {
            return new Money(value, currency);
        }
    }
    public decimal Value
    {
        get
        {
            return _value;
        }
    }
    public Currency Currency
    {
        get
        {
            return _currency;
        }
    }

    public static Money operator *(Money money, decimal factor)
    {
        return new Money(money.Value * factor, money.Currency);
    }
    public static Money operator *(Money money, decimal factor)
    {
        return Money.OfWithException(money.Value * factor, money.Currency);
    }

}
