using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EduControl;

public class ApiResult<T> : Result<T, string>, IConvertToActionResult
{
    private readonly int statusCode;

    public ApiResult(T value) : base(value)
    {
        statusCode = 200;
    }

    public ApiResult(string error, string explain, int statusCode) : base(error, explain)
    {
        this.statusCode = statusCode;
    }

    public static implicit operator ApiResult<T>(T value) => new(value);

    public IActionResult Convert()
    {
        return new ObjectResult(this)
        {
            DeclaredType = typeof (Result<T, string>),
            StatusCode = statusCode
        };
    }
}