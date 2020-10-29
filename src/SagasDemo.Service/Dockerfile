#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SagasDemo.Service/SagasDemo.Service.csproj", "SagasDemo.Service/"]
COPY ["SagasDemo.Infrastructure/SagasDemo.Infrastructure.csproj", "SagasDemo.Infrastructure/"]
COPY ["SagasDemo.Domain/SagasDemo.Domain.csproj", "SagasDemo.Domain/"]
RUN dotnet restore "SagasDemo.Service/SagasDemo.Service.csproj"
COPY . .
WORKDIR "/src/SagasDemo.Service"
RUN dotnet build "SagasDemo.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SagasDemo.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SagasDemo.Service.dll"]
