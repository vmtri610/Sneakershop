#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["InvoiceService/InvoiceService.csproj", "InvoiceService/"]
RUN dotnet restore "InvoiceService/InvoiceService.csproj"
COPY . .
WORKDIR "/src/InvoiceService"
RUN dotnet build "InvoiceService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InvoiceService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# copy Invoice.db to /app/data
COPY ["InvoiceService/Invoice.db", "/app/data/"]
ENTRYPOINT ["dotnet", "InvoiceService.dll"]