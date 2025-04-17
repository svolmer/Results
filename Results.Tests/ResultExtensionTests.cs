using Results.Exceptions;
using Results.Extensions;
using Shouldly;

namespace Results.Tests
{
    public class ResultExtensionTests
    {
        [Fact]
        public void Result_GetErrorOrDefault()
        {
            Result.Success<Error>().GetErrorOrDefault().ShouldBe(Error.Default);
            Result.Success<Error>().GetErrorOrDefault().ShouldBe(Error.Default);
            Result.Success<Error>().GetErrorOrDefault().ShouldBe(Error.Default);

            Result.Failure(Error.Unexpected).GetErrorOrDefault().ShouldBe(Error.Unexpected);
            Result.Failure(Error.Unexpected).GetErrorOrDefault().ShouldBe(Error.Unexpected);
            Result.Failure(Error.Unexpected).GetErrorOrDefault().ShouldBe(Error.Unexpected);
        }

        [Fact]
        public void ResultValue_GetErrorOrDefault()
        {
            Result.Success<int, Error>(0).GetErrorOrDefault().ShouldBe(Error.Default);
            Result.Success<int, Error>(1).GetErrorOrDefault().ShouldBe(Error.Default);
            Result.Success<int?, Error>(1).GetErrorOrDefault().ShouldBe(Error.Default);
            Result.Success<int?, Error>(null).GetErrorOrDefault().ShouldBe(Error.Default);
            Result.Success<string, Error>(Constants.String1).GetErrorOrDefault(Error.Default).ShouldBe(Error.Default);

            Result.Failure<int, Error>(Error.Unexpected).GetErrorOrDefault().ShouldBe(Error.Unexpected);
            Result.Failure<int?, Error>(Error.Unexpected).GetErrorOrDefault().ShouldBe(Error.Unexpected);
            Result.Failure<string, Error>(Error.Unexpected).GetErrorOrDefault().ShouldBe(Error.Unexpected);
        }

        [Fact]
        public void Result_ThrowOnFailure()
        {
            AssertThrow<InvalidOperationException>(() => Result.Success<Error>().ThrowOnFailure(new InvalidOperationException()), false);
            AssertThrow<InvalidOperationException>(() => Result.Success<Error>().ThrowOnFailure(new InvalidOperationException()), false);
            AssertThrow<InvalidOperationException>(() => Result.Success<Error>().ThrowOnFailure(new InvalidOperationException()), false);

            AssertThrow<InvalidOperationException>(() => Result.Failure(Error.Unexpected).ThrowOnFailure(new InvalidOperationException()), true);
            AssertThrow<InvalidOperationException>(() => Result.Failure(Error.Unexpected).ThrowOnFailure(new InvalidOperationException()), true);
            AssertThrow<InvalidOperationException>(() => Result.Failure(Error.Unexpected).ThrowOnFailure(new InvalidOperationException()), true);
        }

        [Fact]
        public void Result_ThrowOnFailureFactory()
        {
            AssertThrow<ResultErrorException>(() => Result.Success<Error>().ThrowOnFailure(), false);
            AssertThrow<ResultErrorException>(() => Result.Success<Error>().ThrowOnFailure(), false);
            AssertThrow<ResultErrorException>(() => Result.Success<Error>().ThrowOnFailure(), false);

            AssertThrow<ResultErrorException>(() => Result.Failure(Error.Unexpected).ThrowOnFailure(), true);
            AssertThrow<ResultErrorException>(() => Result.Failure(Error.Unexpected).ThrowOnFailure(), true);
            AssertThrow<ResultErrorException>(() => Result.Failure(Error.Unexpected).ThrowOnFailure(), true);
        }

        [Fact]
        public void ResultValue_GetValueOrDefault()
        {
            Result.Success<int, Error>().GetValueOrDefault(2).ShouldBe(2);
            Result.Success<int, Error>(1).GetValueOrDefault(2).ShouldBe(1);
            Result.Success<int?, Error>(1).GetValueOrDefault(2).ShouldBe(1);
            Result.Success<int?, Error>(null).GetValueOrDefault(2).ShouldBe(null);
            Result.Success<string, Error>(Constants.String1).GetValueOrDefault(Constants.String2).ShouldBe(Constants.String1);

            Result.Failure<int, Error>(Error.Unexpected).GetValueOrDefault(2).ShouldBe(2);
            Result.Failure<int?, Error>(Error.Unexpected).GetValueOrDefault(2).ShouldBe(2);
            Result.Failure<int?, Error>(Error.Unexpected).GetValueOrDefault().ShouldBe(null);
            Result.Failure<string, Error>(Error.Unexpected).GetValueOrDefault(Constants.String2).ShouldBe(Constants.String2);
        }

        [Fact]
        public void ResultValue_GetValueOrThrowOnFailure()
        {
            AssertThrow<InvalidOperationException>(() => Result.Success<int, Error>().GetValueOrThrowOnFailure(new InvalidOperationException()), true);

            AssertThrow<InvalidOperationException>(() => Result.Success<int, Error>(1).GetValueOrThrowOnFailure(new InvalidOperationException()), false);
            AssertThrow<InvalidOperationException>(() => Result.Success<int?, Error>(1).GetValueOrThrowOnFailure(new InvalidOperationException()), false);
            AssertThrow<InvalidOperationException>(() => Result.Success<int?, Error>(null).GetValueOrThrowOnFailure(new InvalidOperationException()), false);
            AssertThrow<InvalidOperationException>(
                () => Result.Success<string, Error>(Constants.String1).GetValueOrThrowOnFailure(new InvalidOperationException()), false);

            AssertThrow<InvalidOperationException>(() => Result.Failure<int, Error>(Error.Unexpected).GetValueOrThrowOnFailure(new InvalidOperationException()),
                true);
            AssertThrow<InvalidOperationException>(
                () => Result.Failure<int?, Error>(Error.Unexpected).GetValueOrThrowOnFailure(new InvalidOperationException()), true);
            AssertThrow<InvalidOperationException>(
                () => Result.Failure<string, Error>(Error.Unexpected).GetValueOrThrowOnFailure(new InvalidOperationException()), true);
        }

        [Fact]
        public void ResultValue_GetValueOrThrowOnFailureFactory()
        {
            AssertThrow<ResultValueException>(() => Result.Success<int, Error>().GetValueOrThrowOnFailure(), true);

            AssertThrow<ResultErrorException>(() => Result.Success<int, Error>(1).GetValueOrThrowOnFailure(), false);
            AssertThrow<ResultErrorException>(() => Result.Success<int?, Error>(1).GetValueOrThrowOnFailure(), false);
            AssertThrow<ResultErrorException>(() => Result.Success<int?, Error>(null).GetValueOrThrowOnFailure(), false);
            AssertThrow<ResultErrorException>(() => Result.Success<string, Error>(Constants.String1).GetValueOrThrowOnFailure(), false);

            AssertThrow<ResultErrorException>(() => Result.Failure<int, Error>(Error.Unexpected).GetValueOrThrowOnFailure(), true);
            AssertThrow<ResultErrorException>(() => Result.Failure<int?, Error>(Error.Unexpected).GetValueOrThrowOnFailure(), true);
            AssertThrow<ResultErrorException>(() => Result.Failure<string, Error>(Error.Unexpected).GetValueOrThrowOnFailure(), true);
        }

        [Fact]
        public void ResultValue_Values()
        {
            Result.Success<int, Error>().Values().ShouldBeEmpty();
            Result.Success<int, Error>(1).Values().ShouldContain(1);
            Result.Success<int?, Error>(1).Values().ShouldContain(1);
            Result.Success<int?, Error>(null).Values().ShouldContain((int?) null);
            Result.Success<string, Error>(Constants.String1).Values().ShouldContain(Constants.String1);

            Result.Failure<int, Error>(Error.Unexpected).Values().ShouldBeEmpty();
            Result.Failure<int?, Error>(Error.Unexpected).Values().ShouldBeEmpty();
            Result.Failure<int?, Error>(Error.Unexpected).Values().ShouldBeEmpty();
            Result.Failure<string, Error>(Error.Unexpected).Values().ShouldBeEmpty();
        }

        [Fact]
        public void Result_Errors()
        {
            Result.Success<Error>().Errors().ShouldBeEmpty();

            Result.Failure(Error.Unexpected).Errors().ShouldContain(Error.Unexpected);
        }

        [Fact]
        public void ResultValue_Errors()
        {
            Result.Success<int, Error>().Errors().ShouldBeEmpty();
            Result.Success<int, Error>(1).Errors().ShouldBeEmpty();
            Result.Success<int?, Error>(1).Errors().ShouldBeEmpty();
            Result.Success<int?, Error>(null).Errors().ShouldBeEmpty();
            Result.Success<string, Error>(Constants.String1).Errors().ShouldBeEmpty();

            Result.Failure<int, Error>(Error.Unexpected).Errors().ShouldContain(Error.Unexpected);
            Result.Failure<int?, Error>(Error.Unexpected).Errors().ShouldContain(Error.Unexpected);
            Result.Failure<int?, Error>(Error.Unexpected).Errors().ShouldContain(Error.Unexpected);
            Result.Failure<string, Error>(Error.Unexpected).Errors().ShouldContain(Error.Unexpected);
        }

        [Fact]
        public void Result_ToResult()
        {
            Error.Unexpected.ToResult().ShouldBe(Result.Failure(Error.Unexpected));
            Error.Unexpected.ToResult().ShouldBe(Result.Failure(Error.Unexpected));
            Error.Unexpected.ToResult().ShouldBe(Result.Failure(Error.Unexpected));
        }

        [Fact]
        public void ResultValue_ToResult()
        {
            1.ToResult<int, Error>().ShouldBeEquivalentTo(Result.Success<int, Error>(1));
            ((int?) 1).ToResult<int?, Error>().ShouldBeEquivalentTo(Result.Success<int?, Error>(1));
            ((int?) null).ToResult<int?, Error>().ShouldBeEquivalentTo(Result.Success<int?, Error>(null));
            Constants.String1.ToResult<string, Error>().ShouldBeEquivalentTo(Result.Success<string, Error>(Constants.String1));

            Error.Unexpected.ToResult<int, Error>().ShouldBe(Result.Failure<int, Error>(Error.Unexpected));
            Error.Unexpected.ToResult<int?, Error>().ShouldBe(Result.Failure<int?, Error>(Error.Unexpected));
            Error.Unexpected.ToResult<string, Error>().ShouldBe(Result.Failure<string, Error>(Error.Unexpected));
        }


        [Fact]
        public void ResultValue_Flatten()
        {
            Result.Success<Result<Error>, Error>(Result.Success<Error>()).Flatten().ShouldBe(Result.Success<Error>());
            Result.Success<Result<int, Error>, Error>(Result.Success<int, Error>()).Flatten().ShouldBe(Result.Success<int, Error>());
            Result.Success<Result<int, Error>, Error>(Result.Success<int, Error>(1)).Flatten().ShouldBe(Result.Success<int, Error>(1));
            Result.Success<Result<int?, Error>, Error>(Result.Success<int?, Error>(1)).Flatten().ShouldBe(Result.Success<int?, Error>(1));
            Result.Success<Result<int?, Error>, Error>(Result.Success<int?, Error>(null)).Flatten().ShouldBe(Result.Success<int?, Error>(null));
            Result.Success<Result<string, Error>, Error>(Result.Success<string, Error>(Constants.String1)).Flatten()
                .ShouldBe(Result.Success<string, Error>(Constants.String1));

            Result.Success<Result<Error>, Error>(Result.Failure(Error.Unexpected)).Flatten().ShouldBe(Result.Failure(Error.Unexpected));
            Result.Success<Result<int, Error>, Error>(Result.Failure<int, Error>(Error.Unexpected)).Flatten()
                .ShouldBe(Result.Failure<int, Error>(Error.Unexpected));
            Result.Success<Result<int?, Error>, Error>(Result.Failure<int?, Error>(Error.Unexpected)).Flatten()
                .ShouldBe(Result.Failure<int?, Error>(Error.Unexpected));
            Result.Success<Result<string, Error>, Error>(Result.Failure<string, Error>(Error.Unexpected)).Flatten()
                .ShouldBe(Result.Failure<string, Error>(Error.Unexpected));
        }

        private static void AssertThrow<TException>(Action action, bool shouldThrow)
            where TException : Exception
        {
            if (shouldThrow)
                action.ShouldThrow<TException>();
            else
                action.ShouldNotThrow();
        }
    }
}