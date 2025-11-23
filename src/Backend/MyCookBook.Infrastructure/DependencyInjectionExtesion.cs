using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCookBook.Domain.Repositories;
using MyCookBook.Domain.Repositories.User;
using MyCookBook.Infrastructure.DataAccess;
using MyCookBook.Infrastructure.DataAccess.Repositories;
using MyCookBook.Infrastructure.Extensions;

namespace MyCookBook.Infrastructure
{
  public static class DependencyInjectionExtesion
  {
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
      AddRepositories(services);
      AddDbContext_SqlServer(services, configuration);
    }

    private static void AddRepositories(IServiceCollection services)
    {
      services.AddScoped<IUnitOfWork, UnitOfWork>();
      services.AddScoped<IUserReadOnlyRepository, UserRepository>();
      services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
    }
    private static void AddDbContext_SqlServer(IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<MyCookBookDbContext>(dbContextOptions =>
      {
        dbContextOptions.UseSqlServer(configuration.ConnectionString());
      });
    }
  }
}
