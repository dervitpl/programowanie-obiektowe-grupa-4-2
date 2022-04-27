using System;
using System.Collections.Generic;
using System.Linq;

namespace lab8
{
    
    class Program
    {
        record Student(int Id, string Name, int Ects);
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
            Console.WriteLine(evenNumbers.Sum());
            Console.WriteLine(evenNumbers.Min());
            Student[] students =
            {
                new Student(1, "ewa", 67),
                new Student(2, "karol", 67),
                new Student(4, "ewa", 63),
                new Student(7, "ania", 67),
                new Student(5, "karol", 37)
            };
            Console.WriteLine("***********************");
            IEnumerable<string> enumerable =
                from s in students
                orderby s.Name descending orderby s.Ects
                select s.Name + s.Ects;
            Console.WriteLine(string.Join("\n", enumerable));
            Console.WriteLine(string.Join(", ",
                from i in ints
                orderby i descending
                select i));
            Console.WriteLine(string.Join(", ", ints.OrderByDescending( i => i)));

            Console.WriteLine(string.Join(", ", students.OrderByDescending( s => s.Name).ThenBy(s => s.Ects)));
            IEnumerable<IGrouping<string, Student>> studentNameGroup =
            from s in students
            group s by s.Name;
            foreach(var item in studentNameGroup)
            {
                Console.WriteLine(item.Key + " " + string.Join(", ", item));
            };
            //IEnumerable<(string Key, int)> texts =
              //  from u in str.Split(" ")
              //  group str by u into groupItem
             //   select (groupItem.Key, groupItem.Count());
            evenNumbers = ints.Where(i => i % 2 == 0).Select(i => i + 2);
            (int Id, string Name) p = students
                .Where(s => s.Ects > 20)
                .OrderBy(s => s.Id)
                .Select(s => (s.Id, s.Name))
                .FirstOrDefault(s => s.Name.StartsWith("Ż"));
            Console.WriteLine(p);
            Enumerable
                .Range(0, ints.Length)
                .Select(i => ints[i] * ints[i])
                .ToArray();
            //Console.WriteLine(string.Join(", " ));
            Random random = new Random();
            random.Next(100);
            Enumerable
                .Range(0, 100)
                .Select(i => random.Next(99)).ToList().ForEach(s => Console.Write(", ", s));
            int page = 0;
            int size = 3;
            List<Student> pageStudent = students.Skip(page * size).Take(size).ToList();
            Console.WriteLine(string.Join(", ", pageStudent));



        
        }
    }
}
