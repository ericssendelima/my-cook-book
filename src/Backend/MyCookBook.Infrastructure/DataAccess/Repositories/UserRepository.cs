using Microsoft.EntityFrameworkCore;
using MyCookBook.Domain.Entities;
using MyCookBook.Domain.Repositories.User;

namespace MyCookBook.Infrastructure.DataAccess.Repositories
{
  public class UserRepository(MyCookBookDbContext dbContext) : IUserReadOnlyRepository, IUserWriteOnlyRepository
  {
    private readonly MyCookBookDbContext _dbContext = dbContext;
    public async Task Add(User user) => await _dbContext.Users.AddAsync(user);

    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
      return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);
    }
  }
}
