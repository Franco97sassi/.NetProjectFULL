FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Services/Loans/Loans.Api/Loans.Api.csproj"
RUN dotnet publish "Services/Loans/Loans.Api/Loans.Api.csproj" -c Release -o /app/publish --no-restore


FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Loans.Api.dll"]
