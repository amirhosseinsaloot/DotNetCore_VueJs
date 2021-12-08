using Core.Utilities;

namespace Core.Response;

public class ApiResponse
{
    public bool IsSuccess { get; set; }

    public ApiResultBodyCode Code { get; set; }

    public string Message { get; set; }

    public ApiResponse(bool isSuccess, ApiResultBodyCode apiResultBodyCode, string message = null)
    {
        IsSuccess = isSuccess;
        Code = apiResultBodyCode;
        Message = message ?? apiResultBodyCode.GetDisplayName();
    }
}

public class ApiResponse<TData> : ApiResponse where TData : class
{
    public TData Data { get; set; }

    public ApiResponse(bool isSuccess, ApiResultBodyCode apiResultBodyCode, TData data, string message = null)
       : base(isSuccess, apiResultBodyCode, message)
    {
        Data = data;
    }
}
