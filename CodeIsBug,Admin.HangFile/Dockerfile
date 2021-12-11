#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CodeIsBug,Admin.HangFile/CodeIsBug.Admin.Hangfire.csproj", "CodeIsBug,Admin.HangFile/"]
RUN dotnet restore "CodeIsBug,Admin.HangFile/CodeIsBug.Admin.Hangfire.csproj"
COPY . .
WORKDIR "/src/CodeIsBug,Admin.HangFile"
RUN dotnet build "CodeIsBug.Admin.Hangfire.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CodeIsBug.Admin.Hangfire.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CodeIsBug.Admin.Hangfire.dll"]