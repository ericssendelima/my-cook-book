using MyCookBook.Communication.Requests;
using MyCookBook.Communication.Responses;

namespace MyCookBook.Application.UseCases.User.Register
{
  public interface IRegisterUserUseCase
  {
    public Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request);
  }
}
