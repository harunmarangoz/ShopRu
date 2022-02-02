namespace Core.Utilities.Results;

public class ApiResult<T> : ApiResult
{
    public T Data { get; }

    public ApiResult(IDataResult<T> result) : base(result)
    {
        Data = result.Data;
    }
}

public class ApiResult
{
    public ApiResult(IResult result)
    {
        Success = result.Success;
        Message = result.Message;
    }

    public bool Success { get; set; }
    public string Message { get; set; }
}