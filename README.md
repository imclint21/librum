# About Librum

Librum is an opensource and ready to use blog system writted in C#

![image](https://user-images.githubusercontent.com/5221349/55356726-b4355880-54cb-11e9-9bcc-7b1d60c2ff5e.png)

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
