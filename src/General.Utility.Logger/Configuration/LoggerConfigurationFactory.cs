namespace General.Utility.Logger.Configuration;

public static class LoggerConfigurationFactory
{
    public static ILogger CreateLogger(string filePath, string connectionString, string tableName)
    {
        var columnWriters = new Dictionary<string, ColumnWriterBase>
        {
            { "message", new RenderedMessageColumnWriter() },
            { "message_template", new MessageTemplateColumnWriter() },
            { "level", new LevelColumnWriter() },
            { "timestamp", new TimestampColumnWriter() },
            { "exception", new ExceptionColumnWriter() },
            { "log_event", new LogEventSerializedColumnWriter() }
        };

        var logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File(filePath, rollingInterval: RollingInterval.Day) // logs/log-.txt
            .WriteTo.PostgreSQL(
                connectionString: connectionString,
                tableName: tableName,
                columnOptions: columnWriters,
                needAutoCreateTable: true
            )
            .CreateLogger();

        return logger;
    }
}
