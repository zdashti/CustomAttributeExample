using Microsoft.Extensions.Logging;

namespace CustomAttributeExample
{
    public class Person
    {
        [LogAttribute(LogLevel.Critical)]
        public int Id { get; set; }
        [LogAttribute]
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        [LogAttribute]
        public string Role { get; set; }
    }
}
