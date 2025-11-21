using Microsoft.EntityFrameworkCore;
using MyCookBook.Domain.Entities;

namespace MyCookBook.Infrastructure.DataAccess
{
  public class MyCookBookDbContext(DbContextOptions options) : DbContext(options)
  {
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyCookBookDbContext).Assembly);
    }
  }
}
