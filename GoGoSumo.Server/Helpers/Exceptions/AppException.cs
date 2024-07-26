namespace GoGoSumo.Server.Helpers.Exceptions;

// custom exception class for throwing application specific exceptions (e.g. for validation) 
// that can be caught and handled within the application
[Serializable]
public class AppException : Exception
{
    public AppException()
    {
    }

    public AppException(string? message) : base(message)
    {
    }

    public AppException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}