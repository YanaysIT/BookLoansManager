using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace BookLoansManager.Core.DTOs
{
    [Index(nameof(SocialSecurityNumber), IsUnique = true)]
    public class BorrowerBaseDTO
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [MinLength(12), MaxLength(12)]
        public required string SocialSecurityNumber { get; set; } = string.Empty;
    }
}
