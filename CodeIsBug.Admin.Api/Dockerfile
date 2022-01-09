#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 as base
WORKDIR /app

RUN ln -s /lib/x86_64-linux-gnu/libdl-2.24.so /lib/x86_64-linux-gnu/libdl.so
 
RUN echo "deb http://mirrors.aliyun.com/debian stretch main contrib non-free \
deb-src http://mirrors.aliyun.com/debian stretch main contrib non-free \
deb http://mirrors.aliyun.com/debian stretch-updates main contrib non-free \
deb-src http://mirrors.aliyun.com/debian stretch-updates main contrib non-free \
deb http://mirrors.aliyun.com/debian-security stretch/updates main contrib non-free \
deb-src http://mirrors.aliyun.com/debian-security stretch/updates main contrib non-free \
deb http://mirrors.aliyun.com/debian/ stretch-backports main non-free contrib \
deb http://mirrors.ustc.edu.cn/debian stable main contrib non-free \
deb-src http://mirrors.ustc.edu.cn/debian stable main contrib non-free \
deb http://mirrors.ustc.edu.cn/debian stable-proposed-updates main contrib non-free \
deb-src http://mirrors.ustc.edu.cn/debian stable-proposed-updates main contrib non-free \
deb-src http://mirrors.aliyun.com/debian/ stretch-backports main non-free contrib" > /etc/apt/sources.list
 
# RUN apt-get update
# RUN apt-get install -y --allow-unauthenticated libgdiplus libc6-dev libgdiplus libx11-dev && ln -s libgdiplus.so gdiplus.dll
# RUN rm -rf /var/lib/apt/lists/*

RUN apt-get update --fix-missing
RUN apt-get install libgdiplus -y && apt-get install libc6-dev -y && ln -s libgdiplus.so gdiplus.dll  


EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CodeIsBug.Admin.Api/CodeIsBug.Admin.Api.csproj", "CodeIsBug.Admin.Api/"]
COPY ["CodeIsBug.Admin.Services/CodeIsBug.Admin.Services.csproj", "CodeIsBug.Admin.Services/"]
COPY ["CodeIsBug.Admin.Common/CodeIsBug.Admin.Common.csproj", "CodeIsBug.Admin.Common/"]
COPY ["CodeIsBug.Admin.Models/CodeIsBug.Admin.Models.csproj", "CodeIsBug.Admin.Models/"]
RUN dotnet restore "CodeIsBug.Admin.Api/CodeIsBug.Admin.Api.csproj"
COPY . .
WORKDIR "/src/CodeIsBug.Admin.Api"
RUN dotnet build "CodeIsBug.Admin.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CodeIsBug.Admin.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CodeIsBug.Admin.Api.dll"]
