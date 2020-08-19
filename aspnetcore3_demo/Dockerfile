FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["/aspnetcore3.1-demo.csproj", "/"]
RUN dotnet restore "/aspnetcore3.1-demo.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "aspnetcore3.1-demo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "aspnetcore3.1-demo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "aspnetcore3.1-demo.dll"]
