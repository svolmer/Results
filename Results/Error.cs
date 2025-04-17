using System.Diagnostics;
using Results.Abstractions;
using Results.Exceptions;

namespace Results
{
    /// <summary>
    ///     Represents an error in the Result pattern, encapsulating error codes, messages, and optional exceptions.
    /// </summary>
    /// <param name="ErrorCode">The categorized error code.</param>
    /// <param name="Message">A human-readable error message describing what went wrong.</param>
    /// <param name="Exception">The optional original exception that caused this error, if applicable.</param>
    /// <remarks>
    ///     <para>
    ///         The <see cref="Error" /> class is a central part of the Results library's error handling mechanism. It
    ///         implements the railway-oriented programming pattern (a functional approach to error handling) by
    ///         providing a structured way to represent and propagate errors.
    ///     </para>
    ///     <para>
    ///         As an implementation of <see cref="IExceptionWrapper{TError}" />, it provides a bridge between
    ///         traditional exception-based error handling and the functional Result pattern by supporting
    ///         bidirectional conversion between <see cref="Exception" /> and <see cref="Error" />.
    ///     </para>
    ///     <para>
    ///         This record is immutable, ensuring thread safety and consistent error state.
    ///     </para>
    /// </remarks>
    [DebuggerStepThrough]
    public record Error(ErrorCode ErrorCode, string Message, Exception? Exception = null) : IExceptionWrapper<Error>
    {
        /// <summary>
        ///     Gets the default error instance with <see cref="ErrorCode.None" /> and an empty message.
        /// </summary>
        /// <value>A minimal error instance representing no specific error.</value>
        /// <remarks>
        ///     This property satisfies the <see cref="IExceptionWrapper{TError}.Default" /> static abstract property
        ///     requirement and provides a standard "no error" representation.
        /// </remarks>
        public static Error Default => new(ErrorCode.None, string.Empty);

        /// <summary>
        ///     Gets a standard error instance representing an unexpected error.
        /// </summary>
        /// <value>An error with <see cref="ErrorCode.Unexpected" /> and a generic message.</value>
        /// <remarks>
        ///     Use this property for errors that don't fit any specific category or when the exact
        ///     cause of an error is unknown.
        /// </remarks>
        public static Error Unexpected => new(ErrorCode.Unexpected, "An unexpected error occurred.");

        /// <summary>
        ///     Creates an error instance from a standard .NET exception.
        /// </summary>
        /// <param name="exception">The exception to wrap.</param>
        /// <returns>
        ///     An error with <see cref="ErrorCode.Exception" />, the exception's message, and the original exception.
        /// </returns>
        /// <example>
        ///     <code>
        /// try
        /// {
        ///     // Code that might throw
        ///     return Result.Success&lt;int, Error&gt;(42);
        /// }
        /// catch (Exception ex)
        /// {
        ///     return Result.Failure&lt;int, Error&gt;(Error.FromException(ex));
        /// }
        /// </code>
        /// </example>
        /// <remarks>
        ///     This method enables seamless integration between traditional exception-based code and the Result pattern
        ///     by converting exceptions to errors.
        /// </remarks>
        public static Error FromException(Exception exception)
        {
            return new Error(ErrorCode.Exception, exception.Message, exception);
        }

        /// <summary>
        ///     Gets a function that wraps exceptions into error instances.
        /// </summary>
        /// <value>A function that converts <see cref="Exception" /> to <see cref="Error" />.</value>
        /// <remarks>
        ///     This property implements the <see cref="IExceptionWrapper{TError}.WrapException" /> requirement and
        ///     provides a function reference to <see cref="FromException" />.
        /// </remarks>
        public static Func<Exception, Error> WrapException => FromException;

        /// <summary>
        ///     Gets a function that unwraps errors back to exceptions.
        /// </summary>
        /// <value>A function that converts <see cref="Error" /> to <see cref="Exception" />.</value>
        /// <remarks>
        ///     This property implements the <see cref="IExceptionWrapper{TError}.UnwrapException" /> requirement and
        ///     provides a function that calls <see cref="ToException" /> on the error instance.
        /// </remarks>
        public static Func<Error, Exception> UnwrapException => error => error.ToException();

        /// <summary>
        ///     Generates a hash code for the current error instance.
        /// </summary>
        /// <returns>A hash code derived from the error code, message, and exception.</returns>
        /// <remarks>
        ///     This override ensures that errors with identical properties will generate the same hash code, which is
        ///     important for proper behavior in collections and comparisons.
        /// </remarks>
        public override int GetHashCode()
        {
            return new {this.ErrorCode, this.Message, this.Exception}.GetHashCode();
        }

        /// <summary>
        ///     Returns a string representation of the current error.
        /// </summary>
        /// <returns>A formatted string describing the error.</returns>
        /// <remarks>
        ///     <para>The format of the string depends on the available information:</para>
        ///     <list type="bullet">
        ///         <item>
        ///             <description>For <see cref="ErrorCode.None" />, returns "Error()"</description>
        ///         </item>
        ///         <item>
        ///             <description>For empty message, returns "Error(ErrorCode)"</description>
        ///         </item>
        ///         <item>
        ///             <description>Otherwise, returns "Error(ErrorCode): Message"</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public override string ToString()
        {
            return this.ErrorCode == ErrorCode.None ? "Error()" :
                string.IsNullOrEmpty(this.Message) ? $"Error({this.ErrorCode})" : $"Error({this.ErrorCode}): {this.Message}";
        }

        /// <summary>
        ///     Converts the error to a .NET exception.
        /// </summary>
        /// <returns>
        ///     The original exception if available; otherwise, a new <see cref="ResultErrorException" /> with the error
        ///     message.
        /// </returns>
        /// <remarks>
        ///     This method enables interoperability with code that expects traditional exception-based error handling.
        ///     It preserves the original exception when possible to maintain the full error context.
        /// </remarks>
        public Exception ToException()
        {
            return this.Exception ?? new ResultErrorException(this.Message);
        }
    }
}