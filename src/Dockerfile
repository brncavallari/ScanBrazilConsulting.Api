# Use a imagem base do SDK do .NET para construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Instale o OpenSSL
RUN apt-get update && apt-get install -y openssl

# Copie o arquivo .csproj e restaure as dependências
COPY *.csproj .  
RUN dotnet restore  

# Copie o restante dos arquivos do projeto  
COPY . .  

# Gerar o certificado autoassinado  
RUN mkdir -p /app/certs && \
    openssl req -x509 -newkey rsa:4096 -keyout /app/certs/localhost.key -out /app/certs/localhost.crt -days 365 -nodes -subj "/CN=localhost" && \
    openssl pkcs12 -export -out /app/certs/localhost.pfx -inkey /app/certs/localhost.key -in /app/certs/localhost.crt -passout pass:

# Construa a aplicação  # ============================
# BUILD STAGE
# ============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Instalar OpenSSL
RUN apt-get update && apt-get install -y openssl

# Copiar solution
COPY ScanBrazilConsulting.Api.sln .

# Copiar os csproj (importante para cache do restore)
COPY API/*.csproj API/
COPY Domain/*.csproj Domain/
COPY Infrastructure.Data.Mongo/*.csproj Infrastructure.Data.Mongo/
COPY Infrastructure.Data.Query/*.csproj Infrastructure.Data.Query/
COPY Infrastructure.Service/*.csproj Infrastructure.Service/
COPY CrossCutting.Configuration/*.csproj CrossCutting.Configuration/
COPY CrossCutting.Exception/*.csproj CrossCutting.Exception/

# Restaurar dependências
RUN dotnet restore ScanBrazilConsulting.Api.sln

# Copiar todo o código
COPY . .

# Gerar certificado HTTPS
RUN mkdir -p /app/certs && \
    openssl req -x509 -newkey rsa:4096 \
    -keyout /app/certs/localhost.key \
    -out /app/certs/localhost.crt \
    -days 365 -nodes \
    -subj "/CN=localhost" && \
    openssl pkcs12 -export \
    -out /app/certs/localhost.pfx \
    -inkey /app/certs/localhost.key \
    -in /app/certs/localhost.crt \
    -passout pass:

# Publicar API
RUN dotnet publish API/ScanBrazilConsulting.Api.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# ============================
# RUNTIME STAGE
# ============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar aplicação publicada
COPY --from=build /app/publish .

# Copiar certificado
COPY --from=build /app/certs /app/certs

# Variáveis de ambiente para HTTPS
ENV ASPNETCORE_URLS="http://+:80;https://+:443"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/app/certs/localhost.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=""

# Expor portas
EXPOSE 80
EXPOSE 443

# Entry point correto (nome do dll da API)
ENTRYPOINT ["dotnet", "ScanBrazilConsulting.Api.dll"]
