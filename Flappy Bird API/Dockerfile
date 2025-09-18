# --- Build stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything and restore dependencies
COPY . .
RUN dotnet restore

# Publish app in Release mode
RUN dotnet publish -c Release -o /app

# --- Runtime stage ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published files
COPY --from=build /app ./

# Railway sẽ inject biến môi trường PORT khi chạy container
ENV ASPNETCORE_URLS=http://+:${PORT}
ENV DOTNET_RUNNING_IN_CONTAINER=true

# Entry point (thay tên DLL đúng theo project bạn)
ENTRYPOINT ["dotnet", "FlappyBirdAPI.dll"]
