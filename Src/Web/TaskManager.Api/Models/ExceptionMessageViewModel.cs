namespace TaskManager.Api.Models
{
    using Microsoft.AspNetCore.Mvc.Filters;

    public class ExceptionMessageViewModel
    {
        public string StackTrace { get; set; }

        public string Message { get; set; }

        public ExceptionMessageViewModel(ExceptionContext context)
        {
            StackTrace = context.Exception.StackTrace;
            Message = context.Exception.Message;
        }
    }
}
