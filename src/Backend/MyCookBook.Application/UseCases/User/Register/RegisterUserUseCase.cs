using MyCookBook.Communication.Requests;
using MyCookBook.Communication.Responses;
using MyCookBook.Exceptions.ExceptionsBase;

namespace MyCookBook.Application.UseCases.User.Register
{
  public class RegisterUserUseCase : IRegisterUserUseCase
  {
    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
    {
      //Validação dos dados da request
      await Validate(request);

      //Mapear a request em uma entidade

      //Criptografar a senha

      //Salvar no banco de dados

      return new ResponseRegisterUserJson
      {
        Name = request.Name
      };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
      var validator = new RegisterUserValidator();

      var result = validator.Validate(request);

      if (result.IsValid == false)
      {
        var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorMessages);
      }
    }
  }
}
