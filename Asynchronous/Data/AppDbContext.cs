using Microsoft.EntityFrameworkCore;
using Program.Models;


namespace Program.Data;


public class AppDbContext : DbContext
{

    public DbSet<User> Users { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        optionsBuilder.UseSqlServer(@"...");


    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();



    }



}
