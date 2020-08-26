#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["NetCoreEFDemo.Web/NetCoreEFDemo.Web.csproj", "NetCoreEFDemo.Web/"]
COPY ["NetCoreEFDemo.Domain/NetCoreEFDemo.Domain.csproj", "NetCoreEFDemo.Domain/"]
COPY ["NetCoreEFDemo.Infrastructure/NetCoreEFDemo.Infrastructure.csproj", "NetCoreEFDemo.Infrastructure/"]
COPY ["NetCoreEFDemo.Application/NetCoreEFDemo.Application.csproj", "NetCoreEFDemo.Application/"]
COPY ["NetCoreEFDemo.Utility/NetCoreEFDemo.Utility.csproj", "NetCoreEFDemo.Utility/"]
RUN dotnet restore "NetCoreEFDemo.Web/NetCoreEFDemo.Web.csproj"
COPY . .
WORKDIR "/src/NetCoreEFDemo.Web"
RUN dotnet build "NetCoreEFDemo.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetCoreEFDemo.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCoreEFDemo.Web.dll"]
