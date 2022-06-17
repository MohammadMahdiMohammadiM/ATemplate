namespace Infrastructure.Middlewares
{
    public class GlobalEceptionHandlerMiddleware : object
    {
        public GlobalEceptionHandlerMiddleware
            (Microsoft.AspNetCore.Http.RequestDelegate next) : base()
        {
            Next = next;
        }

        private Microsoft.AspNetCore.Http.RequestDelegate Next { get; }


        public async System.Threading.Tasks.Task

            InvokeAsync(Microsoft.AspNetCore.Http.HttpContext httpContext)
        {
            try
            {
                await Next(httpContext);
            }
            catch (System.Exception)
            {
                httpContext.Response.Redirect(location: "Errors/Error" , permanent: false);
            }
        }
    }
}
