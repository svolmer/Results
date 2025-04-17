using System.Diagnostics;

namespace Results.Exceptions
{
    /// <summary>
    ///     Represents an exception that is thrown when a <see cref="Results.Error" /> is converted back to an exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <remarks>
    ///     <para>
    ///         This exception is used when converting an <see cref="Results.Error" /> to an exception via the
    ///         <see cref="Results.Error.ToException" /> method, when the original exception is not available.
    ///     </para>
    ///     <para>
    ///         The exception includes the error message from the original <see cref="Results.Error" />.
    ///     </para>
    /// </remarks>
    [DebuggerStepThrough]
    public class ResultErrorException(string message) : Exception(message);
}