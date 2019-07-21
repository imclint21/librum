# Librum Micro-Blog Engine

Librum is an opensource, lightweight and ready to use blog engine written in C#

[![Travis](https://travis-ci.org/clintnetwork/librum.svg?branch=master)](https://travis-ci.org/clintnetwork/librum)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/clintnetwork/librum/blob/master/LICENSE.md)

![image](https://user-images.githubusercontent.com/5221349/55356726-b4355880-54cb-11e9-9bcc-7b1d60c2ff5e.png)

## How to Deploy

[Read the Tutorial](https://clint.network/article/have-a-blog-with-librum)

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
    "CanonicalUri": "https://clint.network",
    "Contact": "contact@librum.org",
    "Twitter": "librum",
    "Github": "clintnetwork/librum",
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
        "Email": "contact@librum.org",
        "Password": "PASSWORD_IN_SHA1",
        "FullName": "Librum Blog",
        "Twitter": "librum",
        "GitHub": "clintnetwork/librum",
        "Website": "https://librum.org"
    }],
    "DisqusHostname": "librum.disqus.com"
}
```
