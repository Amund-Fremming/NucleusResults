namespace ResultPattern.src
{
    public interface IResult
    {
        bool IsSuccess { get; }
        string Message { get; }
        Exception? Exception { get; }
    }

    public interface IResult<T> : IResult
    {
        T Data { get; }
    }
}