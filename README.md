# Result Pattern

## Purpose
This pattern serves as an alternative to throwing exceptions for handling expected or non-critical errors, 
reducing unnecessary CPU overhead. Instead of relying on exceptions for flow control, Result<T> allows you
to communicate results (data, messages, and exceptions) in a more structured and efficient way.

## Key Benefits
1. Improved Performance: Throwing exceptions incurs a CPU cost, particularly in high-throughput applications where exceptions can degrade performance. Result<T> avoids this by providing a lightweight way to signal success or failure.
2. Enhanced Readability and Maintainability: Using Result<T> makes code flow more intuitive, as it’s clear when an operation succeeded or failed. Methods return a result directly, making it easier for developers to handle the outcome in a structured manner and avoiding null checks.
3. Better User and Developer Experience: This pattern provides meaningful error messages and structured results, giving both end-users and upstream services clear feedback on what went wrong. 

## Usage
We use this wrapper when we catch an exception, we want to return a successful result or we are handling results from a layer under that has Result<T> as a return type.

### Using Result<T> as return type

#### On success
With implicit operators, we dont need to wrap the data in a Result<T> object. 
```C#
public Result<T> DoSomething()
{
	T data = GetData();
	return data;
}
```

#### On failure
```C#
try 
{ ... }
catch(Exception ex)
{
	_logger.LogError(ex, "MethodName: Some additional information.");
	return Result<T>.Failure("Error message for end user", ex);
}
```

### Receiving Result<T> as a return from a method 

#### Basic return handling
```C#
Result<T> result = _service.DoSomething();
if(!result.IsSuccess)
    return result;

var data = result.UnwrapOnSuccess();
```
```C#
Result<T> result = await _service.DoSomething();
if(!result.IsSuccess)
    return result;

var data = result.Data;
```

#### Handling without returns
```C
result.HandleFailure(result =>
{
    // Handle the failure
    Exception e = result.Exception;
    string message = result.Message;
});

result.HandleSuccess(success =>
{
    // Handle the success
    var data = result.Data;
});
```

### Returning ActionResults in controllers
```C
public async Task<IActionResult> GetSomething()
{
    Result<string> result = await _service.GetSomething();
    return result.Resolve(s => Ok(s.Message), f => BadRequest(f.Message));
}
```
