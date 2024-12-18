namespace ResultPattern.src
{
    public interface ISimpleResult
    {
        bool IsError { get; }
        Error? Error { get; }
    }

    public interface ISimpleResult<T> : ISimpleResult
    {
        T Data { get; }
    }
}