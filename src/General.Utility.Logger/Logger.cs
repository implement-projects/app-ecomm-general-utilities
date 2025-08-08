namespace General.Utility.Logger;

public class Logger(ILogger logger) : GUL.ILogger
{
    private readonly ILogger _logger = logger;

    public void Information(string message)
    {
        _logger.Information(message);
    }

    public void Information(string message, params object[] args)
    {
        _logger.Information(message, args);
    }

    public void Error(string message, Exception? exception = null)
    {
        if (exception != null)
            _logger.Error(exception, message);
        else
            _logger.Error(message);
    }

    public void Error(string message, Exception? exception = null, params object[] args)
    {
        if (exception != null)
            _logger.Error(exception, message, args);
        else
            _logger.Error(message, args);
    }

    public void Fatal(string message, Exception? exception = null)
    {
        _logger.Fatal(exception, message);
    }

    public void Warning(string message)
    {
        _logger.Warning(message);
    }

    public void Warning(string message, params object[] args)
    {
        _logger.Warning(message, args);
    }

    public void Debug(string message)
    {
        _logger.Debug(message);
    }

    public void Debug(string message, params object[] args)
    {
        _logger.Debug(message, args);
    }
}
