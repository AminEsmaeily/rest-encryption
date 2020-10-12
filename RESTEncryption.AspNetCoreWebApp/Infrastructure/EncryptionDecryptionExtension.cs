using Microsoft.Extensions.DependencyInjection;

namespace RESTEncryption.AspNetCoreWebApp.Infrastructure
{
    public static class EncryptionDecryptionExtension
    {
        public static IServiceCollection AddEncryptionDecryptionHelper(this IServiceCollection services, EncryptionDecryptionConfig config)
        {
            services.AddTransient(f =>
            {
                return new EncryptionDecryptionHelper(config.Key);
            });
            return services;
        }
    }

}
