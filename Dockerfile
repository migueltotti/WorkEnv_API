# Step 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY *.sln ./
COPY ["WorkEnv.API/WorkEnv.API.csproj", "WorkEnv.API/"]
COPY ["WorkEnv.CrossCutting/WorkEnv.CrossCutting.csproj", "WorkEnv.CrossCutting/"]
COPY ["WorkEnv.Application/WorkEnv.Application.csproj", "WorkEnv.Application/"]
COPY ["WorkEnv.Domain/WorkEnv.Domain.csproj", "WorkEnv.Domain/"]
COPY ["WorkEnv.Infrastructure/WorkEnv.Infrastructure.csproj", "WorkEnv.Infrastructure/"]
RUN dotnet restore "WorkEnv.API/WorkEnv.API.csproj"
COPY . .
WORKDIR "/app/WorkEnv.API"
RUN dotnet build "WorkEnv.API.csproj" -c Release -o /app/build

# Step 2: publish
FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish 

# Step 3: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_HTTP_PORTS=8080
#ENV ASPNETCORE_HTTP_PORTS=443
EXPOSE 8080
#EXPOSE 443
ENTRYPOINT ["dotnet", "WorkEnv.API.dll"]