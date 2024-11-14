namespace ResultPattern
{
    /// <summary>
    /// Represents the result of an operation, encapsulating success or failure state,
    /// optional data, a message, and an exception if applicable.
    /// </summary>
    /// <param name="Message">Descriptive message about the error.</param>
    /// <param name="Exception">Exception thrown, used maily for debugging.</param>
    public record VoidResult(string Message = "", Exception? Exception = null)
    {
        public bool IsSuccess => Exception == null;

        public static VoidResult Success() => new();
        public static VoidResult Failure(string message, Exception exception) => new(message, exception);
    }
}