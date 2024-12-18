using Microsoft.AspNetCore.Mvc;

namespace WrapResults.src
{
    public static class WrapResultExtention
    {
        /// <summary>
        /// Resolves the result of a given <see cref="WrapResult"/> by executing the appropriate action based on its success or failure state.
        /// </summary>
        /// <param name="result">The result object, which indicates whether the operation was successful or not.</param>
        /// <param name="success">A function that takes the <see cref="WrapResult"/> as input and returns a successful <see cref="WrapResult"/> when the operation is successful.</param>
        /// <param name="error">A function that takes the <see cref="WrapResult"/> as input and returns a failure <see cref="WrapResult"/> when the operation fails.</param>
        /// <returns>An <see cref="WrapResult"/> representing either a successful or failure response based on the outcome of the result.</returns>
        public static WrapResult Resolve(this WrapResult result, Func<WrapResult, WrapResult> success, Func<WrapResult, WrapResult> error)
            => result.IsError ? error(result) : success(result);

        /// <summary>
        /// Resolves the result of a given <see cref="WrapResult{T}"/> by executing the appropriate action based on its success or failure state.
        /// </summary>
        /// <param name="result">The result object, which indicates whether the operation was successful or not.</param>
        /// <param name="success">A function that takes the <see cref="WrapResult"/> as input and returns a successful <see cref="WrapResult"/> when the operation is successful.</param>
        /// <param name="error">A function that takes the <see cref="WrapResult"/> as input and returns a failure <see cref="WrapResult"/> when the operation fails.</param>
        /// <returns>An <see cref="WrapResult"/> representing either a successful or failure response based on the outcome of the result.</returns>
        public static WrapResult<T> Resolve<T>(this WrapResult<T> result, Func<WrapResult<T>, WrapResult<T>> success, Func<WrapResult<T>, WrapResult<T>> error)
            => result.IsError ? error(result) : success(result);

        /// <summary>
        /// Resolves the result of a given <see cref="WrapResult{T}"/> by executing the appropriate action based on its success or failure state.
        /// This method is commonly used in controllers to return different <see cref="ActionResult"/>s based on the outcome of the result.
        /// </summary>
        /// <param name="result">The result object, which indicates whether the operation was successful or not.</param>
        /// <param name="success">A function that takes the <see cref="WrapResult"/> as input and returns a successful <see cref="ActionResult"/> when the operation is successful.</param>
        /// <param name="error">A function that takes the <see cref="WrapResult"/> as input and returns a failure <see cref="ActionResult"/> when the operation fails.</param>
        /// <returns>An <see cref="ActionResult"/> representing either a successful or failure response based on the outcome of the result.</returns>
        public static ActionResult Resolve(this WrapResult result, Func<WrapResult, ActionResult> success, Func<WrapResult, ActionResult> error)
            => result.IsError ? error(result) : success(result);

        /// <summary>
        /// Resolves the result of a given <see cref="WrapResult{T}"/> by executing the appropriate action based on its success or failure state.
        /// This method is commonly used in controllers to return different <see cref="ActionResult"/>s based on the outcome of the result.
        /// </summary>
        /// <typeparam name="T">The type of data contained within the <see cref="WrapResult{T}"/>.</typeparam>
        /// <param name="result">The result object, which indicates whether the operation was successful or not.</param>
        /// <param name="success">A function that takes the <see cref="WrapResult{T}"/> as input and returns a successful <see cref="ActionResult"/> when the operation is successful.</param>
        /// <param name="error">A function that takes the <see cref="WrapResult{T}"/> as input and returns a failure <see cref="ActionResult"/> when the operation fails.</param>
        /// <returns>An <see cref="ActionResult"/> representing either a successful or failure response based on the outcome of the result.</returns>
        public static ActionResult Resolve<T>(this WrapResult<T> result, Func<WrapResult<T>, ActionResult> success, Func<WrapResult<T>, ActionResult> error)
            => result.IsError ? error(result) : success(result);

        /// <summary>
        /// Changes type T to type U.
        /// </summary>
        public static WrapResult<U> ToType<T, U>(this WrapResult<T> result) => new(default!, result.Error!);

        /// <summary>
        /// Adds type T to the Result.
        /// </summary>
        public static WrapResult<T> AddType<T>(this WrapResult result) => new(default!, result.Error!);

        /// <summary>
        /// Removes type T from Result.
        /// </summary>
        public static WrapResult RemoveType<T>(this WrapResult<T> result) => new(result.Error);

        /// <summary>
        /// Unwraps the data inside the wrapped result.
        /// </summary>
        public static T UnWrap<T>(this WrapResult<T> result) => result.Data;
    }
}