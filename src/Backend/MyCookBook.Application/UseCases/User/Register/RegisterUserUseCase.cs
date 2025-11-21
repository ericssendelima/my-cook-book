using AutoMapper;
using MyCookBook.Application.Services.Cryptography;
using MyCookBook.Communication.Requests;
using MyCookBook.Communication.Responses;
using MyCookBook.Domain.Repositories;
using MyCookBook.Domain.Repositories.User;
using MyCookBook.Exceptions;
using MyCookBook.Exceptions.ExceptionsBase;

namespace MyCookBook.Application.UseCases.User.Register
{
  public class RegisterUserUseCase : IRegisterUserUseCase
  {
    private readonly IMapper _mapper;
    private readonly PasswordEncripter _passwordEncripter;
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    private readonly IUserWriteOnlyRepository _writeOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserUseCase(
      IMapper mapper,
      PasswordEncripter passwordEncripter,
      IUnitOfWork unitOfWork,
      IUserReadOnlyRepository readOnlyRepository,
      IUserWriteOnlyRepository writeOnlyRepository
      )
    {
      _mapper = mapper;
      _passwordEncripter = passwordEncripter;
      _readOnlyRepository = readOnlyRepository;
      _writeOnlyRepository = writeOnlyRepository;
      _unitOfWork = unitOfWork;
    }
    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
    {
      //Validação dos dados da request
      await Validate(request);

      //Mapear a request em uma entidade
      var user = _mapper.Map<Domain.Entities.User>(request);

      //Criptografar a senha
      user.Password = _passwordEncripter.Encrypt(request.Password);

      //Salvar no banco de dados
      await _writeOnlyRepository.Add(user);

      await _unitOfWork.Commit();

      return new ResponseRegisterUserJson
      {
        Name = request.Name
      };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
      var validator = new RegisterUserValidator();

      var result = validator.Validate(request);

      var emailExists = await _readOnlyRepository.ExistActiveUserWithEmail(request.Email);

      if (emailExists)
      {
        result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
      }

      if (result.IsValid == false)
      {
        var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorMessages);
      }
    }
  }
}
