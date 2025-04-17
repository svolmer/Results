using System.Diagnostics;

namespace Results.Exceptions
{
    /// <summary>
    ///     Represents an exception that is thrown when attempting to access a value from a
    ///     <see cref="Results.Result{TValue, TError}" /> that doesn't have a value.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <remarks>
    ///     <para>
    ///         This exception is typically thrown by methods like
    ///         <see cref="Results.Extensions.ResultExtensions.GetValueOrThrowOnFailure{TValue, TError}" /> when
    ///         attempting to get a value from a successful result that doesn't contain a value.
    ///     </para>
    ///     <para>
    ///         It's used to distinguish between a failure result (which would throw the appropriate error exception)
    ///         and a successful result that simply doesn't have a value.
    ///     </para>
    /// </remarks>
    [DebuggerStepThrough]
    public class ResultValueException(string message) : Exception(message);
}