using BookLoansManager.Core.DTOs;
using BookLoansManager.Core.Entities;
using BookLoansManager.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BooksLoansManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IDbService _dbService;
    public BookController(IDbService dbService) => _dbService = dbService;

    #region Get
    [HttpGet]
    public async Task<ActionResult<List<BookMiniDTO>>> GetBooksAsync()
    {
        var books = await _dbService.GetAsync<Book, BookMiniDTO>();
        if (books.Count == 0)
            return NoContent();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookWithStatusDTO>> GetBookAsync(int id)
    {
        var book = await _dbService.GetSingleAsync<Book, BookWithStatusDTO>(b => b.Id.Equals(id), true);
        if (book is null)
            return NotFound("Book not found.");
        return Ok(book);
    }

    #endregion

    #region Post
    [HttpPost]
    public async Task<ActionResult<Book>> PostBookAsync(BookBaseDTO bookDto)
    {
        try
        {
            var book = await _dbService.AddAsync<Book, BookBaseDTO>(bookDto);
            if (await _dbService.SaveAsync())
            {
                return Created($"{book.Id}", book);
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Couldn't add book.\n{ex}.");
        }
        return BadRequest("Couldn't add book.");
    }
    #endregion

    #region Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBookAsync(int id)
    {
        try
        {
            if (!await _dbService.DeleteAsync<Book>(id))
                return NotFound("Book not found.");
            if (await _dbService.SaveAsync())
                return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Couldn't delete the book.\n{ex}.");
        }
        return BadRequest("Couldn't delete the book.");
    }
    #endregion
}