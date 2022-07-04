using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Presentation.Response;
using System.Text.Json;

namespace Presentation.ActionFilters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            object response;
            //In case is a validation exception we are creating the coresponding response
            if (context.Exception is ValidationException apiValidationException)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                response = GetResponsePayloadFromValidationException(apiValidationException);
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response = new GenericErrorPayload { Message = context.Exception.Message, Status = context.HttpContext.Response.StatusCode.ToString(), Code = "100" };

            }

            context.Result = new JsonResult(response, new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            base.OnException(context);
        }

        private BadRequestValidationErrorPayload GetResponsePayloadFromValidationException(ValidationException ex)
        {
            BadRequestValidationErrorPayload respPayload =
                new BadRequestValidationErrorPayload
                {
                    Code = "1000",
                    Status = StatusCodes.Status400BadRequest.ToString(),
                    Message = "Error while processing request!",
                    Errors = new List<ValidationErrorElement>()
                };
            foreach (ValidationFailure f in ex.Errors)
            {
                ValidationErrorElement el = new ValidationErrorElement
                {
                    Code = f.ErrorCode,
                    Message = f.ErrorMessage,
                    Field = f.FormattedMessagePlaceholderValues.GetValueOrDefault("PropertyName").ToString()
                };
                respPayload.Errors.Add(el);
            }

            return respPayload;
        }
    }
}
