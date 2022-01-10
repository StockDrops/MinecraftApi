# MinecraftApi
This project aims to develop a REST API around Minecraft RCON allowing Minecraft servers to manage and run commands using a REST API on their Minecraft Server.

# Current Endpoints

As of the time of writing this readme (01/09/2022) I've developped the following endpoints more or less. It's all a work in progress but the Run controller works and has been thouroughly tested. Some of the other endpoints in there (the PUT, and DELETE methods) need to be implemented.

You can for now if you want to experiment use docker-compose to run it, use ``docker-compose up`` on the directory project. (or use Visual Studio and hit run on the Docker-Compose project). No need to set up databases. It is all included in the Docker Compose file.

![image](https://user-images.githubusercontent.com/20151415/148724141-d003dfa9-8408-4c59-930a-dd443adc7522.png)


# State

This project started on **01/06/2022**. **So it's extremely fresh**. For now all the code is doing the RCON implementation.

After that we will implement the API allowing remote commands through an API.

**Completed:**

- We have completed the Rcon implementation.
- We have added a first abstraction to make sending commands easy through Json.
- Implemented the first controllers for the API, including CRUD operations for the main items: Plugins, Commands, and Arguments.
- Implemented a Run Controller in charge of running commands.
- Implemented a few tests to ensure good coverage of the different parts of the code.

# Plan

The project is organized such as to increase the flexibility of sending commands. Since this is an API it's intended to be used through a GUI, or other front-end of your choice.

The front-end should be able to send commands to the Minecraft Server through the API built in here while keeping it almost ignorant of how the commands work.

The idea is that through the abstractions called ICommandEntity (names might change), ISettableArgumentEntity, and IPluginEntity, the front-end will be able to offer the user with a structured form where to enter the command since the backend knows quite a lot about the commands to execute.

The backend knows:
- What plugin is linked to that command.
- What is the prefix of the command. For example the command "ban user reason" has a prefix equal to "ban".
- A description of the command (for the user's sake).
- A decriptive name (for the user's sake).
- The list of arguments required, what they are, the order of said argument, and the type. We could even add in the future all the possible values. Say we have a type of User, well the API could provide the backend with a formatted list of user for it to display as possible values for the argument.

All this knowledge allows the backend to fill in the gaps if needed, or in the future do some more complex sanity checks of the commands not available through a simple RCON console.

Now this has power, but it still requires to be setup, you need to add the data to the database. My hope is that as this grows as an open-source project more plugin devs will add their plugin information to this project and allow this information to be prepolutated.

But thankfully I also want the flexibility of just being able to send a raw command.

And that is also implemented in this. The controller Run has a run/raw endpoint that when you do a POST request to it with a query argument command="my command here" then the API will relay this command and send it to the RCON server, and it will relay the response to you:

![image](https://user-images.githubusercontent.com/20151415/148723379-8fe8dc41-fdb2-4344-9a83-81399f51b23a.png)

You can also use the Run endpoint as:

```json
{
  "arguments": [
    {
      "id": 0, //give it if you want automatic ordering with pre-saved commands.
      "name": "string", //optional
      "description": "string", //optional
      "order": 0, //optional: if no id was sent, then we will use the order of the list of Arguments. If you give the Id we will use the order saved in the backend. You can send this to override the order of the list IF this is not saved in the database.
      "required": true, //optional, we will use the database value if possible.
      "commandId": 0, //optional, relational information for linking command to argument
      "value": "string" //REQUIRED! the value of the argument you want to send.
    }
  ],
  "pluginId": 0, //optional
  "name": "string", //optional
  "description": "string", // optional
  "prefix": "string", //optional if id is given (i.e. you are sending a pre-saved command - the number id is hard to remember for a human, but remember this is meant for a machine (API)
  "id": 0 //optional
}
```

So the full json can be simplified to the following **if the command is saved in the database already**:

```json
{
  "arguments": [
    {
      "id": 4, 
      "value": "my argument value" //REQUIRED! the value of the argument you want to send.
    }
  ],
  "id": 5
}
```

And we will construct the API controllers where you will be able to get a list of commands with their ids, and the arguments of course. Like this:

![image](https://user-images.githubusercontent.com/20151415/148723948-fc32b2c9-d2ba-4849-9eb6-3a75ffe1d592.png)

So in here we have request the command with ID = 14. And we get the response to be which could be used later to run the command.

```json
{
  "arguments": [
    {
      "id": 2,
      "name": "Plugin Name",
      "description": "Provide the name of the plugin you want information about.",
      "order": 0,
      "required": false,
      "commandId": 14
    }
  ],
  "pluginId": 7,
  "name": "About",
  "description": "Returns information about the server",
  "prefix": "about",
  "id": 14
}
```


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
