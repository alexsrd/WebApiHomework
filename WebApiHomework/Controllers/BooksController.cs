using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApiBookCatalog.Models;

namespace WebApiBookCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public BooksController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, [FromBody]Book book)
        {
            if (_context.Books.Find(id) == null) return BadRequest();
            var updateBook = _context.Books.Find(id);
            updateBook.Author = book.Author;
            updateBook.Name = book.Name;
            updateBook.PublisherName = book.PublisherName;
            updateBook.YearPublish = book.YearPublish;
            _context.Books.Update(updateBook);
            await _context.SaveChangesAsync();

            return Ok(updateBook);
        }

        // POST: api/Books
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Book> PostBook([FromBody] BookModel bookModel)
        {
            string genre = bookModel.Genre;
            Book book = new Book()
            {
                Name = bookModel.Name,
                PublisherName = bookModel.PublisherName,
                Author = bookModel.Author,
                YearPublish = bookModel.YearPublish
            };
            var Genre = _context.Genres.Include("Books").Where(g => g.Name == genre).FirstOrDefault();
            Genre.Books.Add(book);
            _context.Genres.Update(Genre);
            _context.Books.Add(book);
            _context.SaveChanges();
            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
