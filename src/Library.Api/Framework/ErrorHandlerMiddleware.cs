using Library.Core.Models;
using Library.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Library.Api.Framework
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(context, exception);
            }
        }

        private static Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var errorCode = "error";
            var exceptionType = exception.GetType();
            var statusCode = HttpStatusCode.InternalServerError;
            switch (exception)
            {
                case DomainException e when exceptionType == typeof(DomainException):
                    statusCode = HttpStatusCode.BadRequest;
                    errorCode = e.Code;
                    break;
                case ServiceException e when exceptionType == typeof(ServiceException):
                    errorCode = e.Code;
                    statusCode = (errorCode == ServiceErrorCodes.DoesNotExist) ?  HttpStatusCode.NotFound : HttpStatusCode.BadRequest;
                    break;
                case Exception _ when exceptionType == typeof(UnauthorizedAccessException):
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case Exception _ when exceptionType == typeof(ArgumentException):
                    statusCode = HttpStatusCode.BadRequest; 
                    break;
                case Exception _ when exceptionType == typeof(Exception):
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }
            var response = new 
            { 
                code = errorCode,
                message = exception.Message
            };
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(payload);
        }
    }
}
