using CommonTestUtilities.Requests;
using MyCookBook.Application.UseCases.User.Register;
using MyCookBook.Exceptions;
using Shouldly;

namespace Validators.Test.User.Register
{
  public class RegisterUserValidatorTest
  {
    [Fact]
    public void Sucess()
    {
      var validator = new RegisterUserValidator();

      var request = RequestRegisterUserJsonBuilder.Build();

      var result = validator.Validate(request);

      result.IsValid.ShouldBe(true);
    }

    [Fact]
    public void Error_Name_Empty()
    {
      var validator = new RegisterUserValidator();

      var request = RequestRegisterUserJsonBuilder.Build();
      request.Name = string.Empty;

      var result = validator.Validate(request);

      result.IsValid.ShouldBe(false);
      result.Errors.ShouldSatisfyAllConditions(
        () => result.Errors.ShouldHaveSingleItem(),
        () => result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.NAME_EMPTY))
        );
    }

    [Fact]
    public void Error_Email_Empty()
    {
      var validator = new RegisterUserValidator();

      var request = RequestRegisterUserJsonBuilder.Build();
      request.Email = string.Empty;

      var result = validator.Validate(request);

      result.IsValid.ShouldBe(false);
      result.Errors.ShouldSatisfyAllConditions(
        () => result.Errors.ShouldHaveSingleItem(),
        () => result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_EMPTY))
        );
    }

    [Fact]
    public void Error_Email_Invalid()
    {
      var validator = new RegisterUserValidator();

      var request = RequestRegisterUserJsonBuilder.Build();
      request.Email = "aaaa";

      var result = validator.Validate(request);

      result.IsValid.ShouldBe(false);
      result.Errors.ShouldSatisfyAllConditions(
        () => result.Errors.ShouldHaveSingleItem(),
        () => result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_INVALID))
        );
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Error_Password_Invalid(int passwordLength)
    {
      var validator = new RegisterUserValidator();

      var request = RequestRegisterUserJsonBuilder.Build(passwordLength);

      var result = validator.Validate(request);

      result.IsValid.ShouldBe(false);
      result.Errors.ShouldSatisfyAllConditions(
        () => result.Errors.ShouldHaveSingleItem(),
        () => result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.PASSWORD_INVALID))
        );
    }
  }
}
