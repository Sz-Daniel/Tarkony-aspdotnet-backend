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

# Itt állítod át, hogy a 80‑as porton induljon
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production
ENV MongoDB__ConnectionURI=mongodb+srv://demo_user:Ylugd4eYGYci4AZp@cluster0.fva9gn7.mongodb.net/
ENV MongoDB__DatabaseName=tarkony_asp
ENV MongoDB__CollectionName=items_data
ENTRYPOINT ["dotnet", "Tarkony-aspdotnet-backend.dll"]
