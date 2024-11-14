# Operation Result Pattern

## Purpose
This pattern serves as an alternative to throwing exceptions for handling expected or non-critical errors, 
reducing unnecessary CPU overhead. Instead of relying on exceptions for flow control,
Result<T> allows you to communicate results (data, messages, and exceptions)
in a more structured and efficient way.

## Key Benefits
1. Improved Performance: Throwing exceptions incurs a CPU cost, particularly in high-throughput applications where exceptions can degrade performance. OperationResult<T> avoids this by providing a lightweight way to signal success or failure.
2. Enhanced Readability and Maintainability: Using Result<T> makes code flow more intuitive, as it’s clear when an operation succeeded or failed. Methods return a result directly, making it easier for developers to handle the outcome in a structured manner and avoiding null checks.
3. Better User and Developer Experience: This pattern provides meaningful error messages and structured results, giving both end-users and upstream services clear feedback on what went wrong. 

## Usage
We use this wrapper when we catch an exception, we want to return a successful result or we are handling results from a layer that this as a return type.

**On success**
```C#
return Result<T>.Success(data);
```

**On failure**
```C#
try 
{ ... }
catch(Exception)
{
	return Result<T>.Failure("Error message for end user", exception);
}
```

**When calling a function returning `Result`**
```C#
Result<T> result = _service.DoSomething();
if(!result.IsSuccess)
    return result;

var data = result.Data;
```

## Extra Functionality
When calling a method returning the Result we might want to do a direct return on the result, mostly in a controller.
With the extension method `Resolve` we can easily convert the Result to an IActionResult.
```C#
return _service.DoSomething()
    .Resolve(
        success => Ok(success.Data),
        failure => BadRequest(failure.Message)
	);
```
