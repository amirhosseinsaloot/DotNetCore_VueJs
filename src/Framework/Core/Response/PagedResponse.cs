using Core.Enums;

namespace Core.Response;

public class PagedResponse<TData> : ApiResponse<TData> where TData : class
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public Uri FirstPage { get; set; }

    public Uri LastPage { get; set; }

    public int TotalPages { get; set; }

    public int TotalRecords { get; set; }

    public Uri NextPage { get; set; }

    public Uri PreviousPage { get; set; }

    public PagedResponse(int pageNumber, int pageSize, bool isSuccess, ApiResultBodyCode apiResultBodyCode, TData data, string message = null)
        : base(isSuccess, apiResultBodyCode, data, message)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
