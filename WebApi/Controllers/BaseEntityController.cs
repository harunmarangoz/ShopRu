using Core.Utilities.Results;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebApi.Controllers;

[ApiController]
public class BaseController : Controller
{
    protected IActionResult Ok(IResult result)
    {
        if (result.Success) return base.Ok(new ApiResult(result));
        return BadRequest(new ApiResult(result));
    }

    protected IActionResult Ok<T>(IDataResult<T> result)
    {
        if (result.Success) return base.Ok(new ApiResult<T>(result));
        return BadRequest(new ApiResult<T>(result));
    }
}