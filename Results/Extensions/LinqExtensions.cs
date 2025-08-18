using System.Diagnostics;
using Results.Abstractions;

namespace Results.Extensions
{
    /// <summary>
    ///     Provides LINQ extension methods for Result types to enable query syntax and functional composition.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         These extension methods allow Result types to be used with LINQ query syntax and method chaining. They
    ///         bridge the gap between the Result pattern and LINQ, enabling functional composition of operations that
    ///         might fail.
    ///     </para>
    ///     <para>
    ///         The implementation follows the LINQ monad pattern, mapping the core Result operations to their LINQ
    ///         equivalents: Select (Map), SelectMany (FlatMap), and Where (Filter).
    ///     </para>
    /// </remarks>
    [DebuggerStepThrough]
    public static class LinqExtensions
    {
        /// <summary>
        ///     Projects each result in a sequence into a new form by applying a selector function to the success case.
        /// </summary>
        /// <typeparam name="TResult">The type of the value in the resulting success case.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The sequence of results to project.</param>
        /// <param name="selector">A transform function to apply to the success case of each result.</param>
        /// <returns>
        ///     A sequence of results representing the success or failure of applying the selector function to each result.
        ///     If any input result is a failure, the failure is propagated.
        /// </returns>
        /// <remarks>
        ///     This method enables LINQ query syntax for sequences of results without values.
        /// </remarks>
        public static IEnumerable<Result<TResult, TError>> Select<TResult, TError>(this IEnumerable<Result<TError>> self,
            Func<TResult> selector)
            where TError : class, IExceptionWrapper<TError>
        {
            return self.Select(result => result.Map(selector)).ToList();
        }

        /// <summary>
        ///     Projects each result in a sequence into a new form by applying a selector function to the success case with a value.
        /// </summary>
        /// <typeparam name="TSource">The type of the value in the source results.</typeparam>
        /// <typeparam name="TResult">The type of the value in the resulting success case.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The sequence of results to project.</param>
        /// <param name="selector">A transform function to apply to the success case with a value of each result.</param>
        /// <returns>
        ///     A sequence of results representing the success or failure of applying the selector function to each result.
        ///     If any input result is a failure or a success without a value, the result is propagated.
        /// </returns>
        /// <remarks>
        ///     This method enables LINQ query syntax for sequences of results with values.
        /// </remarks>
        public static IEnumerable<Result<TResult, TError>> Select<TSource, TResult, TError>(this IEnumerable<Result<TSource, TError>> self,
            Func<TSource, TResult> selector)
            where TError : class, IExceptionWrapper<TError>
        {
            return self.Select(result => result.Map(selector)).ToList();
        }

        /// <summary>
        ///     Projects each result in a sequence into a new form by applying a selector function that returns a result.
        /// </summary>
        /// <typeparam name="TResult">The type of the value in the resulting success case.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The sequence of results to project.</param>
        /// <param name="selector">A transform function to apply to the success case of each result, which returns a result.</param>
        /// <returns>
        ///     A sequence of results from applying the selector function to each success case, or propagated failures for
        ///     each input failure.
        /// </returns>
        /// <remarks>
        ///     This method enables LINQ query syntax with multiple from clauses for sequences of results without values.
        /// </remarks>
        public static IEnumerable<Result<TResult, TError>> SelectMany<TResult, TError>(this IEnumerable<Result<TError>> self,
            Func<Result<TResult, TError>> selector)
            where TError : class, IExceptionWrapper<TError>
        {
            return self.Select(result => result.FlatMap(selector)).ToList();
        }

        /// <summary>
        ///     Projects each result in a sequence into a new form by applying a selector function that returns a result.
        /// </summary>
        /// <typeparam name="TSource">The type of the value in the source results.</typeparam>
        /// <typeparam name="TResult">The type of the value in the resulting success case.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The sequence of results to project.</param>
        /// <param name="selector">
        ///     A transform function to apply to the success case with a value of each result, which returns a result.
        /// </param>
        /// <returns>
        ///     A sequence of results from applying the selector function to each success case with a value, or propagated
        ///     results for each input failure or success without a value.
        /// </returns>
        /// <remarks>
        ///     This method enables LINQ query syntax with multiple from clauses for sequences of results with values.
        /// </remarks>
        public static IEnumerable<Result<TResult, TError>> Select<TSource, TResult, TError>(this IEnumerable<Result<TSource, TError>> self,
            Func<TSource, Result<TResult, TError>> selector)
            where TError : class, IExceptionWrapper<TError>
        {
            return self.Select(result => result.FlatMap(selector)).ToList();
        }

        /// <summary>
        ///     Projects each result in a sequence into a new form by applying a two-part selector function.
        /// </summary>
        /// <typeparam name="TCollection">The type of the intermediate value.</typeparam>
        /// <typeparam name="TResult">The type of the value in the resulting success case.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The sequence of results to project.</param>
        /// <param name="collectionSelector">
        ///     A transform function to apply to the success case of each result, which returns a result with a collection.
        /// </param>
        /// <param name="resultSelector">A transform function to apply to each element of the collection.</param>
        /// <returns>
        ///     A sequence of results representing the success or failure of applying both selector functions to each result.
        ///     If any input result is a failure, the failure is propagated.
        /// </returns>
        /// <remarks>
        ///     This method enables LINQ query syntax with let clauses and multiple from clauses for sequences of results
        ///     without values.
        /// </remarks>
        public static IEnumerable<Result<TResult, TError>> SelectMany<TCollection, TResult, TError>(this IEnumerable<Result<TError>> self,
            Func<Result<TCollection, TError>> collectionSelector,
            Func<TCollection, TResult> resultSelector)
            where TError : class, IExceptionWrapper<TError>
        {
            return self.Select(result => result.FlatMap(() => collectionSelector().Map(resultSelector))).ToList();
        }

        /// <summary>
        ///     Projects each result in a sequence into a new form by applying a two-part selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of the value in the source results.</typeparam>
        /// <typeparam name="TCollection">The type of the intermediate value.</typeparam>
        /// <typeparam name="TResult">The type of the value in the resulting success case.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The sequence of results to project.</param>
        /// <param name="collectionSelector">
        ///     A transform function to apply to the success case with a value of each result, which returns a result with
        ///     a collection.
        /// </param>
        /// <param name="resultSelector">
        ///     A transform function to apply to the source value and each element of the collection.
        /// </param>
        /// <returns>
        ///     A sequence of results representing the success or failure of applying both selector functions to each result.
        ///     If any input result is a failure, the failure is propagated.
        /// </returns>
        /// <remarks>
        ///     This method enables LINQ query syntax with let clauses and multiple from clauses for sequences of results
        ///     with values.
        /// </remarks>
        public static IEnumerable<Result<TResult, TError>> SelectMany<TSource, TCollection, TResult, TError>(this IEnumerable<Result<TSource, TError>> self,
            Func<TSource, Result<TCollection, TError>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
            where TError : class, IExceptionWrapper<TError>
        {
            return self.Select(result => result.FlatMap(source => collectionSelector(source).Map(item => resultSelector(source, item)))).ToList();
        }

        /// <summary>
        ///     Filters each result in a sequence based on a predicate.
        /// </summary>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The sequence of results to filter.</param>
        /// <param name="predicate">A function to test if the success case of each result meets a condition.</param>
        /// <returns>
        ///     A sequence where each input result is included if it's a failure or if the predicate returns true;
        ///     otherwise, a new success result without a value is included.
        /// </returns>
        /// <remarks>
        ///     This method enables LINQ query syntax with where clauses for sequences of results without values.
        /// </remarks>
        public static IEnumerable<Result<TError>> Where<TError>(this IEnumerable<Result<TError>> self,
            Func<bool> predicate)
            where TError : class, IExceptionWrapper<TError>
        {
            return self.Where(result => result.IsSuccess && predicate()).ToList();
        }

        /// <summary>
        ///     Filters each result in a sequence based on a predicate applied to its value.
        /// </summary>
        /// <typeparam name="TSource">The type of the value in the source results.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The sequence of results to filter.</param>
        /// <param name="predicate">A function to test if the value of each result meets a condition.</param>
        /// <returns>
        ///     A sequence where each input result is included if it's a failure, a success without a value, or a success
        ///     with a value where the predicate returns true; otherwise, a new success result without a value is included.
        /// </returns>
        /// <remarks>
        ///     This method enables LINQ query syntax with where clauses for sequences of results with values.
        /// </remarks>
        public static IEnumerable<Result<TSource, TError>> Where<TSource, TError>(this IEnumerable<Result<TSource, TError>> self,
            Func<TSource, bool> predicate)
            where TError : class, IExceptionWrapper<TError>
        {
            return self.Where(result => result.IsSuccess && predicate(result.Value)).ToList();
        }

        /// <summary>
        ///     Projects a result into a new form by applying a selector function to the success case.
        /// </summary>
        /// <typeparam name="TResult">The type of the value in the resulting success case.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The result to project.</param>
        /// <param name="selector">A transform function to apply to the success case.</param>
        /// <returns>
        ///     A new result representing the success or failure of applying the selector function. If the input result
        ///     is a failure, the failure is propagated.
        /// </returns>
        /// <remarks>
        ///     This method enables LINQ query syntax for results without values.
        /// </remarks>
        public static Result<TResult, TError> Select<TResult, TError>(this Result<TError> self, Func<TResult> selector)
            where TError : class, IExceptionWrapper<TError>
        {
            return self.Map(selector);
        }

        /// <summary>
        ///     Projects a result into a new form by applying a selector function to the success case with a value.
        /// </summary>
        /// <typeparam name="TSource">The type of the value in the source result.</typeparam>
        /// <typeparam name="TResult">The type of the value in the resulting success case.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The result to project.</param>
        /// <param name="selector">A transform function to apply to the success case with a value.</param>
        /// <returns>
        ///     A new result representing the success or failure of applying the selector function. If the input result
        ///     is a failure or a success without a value, the result is propagated.
        /// </returns>
        /// <remarks>
        ///     This method enables LINQ query syntax for results with values.
        /// </remarks>
        public static Result<TResult, TError> Select<TSource, TResult, TError>(this Result<TSource, TError> self, Func<TSource, TResult> selector)
            where TError : class, IExceptionWrapper<TError>
        {
            return self.Map(selector);
        }

        /// <summary>
        ///     Projects a result into a new form by applying a selector function that returns a result.
        /// </summary>
        /// <typeparam name="TResult">The type of the value in the resulting success case.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The result to project.</param>
        /// <param name="selector">A transform function to apply to the success case, which returns a result.</param>
        /// <returns>
        ///     The result of applying the selector function in the success case, or a failure if the input result is a
        ///     failure.
        /// </returns>
        /// <remarks>
        ///     This method enables LINQ query syntax with multiple from clauses for results without values.
        /// </remarks>
        public static Result<TResult, TError> SelectMany<TResult, TError>(this Result<TError> self, Func<Result<TResult, TError>> selector)
            where TError : class, IExceptionWrapper<TError>
        {
            return self.FlatMap(selector);
        }

        /// <summary>
        ///     Projects a result into a new form by applying a selector function that returns a result.
        /// </summary>
        /// <typeparam name="TSource">The type of the value in the source result.</typeparam>
        /// <typeparam name="TResult">The type of the value in the resulting success case.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The result to project.</param>
        /// <param name="selector">
        ///     A transform function to apply to the success case with a value, which returns a result.
        /// </param>
        /// <returns>
        ///     The result of applying the selector function in the success case with a value, or a propagated result
        ///     otherwise.
        /// </returns>
        /// <remarks>
        ///     This method enables LINQ query syntax with multiple from clauses for results with values.
        /// </remarks>
        public static Result<TResult, TError> SelectMany<TSource, TResult, TError>(this Result<TSource, TError> self,
            Func<TSource, Result<TResult, TError>> selector)
            where TError : class, IExceptionWrapper<TError>
        {
            return self.FlatMap(selector);
        }

        /// <summary>
        ///     Projects each result in a sequence into a new form by applying a two-part selector function.
        /// </summary>
        /// <typeparam name="TCollection">The type of the intermediate value.</typeparam>
        /// <typeparam name="TResult">The type of the value in the resulting success case.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The sequence of results to project.</param>
        /// <param name="collectionSelector">
        ///     A transform function to apply to the success case, which returns a result with a collection.
        /// </param>
        /// <param name="resultSelector">A transform function to apply to each element of the collection.</param>
        /// <returns>
        ///     A sequence of results representing the success or failure of applying both selector functions. If any input result
        ///     is a failure, the failure is propagated.
        /// </returns>
        /// <remarks>
        ///     This method enables LINQ query syntax with let clauses and multiple from clauses for sequences of results
        ///     without values.
        /// </remarks>
        public static Result<TResult, TError> SelectMany<TCollection, TResult, TError>(this Result<TError> self,
            Func<Result<TCollection, TError>> collectionSelector,
            Func<TCollection, TResult> resultSelector)
            where TError : class, IExceptionWrapper<TError>
        {
            return self.FlatMap(() => collectionSelector().Map(resultSelector));
        }

        /// <summary>
        ///     Projects each result in a sequence into a new form by applying a two-part selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of the value in the source results.</typeparam>
        /// <typeparam name="TCollection">The type of the intermediate value.</typeparam>
        /// <typeparam name="TResult">The type of the value in the resulting success case.</typeparam>
        /// <typeparam name="TError">
        ///     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        /// </typeparam>
        /// <param name="self">The sequence of results to project.</param>
        /// <param name="collectionSelector">
        ///     A transform function to apply to the success case with a value of each result, which returns a result with
        ///     a collection.
        /// </param>
        /// <param name="resultSelector">
        ///     A transform function to apply to the source value and each element of the collection.
        /// </param>
        /// <returns>
        ///     A sequence of results representing the success or failure of applying both selector functions to each result.
        ///     If any input result is a failure, the failure is propagated.
        /// </returns>
        /// <remarks>
        ///     This method enables LINQ query syntax with let clauses and multiple from clauses for sequences of results
        ///     with values.
        /// </remarks>
        public static Result<TResult, TError> SelectMany<TSource, TCollection, TResult, TError>(this Result<TSource, TError> self,
            Func<TSource, Result<TCollection, TError>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
            where TError : class, IExceptionWrapper<TError>
        {
            return self.FlatMap(source => collectionSelector(source).Map(item => resultSelector(source, item)));
        }

        ///// <summary>
        /////     Filters a result based on a predicate.
        ///// </summary>
        ///// <typeparam name="TError">
        /////     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        ///// </typeparam>
        ///// <param name="self">The result to filter.</param>
        ///// <param name="predicate">A function to test if the success case meets a condition.</param>
        ///// <returns>
        /////     The input result if it's a failure or if the predicate returns true; otherwise, a new success result
        /////     without a value.
        ///// </returns>
        ///// <remarks>
        /////     This method enables LINQ query syntax with where clauses for results without values.
        ///// </remarks>
        //public static Result<TError> Where<TError>(this Result<TError> self, Func<bool> predicate)
        //    where TError : class, IExceptionWrapper<TError>
        //{
        //    return self.Filter(predicate);
        //}

        ///// <summary>
        /////     Filters a result based on a predicate applied to its value.
        ///// </summary>
        ///// <typeparam name="TSource">The type of the value in the source result.</typeparam>
        ///// <typeparam name="TError">
        /////     The type of error that implements <see cref="IExceptionWrapper{TError}" />.
        ///// </typeparam>
        ///// <param name="self">The result to filter.</param>
        ///// <param name="predicate">A function to test if the value meets a condition.</param>
        ///// <returns>
        /////     The input result if it's a failure, a success without a value, or a success with a value where the
        /////     predicate returns true; otherwise, a new success result without a value.
        ///// </returns>
        ///// <remarks>
        /////     This method enables LINQ query syntax with where clauses for results with values.
        ///// </remarks>
        //public static Result<TSource, TError> Where<TSource, TError>(this Result<TSource, TError> self, Func<TSource, bool> predicate)
        //    where TError : class, IExceptionWrapper<TError>
        //{
        //    return self.Filter(predicate);
        //}
    }
}