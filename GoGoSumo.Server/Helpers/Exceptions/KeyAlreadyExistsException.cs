namespace GoGoSumo.Server.Helpers.Exceptions;

[Serializable]
internal class KeyAlreadyExistsException : AppException
{
    public KeyAlreadyExistsException()
    {
    }

    public KeyAlreadyExistsException(string? message) : base(message)
    {
    }

    public KeyAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}