﻿using BookAPI.Model;
using BookAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookAPI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBooks(int id)
        {
            var book = await _bookRepository.Get(id);

            if (book is null) return NotFound("Produto não encontrado!");

            return book;
        }

        [HttpPost]

        public async Task<ActionResult<Book>> PostBooks([FromBody] Book book)
        {
            var newBook = await _bookRepository.Create(book);
            return CreatedAtAction(nameof(GetBooks), new { id = newBook.Id }, newBook);
         
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var bookToDelete = await _bookRepository.Get(id);

            if (bookToDelete is null) return NotFound("Produto não encontrado!");


            await _bookRepository.Delete(bookToDelete.Id);
            return NoContent();
           
        }

        [HttpPut]
        public async Task<ActionResult<Book>> PutBook(int id, [FromBody] Book book)
        {

            if (id != book.Id)
                return BadRequest("Produto não encontrado!");


            await _bookRepository.Update(book);
            return NoContent();

        }


    }
}
