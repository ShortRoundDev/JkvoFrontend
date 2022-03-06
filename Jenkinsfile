def buildRepo(){
    sh "rm -rf __Jkvo"
    sh "git clone https://github.com/shortrounddev/JkvoFrontend __Jkvo"
    dir("__Jkvo"){
        sh "docker build -t jkvo_fe -f ./Dockerfile.prod ."
    }
    sh "rm -rf __Jkvo"
}

pipeline {
    agent { node('jkvo_staging_fe1') }
    
    environment {
        DB_PASSWORD = credentials('staging-db-password')
    }

    stages {
        stage('Build'){
            steps {
                buildRepo()
            }
        }

        stage('Deploy'){
            steps {
                sh "(docker stop JkvoXyz && docker container rm JkvoXyz) || true"
                sh '''docker run \
                        -d \
                        --name JkvoXyz \
                        -p 80:5001 \
                        -e Database__Host=staging.shortrounddev.com \
                        -e Database__Username=root \
                        -e Database__Shards=1 \
                        -e Database__Password=$DB_Password \
                        jkvo_fe'''
            }
        }
    }
}