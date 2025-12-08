# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Megoldás és csproj másolása
COPY *.sln .
COPY Tarkony-aspdotnet-backend/*.csproj ./Tarkony-aspdotnet-backend/
RUN dotnet restore

# Teljes projekt másolása és build
COPY Tarkony-aspdotnet-backend/. ./Tarkony-aspdotnet-backend/
WORKDIR /source/Tarkony-aspdotnet-backend
RUN dotnet publish -c Release -o /app 

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app ./
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production
ENV MongoDB__ConnectionURI=mongodb+srv://demo_user:ZqtMZnbTIJO1JrZ4@cluster0.fva9gn7.mongodb.net/
ENV MongoDB__DatabaseName=tarkony_asp
ENV MongoDB__CollectionName=items_data
ENV THIRDPARTYAPI__URL=https://api.tarkov.dev/graphql
ENV FRONTEND__URL=https://tarkony-bygtfddsfgebe5df.westeurope-01.azurewebsites.net
ENV CORS__AllowedOrigins=https://tarkony-bygtfddsfgebe5df.westeurope-01.azurewebsites.net
ENTRYPOINT ["dotnet", "Tarkony-aspdotnet-backend.dll"]