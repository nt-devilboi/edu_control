using EduControl.Controllers.Model;
using EduControl.DataBase.ModelBd;
using Microsoft.EntityFrameworkCore;

namespace EduControl.DataBase;

public class ControlTimeDb : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Event?> Events { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Token> Tokens { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=EduControl;Username=Dev;Password=123123");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .HasMany<Book>()
            .WithOne()
            .HasForeignKey(x => x.UserId);
    }
}