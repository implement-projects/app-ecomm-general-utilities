namespace General.Utility.CQRS.Exceptions;

public class HandlerNotFoundException : Exception
{
    public HandlerNotFoundException()
        : base("Age is not valid.") { }

    public HandlerNotFoundException(string message)
        : base(message) { }

    public HandlerNotFoundException(string message, Exception innerException)
        : base(message, innerException) { }

    public HandlerNotFoundException(Type type)
        : base($"Handler not found for type: {type.FullName}") { }

    public HandlerNotFoundException(string message, Type type)
        : base($"{message} {type.FullName}") { }
}
