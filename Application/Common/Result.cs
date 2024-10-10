

namespace Application.Common;
public class Result<TData> : Result
{
    public TData? Data { get; set; }
    public static Result<TData> Success
        (TData? data,
        string? Message = null)
    {
        return new Result<TData>()
        {
            Data = data,
            Message = Message,
            IsSuccess = true
        };
    }
    public static Result<TData> Fail
        (TData? data,
    string? Message = null,
        FailType? Type = null)
    {
        return new Result<TData>()
        {
            Data = data,
            Message = Message,
            IsSuccess = false,
            Type = Type
        };
    }


    public static Result<TData> Fail
       (TData? data
   )
    {
        return new Result<TData>()
        {
            Data = data,
            Message = string.Empty,
            IsSuccess = false,
            Type = null
        };
    }
}
public class Result
{
    public FailType? Type { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public static Result Success(string? Message = null)
    {
        return new Result()
        {

            Message = Message,
            IsSuccess = true
        };
    }
    public static Result Fail(string? Message = null, FailType? Type = null)
    {
        return new Result()
        {

            Message = Message,
            IsSuccess = false,
            Type = Type
        };
    }
}