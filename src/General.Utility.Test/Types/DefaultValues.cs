namespace General.Utility.Test.Types;

public static class DefaultValues
{
    public static CancellationToken _cancellationToken = CancellationToken.None;

    // COMMAND
    public static string _commandHandlerResponse = "Custom CQRS Testing for command handler.";

    // QUERY
    public static string _queryHandlerResponse = "Custom CQRS Testing for query handler.";
}
