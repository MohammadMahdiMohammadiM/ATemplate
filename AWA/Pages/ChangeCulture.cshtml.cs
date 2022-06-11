using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Infrastructure.Middlewares;

namespace AWA.Pages
{
    public class ChangeCultureModel : Infrastructure.BasePageModel
    {
        public Microsoft.AspNetCore.Mvc.IActionResult OnGet(string name)
        {
            var typedHeaders =
                HttpContext.Request.GetTypedHeaders();

            var httpReferer =
                typedHeaders?.Referer?.AbsoluteUri;

            if (string.IsNullOrWhiteSpace(httpReferer))
            {
                return RedirectToPage(pageName: "/Index");
            }

            string defaultCulture = "fa";

            if (string.IsNullOrEmpty(name))
            {
                name =
                    defaultCulture;
            }

            name =
                name
                .Replace(" ", string.Empty)
                .ToLower()
                ;

            switch (name)
            {
                case "fa":
                case "en":
                    {
                        break;
                    }

                default:
                    {
                        name =
                            defaultCulture;

                        break;
                    }
            }

            Infrastructure.Middlewares.CultureCookieHandlerMiddleware
                .SetCulture(cultureName: name);

            Infrastructure.Middlewares.CultureCookieHandlerMiddleware
                .CreateCookie(httpContext: HttpContext, cultureName: name);
            
            return Redirect(url: httpReferer);
        }
    }
}
