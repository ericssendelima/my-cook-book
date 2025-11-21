using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCookBook.Application.Services.AutoMapper;
using MyCookBook.Application.Services.Cryptography;
using MyCookBook.Application.UseCases.User.Register;

namespace MyCookBook.Application
{
  public static class DependencyInjectionExtension
  {
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
      AddPasswordEncripter(services, configuration);
      AddAutoMapper(services);
      AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
      var autoMapper = new AutoMapper.MapperConfiguration(options =>
      {
        options.AddProfile(new AutoMapping());
      }).CreateMapper();

      services.AddScoped(options => autoMapper);
    }

    private static void AddUseCases(IServiceCollection services)
    {
      services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }

    private static void AddPasswordEncripter(IServiceCollection services, IConfiguration configuration)
    {
      var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");

      services.AddScoped(option => new PasswordEncripter(additionalKey!));
    }
  }
}
