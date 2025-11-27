using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using MyCookBook.Application.UseCases.User.Register;
using MyCookBook.Exceptions;
using MyCookBook.Exceptions.ExceptionsBase;
using Shouldly;
using System.Net;
using System.Threading.Tasks;

namespace UseCases.User.Register
{
  public class RegisterUserUseCaseTest
  {
    [Fact]
    public async Task Sucess()
    {
      var request = RequestRegisterUserJsonBuilder.Build();

      var useCase = CreateUseCase();

      var result = await useCase.Execute(request);

      result.ShouldNotBeNull();
      result.Name.ShouldBe(request.Name);
    }

    [Fact]
    public async Task Error_Email_Already_Registered()
    {
      var request = RequestRegisterUserJsonBuilder.Build();

      var useCase = CreateUseCase(request.Email);

      async Task act() => await useCase.Execute(request);

      var exception = await act().ShouldThrowAsync<ErrorOnValidationException>();

      exception.ShouldSatisfyAllConditions(
          () => exception.GetHttpStatus().ShouldBe(HttpStatusCode.BadRequest),
          () => exception.ErrorMessages.Count.ShouldBe(1),
          () => exception.ErrorMessages.ShouldContain(ResourceMessagesException.EMAIL_ALREADY_REGISTERED)

        );
    }


    private static RegisterUserUseCase CreateUseCase(string? email = null)
    {
      var mapper = MapperBuilder.Build();
      var passwordEncripter = PasswordEncripterBuilder.Build();
      var unitOfWork = UnitOfWorkBuilder.Build();
      var writeOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
      var readOnlyRepositoryBuider = new UserReadOnlyRepositoryBuilder();

      if (string.IsNullOrEmpty(email) == false) readOnlyRepositoryBuider.ExistActiveUserWithEmail(email);

      return new RegisterUserUseCase(
        mapper,
        passwordEncripter,
        unitOfWork,
        readOnlyRepositoryBuider.Build(),
        writeOnlyRepository);
    }
  }
}
