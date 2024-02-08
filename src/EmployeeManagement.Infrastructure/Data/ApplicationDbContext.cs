using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Common.Model;

namespace EmployeeManagement.Infrastructure.Data;
public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Team> Teams { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string DbPath = Path.Combine("Data", "EmployeeManagement.db");
        optionsBuilder.UseSqlite($"Filename={DbPath}");

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Employee>().HasKey(e => e.Id);
        builder.Entity<Address>().HasKey(e => e.Id);
        builder.Entity<Job>().HasKey(e => e.Id);
        builder.Entity<Team>().HasKey(e => e.Id);

        builder.Entity<Employee>().HasOne(e => e.Address);
        builder.Entity<Employee>().HasOne(e => e.Job);

        builder.Entity<Employee>().HasMany(e => e.Teams).WithMany(t => t.Employees);

        base.OnModelCreating(builder);
    }
}
