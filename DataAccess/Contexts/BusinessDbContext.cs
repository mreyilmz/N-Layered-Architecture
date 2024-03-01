using System;
using Core.Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Contexts;

public class BusinessDbContext:DbContext
{
	public BusinessDbContext()
	{
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //todo: Add Configuration transfer with projects
        //var connectionString = Configuration.GetValue<string>("ConnectionStrings:Production");
        //optionsBuilder.UseSqlServer("Server=localhost;Database=BusinessSimple;User Id=SA;TrustServerCertificate=true;Password=reallyStrongPwd123;");
        optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=BusinessSimple;User Id=SA;TrustServerCertificate=true;Password=Aymed1121;");
    }
    public DbSet<User> Users { get; set; }
    public DbSet<CardType> CardTypes { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<ProductTransaction> ProductTransactions { get; set; }
    public DbSet<AccountTransaction> AccountTransactions { get; set; }
}

