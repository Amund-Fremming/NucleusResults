using Microsoft.AspNetCore.Mvc;

namespace ResultPattern
{
    /// <summary>
    /// Resolves an <see cref="OperationResult{T}"/> by executing the appropriate function based on success or failure,
    /// and returns the corresponding HTTP response result.
    /// </summary>
    /// <typeparam name="T">The type of data held by the <see cref="OperationResult{T}"/>.</typeparam>
    /// <param name="result">The <see cref="OperationResult{T}"/> instance to resolve.</param>
    /// <param name="onSuccess">The function to execute if the operation was successful. This function should return an <see cref="IActionResult"/>.</param>
    /// <param name="onFailure">The function to execute if the operation failed. This function should return an <see cref="IActionResult"/>.</param>
    /// <returns>The result of the <paramref name="onSuccess"/> or <paramref name="onFailure"/> function, based on the outcome of the operation.</returns>
    public static class OperationResultExtention
    {
        public static IActionResult Resolve<T>(this Result<T> result, Func<Result<T>, IActionResult> success, Func<Result<T>, IActionResult> failure)
            => result.IsSuccess ? success(result)! : failure(result);

        public static ActionResult ToActionResult<T>(this Result<T> result)
            => result.IsSuccess ? new OkObjectResult(result.Data) : new BadRequestObjectResult(result.Message);
    }
}