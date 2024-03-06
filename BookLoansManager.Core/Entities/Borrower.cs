using BookLoansManager.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookLoansManager.Core.Entities;

[Index(nameof(SocialSecurityNumber), IsUnique = true)]
public class Borrower : IEntity
{
    public int Id { get; set; }

    [MaxLength(50)]
    public required string FirstName { get; set; }

    [MaxLength(50)]
    public required string LastName { get; set; }

    [MinLength(12), MaxLength(12)]
    public required string SocialSecurityNumber { get; set; }

    [JsonIgnore]
    public virtual ICollection<Loan>? Loans { get; set; }
}