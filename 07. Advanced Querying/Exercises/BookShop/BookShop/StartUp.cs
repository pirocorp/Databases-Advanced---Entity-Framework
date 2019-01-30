namespace BookShop
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using Microsoft.EntityFrameworkCore;

    using Data;
    using Models;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                //var input = int.Parse(Console.ReadLine());
                var result = GetMostRecentBooks(db);
                Console.WriteLine(result);
            }
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 4200)
                .ToArray();

            context.Books.RemoveRange(books);
            var rowsAffected = context.SaveChanges();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return books.Length;
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.HasValue &&
                            b.ReleaseDate.Value.Year < 2010)
                .ToArray();

            foreach (var book in books)
            {
                book.Price += 5;
            }

            context.SaveChanges();
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categories = context.Categories
                .Include(c => c.CategoryBooks)
                .ThenInclude(cb => cb.Book)
                .GroupBy(c => c.Name)
                .ToDictionary(x => x.Key, x => x.Select(y => y.CategoryBooks.ToArray()).First())
                .ToDictionary(x => x.Key, x => x.Value.Select(bc => bc.Book).ToArray())
                .ToDictionary(x => x.Key, x => x.Value.OrderByDescending(b => b.ReleaseDate).Take(3).ToArray())
                .OrderBy(x => x.Key)
                .ToArray();

            //Another Solution --> Leads to N + 1 queries
            //var otherCategories = context.Categories
            //    .OrderBy(x => x.Name)
            //    .Select(x => new
            //    {
            //        x.Name,
            //        Books = x.CategoryBooks.Select(b => new
            //            {
            //                b.Book.Title,
            //                b.Book.ReleaseDate
            //            })
            //            .OrderByDescending(r => r.ReleaseDate)
            //            .Take(3)
            //            .ToArray()
            //
            //    })
            //    .ToArray();

            var result = new StringBuilder();

            foreach (var category in categories)
            {
                result.AppendLine($"--{category.Key}");

                foreach (var book in category.Value)
                {
                    result.AppendLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
            }

            return result.ToString().Trim();
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            //There is no reason to use include before select!
            //If you change the query so that it no longer returns instances of the entity type that the query began with,
            //then the include operators are ignored.
            var books = context.Categories
                .Include(c => c.CategoryBooks)
                .ThenInclude(cb => cb.Book)
                .Select(c => new
                {
                    c.Name,
                    TotalProfit = c.CategoryBooks.Select(cb => cb.Book).Sum(b => b.Copies * b.Price)
                })
                .ToArray()
                .OrderByDescending(c => c.TotalProfit)
                .ThenBy(c => c.Name)
                .Select(c => $"{c.Name} ${c.TotalProfit:F2}")
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var copiesByAuthor = context.Authors
                .Select(a => new
                {
                    a.FirstName,
                    a.LastName,
                    Copies = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(a => a.Copies) //<-- Sql Side Ordering if goes after first ToArray become C# Side ordering
                .ToArray()
                .Select(a => $"{a.FirstName} {a.LastName} - {a.Copies}")//<-- If this Select goes above first ToArray we will get N+1 query problem!
                .ToArray();

            return string.Join(Environment.NewLine, copiesByAuthor);
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var booksCount = context.Books
                .Count(b => b.Title.Length > lengthCheck);

            return booksCount;
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                .Include(b => b.Author)
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .ToArray()
                .Select(b => $"{b.Title} ({b.Author.FirstName} {b.Author.LastName})")
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => $"{a.FirstName} {a.LastName}")
                .OrderBy(x => x)
                .ToArray();

            return string.Join(Environment.NewLine, authors);
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var dateFormat = "dd-MM-yyyy";
            var dateTime = DateTime.ParseExact(date, dateFormat, CultureInfo.InvariantCulture);

            var books = context.Books
                .Where(b => b.ReleaseDate < dateTime)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:F2}")
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input.ToLower().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).ToList();

            //Single query solution
            var books = context.Books
                .Include(b => b.BookCategories)
                .Where(b => b.BookCategories.Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToArray();

            //N + 1 query solution
            //books = context.Books
            //    .Include(b => b.BookCategories)
            //    .Where(b => b.BookCategories.Any(bc => categories.Any(c => c == bc.Category.Name.ToLower())))
            //    .Select(b => b.Title)
            //    .OrderBy(b => b)
            //    .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(b => b.ReleaseDate.HasValue &&
                            b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => $"{b.Title} - ${b.Price:F2}")
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.EditionType == EditionType.Gold &&
                            b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var ageRestriction = Enum.Parse<AgeRestriction>(command, true);

            var books = context.Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToArray();

            return string.Join(Environment.NewLine, books);
        }
    }
}
