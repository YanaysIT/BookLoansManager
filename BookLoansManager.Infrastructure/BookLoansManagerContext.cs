using BookLoansManager.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLoansManager.Infrastructure
{
    public class BookLoansManagerContext : DbContext
    {
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Borrower> Borrowers => Set<Borrower>();
        public DbSet<Loan> Loans => Set<Loan>();

        public BookLoansManagerContext(DbContextOptions<BookLoansManagerContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Borrower>().Property(x => x.SocialSecurityNumber)
                .IsRequired().HasMaxLength(12).IsFixedLength();
        }


        //    #region [Seeding]
        //    protected override void OnModelCreating(ModelBuilder builder)
        //    {
        //        base.OnModelCreating(builder);
        //        SeedData(builder);
        //    }

        //    void SeedData(ModelBuilder builder)
        //    {
        //        var books = new List<Book>
        //        { 
        //            new() {Id = 1, Title = "Title 1", ISBN = "ISBN1", ReleaseYear = 1967},
        //            new() {Id = 2, Title = "Title 2", ISBN = "ISBN2", ReleaseYear = 2015},
        //            new() {Id = 3, Title = "Title 3", ISBN = "ISBN3", ReleaseYear = 2113},
        //            new() {Id = 4, Title = "Title 4", ISBN = "ISBN4", ReleaseYear = 1765},
        //        };
        //        builder.Entity<Book>().HasData(books);

        //        var borrowers = new List<Borrower>
        //        {
        //            new() {Id = 1, FirstName = "Jackie", LastName = "Chan", SocialSecurityNumber = "200112238753"},
        //            new() {Id = 2, FirstName = "Betty", LastName = "Boop", SocialSecurityNumber = "198803048768"},
        //            new() {Id = 3, FirstName = "Donald", LastName = "Duck", SocialSecurityNumber = "197311197641"},
        //            new() {Id = 4, FirstName = "Peppa", LastName = "Pig", SocialSecurityNumber = "200510126062"}
        //        };
        //        builder.Entity<Borrower>().HasData(borrowers);

        //        var loans = new List<Loan>
        //        {
        //            new() {Id = 1, LoanDate = DateTime.Now.AddDays(-200), ReturnDate = DateTime.Now.AddDays(-100), BookId = 2, BorrowerId =3},
        //            new() {Id = 2, LoanDate = DateTime.Now.AddDays(-52), BookId = 1,BorrowerId = 2},
        //            new() {Id = 3, LoanDate = DateTime.Now.AddDays(-32), BookId = 4,BorrowerId = 2},
        //            new() {Id = 4, LoanDate = DateTime.Now.AddDays(-10), BookId = 2, BorrowerId = 3}
        //        };
        //        builder.Entity<Loan>().HasData(loans);
        //    }
        //    #endregion
    }
}
