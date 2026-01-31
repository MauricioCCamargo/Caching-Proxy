using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

if (args.Contains("--clear-cache"))
{
    new src.Cache().ClearCache();
    Console.WriteLine("Cache cleared");
    Environment.Exit(2);
}

var listenPort = builder.Configuration.GetValue<int?>("port") ?? 5050;
var redirectUrl = builder.Configuration.GetValue<string>("origin") ?? "https://dummyjson.com";

var listenUrl = $"http://localhost:{listenPort}";

builder.WebHost.UseUrls(listenUrl);
var app = builder.Build();

new src.ApiHandler().CreateHandler(app, redirectUrl);

app.Lifetime.ApplicationStarted.Register(() =>
{
    Console.WriteLine($"Listening on {listenUrl}");
});

await app.RunAsync();