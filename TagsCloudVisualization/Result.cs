using System;

namespace TagsCloudVisualization
{
    public struct Result<T>
    {
        public Result(string errorMessage, string warnings="", T value = default(T))
        {

            ErrorMessage = errorMessage;
            Value = value;
            WarningsMessages = warnings;
        }
        public string ErrorMessage { get; }
        public  T Value { get; }
        public string WarningsMessages { get; }
        public bool IsSuccess => ErrorMessage==null;

    }

    public static class Result
    {
        public static Result<T> ReplaceError<T>(this Result<T> input, Func<string, string> replace) =>
            !input.IsSuccess ? Fail<T>(replace(input.ErrorMessage)) : input;
        public static Result<T> AddWarnings<T>(this Result<T> input, string warnings) =>
            new Result<T>(input.ErrorMessage, input.WarningsMessages + warnings, input.Value);

        public static Result<T> RefineError<T>(this Result<T> input, string postingResultsToDb) =>
            input.ReplaceError<T>(error => $"{postingResultsToDb}. {error}");
        public static Result<T> AsResult<T>(this T value) => Ok(value);

        public static Result<T> Ok<T>(T value) => new Result<T>(null, value: value);

        public static Result<T> Fail<T>(string e) => new Result<T>(e);

        public static Result<T> Warning<T>(string message, T value) => new Result<T>(null, $"\t{message}\n", value);

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
            !input.IsSuccess ? Fail<TOutput>(input.ErrorMessage).AddWarnings(input.WarningsMessages) :
            Of(() => continuation(input.Value)).AddWarnings(input.WarningsMessages);

        public static Result<TOutput> Then<TInput, TOutput>(this Result<TInput> input, Func<TInput, Result<TOutput>> continuation) =>
            !input.IsSuccess ? Fail<TOutput>(input.ErrorMessage).AddWarnings(input.WarningsMessages) :
            continuation(input.Value).AddWarnings(input.WarningsMessages);

        public static Result<TInput> OnFail<TInput>(this Result<TInput> input, Action<string> handleError)
        {
            if (!input.IsSuccess) handleError(input.ErrorMessage);
            return input;
        }
        public static Result<TInput> OnWarning<TInput>(this Result<TInput> input, Action<string> handleWarning)
        {
            if (!string.IsNullOrEmpty(input.WarningsMessages)) handleWarning(input.WarningsMessages);
            return input;
        }


    }
}