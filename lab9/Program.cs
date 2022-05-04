using System;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace lab9
{
    class Program
    {
        static void Main(string[] args)
        {
            AppContext context = new AppContext();
            context.Database.EnsureCreated();

            IQueryable<Book> books = from book in context.Books
                                     where book.EditionYear > 2019
                                     select book;
            Console.WriteLine(string.Join("\n", books));
            var list = from book in context.Books
                       join author in context.Authors
                       on book.AuthorId equals author.Id
                       where book.EditionYear > 2019
                       select new { BookAuthor = author.Name, Title = book.Title };
            Console.WriteLine(string.Join("\n", list));
            foreach (var item in list)
            {
                Console.WriteLine(item.BookAuthor);
            }
            list = context.Authors.Join(
                context.Books Where(books => books.EditionYear > 2019),
                a => a.Id,
                b => b.AuthorId,
                (a, b) => new { BookAuthor = a.Name, Title = b.Title }
                );
            foreach (var item in list)
            {
                Console.WriteLine(item.BookCopy);
            }
            list = context.BookCopy.Join(
                context.Books,
                a => a.Id,
                b => b.AuthorId,
                (a, b) => new { BookCopy = a.UniqueNumber, b.Title }
                );
            string xml =
                "<books>" +
                    "<book>" +
                        "<id>1</id>" +
                        "<title>c#</title>" +
                    "</book>" +
                    "<book>" +
                        "<id>2</id>" +
                        "<title>Asp.Net</title>" +
                    "</book>" +
                "</books>";
            XDocument doc = XDocument.Parse(xml);
            System.Collections.Generic.IEnumerable<XElement> booksId =
            doc
                .Elements("books")
                .Elements("book")
                .Select(x => new { Id = x.Elements("id").First().Value ,Title x.Elements("title").First().Value});
            foreach(XElement e in booksId)
            {
                Console.WriteLine(e);
            }
            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/xml");
            client.DownloadString("http://api.nbp.pl/api/exchangerates/tables/C");
            Console.WriteLine(xml);
            string xml =
                "<Code>Usd</Code>" +
                "<Bid>4.30</Bid>" +
                "<Ask>4.40</Ask>";
            
            var rates = XDocument.Parse(xml);
                    .Elements("ArrayOfExchangeRatesTable")
                    .Elements("ExchangeRatesTable")
                    .Elements("Rates")
                    .Elements("Rate")
                    .Select(n => new
                    {
                        Code = n.Element("Code").Value,
                        Ask = n.Element("Ask").Value,
                        Bid = n.Element("Bid").Value
                    });


        }


        public record Book
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int EditionYear { get; set; }
            public int AuthorId { get; set; }
        }
        public record BookCopy
        {
            public int Id { get; set; }
            public int BookId { get; set; }
            public string UniqueNumber { get; set; }
        }
        public record Author
        {
            public int Id { get; set; }
            public string Name { get; set; }
            
        }
        class AppContext : DbContext
        {
            public DbSet<Book> Books { get; set; }
            public DbSet<Author> Authors { get; set; }
            public DbSet<BookCopy> bookCopies { get; set; }
            
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("DATASOURCE=d:\\database\\base.db");
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Book>()
                    Totable
                    
            }
        }
    }
}
