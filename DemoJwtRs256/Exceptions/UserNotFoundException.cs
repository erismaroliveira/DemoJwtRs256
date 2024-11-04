namespace DemoJwtRs256.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string message) : base(message) { }
}
