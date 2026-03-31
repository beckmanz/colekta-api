FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["colekta-api/colekta-api.csproj", "colekta-api/"]
RUN dotnet restore "colekta-api/colekta-api.csproj"

COPY . .
WORKDIR "/src/colekta-api"
RUN dotnet publish "colekta-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "colekta-api.dll"]

