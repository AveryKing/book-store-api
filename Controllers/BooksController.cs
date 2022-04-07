using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _booksService;
        // GET: api/Books
        [HttpGet]
        public async Task<List<Book>> Get()
        {
            return await _booksService.GetBooksAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Book>> Get(string id)
        {
            var book = await _booksService.GetBookAsync(id);
        
            if (book is null)
            {
                return NotFound();
            }

            return book;
        }

        // POST: api/Books
        [HttpPost]
       public async Task<IActionResult> Post([FromBody] Book book)
        {
            await _booksService.CreateBookAsync(book);

            return CreatedAtAction(nameof(Get), new { id = book.Id.ToString() }, book);
        }   

       [HttpPut("{id:length(24)}")]
       public async Task<IActionResult> Update(string id, Book updatedBook)
       {
           var book = await _booksService.GetBookAsync(id);

           if (book is null)
           {
               return NotFound();
           }

           updatedBook.Id = book.Id;

           await _booksService.UpdateBookAsync(updatedBook);

           return NoContent();
       }

       [HttpDelete("{id:length(24)}")]
       public async Task<IActionResult> Delete(string id)
       {
           var book = await _booksService.GetBookAsync(id);

           if (book is null)
           {
               return NotFound();
           }

           await _booksService.DeleteBookAsync(book);

           return NoContent();
       }
    }
}
