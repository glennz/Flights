namespace App.Filters
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Filters;

    /// <summary>
    /// Control the invalid operation message to output to ui.
    /// </summary>
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception.GetType() == typeof(InvalidOperationException))
            {
                var exceptionResponse = new ExceptionResponse
                {
                    Message = context.Exception.Message
                };

                context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest, exceptionResponse);
            }

            if (context.Exception.GetType() == typeof(NullReferenceException))
            {
                var exceptionResponse = new ExceptionResponse
                {
                    Message = context.Exception.Message
                };

                context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest, exceptionResponse);
            }
        }
    }
}