using System.Security.Cryptography;
using System.Text;

namespace MyCookBook.Application.Services.Cryptography
{
  public class PasswordEncripter(string additionalKey)
  {
    private readonly string _additionalKey = additionalKey;

    public string Encrypt(string password)
    {
      var newPassword = $"{password}{_additionalKey}";

      var bytes = Encoding.UTF8.GetBytes(password);
      var hashBytes = SHA512.HashData(bytes);

      return StringBytes(hashBytes);
    }

    private static string StringBytes(byte[] bytes)
    {
      var sb = new StringBuilder();

      foreach (byte b in bytes)
      {
        sb.Append(b.ToString("x2"));
      }

      return sb.ToString();
    }
  }
}