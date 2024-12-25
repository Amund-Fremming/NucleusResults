namespace NucleusResults.Core
{
    /// <summary>
    /// Represents the result of an operation, encapsulating success or failure state,
    /// optional data, a message, and an exception if applicable.
    /// </summary>
    /// <param name="Data">Generic data on sucess.</param>
    /// <param name="Message">Descriptive message if there is an the error.</param>
    /// <param name="Exception">Exception thrown, used maily for debugging.</param>
    public record NucleusResult<T>(T Data, Error Error) : INucleusResult, INucleusResult<T>
    {
        /// <summary>
        /// Indicates if the operation failed or not.
        /// </summary>
        public bool IsError => Error is not null && Error.Exception is not null;

        /// <summary>
        /// Used for getting the error message.
        /// </summary>
        public string Message => Error!.Message;

        /// <summary>
        /// Method for simplifying the creation of a successful Result.
        /// </summary>
        public static NucleusResult<T> Ok(T data) => new(data, null!);

        /// <summary>
        /// Implicit converts data into a successful Result object. Used for retuning data directly without
        /// the need for returning a Result object.
        /// </summary>
        /// <param name="data">Data to be wrapped.</param>
        public static implicit operator NucleusResult<T>(T data) => new(data, null!);

        /// <summary>
        /// Implicit converts error into a failed Result object. Used for retuning error directly without
        /// the need for returning a Result object.
        /// </summary>
        /// <param name="error">Error to be wrapped.</param>
        public static implicit operator NucleusResult<T>(Error error) => new(default!, error);
    }

    /// <summary>
    /// Represents an indication of the result of an operation, encapsulating success or failure state.
    /// </summary>
    /// <param name="Error">Error that occurred.</param>
    public record NucleusResult(Error Error) : INucleusResult
    {
        public bool IsError => Error is not null && Error.Exception is not null;

        /// <summary>
        /// Used for getting the error message.
        /// </summary>
        public string Message => Error!.Message;

        /// <summary>
        /// Method for simplifying the creation of a successful Result.
        /// </summary>
        public static NucleusResult Ok() => new(Error: null!);

        /// <summary>
        /// Implicit converts error into a failed Result object. Used for retuning error directly without
        /// the need for returning a Result object.
        /// </summary>
        /// <param name="error">Error to be wrapped.</param>
        public static implicit operator NucleusResult(Error error) => new(error);

        public static NucleusResult operator &(NucleusResult left, NucleusResult right)
        {
            if (left.IsError)
                return left;

            return right;
        }
    }
}