namespace EquipLease.Infrastructure.Common;

public class Result<TResponse>
{
    public TResponse? Response { get; }
    public string Error { get; }
    public bool IsSuccess { get; }
    
    private Result(TResponse response)
    {
        Response = response;
        Error = string.Empty;
        IsSuccess = true;
    }

    private Result(string error)
    {
        Error = error;
        Response = default;
        IsSuccess = false;
    }
    
    public static Result<TResponse> Success(TResponse response)
    {
        return new Result<TResponse>(response);
    }
    
    public static Result<TResponse> Failure(string error)
    {
        return new Result<TResponse>(error);
    }
}