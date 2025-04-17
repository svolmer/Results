using Results.Abstractions;

namespace Results.Extensions
{
    /// <summary>
    ///     Provides extension methods for delegates to safely execute them and capture exceptions as
    ///     <see cref="Result{TError}" /> objects.
    /// </summary>
    /// <remarks>
    ///     This class contains internal helper methods that convert traditional exception-throwing delegates to
    ///     railway-oriented functions that return Results. These methods are used internally by the mapping operations
    ///     of the Result types.
    /// </remarks>
    internal static class DelegateExtensions
    {
        /// <summary>
        ///     Executes an action and returns a success result if no exception is thrown, or a failure result containing
        ///     the exception otherwise.
        /// </summary>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The action to execute.</param>
        /// <returns>
        ///     A success result if the action executes without throwing; otherwise, a failure result with the wrapped
        ///     exception.
        /// </returns>
        internal static Result<TError> Execute<TError>(this Action self)
            where TError : class, IExceptionWrapper<TError>
        {
            try
            {
                self();
                return Result.Success<TError>();
            }
            catch (Exception exception)
            {
                return Result.Failure(TError.WrapException(exception));
            }
        }

        /// <summary>
        ///     Executes an action with a parameter and returns a success result if no exception is thrown, or a failure
        ///     result containing the exception otherwise.
        /// </summary>
        /// <typeparam name="TValue">The type of the parameter for the action.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The action to execute.</param>
        /// <param name="value">The value to pass to the action.</param>
        /// <returns>
        ///     A success result if the action executes without throwing; otherwise, a failure result with the wrapped
        ///     exception.
        /// </returns>
        internal static Result<TError> Execute<TValue, TError>(this Action<TValue> self, TValue value)
            where TError : class, IExceptionWrapper<TError>
        {
            try
            {
                self(value);
                return Result.Success<TError>();
            }
            catch (Exception exception)
            {
                return Result.Failure(TError.WrapException(exception));
            }
        }

        /// <summary>
        ///     Executes a function and returns a success result with its return value if no exception is thrown, or a
        ///     failure result containing the exception otherwise.
        /// </summary>
        /// <typeparam name="TResult">The return type of the function.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The function to execute.</param>
        /// <returns>
        ///     A success result with the function's return value if it executes without throwing; otherwise, a failure
        ///     result with the wrapped exception.
        /// </returns>
        internal static Result<TResult, TError> Execute<TResult, TError>(this Func<TResult> self)
            where TError : class, IExceptionWrapper<TError>
        {
            try
            {
                return Result.Success<TResult, TError>(self());
            }
            catch (Exception exception)
            {
                return Result.Failure<TResult, TError>(TError.WrapException(exception));
            }
        }

        /// <summary>
        ///     Executes a function with a parameter and returns a success result with its return value if no exception
        ///     is thrown, or a failure result containing the exception otherwise.
        /// </summary>
        /// <typeparam name="TValue">The type of the parameter for the function.</typeparam>
        /// <typeparam name="TResult">The return type of the function.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The function to execute.</param>
        /// <param name="value">The value to pass to the function.</param>
        /// <returns>
        ///     A success result with the function's return value if it executes without throwing; otherwise, a failure
        ///     result with the wrapped exception.
        /// </returns>
        internal static Result<TResult, TError> Execute<TValue, TResult, TError>(this Func<TValue, TResult> self, TValue value)
            where TError : class, IExceptionWrapper<TError>
        {
            try
            {
                return Result.Success<TResult, TError>(self(value));
            }
            catch (Exception exception)
            {
                return Result.Failure<TResult, TError>(TError.WrapException(exception));
            }
        }
    }
}