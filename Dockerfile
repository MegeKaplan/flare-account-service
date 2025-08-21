FROM mcr.microsoft.com/dotnet/sdk:9.0 AS builder

WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app
COPY --from=builder /app/publish .

ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_ENVIRONMENT=Production

EXPOSE 8080

CMD ["dotnet", "Flare.AccountService.dll"]