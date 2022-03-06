def allNodes(Array nodes, Closure closure){
    for(n in nodes){
        node(n){
            closure();
        }
    }
}

def allFe(Closure closure){
    allNodes(FE_NODES, closure)
}

def buildRepo(){
    sh "rm -rf __Jkvo"
    sh "git clone https://github.com/shortrounddev/JkvoFrontend __Jkvo"
    dir("__Jkvo"){
        sh "docker build -t jkvo_fe -f ./Dockerfile.prod ."
    }
    sh "rm -rf __Jkvo"
}

def run(){
    sh "(docker stop JkvoXyz && docker container rm JkvoXyz) || true"
    sh "docker run -d --name JkvoXyz -p 80:5001 jkvo_fe"
}

pipeline {
    agent jkvo_staging_fe1
    stages {
        stage('Build'){
            buildRepo()
        }

        stage('Deploy'){
            run()
        }
    }
}