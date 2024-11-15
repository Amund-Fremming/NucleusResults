namespace ResultPattern.src
{
    /// <summary>
    /// Represents the result of an operation, encapsulating success or failure state,
    /// optional data, a message, and an exception if applicable.
    /// </summary>
    /// <param name="Data">Generic data on sucess.</param>
    /// <param name="Message">Descriptive message if there is an the error.</param>
    /// <param name="Exception">Exception thrown, used maily for debugging.</param>
    public record Result<T>(T Data, string Message = "", Exception? Exception = null) : IResult<T>
    {
        public bool IsSuccess => Data != null;

        /// <summary>
        /// Method for simplifying the creation of a successful Result.
        /// </summary>
        public static Result<T> Success(T data) => new(data);

        /// <summary>
        /// Method for simplifying the creation of a failed Result.
        /// </summary>
        public static Result<T> Failure(string message, Exception exception) => new(default!, message, exception);

        /// <summary>
        /// Implicit converts data into a Result object. Used for retuning data directly without
        /// the need for returning a Result object. Only to be used for successful operations.
        /// </summary>
        /// <param name="data">Data to be wrapped.</param>
        public static implicit operator Result<T>(T data) => new(data);
    }

    /// <summary>
    /// Represents an indication of the result of an operation, encapsulating success or failure state.
    /// </summary>
    /// <param name="Message">Descriptive message if there is an the error.</param>
    /// <param name="Exception">Exception thrown, used maily for debugging.</param>
    public record Result(string Message = "", Exception? Exception = null) : IResult
    {
        public bool IsSuccess => Exception == null;

        /// <summary>
        /// Method for simplifying the creation of a successful Result.
        /// </summary>
        public static Result Success() => new();

        /// <summary>
        /// Method for simplifying the creation of a failed Result.
        /// </summary>
        public static Result Failure(string message, Exception exception) => new(message, exception);
    }
}