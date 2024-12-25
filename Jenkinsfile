pipeline {
    agent any  // agent Jenkins

    environment {
		registry = "omarbennani190/crudlistbook"
		img = "$registry:${env.BUILD_ID}"
		//registerCredential = "docker-hub-login"
        DOCKER_IMAGE = "dotnetapi"  // Nom de l'image Docker à créer
        DOCKER_REGISTRY = "omar6866" // Le nom d'utilisateur Docker Hub
    }

    stages {
        stage('Cloner le projet') {
            steps {
                git branch: 'master', url: 'https://github.com/omarbennani190/crudlistbook.git'
				bat 'dir'
            }
        }
		
        


        stage('Build Docker Image') {
            steps {
                script {
                    // Construire l'image
                    bat 'docker build -t %DOCKER_REGISTRY%/%DOCKER_IMAGE% .'
                }
            }
        }
		
		stage('Lancer l\'image Docker') {
            steps {
				echo "Run image"
                bat returnStdout: true, script: "docker run --rm -d --name %DOCKER_IMAGE% -p 8090:8090 %DOCKER_REGISTRY%/%DOCKER_IMAGE%"
            }
        }

        stage('Exécuter les tests') {
            steps {
                script {
                    bat 'dotnet test BookManagement.Tests/BookManagement.Tests.csproj --logger:xunit'
					
                    // exécuter un test de l'application dans le conteneur Docker
                    bat 'docker run --rm %DOCKER_IMAGE% dotnet test'
                }
            }
        }

        stage('Push l\'image dans Docker Hub') {
            when {
                branch 'main'  // Ne poussez l'image Docker que sur la branche principale
            }
            steps {
                script {
					

                    // Se connecter à Docker Hub et pousser l'image
                    withCredentials([usernamePassword(credentialsId: 'docker-hub-credentials', usernameVariable: 'DOCKER_USER', passwordVariable: 'DOCKER_PASS')]) {
                        bat 'echo $DOCKER_PASS | docker login -u $DOCKER_USER --password-stdin'
                        // Push l'image vers Docker Hub
                        bat 'docker push %DOCKER_REGISTRY%/%DOCKER_IMAGE%'
					}
				}
            }
			post {
                always {
                    // Archiver les résultats des tests
                    xunit(
                        tools: [XUnitPublisher('xunit')],
                        testTimeMargin: '3000'
                    )
                }
            }
        }
    }

    post {
        always {
            // Cette étape est exécutée après chaque pipeline, pour nettoyer les ressources si nécessaire
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