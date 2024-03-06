using BookLoansManager.Core.DTOs;
using BookLoansManager.Core.Entities;
using BookLoansManager.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookLoansManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BorrowerController : ControllerBase
{
    private readonly IDbService _dbService;
    public BorrowerController(IDbService dbService) => _dbService = dbService;

    #region Post
    [HttpPost]
    public async Task<ActionResult<Borrower>> PostBorrowerAsync(BorrowerBaseDTO borrowerDto)
    {
        try
        {
            var borrower = await _dbService.AddAsync<Borrower, BorrowerBaseDTO>(borrowerDto);
            if (await _dbService.SaveAsync())
            {
                return Created($"{borrower.Id}", borrower);
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Couldn't add borrower.\n{ex}.");
        }
        return BadRequest("Couldn't add borrower.");
    }
    #endregion

    #region Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBorrowerAsync(int id)
    {
        try
        {
            if (!await _dbService.DeleteAsync<Borrower>(id))
                return NotFound("Borrower not found.");
            if (await _dbService.SaveAsync())
                return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Couldn't delete the borrower.\n{ex}.");
        }
        return BadRequest("Couldn't delete the borrower.");
    }
    #endregion
}
