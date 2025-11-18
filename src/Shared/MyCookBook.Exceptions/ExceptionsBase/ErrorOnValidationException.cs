namespace MyCookBook.Exceptions.ExceptionsBase
{
  public class ErrorOnValidationException(IList<string> errorMessages) : MyCookBookException
  {
    public IList<string> ErrorMessages { get; set; } = errorMessages;
  }
}
