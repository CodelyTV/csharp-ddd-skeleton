FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /home/app
 
COPY . .
 
RUN dotnet restore ./apps/Mooc/Backend/Backend.csproj 
RUN dotnet publish ./apps/Mooc/Backend/Backend.csproj -o /publish/
 
WORKDIR /publish
 
ENTRYPOINT ["dotnet", "MoocApps.Backend.dll", "--urls", "http://*:8030"]