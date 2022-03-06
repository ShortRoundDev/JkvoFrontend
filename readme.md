
# <img src="https://raw.githubusercontent.com/ShortRoundDev/JkvoFrontend/master/wwwroot/img/favicon.svg" /> Jkvo.xyz

---

## What is this?

Jkvo.xyz is a URL shortener made for an interview with custom ink. It takes in a URL, computes a 32-bit hash, adds an extra byte of metadata, and stores it in a horizontally sharded database. Hitting the resultant link for that hex-string encoded hash (i.e: `mydomain.com/0301f78cdf`) returns a 301 redirect to the original URL.

## What's with the name

Initially I was going to host it on Jkvo.xyz, since it was the shortest 4-letter domain I could find that didn't cost $10,000, but I came up with some thematically better URLs instead

## How to run

This is a .Net 6 application and requires .Net 6 sdk. It has been tested on Windows and Linux

### Setup the database

See [JkvoDb](github.com/shortrounddev/jkvodb) for the database initialization script. It creates a MySql database with the paths table created by default. Build that dockerfile with

```bash
docker build -t jkvo_db .
```

So that `rundocker.sh` has the proper tag to run the docker image by

Running `sh ./Rundocker.sh` will create 3 local MySql Database instances, as well as run the frontend at localhost:5001. These run on the same docker network as each other and communicate with each other.

### Dev Server

If you want to run the development server (locally, not in docker)

- Comment out the last line in `RunDocker.sh` so that only the MySql instances run

- Add the following to your `/etc/hosts` file (`C:\Windows\System32\Drivers\etc\hosts` on windows):
```
db0.jkvolocal 127.0.0.1
db1.jkvolocal 127.0.0.1
db2.jkvolocal 127.0.0.1
```
(since Jkvo.Xyz looks for port 3306, development mode will only connect to the first MySql instance, so feel free to shut down db1 and db2)

The hostname that Jkvo.Xyz looks for (`jkvolocal` by default) is in `appsettings.json > Database > Host` in Development mode, and is set as `Database__Host` in production/staging, so you can change those

The default local MySql login is `root` and `password`.