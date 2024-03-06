namespace BookLoansManager.Core.DTOs
{
    public class LoanCompactDTO 
    {
        public int BookId { get; set; } = 0;
        public int BorrowerId { get; set; } = 0;
    }
    public class LoanBaseDTO : LoanCompactDTO
    {
        public DateTime LoanDate { get; set; } = DateTime.Now;
        public DateTime? ReturnDate { get; set; } = null;
    }

    public class LoanMiniDTO : LoanBaseDTO 
    {
        public int Id { get; set; } = 0;
    }

}
