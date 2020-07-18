using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Volunteer.SharedObjects.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Volunteer.SharedObjects
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = null;
                var statusCode = 500;
                if(ex is FluentValidation.ValidationException vex)
                {
                    statusCode = 400;
                    errorResponse = new ErrorResponse($"{ex.Message}");
                }

                var result = JsonConvert.SerializeObject(errorResponse ?? new ErrorResponse($"Internal server error, msg: {ex.Message}"), new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                context.Response.ContentType = "application / json";
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(result);
            }
        }
    }
}
