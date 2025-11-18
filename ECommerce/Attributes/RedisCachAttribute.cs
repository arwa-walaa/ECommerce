using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.Attributes
{
    public class RedisCachAttribute : ActionFilterAttribute
    {


        //async version after or before 
        public override  Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return base.OnActionExecutionAsync(context, next);
        }


    }
}
