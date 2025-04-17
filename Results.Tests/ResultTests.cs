using Shouldly;

namespace Results.Tests
{
    public class ResultTests
    {
        [Fact]
        public void Result_IsSuccess()
        {
            Result.Success<Error>().IsSuccess.ShouldBeTrue();

            Result.Failure(Error.Unexpected).IsSuccess.ShouldBeFalse();
        }

        [Fact]
        public void Result_Equals()
        {
            // Successes are equal
            AssertEquals(Result.Success<Error>(), Result.Success<Error>(), true);

            // Failures with equal errors are equal
            AssertEquals(Result.Failure(Error.Unexpected), Result.Failure(Error.Unexpected), true);

            // Failures with different errors are not equal
            AssertEquals(Result.Failure(Error.Unexpected), Result.Failure(Error.Default), false);

            // Successes and Failures are not equal 
            AssertEquals(Result.Success<Error>(), Result.Failure(Error.Unexpected), false);
            AssertEquals(Result.Failure(Error.Unexpected), Result.Success<Error>(), false);
        }

        [Fact]
        public void Result_CompareTo()
        {
            // Successes are equal
            AssertLessThan(Result.Success<Error>(), Result.Success<Error>(), false);
            AssertLessThanOrEqualTo(Result.Success<Error>(), Result.Success<Error>(), true);
            AssertGreaterThan(Result.Success<Error>(), Result.Success<Error>(), false);
            AssertGreaterThanOrEqualTo(Result.Success<Error>(), Result.Success<Error>(), true);

            // Failures with equal errors are equal
            AssertLessThan(Result.Failure(Error.Unexpected), Result.Failure(Error.Unexpected), false);
            AssertLessThanOrEqualTo(Result.Failure(Error.Unexpected), Result.Failure(Error.Unexpected), true);
            AssertGreaterThan(Result.Failure(Error.Unexpected), Result.Failure(Error.Unexpected), false);
            AssertGreaterThanOrEqualTo(Result.Failure(Error.Unexpected), Result.Failure(Error.Unexpected), true);

            // Failures with different errors compare as the hash codes of the errors
            var error1 = Result.Failure(Error.FromException(new InvalidOperationException()));
            var error2 = Result.Failure(Error.FromException(new ArgumentException()));
            AssertLessThan(error1, error2, error1.GetHashCode() < error2.GetHashCode());
            AssertLessThanOrEqualTo(error1, error2, error1.GetHashCode() <= error2.GetHashCode());
            AssertGreaterThan(error1, error2, error1.GetHashCode() > error2.GetHashCode());
            AssertGreaterThanOrEqualTo(error1, error2, error1.GetHashCode() >= error2.GetHashCode());

            // Successes are greater than Failures
            AssertLessThan(Result.Success<Error>(), Result.Failure(Error.Unexpected), false);
            AssertLessThanOrEqualTo(Result.Success<Error>(), Result.Failure(Error.Unexpected), false);
            AssertGreaterThan(Result.Success<Error>(), Result.Failure(Error.Unexpected), true);
            AssertGreaterThanOrEqualTo(Result.Success<Error>(), Result.Failure(Error.Unexpected), true);

            // Failures are less than Successes
            AssertLessThan(Result.Failure(Error.Unexpected), Result.Success<Error>(), true);
            AssertLessThanOrEqualTo(Result.Failure(Error.Unexpected), Result.Success<Error>(), true);
            AssertGreaterThan(Result.Failure(Error.Unexpected), Result.Success<Error>(), false);
            AssertGreaterThanOrEqualTo(Result.Failure(Error.Unexpected), Result.Success<Error>(), false);
        }

        [Fact]
        public void Result_GetHashCode()
        {
            Result.Success<Error>().GetHashCode().ShouldBe(0);
            Result.Failure(Error.Unexpected).GetHashCode().ShouldBe(Error.Unexpected.GetHashCode());
        }

        [Fact]
        public void Result_ToString()
        {
            Result.Success<Error>().ToString().ShouldBe("Success()");
            Result.Failure(Error.Unexpected).ToString().ShouldBe("Failure(Error(Unexpected): An unexpected error occurred.)");
        }

        private static void AssertEquals(Result<Error> left, Result<Error> right, bool expectedResult)
        {
            left.Equals(null).ShouldBeFalse();
            left.Equals((object) right).ShouldBe(expectedResult);
            left.Equals(right).ShouldBe(expectedResult);
            EqualityComparer<Result<Error>>.Default.Equals(left, right).ShouldBe(expectedResult);
            (left == right).ShouldBe(expectedResult);
            (left != right).ShouldBe(!expectedResult);
        }

        private static void AssertLessThan(Result<Error> left, Result<Error> right, bool expectedResult)
        {
            if (expectedResult)
            {
                left.CompareTo(null).ShouldBeGreaterThan(0);
                left.CompareTo((object) right).ShouldBeLessThan(0);
                left.CompareTo(right).ShouldBeLessThan(0);
                Comparer<Result<Error>>.Default.Compare(left, right).ShouldBeLessThan(0);
                (left < right).ShouldBeTrue();
            }
            else
            {
                left.CompareTo(null).ShouldBeGreaterThan(0);
                left.CompareTo((object) right).ShouldBeGreaterThanOrEqualTo(0);
                left.CompareTo(right).ShouldBeGreaterThanOrEqualTo(0);
                Comparer<Result<Error>>.Default.Compare(left, right).ShouldBeGreaterThanOrEqualTo(0);
                (left < right).ShouldBeFalse();
            }
        }

        private static void AssertLessThanOrEqualTo(Result<Error> left, Result<Error> right, bool expectedResult)
        {
            if (expectedResult)
            {
                left.CompareTo(null).ShouldBeGreaterThan(0);
                left.CompareTo((object) right).ShouldBeLessThanOrEqualTo(0);
                left.CompareTo(right).ShouldBeLessThanOrEqualTo(0);
                Comparer<Result<Error>>.Default.Compare(left, right).ShouldBeLessThanOrEqualTo(0);
                (left <= right).ShouldBeTrue();
            }
            else
            {
                left.CompareTo(null).ShouldBeGreaterThan(0);
                left.CompareTo((object) right).ShouldBeGreaterThan(0);
                left.CompareTo(right).ShouldBeGreaterThan(0);
                Comparer<Result<Error>>.Default.Compare(left, right).ShouldBeGreaterThan(0);
                (left <= right).ShouldBeFalse();
            }
        }

        private static void AssertGreaterThan(Result<Error> left, Result<Error> right, bool expectedResult)
        {
            if (expectedResult)
            {
                left.CompareTo(null).ShouldBeGreaterThan(0);
                left.CompareTo((object) right).ShouldBeGreaterThan(0);
                left.CompareTo(right).ShouldBeGreaterThan(0);
                Comparer<Result<Error>>.Default.Compare(left, right).ShouldBeGreaterThan(0);
                (left > right).ShouldBeTrue();
            }
            else
            {
                left.CompareTo(null).ShouldBeGreaterThan(0);
                left.CompareTo((object) right).ShouldBeLessThanOrEqualTo(0);
                left.CompareTo(right).ShouldBeLessThanOrEqualTo(0);
                Comparer<Result<Error>>.Default.Compare(left, right).ShouldBeLessThanOrEqualTo(0);
                (left > right).ShouldBeFalse();
            }
        }

        private static void AssertGreaterThanOrEqualTo(Result<Error> left, Result<Error> right, bool expectedResult)
        {
            if (expectedResult)
            {
                left.CompareTo(null).ShouldBeGreaterThan(0);
                left.CompareTo((object) right).ShouldBeGreaterThanOrEqualTo(0);
                left.CompareTo(right).ShouldBeGreaterThanOrEqualTo(0);
                Comparer<Result<Error>>.Default.Compare(left, right).ShouldBeGreaterThanOrEqualTo(0);
                (left >= right).ShouldBeTrue();
            }
            else
            {
                left.CompareTo(null).ShouldBeGreaterThan(0);
                left.CompareTo((object) right).ShouldBeLessThan(0);
                left.CompareTo(right).ShouldBeLessThan(0);
                Comparer<Result<Error>>.Default.Compare(left, right).ShouldBeLessThan(0);
                (left >= right).ShouldBeFalse();
            }
        }
    }
}