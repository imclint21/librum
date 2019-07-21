FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Librum/librum.csproj", "Librum/"]
RUN dotnet restore "Librum/librum.csproj"
COPY . .
WORKDIR "/src/Librum"
RUN dotnet build "librum.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "librum.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Librum.dll"]