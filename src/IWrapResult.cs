namespace WrapResults.src
{
    public interface IWrapResult
    {
        bool IsError { get; }
        Error? Error { get; }
    }

    public interface IWrapResult<T> : IWrapResult
    {
        T Data { get; }
    }
}