docker stop JkvoXyz;
docker container rm JkvoXyz;

docker stop jkvo_db;
docker container rm jkvo_db;

docker network rm jkvo_net;

docker network create jkvo_net;

docker run -p 3306:3306 -d --name jkvo_db0 --net jkvo_net --hostname db0.jkvolocal jkvodb
docker run -p 3307:3306 -d --name jkvo_db1 --net jkvo_net --hostname db1.jkvolocal jkvodb
docker run -p 3308:3306 -d --name jkvo_db2 --net jkvo_net --hostname db2.jkvolocal jkvodb

docker build -t jkvo_fe

docker run -p 5001:5001 -d --net jkvo_net --name JkvoXyz jkvo_fe