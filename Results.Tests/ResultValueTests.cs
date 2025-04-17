using Shouldly;

namespace Results.Tests
{
    public class ResultValueTests
    {
        [Fact]
        public void ResultValue_IsSuccess()
        {
            Result.Success<int, Error>().IsSuccess.ShouldBeTrue();
            Result.Success<int, Error>(1).IsSuccess.ShouldBeTrue();
            Result.Success<int?, Error>().IsSuccess.ShouldBeTrue();
            Result.Success<int?, Error>(1).IsSuccess.ShouldBeTrue();
            Result.Success<int?, Error>(null).IsSuccess.ShouldBeTrue();
            Result.Success<string, Error>().IsSuccess.ShouldBeTrue();
            Result.Success<string, Error>(Constants.String1).IsSuccess.ShouldBeTrue();

            Result.Failure<int, Error>(Error.Unexpected).IsSuccess.ShouldBeFalse();
            Result.Failure<int?, Error>(Error.Unexpected).IsSuccess.ShouldBeFalse();
            Result.Failure<string, Error>(Error.Unexpected).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void ResultValue_Equals()
        {
            // Successes with equal values are equal
            AssertEquals(Result.Success<int, Error>(), Result.Success<int, Error>(), true);
            AssertEquals(Result.Success<int, Error>(1), Result.Success<int, Error>(1), true);
            AssertEquals(Result.Success<int?, Error>(), Result.Success<int?, Error>(), true);
            AssertEquals(Result.Success<int?, Error>(1), Result.Success<int?, Error>(1), true);
            AssertEquals(Result.Success<int?, Error>(null), Result.Success<int?, Error>(null), true);
            AssertEquals(Result.Success<string, Error>(), Result.Success<string, Error>(), true);
            AssertEquals(Result.Success<string, Error>(Constants.String1), Result.Success<string, Error>(Constants.String1), true);

            // Failures with equal errors are equal
            AssertEquals(Result.Failure<int, Error>(Error.Unexpected), Result.Failure<int, Error>(Error.Unexpected), true);
            AssertEquals(Result.Failure<int?, Error>(Error.Unexpected), Result.Failure<int?, Error>(Error.Unexpected), true);
            AssertEquals(Result.Failure<string, Error>(Error.Unexpected), Result.Failure<string, Error>(Error.Unexpected), true);

            // Successes with different values are not equal
            AssertEquals(Result.Success<int, Error>(), Result.Success<int, Error>(2), false);
            AssertEquals(Result.Success<int, Error>(1), Result.Success<int, Error>(), false);
            AssertEquals(Result.Success<int, Error>(1), Result.Success<int, Error>(2), false);
            AssertEquals(Result.Success<int?, Error>(), Result.Success<int?, Error>(2), false);
            AssertEquals(Result.Success<int?, Error>(1), Result.Success<int?, Error>(), false);
            AssertEquals(Result.Success<int?, Error>(1), Result.Success<int?, Error>(2), false);
            AssertEquals(Result.Success<int?, Error>(), Result.Success<int?, Error>(2), false);
            AssertEquals(Result.Success<int?, Error>(null), Result.Success<int?, Error>(), false);
            AssertEquals(Result.Success<int?, Error>(null), Result.Success<int?, Error>(2), false);
            AssertEquals(Result.Success<int?, Error>(), Result.Success<int?, Error>(null), false);
            AssertEquals(Result.Success<int?, Error>(1), Result.Success<int?, Error>(), false);
            AssertEquals(Result.Success<int?, Error>(1), Result.Success<int?, Error>(null), false);
            AssertEquals(Result.Success<string, Error>(), Result.Success<string, Error>(Constants.String2), false);
            AssertEquals(Result.Success<string, Error>(Constants.String1), Result.Success<string, Error>(), false);
            AssertEquals(Result.Success<string, Error>(Constants.String1), Result.Success<string, Error>(Constants.String2), false);

            // Failures with different errors are not equal
            AssertEquals(Result.Failure<int, Error>(Error.Unexpected), Result.Failure<int, Error>(Error.Default), false);
            AssertEquals(Result.Failure<int?, Error>(Error.Unexpected), Result.Failure<int?, Error>(Error.Default), false);
            AssertEquals(Result.Failure<string, Error>(Error.Unexpected), Result.Failure<string, Error>(Error.Default), false);

            // Successes and Failures are not equal 
            AssertEquals(Result.Success<int, Error>(), Result.Failure<int, Error>(Error.Unexpected), false);
            AssertEquals(Result.Success<int, Error>(1), Result.Failure<int, Error>(Error.Unexpected), false);
            AssertEquals(Result.Success<int?, Error>(), Result.Failure<int?, Error>(Error.Unexpected), false);
            AssertEquals(Result.Success<int?, Error>(1), Result.Failure<int?, Error>(Error.Unexpected), false);
            AssertEquals(Result.Success<int?, Error>(null), Result.Failure<int?, Error>(Error.Unexpected), false);
            AssertEquals(Result.Success<string, Error>(), Result.Failure<string, Error>(Error.Unexpected), false);
            AssertEquals(Result.Success<string, Error>(Constants.String1), Result.Failure<string, Error>(Error.Unexpected), false);
            AssertEquals(Result.Failure<int, Error>(Error.Unexpected), Result.Success<int, Error>(), false);
            AssertEquals(Result.Failure<int, Error>(Error.Unexpected), Result.Success<int, Error>(1), false);
            AssertEquals(Result.Failure<int?, Error>(Error.Unexpected), Result.Success<int?, Error>(), false);
            AssertEquals(Result.Failure<int?, Error>(Error.Unexpected), Result.Success<int?, Error>(1), false);
            AssertEquals(Result.Failure<int?, Error>(Error.Unexpected), Result.Success<int?, Error>(null), false);
            AssertEquals(Result.Failure<string, Error>(Error.Unexpected), Result.Success<string, Error>(), false);
            AssertEquals(Result.Failure<string, Error>(Error.Unexpected), Result.Success<string, Error>(Constants.String1), false);
        }

        [Fact]
        public void ResultValue_CompareTo()
        {
            // Successes with equal values are equal
            AssertLessThan(Result.Success<int, Error>(), Result.Success<int, Error>(), false);
            AssertLessThanOrEqualTo(Result.Success<int, Error>(), Result.Success<int, Error>(), true);
            AssertGreaterThan(Result.Success<int, Error>(), Result.Success<int, Error>(), false);
            AssertGreaterThanOrEqualTo(Result.Success<int, Error>(), Result.Success<int, Error>(), true);

            AssertLessThan(Result.Success<int, Error>(1), Result.Success<int, Error>(1), false);
            AssertLessThanOrEqualTo(Result.Success<int, Error>(1), Result.Success<int, Error>(1), true);
            AssertGreaterThan(Result.Success<int, Error>(1), Result.Success<int, Error>(1), false);
            AssertGreaterThanOrEqualTo(Result.Success<int, Error>(1), Result.Success<int, Error>(1), true);

            AssertLessThan(Result.Success<int?, Error>(1), Result.Success<int?, Error>(1), false);
            AssertLessThanOrEqualTo(Result.Success<int?, Error>(1), Result.Success<int?, Error>(1), true);
            AssertGreaterThan(Result.Success<int?, Error>(1), Result.Success<int?, Error>(1), false);
            AssertGreaterThanOrEqualTo(Result.Success<int?, Error>(1), Result.Success<int?, Error>(1), true);

            AssertLessThan(Result.Success<int?, Error>(null), Result.Success<int?, Error>(null), false);
            AssertLessThanOrEqualTo(Result.Success<int?, Error>(null), Result.Success<int?, Error>(null), true);
            AssertGreaterThan(Result.Success<int?, Error>(null), Result.Success<int?, Error>(null), false);
            AssertGreaterThanOrEqualTo(Result.Success<int?, Error>(null), Result.Success<int?, Error>(null), true);

            AssertLessThan(Result.Success<string, Error>(Constants.String1), Result.Success<string, Error>(Constants.String1), false);
            AssertLessThanOrEqualTo(Result.Success<string, Error>(Constants.String1), Result.Success<string, Error>(Constants.String1), true);
            AssertGreaterThan(Result.Success<string, Error>(Constants.String1), Result.Success<string, Error>(Constants.String1), false);
            AssertGreaterThanOrEqualTo(Result.Success<string, Error>(Constants.String1), Result.Success<string, Error>(Constants.String1), true);

            // Failures with equal errors are equal
            AssertLessThan(Result.Failure<int, Error>(Error.Unexpected), Result.Failure<int, Error>(Error.Unexpected), false);
            AssertLessThanOrEqualTo(Result.Failure<int, Error>(Error.Unexpected), Result.Failure<int, Error>(Error.Unexpected), true);
            AssertGreaterThan(Result.Failure<int, Error>(Error.Unexpected), Result.Failure<int, Error>(Error.Unexpected), false);
            AssertGreaterThanOrEqualTo(Result.Failure<int, Error>(Error.Unexpected), Result.Failure<int, Error>(Error.Unexpected), true);

            AssertLessThan(Result.Failure<int?, Error>(Error.Unexpected), Result.Failure<int?, Error>(Error.Unexpected), false);
            AssertLessThanOrEqualTo(Result.Failure<int?, Error>(Error.Unexpected), Result.Failure<int?, Error>(Error.Unexpected), true);
            AssertGreaterThan(Result.Failure<int?, Error>(Error.Unexpected), Result.Failure<int?, Error>(Error.Unexpected), false);
            AssertGreaterThanOrEqualTo(Result.Failure<int?, Error>(Error.Unexpected), Result.Failure<int?, Error>(Error.Unexpected), true);

            AssertLessThan(Result.Failure<string, Error>(Error.Unexpected), Result.Failure<string, Error>(Error.Unexpected), false);
            AssertLessThanOrEqualTo(Result.Failure<string, Error>(Error.Unexpected), Result.Failure<string, Error>(Error.Unexpected), true);
            AssertGreaterThan(Result.Failure<string, Error>(Error.Unexpected), Result.Failure<string, Error>(Error.Unexpected), false);
            AssertGreaterThanOrEqualTo(Result.Failure<string, Error>(Error.Unexpected), Result.Failure<string, Error>(Error.Unexpected), true);

            // Successes with no values are always less than successes with values
            {
                var success1 = Result.Success<int, Error>();
                var success2 = Result.Success<int, Error>(2);
                AssertLessThan(success1, success2, true);
                AssertLessThanOrEqualTo(success1, success2, true);
                AssertGreaterThan(success1, success2, false);
                AssertGreaterThanOrEqualTo(success1, success2, false);
            }

            {
                var success1 = Result.Success<int?, Error>();
                var success2 = Result.Success<int?, Error>(2);
                AssertLessThan(success1, success2, true);
                AssertLessThanOrEqualTo(success1, success2, true);
                AssertGreaterThan(success1, success2, false);
                AssertGreaterThanOrEqualTo(success1, success2, false);
            }

            {
                var success1 = Result.Success<int?, Error>();
                var success2 = Result.Success<int?, Error>(null);
                AssertLessThan(success1, success2, true);
                AssertLessThanOrEqualTo(success1, success2, true);
                AssertGreaterThan(success1, success2, false);
                AssertGreaterThanOrEqualTo(success1, success2, false);
            }

            {
                var success1 = Result.Success<string, Error>();
                var success2 = Result.Success<string, Error>(Constants.String2);
                AssertLessThan(success1, success2, true);
                AssertLessThanOrEqualTo(success1, success2, true);
                AssertGreaterThan(success1, success2, false);
                AssertGreaterThanOrEqualTo(success1, success2, false);
            }

            // Successes with values are always greater than successes with no values
            {
                var success1 = Result.Success<int, Error>(1);
                var success2 = Result.Success<int, Error>();
                AssertLessThan(success1, success2, false);
                AssertLessThanOrEqualTo(success1, success2, false);
                AssertGreaterThan(success1, success2, true);
                AssertGreaterThanOrEqualTo(success1, success2, true);
            }

            {
                var success1 = Result.Success<int?, Error>(2);
                var success2 = Result.Success<int?, Error>();
                AssertLessThan(success1, success2, false);
                AssertLessThanOrEqualTo(success1, success2, false);
                AssertGreaterThan(success1, success2, true);
                AssertGreaterThanOrEqualTo(success1, success2, true);
            }

            {
                var success1 = Result.Success<int?, Error>(null);
                var success2 = Result.Success<int?, Error>();
                AssertLessThan(success1, success2, false);
                AssertLessThanOrEqualTo(success1, success2, false);
                AssertGreaterThan(success1, success2, true);
                AssertGreaterThanOrEqualTo(success1, success2, true);
            }

            {
                var success1 = Result.Success<string, Error>(Constants.String1);
                var success2 = Result.Success<string, Error>();
                AssertLessThan(success1, success2, false);
                AssertLessThanOrEqualTo(success1, success2, false);
                AssertGreaterThan(success1, success2, true);
                AssertGreaterThanOrEqualTo(success1, success2, true);
            }

            // Successes with different values compare as the hash codes of the values
            {
                var success1 = Result.Success<int, Error>(1);
                var success2 = Result.Success<int, Error>(2);
                AssertLessThan(success1, success2, 1 < 2);
                AssertLessThanOrEqualTo(success1, success2, 1 <= 2);
                AssertGreaterThan(success1, success2, 1 > 2);
                AssertGreaterThanOrEqualTo(success1, success2, 1 >= 2);
            }

            {
                var success1 = Result.Success<int?, Error>(1);
                var success2 = Result.Success<int?, Error>(2);
                AssertLessThan(success1, success2, 1 < 2);
                AssertLessThanOrEqualTo(success1, success2, 1 <= 2);
                AssertGreaterThan(success1, success2, 1 > 2);
                AssertGreaterThanOrEqualTo(success1, success2, 1 >= 2);
            }

            {
                var success1 = Result.Success<int?, Error>(null);
                var success2 = Result.Success<int?, Error>(null);
                AssertLessThan(success1, success2, false);
                AssertLessThanOrEqualTo(success1, success2, true);
                AssertGreaterThan(success1, success2, false);
                AssertGreaterThanOrEqualTo(success1, success2, true);
            }

            {
                var success1 = Result.Success<string, Error>(Constants.String1);
                var success2 = Result.Success<string, Error>(Constants.String2);
                AssertLessThan(success1, success2, true);
                AssertLessThanOrEqualTo(success1, success2, true);
                AssertGreaterThan(success1, success2, false);
                AssertGreaterThanOrEqualTo(success1, success2, false);
            }

            // Failures with different errors compare as the hash codes of the errors
            {
                var error1 = Result.Failure<int, Error>(Error.FromException(new InvalidOperationException()));
                var error2 = Result.Failure<int, Error>(Error.FromException(new ArgumentNullException()));
                AssertLessThan(error1, error2, error1.GetHashCode() < error2.GetHashCode());
                AssertLessThanOrEqualTo(error1, error2, error1.GetHashCode() <= error2.GetHashCode());
                AssertGreaterThan(error1, error2, error1.GetHashCode() > error2.GetHashCode());
                AssertGreaterThanOrEqualTo(error1, error2, error1.GetHashCode() >= error2.GetHashCode());
            }

            {
                var error1 = Result.Failure<int?, Error>(Error.FromException(new InvalidOperationException()));
                var error2 = Result.Failure<int?, Error>(Error.FromException(new ArgumentNullException()));
                AssertLessThan(error1, error2, error1.GetHashCode() < error2.GetHashCode());
                AssertLessThanOrEqualTo(error1, error2, error1.GetHashCode() <= error2.GetHashCode());
                AssertGreaterThan(error1, error2, error1.GetHashCode() > error2.GetHashCode());
                AssertGreaterThanOrEqualTo(error1, error2, error1.GetHashCode() >= error2.GetHashCode());
            }

            {
                var error1 = Result.Failure<string, Error>(Error.FromException(new InvalidOperationException()));
                var error2 = Result.Failure<string, Error>(Error.FromException(new ArgumentNullException()));
                AssertLessThan(error1, error2, error1.GetHashCode() < error2.GetHashCode());
                AssertLessThanOrEqualTo(error1, error2, error1.GetHashCode() <= error2.GetHashCode());
                AssertGreaterThan(error1, error2, error1.GetHashCode() > error2.GetHashCode());
                AssertGreaterThanOrEqualTo(error1, error2, error1.GetHashCode() >= error2.GetHashCode());
            }

            // Successes are greater than Failures
            AssertLessThan(Result.Success<int, Error>(), Result.Failure<int, Error>(Error.Unexpected), false);
            AssertLessThan(Result.Success<int, Error>(1), Result.Failure<int, Error>(Error.Unexpected), false);
            AssertLessThanOrEqualTo(Result.Success<int, Error>(1), Result.Failure<int, Error>(Error.Unexpected), false);
            AssertGreaterThan(Result.Success<int, Error>(1), Result.Failure<int, Error>(Error.Unexpected), true);
            AssertGreaterThanOrEqualTo(Result.Success<int, Error>(1), Result.Failure<int, Error>(Error.Unexpected), true);

            AssertLessThan(Result.Success<int?, Error>(), Result.Failure<int?, Error>(Error.Unexpected), false);
            AssertLessThan(Result.Success<int?, Error>(1), Result.Failure<int?, Error>(Error.Unexpected), false);
            AssertLessThanOrEqualTo(Result.Success<int?, Error>(1), Result.Failure<int?, Error>(Error.Unexpected), false);
            AssertGreaterThan(Result.Success<int?, Error>(1), Result.Failure<int?, Error>(Error.Unexpected), true);
            AssertGreaterThanOrEqualTo(Result.Success<int?, Error>(1), Result.Failure<int?, Error>(Error.Unexpected), true);

            AssertLessThan(Result.Success<int?, Error>(), Result.Failure<int?, Error>(Error.Unexpected), false);
            AssertLessThan(Result.Success<int?, Error>(null), Result.Failure<int?, Error>(Error.Unexpected), false);
            AssertLessThanOrEqualTo(Result.Success<int?, Error>(null), Result.Failure<int?, Error>(Error.Unexpected), false);
            AssertGreaterThan(Result.Success<int?, Error>(null), Result.Failure<int?, Error>(Error.Unexpected), true);
            AssertGreaterThanOrEqualTo(Result.Success<int?, Error>(null), Result.Failure<int?, Error>(Error.Unexpected), true);

            AssertLessThan(Result.Success<string, Error>(), Result.Failure<string, Error>(Error.Unexpected), false);
            AssertLessThan(Result.Success<string, Error>(Constants.String1), Result.Failure<string, Error>(Error.Unexpected), false);
            AssertLessThanOrEqualTo(Result.Success<string, Error>(Constants.String1), Result.Failure<string, Error>(Error.Unexpected), false);
            AssertGreaterThan(Result.Success<string, Error>(Constants.String1), Result.Failure<string, Error>(Error.Unexpected), true);
            AssertGreaterThanOrEqualTo(Result.Success<string, Error>(Constants.String1), Result.Failure<string, Error>(Error.Unexpected), true);

            // Failures are less than Successes
            AssertLessThan(Result.Failure<int, Error>(Error.Unexpected), Result.Success<int, Error>(), true);
            AssertLessThan(Result.Failure<int, Error>(Error.Unexpected), Result.Success<int, Error>(1), true);
            AssertLessThanOrEqualTo(Result.Failure<int, Error>(Error.Unexpected), Result.Success<int, Error>(1), true);
            AssertGreaterThan(Result.Failure<int, Error>(Error.Unexpected), Result.Success<int, Error>(1), false);
            AssertGreaterThanOrEqualTo(Result.Failure<int, Error>(Error.Unexpected), Result.Success<int, Error>(1), false);

            AssertLessThan(Result.Failure<int?, Error>(Error.Unexpected), Result.Success<int?, Error>(), true);
            AssertLessThan(Result.Failure<int?, Error>(Error.Unexpected), Result.Success<int?, Error>(1), true);
            AssertLessThanOrEqualTo(Result.Failure<int?, Error>(Error.Unexpected), Result.Success<int?, Error>(1), true);
            AssertGreaterThan(Result.Failure<int?, Error>(Error.Unexpected), Result.Success<int?, Error>(1), false);
            AssertGreaterThanOrEqualTo(Result.Failure<int?, Error>(Error.Unexpected), Result.Success<int?, Error>(1), false);

            AssertLessThan(Result.Failure<int?, Error>(Error.Unexpected), Result.Success<int?, Error>(), true);
            AssertLessThan(Result.Failure<int?, Error>(Error.Unexpected), Result.Success<int?, Error>(null), true);
            AssertLessThanOrEqualTo(Result.Failure<int?, Error>(Error.Unexpected), Result.Success<int?, Error>(null), true);
            AssertGreaterThan(Result.Failure<int?, Error>(Error.Unexpected), Result.Success<int?, Error>(null), false);
            AssertGreaterThanOrEqualTo(Result.Failure<int?, Error>(Error.Unexpected), Result.Success<int?, Error>(null), false);

            AssertLessThan(Result.Failure<string, Error>(Error.Unexpected), Result.Success<string, Error>(), true);
            AssertLessThan(Result.Failure<string, Error>(Error.Unexpected), Result.Success<string, Error>(Constants.String1), true);
            AssertLessThanOrEqualTo(Result.Failure<string, Error>(Error.Unexpected), Result.Success<string, Error>(Constants.String1), true);
            AssertGreaterThan(Result.Failure<string, Error>(Error.Unexpected), Result.Success<string, Error>(Constants.String1), false);
            AssertGreaterThanOrEqualTo(Result.Failure<string, Error>(Error.Unexpected), Result.Success<string, Error>(Constants.String1), false);
        }

        [Fact]
        public void ResultValue_GetHashCode()
        {
            Result.Success<int, Error>().GetHashCode().ShouldBe(0);
            Result.Success<int, Error>(1).GetHashCode().ShouldBe(1.GetHashCode());
            Result.Failure<int, Error>(Error.Unexpected).GetHashCode().ShouldBe(Error.Unexpected.GetHashCode());
            Result.Success<int?, Error>().GetHashCode().ShouldBe(0);
            Result.Success<int?, Error>(2).GetHashCode().ShouldBe(2.GetHashCode());
            Result.Success<int?, Error>(null).GetHashCode().ShouldBe(((int?) null).GetHashCode());
            Result.Failure<int?, Error>(Error.Unexpected).GetHashCode().ShouldBe(Error.Unexpected.GetHashCode());
            Result.Success<string, Error>().GetHashCode().ShouldBe(0);
            Result.Success<string, Error>(Constants.String1).GetHashCode().ShouldBe(Constants.String1.GetHashCode());
            Result.Failure<string, Error>(Error.Unexpected).GetHashCode().ShouldBe(Error.Unexpected.GetHashCode());
        }

        [Fact]
        public void ResultValue_ToString()
        {
            Result.Success<int, Error>().ToString().ShouldBe("Success()");
            Result.Success<int, Error>(1).ToString().ShouldBe("Success(1)");
            Result.Failure<int, Error>(Error.Unexpected).ToString().ShouldBe("Failure(Error(Unexpected): An unexpected error occurred.)");
            Result.Success<int?, Error>().ToString().ShouldBe("Success()");
            Result.Success<int?, Error>(2).ToString().ShouldBe("Success(2)");
            Result.Success<int?, Error>(null).ToString().ShouldBe("Success(null)");
            Result.Failure<int?, Error>(Error.Unexpected).ToString().ShouldBe("Failure(Error(Unexpected): An unexpected error occurred.)");
            Result.Success<string, Error>().ToString().ShouldBe("Success()");
            Result.Success<string, Error>(Constants.String1).ToString().ShouldBe($"Success({Constants.String1})");
            Result.Failure<string, Error>(Error.Unexpected).ToString().ShouldBe("Failure(Error(Unexpected): An unexpected error occurred.)");
        }

        private static void AssertEquals<TValue>(Result<TValue, Error> left, Result<TValue, Error> right, bool expectedResult)
        {
            left.Equals(null).ShouldBeFalse();
            left.Equals((object) right).ShouldBe(expectedResult);
            left.Equals(right).ShouldBe(expectedResult);
            EqualityComparer<Result<TValue, Error>>.Default.Equals(left, right).ShouldBe(expectedResult);
            (left == right).ShouldBe(expectedResult);
            (left != right).ShouldBe(!expectedResult);
        }

        private static void AssertLessThan<TValue>(Result<TValue, Error> left, Result<TValue, Error> right, bool expectedResult)
        {
            if (expectedResult)
            {
                left.CompareTo(null).ShouldBeGreaterThan(0);
                left.CompareTo((object) right).ShouldBeLessThan(0);
                left.CompareTo(right).ShouldBeLessThan(0);
                Comparer<Result<TValue, Error>>.Default.Compare(left, right).ShouldBeLessThan(0);
                (left < right).ShouldBeTrue();
            }
            else
            {
                left.CompareTo(null).ShouldBeGreaterThan(0);
                left.CompareTo((object) right).ShouldBeGreaterThanOrEqualTo(0);
                left.CompareTo(right).ShouldBeGreaterThanOrEqualTo(0);
                Comparer<Result<TValue, Error>>.Default.Compare(left, right).ShouldBeGreaterThanOrEqualTo(0);
                (left < right).ShouldBeFalse();
            }
        }

        private static void AssertLessThanOrEqualTo<TValue>(Result<TValue, Error> left, Result<TValue, Error> right, bool expectedResult)
        {
            if (expectedResult)
            {
                left.CompareTo(null).ShouldBeGreaterThan(0);
                left.CompareTo((object) right).ShouldBeLessThanOrEqualTo(0);
                left.CompareTo(right).ShouldBeLessThanOrEqualTo(0);
                Comparer<Result<TValue, Error>>.Default.Compare(left, right).ShouldBeLessThanOrEqualTo(0);
                (left <= right).ShouldBeTrue();
            }
            else
            {
                left.CompareTo(null).ShouldBeGreaterThan(0);
                left.CompareTo((object) right).ShouldBeGreaterThan(0);
                left.CompareTo(right).ShouldBeGreaterThan(0);
                Comparer<Result<TValue, Error>>.Default.Compare(left, right).ShouldBeGreaterThan(0);
                (left <= right).ShouldBeFalse();
            }
        }

        private static void AssertGreaterThan<TValue>(Result<TValue, Error> left, Result<TValue, Error> right, bool expectedResult)
        {
            if (expectedResult)
            {
                left.CompareTo(null).ShouldBeGreaterThan(0);
                left.CompareTo((object) right).ShouldBeGreaterThan(0);
                left.CompareTo(right).ShouldBeGreaterThan(0);
                Comparer<Result<TValue, Error>>.Default.Compare(left, right).ShouldBeGreaterThan(0);
                (left > right).ShouldBeTrue();
            }
            else
            {
                left.CompareTo(null).ShouldBeGreaterThan(0);
                left.CompareTo((object) right).ShouldBeLessThanOrEqualTo(0);
                left.CompareTo(right).ShouldBeLessThanOrEqualTo(0);
                Comparer<Result<TValue, Error>>.Default.Compare(left, right).ShouldBeLessThanOrEqualTo(0);
                (left > right).ShouldBeFalse();
            }
        }

        private static void AssertGreaterThanOrEqualTo<TValue>(Result<TValue, Error> left, Result<TValue, Error> right, bool expectedResult)
        {
            if (expectedResult)
            {
                left.CompareTo(null).ShouldBeGreaterThan(0);
                left.CompareTo((object) right).ShouldBeGreaterThanOrEqualTo(0);
                left.CompareTo(right).ShouldBeGreaterThanOrEqualTo(0);
                Comparer<Result<TValue, Error>>.Default.Compare(left, right).ShouldBeGreaterThanOrEqualTo(0);
                (left >= right).ShouldBeTrue();
            }
            else
            {
                left.CompareTo(null).ShouldBeGreaterThan(0);
                left.CompareTo((object) right).ShouldBeLessThan(0);
                left.CompareTo(right).ShouldBeLessThan(0);
                Comparer<Result<TValue, Error>>.Default.Compare(left, right).ShouldBeLessThan(0);
                (left >= right).ShouldBeFalse();
            }
        }
    }
}