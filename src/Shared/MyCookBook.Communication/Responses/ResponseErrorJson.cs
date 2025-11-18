namespace MyCookBook.Communication.Responses
{
  public class ResponseErrorJson(IList<string> errors)
  {
    public IList<string> Errors { get; set; } = errors;

    public ResponseErrorJson(string error) : this([error])
    {      
    }
  }
}
