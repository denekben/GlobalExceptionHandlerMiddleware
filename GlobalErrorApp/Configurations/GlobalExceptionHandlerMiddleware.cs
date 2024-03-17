using GlobalErrorApp.Exceptions;
using System.Net;
using System.Text.Json;

using KeyNotFoundException = GlobalErrorApp.Exceptions.KeyNotFoundException;
using NotImplementedException = GlobalErrorApp.Exceptions.NotImplementedException;
using UnauthorizedAccessException = GlobalErrorApp.Exceptions.UnauthorizedAccessException;


namespace GlobalErrorApp.Configurations {
    public class GlobalExceptionHandlerMiddleware {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext context) {
            try {
                await _next(context);
            }
            catch (Exception ex) {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex) {
            HttpStatusCode status;
            var stackTrace = string.Empty;
            string message = "";
            
            var exceptionType = ex.GetType();

            if(exceptionType == typeof(BadRequestException)) {
                message = ex.Message;
                status = HttpStatusCode.BadRequest;
                stackTrace = ex.StackTrace;
            }
            else if(exceptionType==typeof(NotFoundException)) {
                message = ex.Message;
                status = HttpStatusCode.NotFound;
                stackTrace = ex.StackTrace;
            }
            else if (exceptionType == typeof(NotImplementedException)) {
                message = ex.Message;
                status = HttpStatusCode.NotImplemented;
                stackTrace = ex.StackTrace;
            }
            else if (exceptionType == typeof(KeyNotFoundException)) {
                message = ex.Message;
                status = HttpStatusCode.NotFound;
                stackTrace = ex.StackTrace;
            }
            else if (exceptionType == typeof(UnauthorizedAccessException)) {
                message = ex.Message;
                status = HttpStatusCode.Unauthorized;
                stackTrace = ex.StackTrace;
            }
            else {
                message = ex.Message;
                status = HttpStatusCode.InternalServerError;
                stackTrace = ex.StackTrace;
            }

            var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(exceptionResult);

        }
    }
}
