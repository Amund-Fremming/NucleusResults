# SimpleResults 

## Purpose
This pattern serves as an alternative to throwing exceptions for handling expected or non-critical errors, 
reducing unnecessary CPU overhead. Instead of relying on exceptions for flow control, SimpleResult<T> allows you
to communicate results (data, messages, and exceptions) in a more structured and efficient way.

## Key Benefits
1. Improved Performance: Throwing exceptions incurs a CPU cost, particularly in high-throughput applications where exceptions can degrade performance. SimpleResult<T> avoids this by providing a lightweight way to signal success or failure.
2. Enhanced Readability and Maintainability: Using SimpleResult<T> makes code flow more intuitive, as it’s clear when an operation succeeded or failed. Methods return a result directly, making it easier for developers to handle the outcome in a structured manner and avoiding null checks.
3. Better User and Developer Experience: This pattern provides meaningful error messages and structured results, giving both end-users and upstream services clear feedback on what went wrong. 

## Usage

### Success Case
```C#
public SimpleResult<Game> CreateGame()
{
	var game = new Game();
	return game;
}
````

### Failure Case
```C#
public SimpleResult<Game> CreateGame()
{
	var error = new Error("Failed to create game");
	return error;
}
```

### Handling Results

**When called from outer class**
```C#
public SimpleResult<SomeOtherObject> OuterMethod()
{
	var result = InnerMethod();
	if (result.IsError)
		return result.Error;
	
	var game = result.Data;
	...
}
```

**When called from controller**
```C#
public IActionResult CreateGame()
{
	var result = CreateGame();
	return result.Resolve(
		success => Ok(result.Data),
		error => BadRequest(result.Message));
}
```

**Cases where the type T needs to change**
```C#
public SimpleResult<Alfa> DoSomething()
{
	SimpleResult<Beta> result = InnerMethod();
	if (result.IsError)
		return result.Error;
	
	return result.ToType<Beta, Alfa>();	
}
```
```C#
public Result DoSomething()
{
	Result<Beta> result = InnerMethod();
	if (result.IsError)
		return result.Error;
	
	return result.RemoveType();	
}
```
```C#
public Result<Alfa> DoSomething()
{
	Result result = InnerMethod();
	if (result.IsError)
		return result.Error;
	
	return result.AddType<Alfa>();	
}
```
