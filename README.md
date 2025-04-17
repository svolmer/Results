# Results

A modern functional-style error handling library for .NET applications that implements the Result pattern (also known as Either monad in functional programming).

## Overview

Results is a lightweight, zero-dependency library that provides a robust alternative to exception-based error handling through Railway Oriented Programming. It enables clean, predictable error flow and composition of operations that might fail.

## Key Features

- **Railway Oriented Programming** - Handle success and failure paths explicitly without exception throws
- **Immutable Result Types** - Thread-safe, side-effect free result representations
- **Comprehensive LINQ Support** - Query syntax and fluent extension methods
- **Exception Bridging** - Seamless integration with traditional exception-based code
- **Type Safety** - Compile-time error handling enforcement
- **Functional Composition** - Chain operations together elegantly
- **Nullable Reference Type Friendly** - Full support for C# nullable reference types
- **Structured Error Information** - Rich error objects with codes and messages
- **NET 9.0 Ready** - Built for modern .NET development

## Core Concepts

### The Result Pattern

The Result pattern represents operations that can succeed or fail by returning a Result object instead of throwing exceptions. This approach makes error handling explicit and enables functional composition of operations.

Results come in two forms:
- `Result<TError>` - Represents success or failure without a value
- `Result<TValue, TError>` - Represents success with a value or failure with an error

### Railway Oriented Programming

Railway Oriented Programming is a functional approach to error handling that treats success and failure as two parallel tracks. Operations can continue along either track, with appropriate handling for each case.

### Error Handling

The library uses the `Error` record type as the standard error representation, which implements the `IExceptionWrapper<TError>` interface. This allows for structured error information and seamless conversion between Result-based and exception-based code.

## Basic Usage

### Creating Results

```csharp
// Success without value
var success = Result.Success<Error>();

// Success with value
var successWithValue = Result.Success<int, Error>(42);

// Failure with error
var failure = Result.Failure(Error.Unexpected);

// Failure with custom error
var customFailure = Result.Failure(new Error(ErrorCode.None, "Custom error message"));
```

### Checking Result State

```csharp
if (result.IsSuccess)
{
    // Handle success case
}
else
{
    // Handle failure case
    var error = result.GetErrorOrDefault();
    Console.WriteLine($"Operation failed: {error.Message}");
}
```

### Getting Values

```csharp
// Get value or default
int value = result.GetValueOrDefault();

// Get value or throw on failure
int value = result.GetValueOrThrowOnFailure();

// Pattern matching
var displayValue = result.IsSuccess ? result.GetValueOrDefault().ToString() : "No value";
```

### Railway Oriented Programming

```csharp
// Chain operations with LINQ
var result = from value in GetUserResult()
             from permissions in GetPermissionsResult(value)
             from report in GenerateReportResult(value, permissions)
             select report;

// Alternatively, use extension methods
var result = GetUserResult()
    .FlatMap(user => GetPermissionsResult(user))
    .FlatMap(permissions => GenerateReportResult(user, permissions));
```

### Functional Continuation

```csharp
// Continue with different actions based on result state
var message = result.ContinueWith(
    onSuccess: value => $"Operation succeeded with value: {value}",
    onFailure: error => $"Operation failed: {error.Message}"
);
```

### Bridging with Exception-Based Code

```csharp
// Convert exceptions to Results
public Result<User, Error> GetUser(int userId)
{
    try
    {
        var user = repository.GetUser(userId);
        return Result.Success<User, Error>(user);
    }
    catch (Exception ex)
    {
        return Result.Failure<User, Error>(Error.FromException(ex));
    }
}

// Convert Results back to exceptions when needed
public User GetUserOrThrow(int userId)
{
    return GetUser(userId).GetValueOrThrowOnFailure();
}
```

## Advanced Usage

### Custom Error Types

```csharp
public record CustomError(string Code, string Message) : IExceptionWrapper<CustomError>
{
    public static CustomError Default => new("NONE", string.Empty);
    
    public static Func<Exception, CustomError> WrapException => 
        ex => new CustomError("EXCEPTION", ex.Message);
    
    public static Func<CustomError, Exception> UnwrapException => 
        error => new Exception(error.Message);
}
```

### Filtering Results

```csharp
// Only proceed if the value meets a condition
var validResult = result.Filter(value => value > 0);
```

### Working with Collections of Results

```csharp
// Get all values from successful results
var values = results.SelectMany(r => r.Values());

// Get all errors from failed results
var errors = results.SelectMany(r => r.Errors());
```

## Benefits Over Traditional Error Handling

1. **Explicit Error Handling** - No forgotten try/catch blocks
2. **Composable Operations** - Chain operations with clear error flow
3. **No Exception Performance Cost** - Avoid the overhead of throwing exceptions
4. **Better Readability** - Clear distinction between success and failure paths
5. **Comprehensive Error Information** - Structured error data instead of exception messages

## License

MIT
