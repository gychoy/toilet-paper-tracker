#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ToiletPaperTracker.BlazorServer/ToiletPaperTracker.BlazorServer.csproj", "ToiletPaperTracker.BlazorServer/"]
COPY ["ToiletPaperTracker.Core/ToiletPaperTracker.Core.csproj", "ToiletPaperTracker.Core/"]
COPY ["ToiletPaperTracker.Contracts/ToiletPaperTracker.Contracts.csproj", "ToiletPaperTracker.Contracts/"]
RUN dotnet restore "ToiletPaperTracker.BlazorServer/ToiletPaperTracker.BlazorServer.csproj"
COPY . .
WORKDIR "/src/ToiletPaperTracker.BlazorServer"
RUN dotnet build "ToiletPaperTracker.BlazorServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToiletPaperTracker.BlazorServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToiletPaperTracker.BlazorServer.dll"]