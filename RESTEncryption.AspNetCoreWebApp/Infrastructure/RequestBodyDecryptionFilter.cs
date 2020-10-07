using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;

namespace RESTEncryption.AspNetCoreWebApp.Infrastructure
{
    public class RequestBodyDecryptionFilter<T> : IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // We don't need this method here
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var request = context.HttpContext.Request;
            if (request.Body.CanSeek)
                request.Body.Position = 0;
            var reader = new StreamReader(request.Body);
            string content = reader.ReadToEndAsync().Result;
        }
    }
}
