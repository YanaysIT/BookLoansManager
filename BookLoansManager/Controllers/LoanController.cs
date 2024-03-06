using BookLoansManager.Core.DTOs;
using BookLoansManager.Core.Entities;
using BookLoansManager.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookLoansManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoanController : ControllerBase
{
    private readonly IDbService _dbService;
    public LoanController(IDbService dbService) => _dbService = dbService;

    #region Get
    [HttpGet("{id}")]
    public async Task<ActionResult<LoanMiniDTO>> GetLoanAsync(int id)
    {
        var loan = await _dbService.GetSingleAsync<Loan, LoanMiniDTO>(l => l.Id.Equals(id));
        if (loan is null)
            return NotFound("Loan not found.");
        return Ok(loan);
    }
    #endregion

    #region Post
    [HttpPost]
    public async Task<ActionResult<Loan>> PostLoanAsync(LoanCompactDTO loanDto)
    {
        try
        {
            var book = await _dbService.GetSingleAsync<Book, BookWithStatusDTO>(b => b.Id.Equals(loanDto.BookId), true);
            if (book is null)
                return NotFound("Book not found.");
            if (book.IsLoaned)
                return BadRequest("Couldn't check out the book. It's already loaned.");
            if (!await _dbService.AnyAsync<Borrower>(l => l.Id == loanDto.BorrowerId))
                return NotFound("Borrower not found.");
            var loan = await _dbService.AddAsync<Loan, LoanCompactDTO>(loanDto);

            if (await _dbService.SaveAsync())
            {
                return Created($"{loan.Id}", loan);
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Couldn't check out the book.\n{ex}.");
        }
        return BadRequest("Couldn't add check out the book.");
    }
    #endregion

    #region Patch

    [HttpPatch("{id}")]
    public async Task<ActionResult> PatchLoanAsync(int id)
    {
        try
        {
            var loanToClose = await _dbService.AnyAsync<Loan>(l => l.Id == id && l.ReturnDate == null);
            if (!loanToClose)
                return NotFound($"Loan doesn't exist or the book has been already checked in.");
            await _dbService.UpdateAsync<Loan>(l => l.Id == id, l => l.ReturnDate = DateTime.Now);
            if (await _dbService.SaveAsync())
                return Ok("The book has been ckecked in.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Couldn't check in the book.\n{ex}.");
        }
        return BadRequest("Couldn't check in the book.");
    }
    #endregion
}