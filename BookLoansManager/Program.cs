using AutoMapper;
using BookLoansManager.Core.DTOs;
using BookLoansManager.Core.Entities;
using BookLoansManager.Core.Interfaces;
using BookLoansManager.Infrastructure;
using BookLoansManager.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BookLoansManagerContext>(opt => opt.UseSqlServer(
    builder.Configuration.GetConnectionString("BookLoansManagerConnection")));
ConfigureAutoMapper(builder.Services);

builder.Services.AddScoped<IDbService, DbService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();

void ConfigureAutoMapper(IServiceCollection service)
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<Book, BookBaseDTO>().ReverseMap();
        cfg.CreateMap<Book, BookMiniDTO>().ReverseMap();
        cfg.CreateMap<Book, BookWithStatusDTO>().ReverseMap();
        cfg.CreateMap<Borrower, BorrowerBaseDTO>().ReverseMap();
        cfg.CreateMap<Borrower, BookMiniDTO>().ReverseMap();
        cfg.CreateMap<Loan, LoanCompactDTO>().ReverseMap();
        cfg.CreateMap<Loan, LoanBaseDTO>().ReverseMap();
        cfg.CreateMap<Loan, LoanMiniDTO>().ReverseMap();
    });
    var mapper = config.CreateMapper();
    service.AddSingleton(mapper);
}