using System;
using System.Collections.Generic;
using System.Linq;

namespace lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] ints = { 4, 6, 7, 3, 2, 8, 9 };
            IEnumerable<int> evenNumbers =
                from n in ints
                where n % 2 == 0
                select n;
            Console.WriteLine(string.Join(", ", evenNumbers));
            Predicate<int> intPredicate = n =>
            {
                Console.WriteLine("wywołanie predykatu dla " + n);
                return n % 2 == 0;
            };

            evenNumbers =
                from n in ints
                where intPredicate.Invoke(n)
                select n;
            evenNumbers =
                from n in evenNumbers
                where n > 5
                select n;
            Console.WriteLine("wywołanie ewaluacji wyrażenia LINQ");
            Console.WriteLine(string.Join(", ", evenNumbers));
        }
    }
}
