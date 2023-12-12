#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src
COPY ["GrpcMicroService/GrpcMicroService.csproj", "GrpcMicroService/"]
COPY ["GrpcMicroServiceStore/GrpcMicroServiceStore.csproj", "GrpcMicroServiceStore/"]
RUN dotnet restore "GrpcMicroService/GrpcMicroService.csproj"
COPY . .
WORKDIR "/src/GrpcMicroService"
RUN dotnet build "GrpcMicroService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GrpcMicroService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GrpcMicroService.dll"]