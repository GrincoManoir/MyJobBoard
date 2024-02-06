using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyJobBoard.Domain.Entities;
using System;
using System.Text.Json;

public class MyJobBoardBusinessDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly IConfiguration _configuration;

    public MyJobBoardBusinessDbContext(
        DbContextOptions<MyJobBoardBusinessDbContext> options,
        IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Document> Documents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

            /*string? connectionString = _configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");*/
            string? connectionString = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
            if (connectionString is null)
                throw new ApplicationException("Database connection strings are missing");

            optionsBuilder.UseSqlServer(connectionString);
        }
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Document>()
               .Property(doc => doc.Content)
               .Metadata.SetPropertyAccessMode(PropertyAccessMode.Property);
    }
}
