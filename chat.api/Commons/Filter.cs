using chat.api.Commons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ApiResponseWrapperFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception != null)
            return; // let exception middleware handle errors

        if (context.Result is ObjectResult objectResult)
        {
            var value = objectResult.Value;

            var apiResponse = new ApiResponse<object>(
                code: objectResult.StatusCode ?? 200,
                message: "Success",
                data: value
            );

            context.Result = new ObjectResult(apiResponse)
            {
                StatusCode = objectResult.StatusCode ?? 200
            };
        }
    }
}
