using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Middlewares
{
    public static class ExtensionMethods
    {
        static ExtensionMethods()
        {
        }

        public static Microsoft.AspNetCore.Builder.IApplicationBuilder

            UseCustomStaticFiles(this Microsoft.AspNetCore.Builder.IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomStaticFilesHandlerMiddleware>();
        }

        public static Microsoft.AspNetCore.Builder.IApplicationBuilder

            UseGlobalException(this Microsoft.AspNetCore.Builder.IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalEceptionHandlerMiddleware>();
        }

        public static Microsoft.AspNetCore.Builder.IApplicationBuilder

           UseCultureCookieHandlerMiddlware(this Microsoft.AspNetCore.Builder.IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CultureCookieHandlerMiddleware>();
        }
    }
}
