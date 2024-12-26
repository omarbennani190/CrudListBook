CRUD List Book - CI/CD Pipeline avec Jenkins et Docker

Ce projet CRUD List Book est une API Web construite avec ASP.NET Core, intégrée avec un pipeline CI/CD dans Jenkins pour assurer l'intégration continue, les tests automatisés et le déploiement dans Docker Hub.
🚀 Fonctionnalités du Pipeline Jenkins

Le fichier Jenkinsfile décrit les étapes suivantes :

    Cloner le projet :
        Clone le dépôt GitHub depuis CRUD List Book dans Jenkins.
    Construire le projet :
        Compile le projet avec .NET SDK en utilisant la commande dotnet build.
    Exécuter les tests unitaires :
        Utilise xUnit pour exécuter les tests unitaires du projet avec dotnet test.
        Archive les résultats de tests au format .trx.
    Construire une image Docker :
        Construit une image Docker du projet à l'aide de Docker.
    Pousser l'image Docker :
        Connecte Docker à Docker Hub et pousse l'image construite dans le registre Docker.

🛠️ Prérequis

    Jenkins installé et configuré.
    Docker installé sur l'agent Jenkins.
    Plugins Jenkins requis :
        Git Plugin
        Pipeline
        Docker Pipeline
    Identifiants Docker Hub :
        Configurez un credential dans Jenkins avec l'ID docker-hub-credentials.

📁 Structure du Projet

/crudlistbook
├── EchallengeListBook           # Projet principal de l'API Web
├── BookManagement.Tests         # Projet de tests unitaires
├── Jenkinsfile                  # Configuration du pipeline Jenkins
├── Dockerfile                   # Instructions pour construire l'image Docker
├── README.md                    # Documentation du projet

🐳 Docker

L'image Docker créée est hébergée sur Docker Hub 
Construire et tester localement

    Construire l'image Docker :

docker build -t <nomuserdocker/image> .

Exécuter le conteneur Docker :

    docker run -d -p 8080:80 <nomuserdocker/image>

    Accéder à l'API :
    Ouvrez un navigateur et accédez à http://localhost:8080.

🧪 Résultats des Tests

Les tests unitaires sont exécutés à chaque pipeline et les résultats sont archivés pour consultation.
Construction et tests localement :

dotnet build EchallengeListBook/EchallengeListBook.csproj
dotnet test BookManagement.Tests/BookManagement.Tests.csproj

💡 Configuration Jenkins
Exemple d'intégration Docker Hub :

    Dans le fichier Jenkinsfile, configurez l'identifiant de connexion Docker Hub via :
    withCredentials([usernamePassword(credentialsId: 'docker-hub-credentials', usernameVariable: 'DOCKER_USERNAME', passwordVariable: 'DOCKER_PASSWORD')])
