using ImageCompress.AccountSQL.DBContents;
using ImageCompress.AccountSQL.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// builder.WebHost.ConfigureKestrel(options =>
// {
//     // Setup a HTTP/2 endpoint without TLS.
//     options.ListenLocalhost(5243, o => o.Protocols =
//         HttpProtocols.Http2);
// });

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSingleton<SecertService>();
builder.Services.AddDbContext<PostgresContext>(options =>
{
    var secertService = builder.Services.BuildServiceProvider().GetService<SecertService>();
    options.UseNpgsql(secertService!.GetSecret("imagecompress-393703", "postgresql-connectstring"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<AccountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
