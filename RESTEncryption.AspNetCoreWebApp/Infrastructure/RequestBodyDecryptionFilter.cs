using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;
using System.Text;

namespace RESTEncryption.AspNetCoreWebApp.Infrastructure
{
    public class RequestBodyDecryptionFilter : IResourceFilter
    {
        private readonly EncryptionDecryptionHelper _encryptionDecryptionHelper;
        public RequestBodyDecryptionFilter(EncryptionDecryptionHelper encryptionDecryptionHelper)
        {
            _encryptionDecryptionHelper = encryptionDecryptionHelper;
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // We don't need this method here
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            try
            {
                var request = context.HttpContext.Request;
                if (request.Body.CanSeek)
                    request.Body.Position = 0;
                using StreamReader reader = new StreamReader(request.Body);
                var encryptedContent = reader.ReadToEndAsync().Result;
                var decryptedContent = _encryptionDecryptionHelper.DecryptString(encryptedContent);
                var newBody = Encoding.ASCII.GetBytes(decryptedContent);
                request.Body = new MemoryStream(newBody);

                if (request.Headers.ContainsKey("Content-Type"))
                    request.Headers["Content-Type"] = new Microsoft.Extensions.Primitives.StringValues("application/json");
                else
                    request.Headers.Add("Content-Type", new Microsoft.Extensions.Primitives.StringValues("application/json"));
            }
            catch
            {
                context.Result = new BadRequestResult();
            }
        }
    }
}
