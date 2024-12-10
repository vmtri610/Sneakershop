using Grpc.Net.Client;
using OrderService.Models;
using OrderService.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddScoped<OrderContext>();

using var productChannel = GrpcChannel.ForAddress("http://product.sneakershop");
var productClient = new ProductService.Product.ProductClient(productChannel);
builder.Services.Add(ServiceDescriptor.Singleton(typeof(ProductService.Product.ProductClient), productClient));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<OrderService.Services.OrderService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
