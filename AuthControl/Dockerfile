FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["AuthControl.csproj", "./"]
RUN dotnet restore "AuthControl.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "AuthControl.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthControl.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet AuthControl.dll

