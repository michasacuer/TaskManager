namespace TaskManager.Api.Filters
{
    using System;
    using System.Net;
    using FluentValidation;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using TaskManager.Api.Models;
    using TaskManager.Common.Exceptions;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            if (context.Exception is ValidationException)
            {
                context.Result = new JsonResult(new ExceptionMessageViewModel(context));
            }
            else if (context.Exception is EntityNotFoundException)
            {
                context.Result = new JsonResult(new ExceptionMessageViewModel(context));
            }
            else if (context.Exception is EntityAlreadyExistsException)
            {
                context.Result = new JsonResult(new ExceptionMessageViewModel(context));
            }
            else if (context.Exception is UserHaveNoPermissionException)
            {
                context.Result = new JsonResult(new ExceptionMessageViewModel(context));
            }
            else if (context.Exception is InvalidUserException)
            {
                context.Result = new JsonResult(new ExceptionMessageViewModel(context));
            }
            else if (context.Exception is UserNotFoundException)
            {
                context.Result = new JsonResult(new ExceptionMessageViewModel(context));
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Result = new JsonResult(new ExceptionMessageViewModel(context));
            }
        }
    }
}
