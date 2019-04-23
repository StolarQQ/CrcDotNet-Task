using System;
using System.Threading.Tasks;
using AutoMapper;
using CrcAspNetCore.Api.Models;
using CrcAspNetCore.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CrcAspNetCore.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetBookById")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound($"Book with id '{id}' was not found ");
            }

            return Ok(book);
        }

        [HttpGet("isbn/{isbn}", Name = "GetBookByIsbn")]
        public async Task<IActionResult> GetByIsbn(string isbn)
        {
            var book = await _bookRepository.GetByIsbnAsync(isbn);

            if (book == null)
            {
                return NotFound($"Book with isbn '{isbn}' was not found ");
            }

            return Ok(book);
        }

        [HttpGet(Name = "Search")]
        public async Task<IActionResult> Search(string phrase)
        {
            var books = await _bookRepository.SearchByPhraseAsync(phrase);

            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreationBookDto book)
        {
            if (book == null)
            {
                return NotFound("Book cannot be null");
            }

            var bookExist = await _bookRepository.GetByIsbnAsync(book.Isbn);

            if (bookExist != null)
            {
                return Conflict($"Book with isbn '{book.Isbn}' already exist");
            }

            var finalBook = _mapper.Map<Book>(book);

            await _bookRepository.AddAsync(finalBook);

            return CreatedAtRoute("GetBookByIsbn", new { isbn = book.Isbn }, finalBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]CreationBookDto book)
        {
            try
            {
                var bookToUpdate = await _bookRepository.GetByIdAsync(id);
                if (bookToUpdate == null)
                {
                    return NotFound($"Book with id '{id}' not found ");
                }

                _mapper.Map(book, bookToUpdate);

                await _bookRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, "Updating failure");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound($"Book with id '{id}' not found ");
            }

            await _bookRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}