docker stop JkvoXyz
docker container rm JkvoXyz

docker stop jkvo_db0
docker container rm jkvo_db0
docker stop jkvo_db1
docker container rm jkvo_db1
docker stop jkvo_db2
docker container rm jkvo_db2

docker network rm jkvo_net

docker network create jkvo_net

docker run -p 3306:3306 -d --name jkvo_db0 --net jkvo_net --hostname db0.jkvolocal jkvodb
docker run -p 3307:3306 -d --name jkvo_db1 --net jkvo_net --hostname db1.jkvolocal jkvodb
docker run -p 3308:3306 -d --name jkvo_db2 --net jkvo_net --hostname db2.jkvolocal jkvodb

docker build -t jkvo_fe -f ./Dockerfile.dev .
docker run -p 5001:5001 -d --name JkvoXyz --net jkvo_net jkvo_fe