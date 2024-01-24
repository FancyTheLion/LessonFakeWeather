# LessonFakeWeather
First ASP.NET Core lesson

# Icons
Taken from https://www.iconsdb.com, CC-BY-ND 3.0

# Docker

1) Create virtual network

docker network create -d bridge weather-app-net

2) Create PostgreSQL image

docker build -f dockerfile-weather-app-postgresql -t weather-app-postgres .

3) Run infrastructure container

Test:
docker-compose -f docker-compose-infrastructure-weather-app.yml up

Run:
docker-compose -f docker-compose-infrastructure-weather-app.yml up -d

--

Stop containers

1) List containers:

docker container ls

2) Stop container:

docker-compose -f docker-compose-infrastructure-weather-app.yml down

