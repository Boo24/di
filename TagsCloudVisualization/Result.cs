using System;

namespace TagsCloudVisualization
{
    public struct Result<T>
    {
        public Result(string error, T value = default(T))
        {
            Error = error;
            Value = value;
        }
        public string Error { get; }
        public  T Value { get; }
        public bool IsSuccess => Error == null;
        public T GetValueOrThrow()
        {
            if (IsSuccess) return Value;
            throw new InvalidOperationException($"No value. Only Error {Error}");
        }
    }

    public static class Result
    {
        public static Result<T> ReplaceError<T>(this Result<T> input, Func<string, string> replace) =>
            !input.IsSuccess ? Fail<T>(replace(input.Error)) : input;

        public static Result<T> RefineError<T>(this Result<T> input, string postingResultsToDb) =>
            input.ReplaceError<T>(error => postingResultsToDb + ". " + error);
        public static Result<T> AsResult<T>(this T value) => Ok(value);

        public static Result<T> Ok<T>(T value) => new Result<T>(null, value);

        public static Result<T> Fail<T>(string e) => new Result<T>(e);

        public static Result<T> Of<T>(Func<T> f, string error = null)
        {
            try
            {
                return Ok(f());
            }
            catch (Exception e)
            {
                return Fail<T>(error ?? e.Message);
            }
        }
        public static Result<TOutput> Then<TInput, TOutput>(this Result<TInput> input, Func<TInput, TOutput> continuation) =>
            !input.IsSuccess ? Fail<TOutput>(input.Error) : Of(() => continuation(input.Value));

        public static Result<TOutput> Then<TInput, TOutput>(this Result<TInput> input, Func<TInput, Result<TOutput>> continuation) =>
            !input.IsSuccess ? Fail<TOutput>(input.Error) : continuation(input.Value);
        public static Result<TInput> OnFail<TInput>(this Result<TInput> input, Action<string> handleError)
        {
            if (!input.IsSuccess) handleError(input.Error);
            return input;
        }
    }
}