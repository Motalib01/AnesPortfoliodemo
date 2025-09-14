using AnesPortfolio.Domain.Contact;
using AnesPortfolio.Domain.Projects;
using AnesPortfolio.Domain.Resume;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AnesPortfolio.Infrastructure;

public class PortfolioContext : DbContext
{
    public PortfolioContext(DbContextOptions<PortfolioContext> options) : base(options) { }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Resume> Resumes { get; set; }
    public DbSet<ContactInfo> ContactInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        var stringListConverter = new ValueConverter<List<string>, string>(
            v => string.Join(";", v),                     
            v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList() 
        );

        modelBuilder.Entity<Project>()
            .Property(p => p.Images)
            .HasConversion(stringListConverter);

        base.OnModelCreating(modelBuilder);
    }
}