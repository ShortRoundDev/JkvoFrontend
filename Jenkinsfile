def buildRepo(){
    sh "rm -rf __Jkvo"
    sh "git clone -b ${env.BRANCH_NAME} https://github.com/shortrounddev/JkvoFrontend __Jkvo"
    dir("__Jkvo"){
        sh "docker build -t jkvo_fe -f ./Dockerfile.prod ."
    }
    sh "rm -rf __Jkvo"
}

def deployContainer(){
    sh "(docker stop JkvoXyz && docker container rm JkvoXyz) || true"
    sh '''docker run \
            -d \
            --name JkvoXyz \
            -p 80:5001 \
            -e Database__Host=cstm.ink \
            -e Database__Username=root \
            -e Database__Shards=3 \
            -e Database__Password=$DB_PASSWORD \
            jkvo_fe'''
}

def runOnAll(Closure closure){
    for(fe in ["jkvo_prod_fe1", "jkvo_prod_fe2", "jkvo_prod_fe3"]){
        node(fe){
            closure();
        }
    }
}

pipeline {
    agent none
    
    environment {
        DB_PASSWORD = credentials('prod-db-password')
    }

    stages {
        stage('Build'){
            steps {
                runOnAll(this.&buildRepo);
            }
        }

        stage('Deploy'){
            steps {
                runOnAll(this.&deployContainer);
            }
        }
    }
}