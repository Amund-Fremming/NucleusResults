using Microsoft.AspNetCore.Mvc;

namespace NucleusResult.src
{
    public static class NucleusResultExtention
    {
        /// <summary>
        /// Resolves the result of a given <see cref="NucleusResult"/> by executing the appropriate action based on its success or failure state.
        /// </summary>
        /// <param name="result">The result object, which indicates whether the operation was successful or not.</param>
        /// <param name="success">A function that takes the <see cref="NucleusResult"/> as input and returns a successful <see cref="NucleusResult"/> when the operation is successful.</param>
        /// <param name="error">A function that takes the <see cref="NucleusResult"/> as input and returns a failure <see cref="NucleusResult"/> when the operation fails.</param>
        /// <returns>An <see cref="NucleusResult"/> representing either a successful or failure response based on the outcome of the result.</returns>
        public static NucleusResult Resolve(this NucleusResult result, Func<NucleusResult, NucleusResult> success, Func<NucleusResult, NucleusResult> error)
            => result.IsError ? error(result) : success(result);

        /// <summary>
        /// Resolves the result of a given <see cref="NucleusResult{T}"/> by executing the appropriate action based on its success or failure state.
        /// </summary>
        /// <param name="result">The result object, which indicates whether the operation was successful or not.</param>
        /// <param name="success">A function that takes the <see cref="NucleusResult"/> as input and returns a successful <see cref="NucleusResult"/> when the operation is successful.</param>
        /// <param name="error">A function that takes the <see cref="NucleusResult"/> as input and returns a failure <see cref="NucleusResult"/> when the operation fails.</param>
        /// <returns>An <see cref="NucleusResult"/> representing either a successful or failure response based on the outcome of the result.</returns>
        public static NucleusResult<T> Resolve<T>(this NucleusResult<T> result, Func<NucleusResult<T>, NucleusResult<T>> success, Func<NucleusResult<T>, NucleusResult<T>> error)
            => result.IsError ? error(result) : success(result);

        /// <summary>
        /// Resolves the result of a given <see cref="NucleusResult{T}"/> by executing the appropriate action based on its success or failure state.
        /// This method is commonly used in controllers to return different <see cref="ActionResult"/>s based on the outcome of the result.
        /// </summary>
        /// <param name="result">The result object, which indicates whether the operation was successful or not.</param>
        /// <param name="success">A function that takes the <see cref="NucleusResult"/> as input and returns a successful <see cref="ActionResult"/> when the operation is successful.</param>
        /// <param name="error">A function that takes the <see cref="NucleusResult"/> as input and returns a failure <see cref="ActionResult"/> when the operation fails.</param>
        /// <returns>An <see cref="ActionResult"/> representing either a successful or failure response based on the outcome of the result.</returns>
        public static ActionResult Resolve(this NucleusResult result, Func<NucleusResult, ActionResult> success, Func<NucleusResult, ActionResult> error)
            => result.IsError ? error(result) : success(result);

        /// <summary>
        /// Resolves the result of a given <see cref="NucleusResult{T}"/> by executing the appropriate action based on its success or failure state.
        /// This method is commonly used in controllers to return different <see cref="ActionResult"/>s based on the outcome of the result.
        /// </summary>
        /// <typeparam name="T">The type of data contained within the <see cref="NucleusResult{T}"/>.</typeparam>
        /// <param name="result">The result object, which indicates whether the operation was successful or not.</param>
        /// <param name="success">A function that takes the <see cref="NucleusResult{T}"/> as input and returns a successful <see cref="ActionResult"/> when the operation is successful.</param>
        /// <param name="error">A function that takes the <see cref="NucleusResult{T}"/> as input and returns a failure <see cref="ActionResult"/> when the operation fails.</param>
        /// <returns>An <see cref="ActionResult"/> representing either a successful or failure response based on the outcome of the result.</returns>
        public static ActionResult Resolve<T>(this NucleusResult<T> result, Func<NucleusResult<T>, ActionResult> success, Func<NucleusResult<T>, ActionResult> error)
            => result.IsError ? error(result) : success(result);

        /// <summary>
        /// Changes type T to type U.
        /// </summary>
        public static NucleusResult<U> ToType<T, U>(this NucleusResult<T> result) => new(default!, result.Error!);

        /// <summary>
        /// Adds type T to the Result.
        /// </summary>
        public static NucleusResult<T> AddType<T>(this NucleusResult result) => new(default!, result.Error!);

        /// <summary>
        /// Removes type T from Result.
        /// </summary>
        public static NucleusResult RemoveType<T>(this NucleusResult<T> result) => new(result.Error);

        /// <summary>
        /// Unwraps the data inside the wrapped result.
        /// </summary>
        public static T UnWrap<T>(this NucleusResult<T> result) => result.Data;
    }
}