using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var webApplicationOptions =
    new Microsoft.AspNetCore.Builder.WebApplicationOptions
    {
        EnvironmentName =
            Microsoft.Extensions.Hosting.Environments.Development,

        //EnvironmentName =
        //	Microsoft.Extensions.Hosting.Environments.Production,
    };

var builder =
    Microsoft.AspNetCore.Builder
    .WebApplication.CreateBuilder(options: webApplicationOptions);

builder.Services.AddRazorPages();

var app =
    builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Errors/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

//app.UseAuthentication();

//app.UseAuthorization();

app.MapRazorPages();

app.Run();
