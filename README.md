# MicroServices with .Net Core

Microservices is an architectural style that structures an application as a collection of services that are

- Highly maintainable and testable
- Loosely coupled
- Independently deployable
- Organized around business capabilities
- Owned by a small team

## Microservices Pros & Cons

Pros  | Cons
------------- | -------------
Designed for continous deployment  | Complexity of distributed system
Easy to understand code base  | Troubshooting hard across services
Faster builds  | No way to maintain transaction across services
Highly scalable  | Shared code to be moved to libraries
Isolation from failures  | 
Independent deployments & failures | 
Faster adoption of new tech | 

## when to use Microservices?

One word 'autonomous' teams. Start into microservices when

- Small team cannot handle a large code base
- Difficult to build & deploy huge codebase
- Long testing cycles and enhanced time to market
- Unable to adopt newer tech due to huge code base

## About

Repository provides implementation of microservices using .Net Core, RabbitMQ, MassTransit, MongoDB & Docker.

### Projects

**Play.Catalog.Service:** One of the microservice used to do CRUD operations on Catalog Items in MongoDB. Also publish event on RabbitMQ bus on every item creation/updation/deletion to be consumed by inventory service.

**Play.Inventory.Service:** One of the microservice used to do CRUD operations on Inventory Items in MongoDB. Also add multiple consumers for different queues in RabbitMQ bus. 

**Play.Common:** Consider this as packages project which contains class libraries for multple settings like RabbitMQ, MongoDB & App Settings.



## Installation

Require following software

- VS Code [Download](https://code.visualstudio.com/download "Download Vs code here")  or Visual Studio Community 2022   [Download](https://visualstudio.microsoft.com/downloads/ "Download here")
- Docker Desktop  [Download](https://www.docker.com/products/docker-desktop/ "Download Vs code here") 
- Postman to test

