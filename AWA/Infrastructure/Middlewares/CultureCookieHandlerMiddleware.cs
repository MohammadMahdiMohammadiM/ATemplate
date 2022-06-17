using System.Linq;

namespace Infrastructure.Middlewares
{
    public class CultureCookieHandlerMiddleware : object
    {
        #region Static Member(s)
        public readonly static string CookieName = "Culture.Cookie";

        public static void SetCulture(string cultureName)
        {
            var cultureInfo = new System.Globalization.CultureInfo(name: cultureName);

            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }


        public static void CreateCookie
            (Microsoft.AspNetCore.Http.HttpContext httpContext, string cultureName)
        {
            var cookieOptions =
                new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Path = "/",

                    Secure = false,

                    HttpOnly = false,

                    IsEssential = false,

                    MaxAge = null,

                    Expires =
                        System.DateTimeOffset.UtcNow.AddYears(1),

                    SameSite =
                        Microsoft.AspNetCore.Http.SameSiteMode.Unspecified,
                };

            httpContext.Response.Cookies.Delete(key: CookieName);

            if (string.IsNullOrWhiteSpace(cultureName) == false)
            {
                cultureName =
                    cultureName.Substring(startIndex: 0, length: 2)
                    .ToLower()
                    ;

                httpContext.Response.Cookies.Append(key: CookieName,
                    value: cultureName, options: cookieOptions);
            }
        }
        #endregion /Static Member(s)

        public CultureCookieHandlerMiddleware
                    (Microsoft.AspNetCore.Http.RequestDelegate next) : base()

        {
            Next = next;
        }

        private Microsoft.AspNetCore.Http.RequestDelegate Next { get; }

        public async System.Threading.Tasks.Task InvokeAsync
            (Microsoft.AspNetCore.Http.HttpContext httpContext)
        {
            var cultureName =
                httpContext.Request.Cookies[key: CookieName]?
                [..2]
                .ToLower();

            switch (cultureName)
            {
                case "fa":
                case "en":
                    {
                        SetCulture(cultureName: cultureName);

                        break;
                    }
                default:
                    {
                        SetCulture(cultureName: "fa");

                        break;
                    }
            }

            await Next(context: httpContext);
        }
    }
}
