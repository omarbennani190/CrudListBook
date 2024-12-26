pipeline {
    agent any

    environment {
        DOCKER_IMAGE = "omar6866/dotnetapi"  // Le nom d'utilisateur Docker Hub et Nom de l'image Docker à créer
    }

    stages {
        stage('Cloner le projet') {
            steps {
                git branch: 'master', url: 'https://github.com/omarbennani190/crudlistbook.git'
				bat 'dir'
            }
        }
		
        stage('Construction projet') {
            steps {
                script {
                    bat 'dotnet build EchallengeListBook/EchallengeListBook.csproj'
                }
            }
        }

        stage('Exécuter les tests') {
            steps {
                script {
                    //Executer et archiver les résultats des tests
                    bat 'dotnet test BookManagement.Tests/BookManagement.Tests.csproj --logger "trx;LogFileName=test-results.trx"'
                }
            }
        }

        stage('Construire Docker Image') {
            steps {
                script {
                    bat 'docker build -t %DOCKER_IMAGE% .'
                }
            }
        }
		
        
        stage('Push Docker Image') {
            steps {
                script {
                    withCredentials([usernamePassword(credentialsId: 'docker-hub-credentials', usernameVariable: 'DOCKER_USERNAME', passwordVariable: 'DOCKER_PASSWORD')]) {
                        bat """
                            echo %DOCKER_PASSWORD% | docker login -u %DOCKER_USERNAME% --password-stdin https://index.docker.io/
                            docker tag %DOCKER_IMAGE% %DOCKER_IMAGE%:latest
                            docker push %DOCKER_IMAGE%:latest
                        """
                    }
                }
            }
        }
    }

    post {
        always {
            echo 'Pipeline terminé.'
        }

        success {
            echo 'Pipeline exécuté avec succès!'
        }

        failure {
            echo 'Le pipeline a échoué.'
        }
    }
}