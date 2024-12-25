# Utilisation de l'image officielle .NET SDK 8 pour la phase de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Mettre à jour et installer les dépendances système
RUN apt-get update && apt-get install -y \
    curl \
    libunwind8 \
    libssl3 \
    && rm -rf /var/lib/apt/lists/*

# Installer xUnit runner comme dépendance NuGet
RUN dotnet add "EchallengeListBook/EchallengeListBook.csproj" package xunit.runner.console

# Ajouter le chemin des outils au PATH
ENV PATH="$PATH:/root/.dotnet/tools"

# Copier et restaurer le projet
COPY ["EchallengeListBook/EchallengeListBook.csproj", "EchallengeListBook/"]
RUN dotnet restore "EchallengeListBook/EchallengeListBook.csproj"

# Copier les fichiers restants
COPY . .

# Construire le projet
WORKDIR /app/EchallengeListBook
RUN dotnet build "EchallengeListBook.csproj" -c Release -o /app/build

# Publier le projet
RUN dotnet publish "EchallengeListBook.csproj" -c Release -o /app/publish

# Étape finale
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EchallengeListBook.dll"]
