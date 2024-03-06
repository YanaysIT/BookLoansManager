using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BookLoansManager.Core.DTOs
{
    [Index(nameof(ISBN), IsUnique = true)]
    public class BookBaseDTO
    {
        [Required, MaxLength(20)]
        public string ISBN { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        public int ReleaseYear { get; set; } = 0;
    }

    public class BookMiniDTO : BookBaseDTO
    {
        public int Id { get; set; } = 0;
    }

    public class BookWithStatusDTO : BookMiniDTO
    {
        public bool IsLoaned { get; set; }
    }
}
