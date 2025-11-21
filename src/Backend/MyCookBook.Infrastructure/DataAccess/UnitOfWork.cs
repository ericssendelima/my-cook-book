using Microsoft.EntityFrameworkCore;
using MyCookBook.Domain.Repositories;

namespace MyCookBook.Infrastructure.DataAccess
{
  public class UnitOfWork(MyCookBookDbContext dbContext) : IUnitOfWork
  {
    private readonly MyCookBookDbContext _dbContext = dbContext;
    public async Task Commit() => await _dbContext.SaveChangesAsync();
  }
}
