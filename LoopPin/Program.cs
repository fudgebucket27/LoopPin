using LoopPin.Models;
using LoopPin.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<IPinataService, PinataService>();
builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    ApiKeyHelper._apiKey = Environment.GetEnvironmentVariable("APPSETTING_PINATAAPIKEY");
    ApiKeyHelper._apiKeySecret = Environment.GetEnvironmentVariable("APPSETTING_PINATAAPIKEYSECRET");
}
else
{
    ApiKeyHelper._apiKey = Environment.GetEnvironmentVariable("PINATAAPIKEY", EnvironmentVariableTarget.Machine);
    ApiKeyHelper._apiKeySecret = Environment.GetEnvironmentVariable("PINATAAPIKEYSECRET", EnvironmentVariableTarget.Machine);
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();