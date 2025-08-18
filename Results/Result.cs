using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Results.Abstractions;
using Results.Extensions;

namespace Results
{
    /// <summary>
    ///     Provides factory methods for creating <see cref="Result{TError}" /> and <see cref="Result{TValue, TError}" />
    ///     instances.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This class is the entry point for the Railway Oriented Programming pattern in this library. It offers
    ///         methods to create results representing both success and failure outcomes.
    ///     </para>
    ///     <para>
    ///         Railway Oriented Programming is a functional approach to error handling that treats success and failure
    ///         as two parallel tracks. Operations can continue along either track, with appropriate handling for each
    ///         case.
    ///     </para>
    /// </remarks>
    [DebuggerStepThrough]
    public static class Result
    {
        /// <summary>
        ///     Creates a successful result without a value.
        /// </summary>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <returns>A successful result without a value.</returns>
        public static Result<TError> Success<TError>() where TError : class, IExceptionWrapper<TError>
        {
            return new Result<TError>();
        }

        /// <summary>
        ///     Creates a failed result with the specified error.
        /// </summary>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="error">The error that describes the failure.</param>
        /// <returns>A failed result containing the specified error.</returns>
        public static Result<TError> Failure<TError>(TError error) where TError : class, IExceptionWrapper<TError>
        {
            return new Result<TError>(error);
        }

        /// <summary>
        ///     Creates a successful result with the specified value.
        /// </summary>
        /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="value">The value to be contained in the successful result.</param>
        /// <returns>A successful result containing the specified value.</returns>
        public static Result<TValue, TError> Success<TValue, TError>(TValue value) where TError : class, IExceptionWrapper<TError>
        {
            return new Result<TValue, TError>(value);
        }

        /// <summary>
        ///     Creates a failed result with the specified error.
        /// </summary>
        /// <typeparam name="TValue">
        ///     The type of the value that would be contained in a successful result.
        /// </typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="error">The error that describes the failure.</param>
        /// <returns>A failed result containing the specified error.</returns>
        public static Result<TValue, TError> Failure<TValue, TError>(TError error) where TError : class, IExceptionWrapper<TError>
        {
            return new Result<TValue, TError>(error);
        }
    }

    /// <summary>
    ///     Represents the outcome of an operation that can either succeed or fail, without returning a value.
    /// </summary>
    /// <typeparam name="TError">
    ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
    /// </typeparam>
    /// <remarks>
    ///     <para>
    ///         This struct implements the Result pattern for operations that don't return a meaningful value but can
    ///         fail. It's useful for void-returning methods that need to communicate success or failure with
    ///         structured error information.
    ///     </para>
    ///     <para>
    ///         Results are immutable and provide value semantics, meaning two results with the same state are
    ///         considered equal.
    ///     </para>
    /// </remarks>
    [DebuggerStepThrough]
    public class Result<TError> : IEquatable<Result<TError>>, IComparable<Result<TError>>, IComparable
        where TError : class, IExceptionWrapper<TError>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Result{TError}" /> struct representing a success.
        /// </summary>
        internal Result()
        {
            this.IsSuccess = true;
            this.Error = null;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Result{TError}" /> struct representing a failure.
        /// </summary>
        /// <param name="error">The error describing the failure.</param>
        internal Result(TError error)
        {
            this.IsSuccess = false;
            this.Error = error;
        }

        /// <summary>
        ///     Determines whether two results are equal.
        /// </summary>
        /// <param name="left">The first result to compare.</param>
        /// <param name="right">The second result to compare.</param>
        /// <returns><c>true</c> if the results are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Result<TError> left, Result<TError> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Determines whether two results are not equal.
        /// </summary>
        /// <param name="left">The first result to compare.</param>
        /// <param name="right">The second result to compare.</param>
        /// <returns><c>true</c> if the results are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Result<TError> left, Result<TError> right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        ///     Determines whether the first result is less than the second result.
        /// </summary>
        /// <param name="left">The first result to compare.</param>
        /// <param name="right">The second result to compare.</param>
        /// <returns>
        ///     <c>true</c> if the first result is less than the second result; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <(Result<TError> left, Result<TError> right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        ///     Determines whether the first result is less than or equal to the second result.
        /// </summary>
        /// <param name="left">The first result to compare.</param>
        /// <param name="right">The second result to compare.</param>
        /// <returns>
        ///     <c>true</c> if the first result is less than or equal to the second result; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <=(Result<TError> left, Result<TError> right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        ///     Determines whether the first result is greater than the second result.
        /// </summary>
        /// <param name="left">The first result to compare.</param>
        /// <param name="right">The second result to compare.</param>
        /// <returns>
        ///     <c>true</c> if the first result is greater than the second result; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >(Result<TError> left, Result<TError> right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        ///     Determines whether the first result is greater than or equal to the second result.
        /// </summary>
        /// <param name="left">The first result to compare.</param>
        /// <param name="right">The second result to compare.</param>
        /// <returns>
        ///     <c>true</c> if the first result is greater than or equal to the second result; otherwise,
        ///     <c>false</c>.
        /// </returns>
        public static bool operator >=(Result<TError> left, Result<TError> right)
        {
            return left.CompareTo(right) >= 0;
        }

        /// <summary>
        ///     Gets a value indicating whether the operation was successful.
        /// </summary>
        /// <value><c>true</c> if the operation was successful; otherwise, <c>false</c>.</value>
        /// <remarks>
        ///     When this property is <c>false</c>, the <see cref="Error" /> property will contain an error describing
        ///     the failure.
        /// </remarks>
        [MemberNotNullWhen(false, nameof(Error))]
        public bool IsSuccess { get; }

        /// <summary>
        ///     Compares the current result with another object and returns an integer that indicates whether the
        ///     current result precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this result.</param>
        /// <returns>
        ///     A value that indicates the relative order of the objects being compared. The return value has these
        ///     meanings:
        ///     <list type="table">
        ///         <item>
        ///             <term>Less than zero</term>
        ///             <description>This result precedes <paramref name="obj" /> in the sort order.</description>
        ///         </item>
        ///         <item>
        ///             <term>Zero</term>
        ///             <description>
        ///                 This result occurs in the same position in the sort order as <paramref name="obj" />.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>Greater than zero</term>
        ///             <description>This result follows <paramref name="obj" /> in the sort order.</description>
        ///         </item>
        ///     </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="obj" /> is not the same type as this instance.
        /// </exception>
        public int CompareTo(object? obj)
        {
            if (obj is null) return 1;

            return obj is Result<TError> other
                ? this.CompareTo(other)
                : throw new ArgumentException("Object is not the same type as this instance.", nameof(obj));
        }

        /// <summary>
        ///     Compares the current result with another result and returns an integer that indicates whether the
        ///     current result precedes, follows, or occurs in the same position in the sort order as the other result.
        /// </summary>
        /// <param name="other">A result to compare with this result.</param>
        /// <returns>
        ///     A value that indicates the relative order of the objects being compared. The return value has these
        ///     meanings:
        ///     <list type="table">
        ///         <item>
        ///             <term>Less than zero</term>
        ///             <description>This result precedes <paramref name="other" /> in the sort order.</description>
        ///         </item>
        ///         <item>
        ///             <term>Zero</term>
        ///             <description>
        ///                 This result occurs in the same position in the sort order as <paramref name="other" />.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>Greater than zero</term>
        ///             <description>This result follows <paramref name="other" /> in the sort order.</description>
        ///         </item>
        ///     </list>
        /// </returns>
        /// <remarks>
        ///     <para>Successful results are greater than failed results.</para>
        ///     <para>Failed results are compared by the hash codes of their errors.</para>
        /// </remarks>
        public int CompareTo(Result<TError>? other)
        {
            if (other is null) return 1;
            if (this.IsSuccess && !other.IsSuccess) return 1;
            if (!this.IsSuccess && other.IsSuccess) return -1;
            if (!this.IsSuccess && !other.IsSuccess) return this.Error.GetHashCode().CompareTo(other.Error.GetHashCode());

            return 0;
        }

        /// <summary>
        ///     Determines whether the specified result is equal to the current result.
        /// </summary>
        /// <param name="other">The result to compare with the current result.</param>
        /// <returns><c>true</c> if the specified result is equal to the current result; otherwise, <c>false</c>.</returns>
        /// <remarks>
        ///     <para>Two successful results are always equal.</para>
        ///     <para>Two failed results are equal if their errors are equal.</para>
        ///     <para>A successful result and a failed result are never equal.</para>
        /// </remarks>
        public bool Equals(Result<TError>? other)
        {
            if (other is null) return false;
            if (this.IsSuccess && other.IsSuccess) return true;
            if (!this.IsSuccess && !other.IsSuccess) return EqualityComparer<TError>.Default.Equals(this.Error, other.Error);

            return false;
        }

        /// <summary>
        ///     Determines whether the specified object is equal to the current result.
        /// </summary>
        /// <param name="obj">The object to compare with the current result.</param>
        /// <returns><c>true</c> if the specified object is equal to the current result; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            return obj is Result<TError> result && this.Equals(result);
        }

        /// <summary>
        ///     Returns a hash code for the current result.
        /// </summary>
        /// <returns>A hash code for the current result.</returns>
        /// <remarks>
        ///     <para>For successful results, the hash code is 0.</para>
        ///     <para>For failed results, the hash code is the hash code of the error.</para>
        /// </remarks>
        public override int GetHashCode()
        {
            return this.IsSuccess ? 0 : this.Error.GetHashCode();
        }

        /// <summary>
        ///     Returns a string representation of the current result.
        /// </summary>
        /// <returns>A string representation of the current result.</returns>
        /// <remarks>
        ///     <para>For successful results, the string is "Success()".</para>
        ///     <para>
        ///         For failed results, the string is "Failure(error)" where error is the string representation of the
        ///         error.
        ///     </para>
        /// </remarks>
        public override string ToString()
        {
            return this.IsSuccess ? "Success()" : $"Failure({this.Error})";
        }

        /// <summary>
        ///     Gets the error describing the failure, or <c>null</c> if the operation was successful.
        /// </summary>
        /// <value>The error describing the failure, or <c>null</c> if the operation was successful.</value>
        internal TError? Error { get; }

        /// <summary>
        ///     Maps the success case to a new result by applying the specified action.
        /// </summary>
        /// <param name="mapping">The action to apply in the success case.</param>
        /// <returns>
        ///     A new result representing the success or failure of applying the action. If the current result is a
        ///     failure, the failure is propagated.
        /// </returns>
        internal Result<TError> Map(Action mapping)
        {
            return this.Match(
                mapping.Execute<TError>,
                Result.Failure
            );
        }

        /// <summary>
        ///     Maps the success case to a new result with a value by applying the specified function.
        /// </summary>
        /// <typeparam name="TResult">The type of the value in the resulting success case.</typeparam>
        /// <param name="mapping">The function to apply in the success case.</param>
        /// <returns>
        ///     A new result representing the success or failure of applying the function. If the current result is a
        ///     failure, the failure is propagated.
        /// </returns>
        internal Result<TResult, TError> Map<TResult>(Func<TResult> mapping)
        {
            return this.Match(
                mapping.Execute<TResult, TError>,
                Result.Failure<TResult, TError>
            );
        }

        /// <summary>
        ///     Flat-maps the success case to a new result by applying the specified function.
        /// </summary>
        /// <typeparam name="TResult">The type of the value in the resulting success case.</typeparam>
        /// <param name="mapping">The function to apply in the success case, which itself returns a result.</param>
        /// <returns>
        ///     The result of applying the function in the success case, or a failure if the current result is a
        ///     failure.
        /// </returns>
        internal Result<TResult, TError> FlatMap<TResult>(Func<Result<TResult, TError>> mapping)
        {
            return this.Match(
                mapping,
                Result.Failure<TResult, TError>
            );
        }

        /// <summary>
        ///     Pattern matches on the current result, applying the appropriate function based on the result state.
        /// </summary>
        /// <typeparam name="TResult">The type of the result of the functions.</typeparam>
        /// <param name="onSuccess">The function to apply in the success case.</param>
        /// <param name="onFailure">The function to apply in the failure case.</param>
        /// <returns>The result of applying the appropriate function.</returns>
        private TResult Match<TResult>(Func<TResult> onSuccess, Func<TError, TResult> onFailure)
        {
            return this.IsSuccess ? onSuccess() : onFailure(this.Error);
        }
    }

    /// <summary>
    ///     Represents the outcome of an operation that can either succeed with a value or fail.
    /// </summary>
    /// <typeparam name="TValue">The type of the value in the success case.</typeparam>
    /// <typeparam name="TError">
    ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
    /// </typeparam>
    /// <remarks>
    ///     <para>
    ///         This struct implements the Result pattern for operations that return a value but can fail. It
    ///         represents one of three possible states:
    ///         <list type="bullet">
    ///             <item>
    ///                 <description>Success with a value</description>
    ///             </item>
    ///             <item>
    ///                 <description>Success without a value</description>
    ///             </item>
    ///             <item>
    ///                 <description>Failure with an error</description>
    ///             </item>
    ///         </list>
    ///     </para>
    ///     <para>
    ///         Results are immutable and provide value semantics, meaning two results with the same state are
    ///         considered equal.
    ///     </para>
    /// </remarks>
    [DebuggerStepThrough]
    public class Result<TValue, TError> : IEquatable<Result<TValue, TError>>, IComparable<Result<TValue, TError>>, IComparable
        where TError : class, IExceptionWrapper<TError>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Result{TValue, TError}" /> struct representing a success
        ///     with a value.
        /// </summary>
        /// <param name="value">The value representing the successful result.</param>
        internal Result(TValue value)
        {
            this.IsSuccess = true;
            this.Value = value;
            this.Error = null;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Result{TValue, TError}" /> struct representing a failure.
        /// </summary>
        /// <param name="error">The error describing the failure.</param>
        internal Result(TError error)
        {
            this.IsSuccess = false;
            this.Value = default;
            this.Error = error;
        }

        /// <summary>
        ///     Determines whether two results are equal.
        /// </summary>
        /// <param name="left">The first result to compare.</param>
        /// <param name="right">The second result to compare.</param>
        /// <returns><c>true</c> if the results are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Result<TValue, TError> left, Result<TValue, TError> right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Determines whether two results are not equal.
        /// </summary>
        /// <param name="left">The first result to compare.</param>
        /// <param name="right">The second result to compare.</param>
        /// <returns><c>true</c> if the results are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Result<TValue, TError> left, Result<TValue, TError> right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        ///     Determines whether the first result is less than the second result.
        /// </summary>
        /// <param name="left">The first result to compare.</param>
        /// <param name="right">The second result to compare.</param>
        /// <returns>
        ///     <c>true</c> if the first result is less than the second result; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <(Result<TValue, TError> left, Result<TValue, TError> right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        ///     Determines whether the first result is less than or equal to the second result.
        /// </summary>
        /// <param name="left">The first result to compare.</param>
        /// <param name="right">The second result to compare.</param>
        /// <returns>
        ///     <c>true</c> if the first result is less than or equal to the second result; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator <=(Result<TValue, TError> left, Result<TValue, TError> right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        ///     Determines whether the first result is greater than the second result.
        /// </summary>
        /// <param name="left">The first result to compare.</param>
        /// <param name="right">The second result to compare.</param>
        /// <returns>
        ///     <c>true</c> if the first result is greater than the second result; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator >(Result<TValue, TError> left, Result<TValue, TError> right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        ///     Determines whether the first result is greater than or equal to the second result.
        /// </summary>
        /// <param name="left">The first result to compare.</param>
        /// <param name="right">The second result to compare.</param>
        /// <returns>
        ///     <c>true</c> if the first result is greater than or equal to the second result; otherwise,
        ///     <c>false</c>.
        /// </returns>
        public static bool operator >=(Result<TValue, TError> left, Result<TValue, TError> right)
        {
            return left.CompareTo(right) >= 0;
        }

        /// <summary>
        ///     Gets a value indicating whether the operation was successful.
        /// </summary>
        /// <value><c>true</c> if the operation was successful; otherwise, <c>false</c>.</value>
        /// <remarks>
        ///     When this property is <c>false</c>, the <see cref="Error" /> property will contain an error describing
        ///     the failure.
        /// </remarks>
        [MemberNotNullWhen(false, nameof(Error))]
        [MemberNotNullWhen(true, nameof(Value))]
        public bool IsSuccess { get; }

        /// <summary>
        ///     Compares the current result with another object and returns an integer that indicates whether the
        ///     current result precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this result.</param>
        /// <returns>
        ///     A value that indicates the relative order of the objects being compared. The return value has these
        ///     meanings:
        ///     <list type="table">
        ///         <item>
        ///             <term>Less than zero</term>
        ///             <description>This result precedes <paramref name="obj" /> in the sort order.</description>
        ///         </item>
        ///         <item>
        ///             <term>Zero</term>
        ///             <description>
        ///                 This result occurs in the same position in the sort order as <paramref name="obj" />.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>Greater than zero</term>
        ///             <description>This result follows <paramref name="obj" /> in the sort order.</description>
        ///         </item>
        ///     </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="obj" /> is not the same type as this instance.
        /// </exception>
        public int CompareTo(object? obj)
        {
            if (obj is null) return 1;

            return obj is Result<TValue, TError> other
                ? this.CompareTo(other)
                : throw new ArgumentException("Object is not the same type as this instance.", nameof(obj));
        }

        /// <summary>
        ///     Compares the current result with another result and returns an integer that indicates whether the
        ///     current result precedes, follows, or occurs in the same position in the sort order as the other result.
        /// </summary>
        /// <param name="other">A result to compare with this result.</param>
        /// <returns>
        ///     A value that indicates the relative order of the objects being compared. The return value has these
        ///     meanings:
        ///     <list type="table">
        ///         <item>
        ///             <term>Less than zero</term>
        ///             <description>This result precedes <paramref name="other" /> in the sort order.</description>
        ///         </item>
        ///         <item>
        ///             <term>Zero</term>
        ///             <description>
        ///                 This result occurs in the same position in the sort order as <paramref name="other" />.
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <term>Greater than zero</term>
        ///             <description>This result follows <paramref name="other" /> in the sort order.</description>
        ///         </item>
        ///     </list>
        /// </returns>
        /// <remarks>
        ///     <para>Successful results are greater than failed results.</para>
        ///     <para>Failed results are compared by the hash codes of their errors.</para>
        ///     <para>Successful results with values are greater than successful results without values.</para>
        ///     <para>Successful results with values are compared by the values themselves.</para>
        /// </remarks>
        public int CompareTo(Result<TValue, TError>? other)
        {
            if (other is null) return 1;
            if (this.IsSuccess && !other.IsSuccess) return 1;
            if (!this.IsSuccess && other.IsSuccess) return -1;
            if (!this.IsSuccess && !other.IsSuccess) return this.Error.GetHashCode().CompareTo(other.Error.GetHashCode());

            return Comparer<TValue>.Default.Compare(this.Value, other.Value);
        }

        /// <summary>
        ///     Determines whether the specified result is equal to the current result.
        /// </summary>
        /// <param name="other">The result to compare with the current result.</param>
        /// <returns><c>true</c> if the specified result is equal to the current result; otherwise, <c>false</c>.</returns>
        /// <remarks>
        ///     <para>Two successful results without values are equal.</para>
        ///     <para>Two successful results with values are equal if their values are equal.</para>
        ///     <para>Two failed results are equal if their errors are equal.</para>
        ///     <para>A successful result and a failed result are never equal.</para>
        ///     <para>A successful result with a value and a successful result without a value are never equal.</para>
        /// </remarks>
        public bool Equals(Result<TValue, TError>? other)
        {
            if (other is null) return false;
            if (this.IsSuccess && !other.IsSuccess) return false;
            if (!this.IsSuccess && other.IsSuccess) return false;
            if (!this.IsSuccess && !other.IsSuccess) return EqualityComparer<TError>.Default.Equals(this.Error, other.Error);

            return EqualityComparer<TValue>.Default.Equals(this.Value, other.Value);
        }

        /// <summary>
        ///     Determines whether the specified object is equal to the current result.
        /// </summary>
        /// <param name="obj">The object to compare with the current result.</param>
        /// <returns><c>true</c> if the specified object is equal to the current result; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            return obj is Result<TValue, TError> result && this.Equals(result);
        }

        /// <summary>
        ///     Returns a hash code for the current result.
        /// </summary>
        /// <returns>A hash code for the current result.</returns>
        /// <remarks>
        ///     <para>For successful results with values, the hash code is the hash code of the value.</para>
        ///     <para>For successful results without values, the hash code is 0.</para>
        ///     <para>For failed results, the hash code is the hash code of the error.</para>
        /// </remarks>
        public override int GetHashCode()
        {
            return this.IsSuccess ? this.Value.GetHashCode() : this.Error.GetHashCode();
        }

        /// <summary>
        ///     Returns a string representation of the current result.
        /// </summary>
        /// <returns>A string representation of the current result.</returns>
        /// <remarks>
        ///     <para>
        ///         For successful results with values, the string is "Success(value)" where value is the string
        ///         representation of the value.
        ///     </para>
        ///     <para>For successful results with null values, the string is "Success(null)".</para>
        ///     <para>For successful results without values, the string is "Success()".</para>
        ///     <para>
        ///         For failed results, the string is "Failure(error)" where error is the string representation of the
        ///         error.
        ///     </para>
        /// </remarks>
        public override string ToString()
        {
            return this.IsSuccess ? this.Value is null ? "Success(null)" : $"Success({this.Value})" : $"Failure({this.Error})";
        }

        /// <summary>
        ///     Gets the value contained in the result, or <c>null</c> if the result does not have a value.
        /// </summary>
        /// <value>The value contained in the result, or <c>null</c> if the result does not have a value.</value>
        internal TValue? Value { get; }

        /// <summary>
        ///     Gets the error describing the failure, or <c>null</c> if the operation was successful.
        /// </summary>
        /// <value>The error describing the failure, or <c>null</c> if the operation was successful.</value>
        internal TError? Error { get; }

        /// <summary>
        ///     Maps the success case with a value to a new result by applying the specified action.
        /// </summary>
        /// <param name="mapping">The action to apply in the success case with a value.</param>
        /// <returns>
        ///     A new result representing the success or failure of applying the action. If the current result is a
        ///     failure or a success without a value, the result is propagated.
        /// </returns>
        internal Result<TError> Map(Action<TValue> mapping)
        {
            return this.Match(
                mapping.Execute<TValue, TError>,
                Result.Failure
            );
        }

        /// <summary>
        ///     Maps the success case with a value to a new result with a different value by applying the specified
        ///     function.
        /// </summary>
        /// <typeparam name="TResult">The type of the value in the resulting success case.</typeparam>
        /// <param name="mapping">The function to apply in the success case with a value.</param>
        /// <returns>
        ///     A new result representing the success or failure of applying the function. If the current result is a
        ///     failure or a success without a value, the result is propagated.
        /// </returns>
        internal Result<TResult, TError> Map<TResult>(Func<TValue, TResult> mapping)
        {
            return this.Match(
                mapping.Execute<TValue, TResult, TError>,
                Result.Failure<TResult, TError>
            );
        }

        /// <summary>
        ///     Flat-maps the success case with a value to a new result by applying the specified function.
        /// </summary>
        /// <param name="mapping">
        ///     The function to apply in the success case with a value, which itself returns a result.
        /// </param>
        /// <returns>
        ///     The result of applying the function in the success case with a value, or a propagated result otherwise.
        /// </returns>
        internal Result<TError> FlatMap(Func<TValue, Result<TError>> mapping)
        {
            return this.Match(
                mapping,
                Result.Failure
            );
        }

        /// <summary>
        ///     Flat-maps the success case with a value to a new result with a different value by applying the specified
        ///     function.
        /// </summary>
        /// <typeparam name="TResult">The type of the value in the resulting success case.</typeparam>
        /// <param name="mapping">
        ///     The function to apply in the success case with a value, which itself returns a result.
        /// </param>
        /// <returns>
        ///     The result of applying the function in the success case with a value, or a propagated result otherwise.
        /// </returns>
        internal Result<TResult, TError> FlatMap<TResult>(Func<TValue, Result<TResult, TError>> mapping)
        {
            return this.Match(
                mapping,
                Result.Failure<TResult, TError>
            );
        }

        /// <summary>
        ///     Pattern matches on the current result, applying the appropriate function based on the result state.
        /// </summary>
        /// <typeparam name="TResult">The type of the result of the functions.</typeparam>
        /// <param name="onSuccess">The function to apply in the success case with a value.</param>
        /// <param name="onFailure">The function to apply in the failure case.</param>
        /// <returns>The result of applying the appropriate function.</returns>
        private TResult Match<TResult>(Func<TValue, TResult> onSuccess, Func<TError, TResult> onFailure)
        {
            return this.IsSuccess ? onSuccess(this.Value) : onFailure(this.Error);
        }
    }
}