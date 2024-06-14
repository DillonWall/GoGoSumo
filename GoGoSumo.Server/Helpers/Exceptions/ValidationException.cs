namespace GoGoSumo.Server.Helpers.Exceptions;

public class ValidationException : AppException
{
    public ValidationException() : base() { }
    public ValidationException(string message) : base(message) { }
}
