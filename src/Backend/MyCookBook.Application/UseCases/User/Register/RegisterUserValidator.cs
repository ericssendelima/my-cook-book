using FluentValidation;
using MyCookBook.Communication.Requests;

namespace MyCookBook.Application.UseCases.User.Register
{
  public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
  {
    public RegisterUserValidator()
    {
      RuleFor(user => user.Name).NotEmpty();
      RuleFor(user => user.Email).NotEmpty();
      RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6);

      When(user => string.IsNullOrEmpty(user.Email) == false, () =>
      {
        RuleFor(user => user.Email).EmailAddress();
      });
    }
  }
}
