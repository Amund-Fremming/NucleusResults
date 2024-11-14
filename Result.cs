namespace ResultPattern
{
    /// <summary>
    /// Represents the result of an operation, encapsulating success or failure state,
    /// optional data, a message, and an exception if applicable.
    /// </summary>
    /// <param name="Data">Generic data on sucess.</param>
    /// <param name="Message">Descriptive message about the error.</param>
    /// <param name="Exception">Exception thrown, used maily for debugging.</param>
    public record Result<T>(T Data, string Message = "", Exception? Exception = null)
    {
        public bool IsSuccess => Data != null;

        public static Result<T> Success(T data) => new(data);
        public static Result<T> Failure(string message, Exception exception) => new(default, message, exception);
    }
}