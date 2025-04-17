using System.Runtime.CompilerServices;
using Results.Abstractions;
using Results.Exceptions;

namespace Results.Extensions
{
    /// <summary>
    ///     Provides extension methods for working with <see cref="Result{TError}" /> and
    ///     <see cref="Result{TValue, TError}" /> types.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This class contains various utility methods for extracting values and errors from results, converting
    ///         between results and exceptions, and transforming results in different ways.
    ///     </para>
    ///     <para>
    ///         These extensions make working with the Result pattern more convenient by providing common operations
    ///         for handling success and failure cases.
    ///     </para>
    /// </remarks>
    public static class ResultExtensions
    {
        /// <summary>
        ///     Gets the error from a failed result, or a default value if the result is successful.
        /// </summary>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The result to get the error from.</param>
        /// <param name="defaultValue">
        ///     The default value to return if the result is successful. If null,
        ///     <see cref="IExceptionWrapper{TError}.Default" /> is used.
        /// </param>
        /// <returns>
        ///     The error from a failed result, or the specified default value (or
        ///     <see cref="IExceptionWrapper{TError}.Default" />) if the result is successful.
        /// </returns>
        public static TError GetErrorOrDefault<TError>(this Result<TError> self, TError? defaultValue = null)
            where TError : class, IExceptionWrapper<TError>
        {
            return !self.IsSuccess ? self.Error : defaultValue ?? TError.Default;
        }

        /// <summary>
        ///     Gets the error from a failed result, or a default value if the result is successful.
        /// </summary>
        /// <typeparam name="TValue">The type of the value in the success case.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The result to get the error from.</param>
        /// <param name="defaultValue">
        ///     The default value to return if the result is successful. If null,
        ///     <see cref="IExceptionWrapper{TError}.Default" /> is used.
        /// </param>
        /// <returns>
        ///     The error from a failed result, or the specified default value (or
        ///     <see cref="IExceptionWrapper{TError}.Default" />) if the result is successful.
        /// </returns>
        public static TError GetErrorOrDefault<TValue, TError>(this Result<TValue, TError> self, TError? defaultValue = null)
            where TError : class, IExceptionWrapper<TError>
        {
            return !self.IsSuccess ? self.Error : defaultValue ?? TError.Default;
        }

        /// <summary>
        ///     Throws an exception if the result is a failure.
        /// </summary>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The result to check.</param>
        /// <param name="exception">
        ///     The exception to throw if the result is a failure. If null, the error is unwrapped to an exception.
        /// </param>
        /// <exception cref="Exception">
        ///     Thrown if the result is a failure, with the provided exception or an unwrapped exception from the error.
        /// </exception>
        public static void ThrowOnFailure<TError>(this Result<TError> self, Exception? exception = null)
            where TError : class, IExceptionWrapper<TError>
        {
            if (!self.IsSuccess) throw exception ?? TError.UnwrapException(self.Error);
        }

        /// <summary>
        ///     Gets the value from a successful result with a value, or a default value otherwise.
        /// </summary>
        /// <typeparam name="TValue">The type of the value in the success case.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The result to get the value from.</param>
        /// <param name="defaultValue">
        ///     The default value to return if the result is a failure or a success without a value.
        /// </param>
        /// <returns>
        ///     The value from a successful result with a value, or the specified default value otherwise.
        /// </returns>
        public static TValue GetValueOrDefault<TValue, TError>(this Result<TValue, TError> self, TValue defaultValue = default!)
            where TError : class, IExceptionWrapper<TError>
        {
            return self is {IsSuccess: true, HasValue: true} ? self.Value : defaultValue;
        }

        /// <summary>
        ///     Gets the value from a successful result with a value, or throws an exception otherwise.
        /// </summary>
        /// <typeparam name="TValue">The type of the value in the success case.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The result to get the value from.</param>
        /// <param name="exception">
        ///     The exception to throw if the result is a failure or a success without a value. If null, an appropriate
        ///     exception is created.
        /// </param>
        /// <param name="callerName">The name of the caller expression, automatically provided by the compiler.</param>
        /// <returns>The value from a successful result with a value.</returns>
        /// <exception cref="Exception">
        ///     Thrown if the result is a failure (with the provided exception or an unwrapped exception from the error)
        ///     or a success without a value (with the provided exception or a <see cref="ResultValueException" />).
        /// </exception>
        public static TValue GetValueOrThrowOnFailure<TValue, TError>(this Result<TValue, TError> self, Exception? exception = null,
            [CallerArgumentExpression(nameof(self))]
            string callerName = "")
            where TError : class, IExceptionWrapper<TError>
        {
            if (self.IsSuccess)
            {
                if (self.HasValue) return self.Value;

                throw exception ?? new ResultValueException($"'{callerName}' has no value.");
            }

            throw exception ?? TError.UnwrapException(self.Error);
        }

        /// <summary>
        ///     Returns an enumerable that yields the value from a successful result with a value, or an empty enumerable
        ///     otherwise.
        /// </summary>
        /// <typeparam name="TValue">The type of the value in the success case.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The result to get the value from.</param>
        /// <returns>
        ///     An enumerable with the value if the result is a successful result with a value; otherwise, an empty
        ///     enumerable.
        /// </returns>
        /// <remarks>
        ///     This method is useful for working with collections of results, allowing you to easily collect all
        ///     successful values.
        /// </remarks>
        public static IEnumerable<TValue> Values<TValue, TError>(this Result<TValue, TError> self)
            where TError : class, IExceptionWrapper<TError>
        {
            if (self.HasValue) yield return self.Value;
        }

        /// <summary>
        ///     Returns an enumerable that yields the error from a failed result, or an empty enumerable if the result
        ///     is successful.
        /// </summary>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The result to get the error from.</param>
        /// <returns>An enumerable with the error if the result is a failure; otherwise, an empty enumerable.</returns>
        /// <remarks>
        ///     This method is useful for working with collections of results, allowing you to easily collect all
        ///     errors.
        /// </remarks>
        public static IEnumerable<TError> Errors<TError>(this Result<TError> self)
            where TError : class, IExceptionWrapper<TError>
        {
            if (!self.IsSuccess) yield return self.Error;
        }

        /// <summary>
        ///     Returns an enumerable that yields the error from a failed result, or an empty enumerable if the result
        ///     is successful.
        /// </summary>
        /// <typeparam name="TValue">The type of the value in the success case.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The result to get the error from.</param>
        /// <returns>An enumerable with the error if the result is a failure; otherwise, an empty enumerable.</returns>
        /// <remarks>
        ///     This method is useful for working with collections of results, allowing you to easily collect all
        ///     errors.
        /// </remarks>
        public static IEnumerable<TError> Errors<TValue, TError>(this Result<TValue, TError> self)
            where TError : class, IExceptionWrapper<TError>
        {
            if (!self.IsSuccess) yield return self.Error;
        }

        /// <summary>
        ///     Converts a value to a successful result with that value.
        /// </summary>
        /// <typeparam name="TValue">The type of the value to convert.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns>A successful result containing the specified value.</returns>
        public static Result<TValue, TError> ToResult<TValue, TError>(this TValue value)
            where TError : class, IExceptionWrapper<TError>
        {
            return Result.Success<TValue, TError>(value);
        }

        /// <summary>
        ///     Converts an error to a failed result with that error.
        /// </summary>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="error">The error to convert.</param>
        /// <returns>A failed result containing the specified error.</returns>
        public static Result<TError> ToResult<TError>(this TError error)
            where TError : class, IExceptionWrapper<TError>
        {
            return Result.Failure(error);
        }

        /// <summary>
        ///     Converts an error to a failed result with that error and a specified value type.
        /// </summary>
        /// <typeparam name="TValue">
        ///     The type of the value that would be contained in a successful result.
        /// </typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="error">The error to convert.</param>
        /// <returns>A failed result containing the specified error and value type.</returns>
        public static Result<TValue, TError> ToResult<TValue, TError>(this TError error)
            where TError : class, IExceptionWrapper<TError>
        {
            return Result.Failure<TValue, TError>(error);
        }

        /// <summary>
        ///     Flattens a nested result, converting <c>Result&lt;Result&lt;TError&gt;, TError&gt;</c> to
        ///     <c>Result&lt;TError&gt;</c>.
        /// </summary>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The nested result to flatten.</param>
        /// <returns>
        ///     The inner result if the outer result is successful; otherwise, a failed result with the error from the
        ///     outer result.
        /// </returns>
        public static Result<TError> Flatten<TError>(this Result<Result<TError>, TError> self)
            where TError : class, IExceptionWrapper<TError>
        {
            return self.FlatMap(result => result);
        }

        /// <summary>
        ///     Flattens a nested result, converting <c>Result&lt;Result&lt;TValue, TError&gt;, TError&gt;</c> to
        ///     <c>Result&lt;TValue, TError&gt;</c>.
        /// </summary>
        /// <typeparam name="TValue">The type of the value in the success case of the inner result.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The nested result to flatten.</param>
        /// <returns>
        ///     The inner result if the outer result is successful; otherwise, a failed result with the error from the
        ///     outer result.
        /// </returns>
        public static Result<TValue, TError> Flatten<TValue, TError>(this Result<Result<TValue, TError>, TError> self)
            where TError : class, IExceptionWrapper<TError>
        {
            return self.FlatMap(result => result);
        }

        /// <summary>
        ///     Maps the success case to a new result by applying the specified action.
        /// </summary>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The result to map.</param>
        /// <param name="selector">The action to apply in the success case.</param>
        /// <returns>
        ///     A new result representing the success or failure of applying the action. If the current result is a
        ///     failure, the failure is propagated.
        /// </returns>
        /// <remarks>
        ///     This is an alias for <see cref="Result{TError}.Map(Action)" /> to maintain compatibility with LINQ
        ///     method names.
        /// </remarks>
        public static Result<TError> Select<TError>(this Result<TError> self, Action selector)
            where TError : class, IExceptionWrapper<TError>
        {
            return self.Map(selector);
        }

        /// <summary>
        ///     Maps the success case with a value to a new result by applying the specified action.
        /// </summary>
        /// <typeparam name="TValue">The type of the value in the source result.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The result to map.</param>
        /// <param name="selector">The action to apply in the success case with a value.</param>
        /// <returns>
        ///     A new result representing the success or failure of applying the action. If the current result is a
        ///     failure or a success without a value, the result is propagated.
        /// </returns>
        /// <remarks>
        ///     This is an alias for <see cref="Result{TValue, TError}.Map(Action{TValue})" /> to maintain compatibility
        ///     with LINQ method names.
        /// </remarks>
        public static Result<TError> Select<TValue, TError>(this Result<TValue, TError> self, Action<TValue> selector)
            where TError : class, IExceptionWrapper<TError>
        {
            return self.Map(selector);
        }
    }
}