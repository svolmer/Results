using Results.Extensions;
using Shouldly;

namespace Results.Tests
{
    public class LinqExtensionsTests
    {
        [Fact]
        public void Result_Select()
        {
            Result.Success<Error>().Select(() => 1).GetValueOrDefault().ShouldBe(1);
            Result.Failure(Error.Unexpected).Select(() => 1).GetErrorOrDefault().ShouldBe(Error.Unexpected);

            Result.Success<Error>().Select(() => { }).ShouldBe(Result.Success<Error>());
            Result.Failure(Error.Unexpected).Select(() => { }).GetErrorOrDefault().ShouldBe(Error.Unexpected);
        }

        [Fact]
        public void ResultValue_Select()
        {
            Result.Success<int, Error>(1).Select(x => x + 1).GetValueOrDefault().ShouldBe(2);
            Result.Success<int?, Error>(1).Select(x => x.HasValue ? x.Value + 1 : -1).GetValueOrDefault().ShouldBe(2);
            Result.Success<int?, Error>(null).Select(x => x.HasValue ? x.Value + 1 : -1).GetValueOrDefault().ShouldBe(-1);
            Result.Success<string, Error>(Constants.String1).Select(x => x.ToUpper()).GetValueOrDefault(string.Empty).ShouldBe(Constants.String1.ToUpper());

            (from x in Result.Success<int, Error>(1) select x + 1).GetValueOrDefault().ShouldBe(2);
            (from x in Result.Success<int?, Error>(1) select x.HasValue ? x.Value + 1 : -1).GetValueOrDefault().ShouldBe(2);
            (from x in Result.Success<int?, Error>(null) select x.HasValue ? x.Value + 1 : -1).GetValueOrDefault().ShouldBe(-1);
            (from x in Result.Success<string, Error>(Constants.String1) select x.ToUpper()).GetValueOrDefault(string.Empty)
                .ShouldBe(Constants.String1.ToUpper());

            Result.Failure<int, Error>(Error.Unexpected).Select(x => x + 1).GetErrorOrDefault().ShouldBe(Error.Unexpected);
            Result.Failure<int?, Error>(Error.Unexpected).Select(x => x.HasValue ? x.Value + 1 : -1).GetErrorOrDefault().ShouldBe(Error.Unexpected);
            Result.Failure<string, Error>(Error.Unexpected).Select(x => x.ToUpper()).GetErrorOrDefault().ShouldBe(Error.Unexpected);

            (from x in Result.Failure<int, Error>(Error.Unexpected) select x + 1).GetErrorOrDefault().ShouldBe(Error.Unexpected);
            (from x in Result.Failure<int?, Error>(Error.Unexpected) select x.HasValue ? x.Value + 1 : -1).GetErrorOrDefault()
                .ShouldBe(Error.Unexpected);
            (from x in Result.Failure<string, Error>(Error.Unexpected) select x.ToUpper()).GetErrorOrDefault().ShouldBe(Error.Unexpected);

            Result.Success<int, Error>(1).Select(_ => { }).ShouldBe(Result.Success<Error>());
            Result.Success<int?, Error>(1).Select(_ => { }).ShouldBe(Result.Success<Error>());
            Result.Success<int?, Error>(null).Select(_ => { }).ShouldBe(Result.Success<Error>());
            Result.Success<string, Error>(Constants.String1).Select(_ => { }).ShouldBe(Result.Success<Error>());

            Result.Failure<int, Error>(Error.Unexpected).Select(_ => { }).GetErrorOrDefault().ShouldBe(Error.Unexpected);
            Result.Failure<int?, Error>(Error.Unexpected).Select(_ => { }).GetErrorOrDefault().ShouldBe(Error.Unexpected);
            Result.Failure<string, Error>(Error.Unexpected).Select(_ => { }).GetErrorOrDefault().ShouldBe(Error.Unexpected);
        }

        [Fact]
        public void Result_Where()
        {
            Result.Success<Error>().Select(() => { }).Where(() => true).ShouldBe(Result.Success<Error>());
            Result.Success<Error>().Select(() => { }).Where(() => false).ShouldBe(Result.Success<Error>());

            Result.Failure(Error.Unexpected).Select(() => { }).Where(() => true).GetErrorOrDefault().ShouldBe(Error.Unexpected);
            Result.Failure(Error.Unexpected).Select(() => { }).Where(() => false).GetErrorOrDefault().ShouldBe(Error.Unexpected);
        }

        [Fact]
        public void ResultValue_Where()
        {
            Result.Success<int, Error>(1).Where(x => x > 0).GetValueOrDefault().ShouldBe(1);
            Result.Success<int?, Error>(1).Where(x => x.HasValue).GetValueOrDefault().ShouldBe(1);
            Result.Success<int?, Error>(null).Where(x => x is null).GetValueOrDefault().ShouldBe(null);
            Result.Success<string, Error>(Constants.String1).Where(x => !string.IsNullOrEmpty(x)).GetValueOrDefault(string.Empty).ShouldBe(Constants.String1);

            (from x in Result.Success<int, Error>(1) where x > 0 select x).GetValueOrDefault().ShouldBe(1);
            (from x in Result.Success<int?, Error>(1) where x.HasValue select x).GetValueOrDefault().ShouldBe(1);
            (from x in Result.Success<int?, Error>(null) where x is null select x).GetValueOrDefault().ShouldBe(null);
            (from x in Result.Success<string, Error>(Constants.String1) where !string.IsNullOrEmpty(x) select x).GetValueOrDefault(string.Empty)
                .ShouldBe(Constants.String1);

            Result.Success<int, Error>(1).Where(x => x < 0).GetValueOrDefault(-1).ShouldBe(-1);
            Result.Success<int?, Error>(1).Where(x => !x.HasValue).GetValueOrDefault(-1).ShouldBe(-1);
            Result.Success<int?, Error>(null).Where(x => x is not null).GetValueOrDefault(-1).ShouldBe(-1);
            Result.Success<string, Error>(Constants.String1).Where(x => string.IsNullOrEmpty(x)).GetValueOrDefault("None").ShouldBe("None");

            (from x in Result.Success<int, Error>(1) where x < 0 select x).GetValueOrDefault(-1).ShouldBe(-1);
            (from x in Result.Success<int?, Error>(1) where !x.HasValue select x).GetValueOrDefault(-1).ShouldBe(-1);
            (from x in Result.Success<int?, Error>(null) where x is not null select x).GetValueOrDefault(-1).ShouldBe(-1);
            (from x in Result.Success<string, Error>(Constants.String1) where string.IsNullOrEmpty(x) select x).GetValueOrDefault("None").ShouldBe("None");

            Result.Failure<int, Error>(Error.Unexpected).Where(x => x > 0).GetErrorOrDefault().ShouldBe(Error.Unexpected);
            Result.Failure<int?, Error>(Error.Unexpected).Where(x => x.HasValue).GetErrorOrDefault().ShouldBe(Error.Unexpected);
            Result.Failure<string, Error>(Error.Unexpected).Where(x => !string.IsNullOrEmpty(x)).GetErrorOrDefault().ShouldBe(Error.Unexpected);

            Result.Failure<int, Error>(Error.Unexpected).Where(x => x < 0).GetErrorOrDefault().ShouldBe(Error.Unexpected);
            Result.Failure<int?, Error>(Error.Unexpected).Where(x => !x.HasValue).GetErrorOrDefault().ShouldBe(Error.Unexpected);
            Result.Failure<string, Error>(Error.Unexpected).Where(x => string.IsNullOrEmpty(x)).GetErrorOrDefault().ShouldBe(Error.Unexpected);
        }
    }
}