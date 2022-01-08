# MinecraftApi
This project aims to develop a REST API around Minecraft RCON allowing Minecraft servers to manage and run commands using a REST API on their Minecraft Server.

# State

This project started on 01/06/2022. So it's extremely fresh. For now all the code is doing the RCON implementation.

After that we will implement the API allowing remote commands through an API.

# Contributing

Contributions are always welcome. We ask you to adhere to our Code of Conduct, and of course keep the same license. All code contributions are considered to be acceptance that you will provide the same license we use in the project.

Also feel free to join our discord at https://discord.gg/stockdrops to discuss the project.

# Architecture

In this project we are trying to follow Clean architecture principles by keeping a loose coupling between elements. See: https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures

The idea is to keep the Core library as the place for all interfaces and POCOs, and DTOs. This project defines the objects used all around the app.

Then we will have infrastructure libraries which use the POCOs/Interfaces previously defined and implement an specific infrastructure.

For example linking to a database, or communication with RCON. The RCON project implements the interfaces and defines the services related to talking with RCON but uses the interfaces in the Core.

Later we could think of subdividing the Core into sections, so we can have MinecraftApi.Rcon and the MinecraftApi.Core.Rcon, the first one is the Rcon project itself, it communicates with an RCON server,
but the Core.Rcon defines the interfaces/Poco objects used. This way we have the clear separation between infratructure and core projects, and keeping the Core project modular so if you just want to use the rcon project you wouldn't need
all the extra stuff the core.

To sum up:

We would have Core subdivided into smaller cores which define interfaces/pocos related to that specific topic (i.e. Rcon, Api, etc...).

Then we would have infrastructure projects (without the core keyword) that implement the core libraries with as little dependencies as possible.

In this way we can distribute as many nuget packages as possible for multiple applications without adding redundency nor unnecessary dependencies.

# Database

The project aims to be compatible with MySQL and MS Sql Server. We will allow you to choose which one to use and we are aiming at making the DB creation as simple as possible by using Docker.

Remember that if using MS Sql Server you still need to have a valid license for it when you accept the EULA as part of the docker-compose command.

You can find more information here: https://github.com/Microsoft/mssql-docker/issues/200#issuecomment-345314344 (as you can see there's no need to enter a license key or other but you need to legally have a license).