using backend.Database;
using backend.Infrastructure;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthorization();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IDatabaseApplication, DatabaseApplication>();

ThreadPool.SetMinThreads(100, 100);

builder.WebHost.UseKestrel().ConfigureKestrel(options =>
{
    options.ListenAnyIP(8081, ListenOptions =>
    {
        ListenOptions.Protocols = HttpProtocols.Http2;
    });
    options.ListenAnyIP(8080, ListenOptions =>
    {
        ListenOptions.Protocols = HttpProtocols.Http1;
    });
});

var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.UseAuthorization();

await app.RunAsync();
