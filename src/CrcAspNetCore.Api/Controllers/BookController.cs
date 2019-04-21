using System.Threading.Tasks;
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

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet("{id}", Name = "GetBookById")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound($"Book with id '{id}' was not found ");
            }

            return Ok(book);
        }

        [HttpGet(Name = "GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookRepository.GetAllAsync();
            
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Book book)
        {            
            if (book == null)
            {
                return NotFound("Book cannot be null");
            }

            await _bookRepository.AddAsync(book);

            return CreatedAtRoute("GetBookById", new {id = book.Id}, book);
        }
        
        [HttpPut]
        public async Task<IActionResult> Update(int id, Book book)
        {
            var bookToUpdate = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound($"Book with id '{id}' not found ");
            }

            await _bookRepository.UpdateAsync(id, bookToUpdate);

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