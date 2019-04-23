using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CrcAspNetCore.Api.AutoMapper;
using CrcAspNetCore.Api.Controllers;
using CrcAspNetCore.Api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CrcAspNetCore.UnitTests.Controllers
{
    public class BookControllerUnitTests
    {
        // Get
        [Fact]
        public async Task get_book_by_id_with_existing_id_should_return_status_code_200()
        {
            // Arrange
            var repository = BookContextMocker.GetInMemoryBookRepository
                (nameof(get_book_by_id_with_existing_id_should_return_status_code_200));
            var mapperMock = new Mock<IMapper>();
            var controller = new BookController(repository, mapperMock.Object);
            var expectedAuthor = "Jon Skeet";

            //Act
            var response = await controller.GetById(2) as ObjectResult;
            var book = response.Value as Book;

            //Assert
            response.StatusCode.Should().Be(200);
            book.Author.Should().BeEquivalentTo(expectedAuthor);
        }

        [Fact]
        public async Task get_book_by_id_with_not_existing_id_should_return_status_code_404()
        {
            // Arrange
            var repository = BookContextMocker.GetInMemoryBookRepository
                (nameof(get_book_by_id_with_not_existing_id_should_return_status_code_404));
            var mapperMock = new Mock<IMapper>();
            var controller = new BookController(repository, mapperMock.Object);
            var exceptedMessage = "Book with id '7' was not found ";

            //Act
            var response = await controller.GetById(7) as ObjectResult;

            //Assert
            response.StatusCode.Should().Be(404);
            response.Value.Should().BeEquivalentTo(exceptedMessage);
        }

        [Fact]
        public async Task get_book_by_isbn_with_existing_isbn_should_return_status_code_200()
        {
            // Arrange
            var repository = BookContextMocker.GetInMemoryBookRepository
                (nameof(get_book_by_isbn_with_existing_isbn_should_return_status_code_200));
            var mapperMock = new Mock<IMapper>();
            var controller = new BookController(repository, mapperMock.Object);
            var expectedTitle = "Clean Architecture: A Craftsman's Guide to Software Structure and Design";

            //Act
            var response = await controller.GetByIsbn("978-0134494166") as ObjectResult;
            var book = response.Value as Book;

            //Assert
            response.StatusCode.Should().Be(200);
            book.Title.Should().BeEquivalentTo(expectedTitle);
        }

        [Fact]
        public async Task get_book_by_isbn_with_not_existing_isbn_should_return_status_code_404()
        {
            // Arrange
            var repository = BookContextMocker.GetInMemoryBookRepository
                (nameof(get_book_by_isbn_with_not_existing_isbn_should_return_status_code_404));
            var mapperMock = new Mock<IMapper>();
            var controller = new BookController(repository, mapperMock.Object);
            var exceptedMessage = "Book with isbn '1111111111111' was not found ";

            //Act
            var response = await controller.GetByIsbn("1111111111111") as ObjectResult;

            //Assert
            response.StatusCode.Should().Be(404);
            response.Value.Should().BeEquivalentTo(exceptedMessage);
        }

        [Fact]
        public async Task search_book_by_phrase_code_should_return_status_code_200()
        {
            // Arrange
            var repository = BookContextMocker.GetInMemoryBookRepository
                (nameof(search_book_by_phrase_code_should_return_status_code_200));
            var mapperMock = new Mock<IMapper>();
            var controller = new BookController(repository, mapperMock.Object);
            var countOfExceptedBooks = 2;

            //Act
            var response = await controller.Search("Code") as ObjectResult;
            var books = response.Value as IEnumerable<Book>;

            //Assert
            response.StatusCode.Should().Be(200);
            books.Count().Should().Be(countOfExceptedBooks);
        }

        [Fact]
        public async Task search_book_by_phrase_WeirdDummyTestPhrase_should_return_empty_list_with_status_code_200()
        {
            // Arrange
            var repository = BookContextMocker.GetInMemoryBookRepository
                (nameof(search_book_by_phrase_WeirdDummyTestPhrase_should_return_empty_list_with_status_code_200));
            var mapperMock = new Mock<IMapper>();
            var controller = new BookController(repository, mapperMock.Object);
            var countOfExpectedBook = 0;

            //Act
            var response = await controller.Search("WeirdDummyTestPhrase") as ObjectResult;
            var books = response.Value as IEnumerable<Book>;

            //Assert
            response.StatusCode.Should().Be(200);
            books.Count().Should().Be(countOfExpectedBook);
        }
        // Post
        [Fact]
        public async Task post_book_with_correct_data_should_return_status_code_201()
        {
            //Arrange
            var repository = BookContextMocker.GetInMemoryBookRepository
                (nameof(post_book_with_correct_data_should_return_status_code_201));
            var mapper = AutoMapperConfig.Initialize();
            var controller = new BookController(repository, mapper);
            
            //Act
            var response = await controller.Post(CorrectDummyCreationBookDto()) as ObjectResult;
            var item = response?.Value as Book;
         
            //Assert
            response.StatusCode.Should().Be(201);
            item.Title.Should().BeEquivalentTo("Adaptive Code");
        }

        [Fact]
        public async Task post_book_with_exist_isbn_should_return_status_code_409()
        {
            //Arrange
            var repository = BookContextMocker.GetInMemoryBookRepository
                (nameof(post_book_with_exist_isbn_should_return_status_code_409));
            var mapper = AutoMapperConfig.Initialize();
            var controller = new BookController(repository, mapper);

            //Act
            var response = await controller.Post(ExistIsbnDummyCreationBookDto());

            //Assert
            response.Should().BeOfType<ConflictObjectResult>();
        }

        [Fact]
        public async Task update_book_with_correct_data_should_return_status_code_204()
        {
            //Arrange
            var repository = BookContextMocker.GetInMemoryBookRepository
                (nameof(update_book_with_correct_data_should_return_status_code_204));
            var mapper = AutoMapperConfig.Initialize();
            var controller = new BookController(repository, mapper);

            //Act
            var response = await controller.Put(2, ExistIsbnDummyCreationBookDto());

            //Assert
            response.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task update_book_with_invalid_book_id_should_return_status_code_404()
        {
            //Arrange
            var repository = BookContextMocker.GetInMemoryBookRepository
                (nameof(post_book_with_exist_isbn_should_return_status_code_409));
            var mapper = AutoMapperConfig.Initialize();
            var controller = new BookController(repository, mapper);

            //Act
            var response = await controller.Put(222, ExistIsbnDummyCreationBookDto()) as ObjectResult;

            //Assert
            response.StatusCode.Should().Be(404);
        }

        // Delete
        [Fact]
        public async Task delete_book_with_existing_id_should_return_status_code_204()
        {
            //Arrange
            var repository = BookContextMocker.GetInMemoryBookRepository
                (nameof(delete_book_with_existing_id_should_return_status_code_204));
            var mapperMock = new Mock<IMapper>();
            var controller = new BookController(repository, mapperMock.Object);

            //Act 
            var response = await controller.Delete(2);

            //Assert
            response.Should().BeOfType<NoContentResult>();
        }
        
        [Fact]
        public async Task delete_book_with_not_existing_id_should_return_status_code_404()
        {
            //Arrange
            var repository = BookContextMocker.GetInMemoryBookRepository
                (nameof(delete_book_with_not_existing_id_should_return_status_code_404));
            var mapperMock = new Mock<IMapper>();
            var controller = new BookController(repository, mapperMock.Object);

            //Act 
            var response = await controller.Delete(999);

            //Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        /// <summary>
        /// Dummy object with correct data for Post/Put tests.
        /// </summary>
        /// <returns>CreationBookDto</returns>
        private CreationBookDto CorrectDummyCreationBookDto()
        {
            var book = new CreationBookDto
            {
                Title = "Adaptive Code",
                Author = "Gary McLean Hall",
                Description = "By applying this book’s principles, you can create code" +
                              " that accommodates new requirements and unforeseen scenarios without significant rewrites.",
                Isbn = "978-1509302581",
                PublishedAt = new DateTime(2004, 10, 02)
            };

            return book;
        }

        /// <summary>
        /// Dummy object with existing isbn
        /// </summary>
        /// <returns>CreationBookDto</returns>
        private CreationBookDto ExistIsbnDummyCreationBookDto()
        {
            var book = new CreationBookDto
            {
                Title = "Adaptive Code",
                Author = "Gary McLean Hall",
                Description = "By applying this book’s principles, you can create code" +
                              " that accommodates new requirements and unforeseen scenarios without significant rewrites.",
                Isbn = "978-0134494166",
                PublishedAt = new DateTime(2004, 10, 02)
            };

            return book;
        }
    }
}
