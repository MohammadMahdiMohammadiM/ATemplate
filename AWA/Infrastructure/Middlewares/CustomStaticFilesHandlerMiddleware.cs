using Microsoft.AspNetCore.Http;

namespace Infrastructure.Middlewares
{
    public class CustomStaticFilesHandlerMiddleware : object
    {
        public CustomStaticFilesHandlerMiddleware
            (Microsoft.AspNetCore.Http.RequestDelegate next,
            Microsoft.Extensions.Hosting.IHostEnvironment hostEnvironment) : base()
        {
            Next = next;
            HostEnvironment = hostEnvironment;
        }

        private Microsoft.AspNetCore.Http.RequestDelegate Next { get; }

        private Microsoft.Extensions.Hosting.IHostEnvironment HostEnvironment { get; }

        public async System.Threading.Tasks.Task
          InvokeAsync(Microsoft.AspNetCore.Http.HttpContext httpContext)
        {
            string requestPath =
                httpContext.Request.Path;

            if (string.IsNullOrWhiteSpace(requestPath) || requestPath == "/")
            {
                await Next(httpContext);
                return;
            }
            if (requestPath.StartsWith("/") == false)
            {
                await Next(httpContext);
                return;
            }

            requestPath =
                requestPath[1..];

            var rootPath =
                HostEnvironment.ContentRootPath;

            var physicalPathName =
                System.IO.Path.Combine(path1: rootPath, path2: "wwwroot", path3: requestPath);

            if (System.IO.File.Exists(physicalPathName) == false)
            {
                await Next(httpContext);
                return;
            }

            string? fileExtension =
                System.IO.Path.GetExtension(physicalPathName)?.ToLower();

            switch (fileExtension)
            {
                case ".htm":
                case ".html":
                    {
                        httpContext.Response.StatusCode = 200;
                        httpContext.Response.ContentType = "text/html";
                        break;
                    }

                case "css":
                    {
                        httpContext.Response.StatusCode = 200;
                        httpContext.Response.ContentType = "text/css";
                        break;
                    }

                case ".js":
                    {
                        httpContext.Response.StatusCode = 200;
                        httpContext.Response.ContentType = "application/x-javascript";
                        break ;
                    }

                case ".jpg":
                case ".jpeg":
                    {
                        httpContext.Response.StatusCode = 200;
                        httpContext.Response.ContentType = "image/jpeg";
                        break;
                    }

                case ".txt":
                    {
                        httpContext.Response.StatusCode = 200;
                        httpContext.Response.ContentType = "text/plain";
                        break;
                    }

                default:
                    {
                        await Next(httpContext);
                        return;
                    }

            }

            await httpContext.Response
                .SendFileAsync(fileName: physicalPathName);
        }
    }
}




