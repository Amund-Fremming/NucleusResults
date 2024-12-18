# NucleusResults 

## Purpose
This pattern serves as an alternative to throwing exceptions for handling expected or non-critical errors, 
reducing unnecessary CPU overhead. Instead of relying on exceptions for flow control, NucleusResult<T> allows you
to communicate results (data, messages, and exceptions) in a more structured and efficient way.

## Key Benefits
1. Improved Performance: Throwing exceptions incurs a CPU cost, particularly in high-throughput applications where exceptions can degrade performance. NucleusResult<T> avoids this by providing a lightweight way to signal success or failure.
2. Enhanced Readability and Maintainability: Using NucleusResult<T> makes code flow more intuitive, as it’s clear when an operation succeeded or failed. Methods return a result directly, making it easier for developers to handle the outcome in a structured manner and avoiding null checks.
3. Better User and Developer Experience: This pattern provides meaningful error messages and structured results, giving both end-users and upstream services clear feedback on what went wrong. 

## Usage

### Success Case
```C#
public NucleusResult<Game> CreateGame()
{
	var game = new Game();
	return game;
}
````

### Failure Case
```C#
public NucleusResult<Game> CreateGame()
{
	try { ... }
	catch(Exception ex)
	{
		var error = new Error(ex, "Failed to create game");
		return error;
	}
}
```

### Handling Results

**Direct return, can be Error or Success**
```C#
public NucleusResult<SomeOtherObject> OuterMethod()
{
	var result = InnerMethod();
	return result;
}
```

**When called from outer class**
```C#
public NucleusResult<SomeOtherObject> OuterMethod()
{
	var result = InnerMethod();
	if (result.IsError)
		return result.Error;
	
	var game = result.UnWrap();
	...
	(Do something more)
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

### Cases where the type T needs to change**
```C#
public NucleusResult<Alfa> DoSomething()
{
	NucleusResult<Beta> result = InnerMethod();
	if (result.IsError)
		return result.Error;
	
	return result.ToType<Beta, Alfa>();	
}
```
```C#
public NucleusResult DoSomething()
{
	NucleusResult<Beta> result = InnerMethod();
	if (result.IsError)
		return result.Error;
	
	return result.RemoveType();	
}
```
```C#
public NucleusResult<Alfa> DoSomething()
{
	NucleusResult result = InnerMethod();
	if (result.IsError)
		return result.Error;
	
	return result.AddType<Alfa>();	
}
```
