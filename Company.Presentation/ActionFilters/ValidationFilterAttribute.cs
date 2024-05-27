using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Company.Presentation.ActionFilters;

public class ValidationFilterAttribute : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.RouteData.Values["action"];
        var controller = context.RouteData.Values["controller"];

        var param = context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;
        if (param is null)
        {
            context.Result = new BadRequestObjectResult($"Объект равен null. Контроллер: {controller}, action: {action}");
            return;
        }

        if (!context.ModelState.IsValid)
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
    }
}
