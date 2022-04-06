using System;
using System.Collections;
using System.Collections.Generic;



namespace lab5
{
    public record Book(string Title, string Author);



    class Library : IEnumerable<Book>
    {
        internal Book[] _books =
        {
            new Book("C#", "Freeman"),
            new Book("ASP.nET", "Freeman"),
            null,
            new Book("Java", "Bloch")
        };



        public IEnumerator<Book> GetEnumerator()
        {
            foreach(Book book in _books)
            {
                if (book != null)
                {
                    yield return book;
                }
            }
        }



        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class Team: IEnumerable<string>
    {
        public String Leader;
        public String ScrumMaster;
        public String Developer;

        public IEnumerator<string> GetEnumerator()
        {
            yield return Leader;            //wartość zwrócona przy pierwszym wywołaniu Movenext
            yield return ScrumMaster;
            yield return Developer;
            for(int i = 0; i < 5; i++)
            {
                yield return "Vacat";
            }
                    
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class BookEnumerator : IEnumerator<Book>

    {
        private Library _library;
        int index = -1;



        public BookEnumerator(Library library)
        {
            _library = library;
        }



        public Book Current
        {
            get
            {
                while (_library._books[index] == null)
                {
                    index++;
                }
                if (index < _library._books.Length)
                {
                    return _library._books[index];
                }
                throw new Exception();
                
            }
        }



        object IEnumerator.Current => Current;



        public void Dispose()
        {
            
        }



        public bool MoveNext()
        {
            return ++index < _library._books.Length;
        }



        public void Reset()
        {
            
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Library books = new Library();
            IEnumerator<Book> enumerator = books.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
            for (var e = books.GetEnumerator(); e.MoveNext();)
            {
                Console.WriteLine(e.Current);
            }
            foreach (Book book in books)
            {
                Console.WriteLine(book);
            }
            Team team = new Team()
            {
                Leader = "Kowalski",
                ScrumMaster = "Nowak",
                Developer = "Szpak"
            };
            foreach(string member in team)
            {
                Console.WriteLine(member);
            }
        }
    }
}