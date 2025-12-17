FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
RUN ls
WORKDIR /src
RUN ls
COPY . .
RUN ls

COPY AspNet.Blog.Web/AspNet.Blog.Web.csproj ./AspNet.Blog.Web/AspNet.Blog.Web.csproj
RUN pwd
RUN ls
RUN dotnet restore AspNet.Blog.Web/AspNet.Blog.Web.csproj

COPY AspNet.Blog.Web ./AspNet.Blog.Web
RUN dotnet build AspNet.Blog.Web/AspNet.Blog.Web.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish AspNet.Blog.Web.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNet.Blog.Web.dll"]
