using Microsoft.Extensions.Logging;
namespace CustomAttributeExample
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class LogAttribute : System.Attribute
    {
        public LogLevel LogLevel { get; }

        public LogAttribute(LogLevel logLevel = LogLevel.Warning)
        {
            LogLevel = logLevel;
        }
    }
}
