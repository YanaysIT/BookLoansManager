using BookLoansManager.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookLoansManager.Core.Entities;

[Index(nameof(ISBN), IsUnique = true)]
public class Book : IEntity
{
    public int Id { get; set; }

    [MaxLength(100)]
    public required string Title { get; set; }
    [MaxLength (20)]
    public required string ISBN { get; set; }
   
    public int ReleaseYear { get; set; }

    public bool IsLoaned => Loans?.Any(l => l.ReturnDate == null) == true;

    [JsonIgnore]
    public virtual ICollection<Loan>? Loans { get; set; }
}
