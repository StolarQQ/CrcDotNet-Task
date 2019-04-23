using System;
using CrcAspNetCore.Api.DbContexts;
using CrcAspNetCore.Api.Models;

namespace CrcAspNetCore.UnitTests
{
    public static class BookContextExtensions
    {
        public static void FillDatabase(this BookContext dbContext)
        {
            dbContext.Books.Add
            (
                new Book()
                {
                    Id = 2,
                    Title = "C# in Depth",
                    Author = "Jon Skeet",
                    Description = "C# in Depth, Fourth Edition is your key to unlocking" +
                                  " the powerful new features added to the language in C# 5, 6, and 7.t",
                    Isbn = "978-1617294532",
                    PublishedAt = new DateTime(2019,03,23)
                }
            );

            dbContext.Books.Add
            (
                new Book()
                {
                    Id = 3,
                    Title = "Pro C# 7",
                    Author = "Andrew Troelsen",
                    Description = "This essential classic title provides a comprehensive" +
                                  " foundation in the C# programming language and the frameworks it lives in. ",
                    Isbn = "978-1484230176",
                    PublishedAt = new DateTime(2017, 04, 21)
                }
            );

            dbContext.Books.Add
            (
                new Book()
                {
                    Id = 4,
                    Title = "Clean Architecture: A Craftsman's Guide to Software Structure and Design",
                    Author = "Robert C. Martin",
                    Description = "Uncle Bob presents the universal rules of software" +
                                  " architecture that will help you dramatically improve developer productivity throughout the life of any software system.",
                    Isbn = "978-0134494166",
                    PublishedAt = new DateTime(2017, 09, 20)
                }
            );

            dbContext.Books.Add
            (
                new Book()
                {
                    Id = 5,
                    Title = "Working Effectively with Legacy Code",
                    Author = "Michael Feathers",
                    Description = "In this book, Michael Feathers offers start-to-finish strategies for working more effectively with large, untested legacy code bases",
                    Isbn = "978-0131177055",
                    PublishedAt = new DateTime(2004, 10, 02)
                }
            );

            dbContext.Books.Add
            (
                new Book()
                {
                    Id = 6,
                    Title = "Code Complete: A Practical Handbook of Software Construction",
                    Author = "Steve McConnel",
                    Description = "Widely considered one of the best practical guides to programming," +
                                  " Steve McConnell’s original CODE COMPLETE has been helping developers write better software for more than a decade",
                    Isbn = "978-0735619678",
                    PublishedAt = new DateTime(2004, 06, 19)
                }
            );

            dbContext.SaveChanges();
        }
    }
}
