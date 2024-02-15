using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyJobBoard.Domain.Entities;

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
    public DbSet<Company> Companies { get; set; }
    public DbSet<Interlocutor> Interlocutors { get; set; }
    public DbSet<Opportunity> Opportunities { get; set; }
    public DbSet<OpportunityStep> OpportunitySteps { get; set; }
    public DbSet<OpportunityInterlocutor> OpportunityInterlocutors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

            string? connectionString = _configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
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

        modelBuilder.Entity<Company>()
            .Property(c => c.Websites)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

        modelBuilder.Entity<Company>()
            .Property(c => c.SocialNetworks)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

        modelBuilder.Entity<Opportunity>()
         .HasMany(o => o.Interlocutors)
         .WithMany(i => i.Opportunities)
         .UsingEntity<OpportunityInterlocutor>(
             j =>
             {
                 j.HasOne(io => io.Interlocutor)
                     .WithMany()
                     .HasForeignKey(io => io.InterlocutorId)
                     .OnDelete(DeleteBehavior.NoAction);
             }
         );

        modelBuilder.Entity<Interlocutor>()
         .HasMany(o => o.Opportunities)
         .WithMany(i => i.Interlocutors)
         .UsingEntity<OpportunityInterlocutor>(
             j =>
             {
                 j.HasOne(io => io.Opportunity)
                     .WithMany()
                     .HasForeignKey(io => io.OpportunityId)
                     .OnDelete(DeleteBehavior.NoAction);
             }
         );

    }
}
