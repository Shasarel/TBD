namespace TBD.Core.Validation
{
    public class ValidationResult
    {
        public ValidationResult(string key, string message)
        {
            Key = key;
            Message = message;
        }
        public string Key { get; }
        public string Message { get; }
    }
}
