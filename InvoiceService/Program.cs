using InvoiceService.Models;
using InvoiceService.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
// db sqlite at /app/data/Invoice.db



builder.Services.AddGrpc();
builder.Services.AddScoped<InvoiceContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<InvoiceService.Services.InvoiceService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
