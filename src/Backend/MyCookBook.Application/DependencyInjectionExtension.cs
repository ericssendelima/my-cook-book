using Microsoft.Extensions.DependencyInjection;
using MyCookBook.Application.UseCases.User.Register;

namespace MyCookBook.Application
{
  public static class DependencyInjectionExtension
  {
    public static void AddApplication(this IServiceCollection services)
    {
      AddUseCases(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
      services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }
  }
}
