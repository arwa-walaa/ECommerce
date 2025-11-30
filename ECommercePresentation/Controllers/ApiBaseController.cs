using ECommerce.Shared.CommenResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePresentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ApiBaseController : ControllerBase
    {
        //handel request without value
        //if Resut Success  return No Content[204]
        //If Result failed return problem details with status code according to error type

        //handel request with value
        //if Resut Success 
        //If Result failed 


        protected IActionResult HandelResult(Result result)
        {
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return ProblemDetailsFromErrors(result.Errors);
        }
        protected ActionResult<T> HandelResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result.Valuet);
            }
            return ProblemDetailsFromErrors(result.Errors);
        }

        private ActionResult ProblemDetailsFromErrors(IReadOnlyList<Error> errors)
        {

            if (errors.Count == 0)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError,
                    title: "Internal server Error",
                    detail: "UnExpected Error Occured");
            }
            if (errors.All(E => E.Type == ErrorType.Validation))
            {
                return HandelValidationProblem(errors);


            }
            return HandelSingleErrorProblem(errors[0]);
        }   
        private ActionResult HandelValidationProblem(IReadOnlyList<Error> errors)
        {
            var ModelState = new ModelStateDictionary();
            foreach (var error in errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return ValidationProblem(ModelState);
        }
    
        private ActionResult HandelSingleErrorProblem(Error error)
        {
            return Problem(
                title: error.Code,
                detail: error.Description,
                type: error.Type.ToString(),
                statusCode: MapErrorTypeToStatusCode(error.Type)

                );
        }

        private static int MapErrorTypeToStatusCode(ErrorType errorType)
        {
            return errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.InvalidCredentials => StatusCodes.Status401Unauthorized,
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError,
            };
        }

        protected string? GetEmailFromToken()
        {
            return User.FindFirstValue(ClaimTypes.Email);
        }

    }
}
