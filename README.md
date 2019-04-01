# Librum Micro-Blog

Librum is an opensource, light and ready to use blog system written in C#

![](https://travis-ci.org/clintnetwork/librum.svg?branch=master)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

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

## Default Configuration File (librum.json)

```json
{
    "BlogName": "Librum Blog",
    "Description": "A little demonstration of Librum 🚀.",
    "Keywords": "librum,blog,csharp,blogging,netcore,demo",
    "Icon": "fas fa-square",
    "CanonicalUri": "https://blog.clint.network",
    "Contact": "support@librum.org",
    "Twitter": "clint_network",
    "Github": "clintnetwork",
    "Menu": [
        {
            "Title": "About Librum",
            "Link": "/article/about"
        }
    ],
    "Configuration": {
        "FixedNavbar": false,
        "DisplayHeader": true
    },
    "UsersStore": [{
        "Role": "Administrator",
        "Email": "me@clint.network",
        "Password": "ea605b0e8114409e5aaff2dc3769a19160bc3d08",
        "FullName": "Clint.Network",
        "Twitter": "clint_network",
        "GitHub": "clintnetwork",
        "Website": "https://stratis.guru"
    }],
    "DisqusHostname": "librum.disqus.com"
}
```
