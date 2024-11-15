namespace ResultPattern.src
{
    public static class ResultExtensionsForNonGeneric
    {
        /// <summary>
        /// Unwraps the error message from a <see cref="Result"/> when the operation fails.
        /// Throws a <see cref="ResultException"/> if the result indicates success.
        /// </summary>
        /// <param name="result">The result object to unwrap.</param>
        /// <returns>The error message if the result indicates failure.</returns>
        /// <exception cref="ResultException">Thrown if the result was successful.</exception>
        public static string UnwrapOnFailure(this Result result)
        {
            if (result.IsSuccess)
                throw new ResultException("This method can only be used on failure.");

            return result.Message;
        }

        /// <summary>
        /// Executes the provided action on the result if the operation fails.
        /// </summary>
        /// <param name="result">The result object.</param>
        /// <param name="action">The action to invoke if the result indicates failure.</param>
        /// <exception cref="ResultException">Thrown if the result indicates success.</exception>
        public static void HandleFailure(this Result result, Action<Result> action)
        {
            if (result.IsSuccess)
                throw new ResultException("Operation was successful, cannot handle failure.");

            action.Invoke(result);
        }
    }
}