using MyCookBook.Communication.Requests;
using MyCookBook.Communication.Responses;

namespace MyCookBook.Application.UseCases.User.Register
{
  public class RegisterUserUseCase : IRegisterUserUseCase
  {
    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
    {
      return new ResponseRegisterUserJson
      {
        Name = request.Name
      };
    }
  }
}
