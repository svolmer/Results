namespace Results.Abstractions
{
    /// <summary>
    ///     Defines a contract for error types that can wrap and unwrap exceptions.
    /// </summary>
    /// <typeparam name="TError">The specific error type that implements this interface.</typeparam>
    /// <remarks>
    ///     <para>
    ///         This interface serves as the core abstraction for error handling in the Results library. It enables
    ///         bidirectional conversion between traditional exceptions and structured error objects.
    ///     </para>
    ///     <para>
    ///         Implementations of this interface bridge the gap between exception-based and railway-oriented error
    ///         handling approaches, allowing seamless integration between different coding styles.
    ///     </para>
    ///     <para>
    ///         The interface uses static abstract members (C# 11+ feature) to define class-level functionality
    ///         required for error types.
    ///     </para>
    /// </remarks>
    public interface IExceptionWrapper<TError>
        where TError : class
    {
        /// <summary>
        ///     Gets a default instance of the error type representing the absence of a specific error.
        /// </summary>
        /// <value>A minimal error instance that can be used as a default or "no error" placeholder.</value>
        /// <remarks>
        ///     This should typically return an instance with minimal or empty properties, suitable for use when no
        ///     specific error information is available.
        /// </remarks>
        static abstract TError Default { get; }

        /// <summary>
        ///     Gets a function that converts exceptions to the error type.
        /// </summary>
        /// <value>A function that takes an <see cref="Exception" /> and returns a <typeparamref name="TError" />.</value>
        /// <remarks>
        ///     This function enables integration with exception-based code by providing a standardized way to convert
        ///     exceptions to structured errors.
        /// </remarks>
        static abstract Func<Exception, TError> WrapException { get; }

        /// <summary>
        ///     Gets a function that converts the error type back to exceptions.
        /// </summary>
        /// <value>A function that takes a <typeparamref name="TError" /> and returns an <see cref="Exception" />.</value>
        /// <remarks>
        ///     This function enables integration with code that expects exceptions by providing a standardized way to
        ///     convert structured errors back to exceptions.
        /// </remarks>
        static abstract Func<TError, Exception> UnwrapException { get; }
    }
}