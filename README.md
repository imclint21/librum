# About Librum

Librum is an opensource and ready to use blog system writted in C#

![image](https://user-images.githubusercontent.com/5221349/55349001-d83b6e80-54b8-11e9-990a-30eb295c1aa4.png)

## Deploy with Docker

To use Librum with Docker you need to enable Docker Swarm mode and run this commands:

```bash
docker swarm init

docker config create librum librum.json #Change librum.json to point on a valid configuration file

docker service create \
     --name librum \
     --config source=librum,target=/app/librum.json,mode=0440 \
     --publish published=80,target=80 \
     clintnetwork/librum:latest
```