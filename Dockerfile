# Utilisation de l'image officielle .NET SDK 8 pour la phase de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 8091

# Mettre à jour et installer les dépendances système
RUN apt-get update && apt-get install -y \
    curl \
    libunwind8 \
    libssl3 \
    && rm -rf /var/lib/apt/lists/*

# Copier les fichiers restants
COPY . .

# Ajouter les dépendances de test nécessaires
RUN dotnet add BookManagement.Tests/BookManagement.Tests.csproj package xunit
RUN dotnet add BookManagement.Tests/BookManagement.Tests.csproj package xunit.runner.visualstudio

# Restaurer les dépendances
RUN dotnet restore "EchallengeListBook/EchallengeListBook.csproj"
RUN dotnet restore "BookManagement.Tests/BookManagement.Tests.csproj"

# Publier le projet
RUN dotnet publish "EchallengeListBook/EchallengeListBook.csproj" -c Release -o /app/publish
RUN dotnet publish "BookManagement.Tests/BookManagement.Tests.csproj" -c Release -o /app/publish

# Étape finale
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EchallengeListBook.dll"]
