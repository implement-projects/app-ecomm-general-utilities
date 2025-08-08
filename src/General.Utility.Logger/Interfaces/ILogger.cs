namespace General.Utility.Logger.Interfaces;

public interface ILogger
{
    /// Information
    void Information(string message);
    void Information(string message, params object[] args);
    
    // Error
    void Error(string message, Exception? exception = null);
    void Error(string message, Exception? exception = null, params object[] args);

    // Fatal
    void Fatal(string message, Exception? exception = null);

    // Warning
    void Warning(string message);
    void Warning(string message, params object[] args);

    // Debug
    void Debug(string message);
    void Debug(string message, params object[] args);
}
