FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish

EXPOSE 80
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
WORKDIR bin/Debug/net6.0/publish
ENTRYPOINT ["dotnet", "etmDiaryApi.dll"]