using Microsoft.AspNetCore.Mvc;

namespace ResultPattern.src
{
    public static class ResultExtention
    {
        /// <summary>
        /// Resolves the result of a given <see cref="Result{T}"/> by executing the appropriate action based on its success or failure state.
        /// This method is commonly used in controllers to return different <see cref="ActionResult"/>s based on the outcome of the result.
        /// </summary>
        /// <typeparam name="T">The type of data contained within the <see cref="Result{T}"/>.</typeparam>
        /// <param name="result">The result object, which indicates whether the operation was successful or not.</param>
        /// <param name="success">A function that takes the <see cref="Result{T}"/> as input and returns a successful <see cref="ActionResult"/> when the operation is successful.</param>
        /// <param name="failure">A function that takes the <see cref="Result{T}"/> as input and returns a failure <see cref="ActionResult"/> when the operation fails.</param>
        /// <returns>An <see cref="ActionResult"/> representing either a successful or failure response based on the outcome of the result.</returns>
        public static ActionResult Resolve<T>(this Result<T> result, Func<Result<T>, ActionResult> success, Func<Result<T>, ActionResult> failure)
            => result.IsSuccess ? success(result) : failure(result);

        /// <summary>
        /// Unwraps the data from a <see cref="Result{T}"/> when the operation is successful.
        /// Throws a <see cref="ResultException"/> if the result indicates failure.
        /// </summary>
        /// <typeparam name="T">The type of data contained within the <see cref="Result{T}"/>.</typeparam>
        /// <param name="result">The result object to unwrap.</param>
        /// <returns>The data of type <typeparamref name="T"/> if the result is successful.</returns>
        /// <exception cref="ResultException">Thrown if the result is not successful.</exception>
        public static T UnwrapOnSuccess<T>(this Result<T> result)
        {
            if (result.IsSuccess)
                return result.Data;

            throw new ResultException("This method can only be used on success.");
        }

        /// <summary>
        /// Unwraps the error message from a <see cref="Result{T}"/> when the operation fails.
        /// Throws a <see cref="ResultException"/> if the result indicates success.
        /// </summary>
        /// <param name="result">The result object to unwrap.</param>
        /// <returns>The error message.</returns>
        /// <exception cref="ResultException">Thrown if the result was successful.</exception>
        public static string UnwrapOnFailure<T>(this Result<T> result)
        {
            if (result.IsSuccess)
                throw new ResultException("This method can only be used on failure.");

            return result.Message;
        }

        /// <summary>
        /// Executes the provided action on the result if the operation is successful.
        /// </summary>
        /// <typeparam name="T">The type of data contained within the <see cref="Result{T}"/>.</typeparam>
        /// <param name="result">The result object.</param>
        /// <param name="action">The action to invoke if the result is successful.</param>
        /// <exception cref="ResultException">Thrown if the result is not successful.</exception>
        public static void HandleSuccess<T>(this Result<T> result, Action<Result<T>> action)
        {
            if (!result.IsSuccess)
                throw new ResultException("Operation was not successful.");

            action.Invoke(result);
        }

        /// <summary>
        /// Executes the provided action on the result if the operation fails.
        /// </summary>
        /// <typeparam name="T">The type of data contained within the <see cref="Result{T}"/>.</typeparam>
        /// <param name="result">The result object.</param>
        /// <param name="action">The action to invoke if the result fails.</param>
        public static void HandleFailure<T>(this Result<T> result, Action<Result<T>> action)
        {
            if (result.IsSuccess)
                throw new ResultException("Operation was successful, cannot handle failure.");

            action.Invoke(result);
        }
    }
}