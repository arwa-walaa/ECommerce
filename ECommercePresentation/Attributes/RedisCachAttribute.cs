using ECommerceServiceApstarction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePresentation.Attributes
{
    public class RedisCachAttribute : ActionFilterAttribute
    {
        private readonly int _durationInMin;

        public RedisCachAttribute(int DurationInMin = 5)
        {
            _durationInMin = DurationInMin;
        }

        //async version after or before 
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //get cach servce 
            var cachService = context.HttpContext.RequestServices.GetRequiredService<ICachService>();
            //craete the cach key from the request
            var cachKey = CreateCachKey(context.HttpContext.Request);
            //check if cach exist
            var cachData = await cachService.GetAsync(cachKey);

            //if exist return the cach data
            if (cachData is not null)
            {
                //create the response from the cach data
                context.Result = new ContentResult()
                {
                    Content = cachData,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            //if not exist run the action
            var executedContext = await next.Invoke();
            if (executedContext.Result is OkObjectResult result)
            {
                //set the cach 
                await cachService.SetAsync(cachKey, result.Value!, TimeSpan.FromMinutes(5));
            }
            //get the result from the action


            //if not exist run the action

        }

        private string CreateCachKey(HttpRequest request)
        {
            StringBuilder keyBuilder = new StringBuilder();
            keyBuilder.Append(request.Path);
            foreach (var item in request.Query.OrderBy(X => X.Key))
            {
                keyBuilder.Append($"|{item.Key}-{item.Value}");
            }
            return keyBuilder.ToString();

        }


    }
}
