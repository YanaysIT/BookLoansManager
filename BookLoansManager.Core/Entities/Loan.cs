using BookLoansManager.Core.Interfaces;
using System.Text.Json.Serialization;

namespace BookLoansManager.Core.Entities;

public class Loan : IEntity
{
    public int Id { get; set; }
    public required DateTime LoanDate { get; set; } = DateTime.Now;
    public DateTime? ReturnDate { get; set; } = null;
    public int BookId { get; set; }
    public int BorrowerId { get; set; }

    [JsonIgnore]
    public virtual Book? Book { get; set; }
    [JsonIgnore]
    public virtual Borrower? Borrower { get; set; }
}
