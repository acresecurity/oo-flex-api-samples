# Flex API Console Sample

Fully functional console application that demonstates how to utilize the Flex API.

## Requirements

- The projects all require the [.NET 7.0 SDK](https://dotnet.microsoft.com/en-us/download).
- Flex API license
- Configured Flex Identity client. See helpful information below.

## Features

- Alarm Handling
    - Displaying alarms
    - Acknowledging, clearing and dimissing alarms
    - Verifying user permissions
    - Requiring dispatch text when required
- Realtime Messaging via the Flex MQTT broker
    - Subscribing to alarms, events, and status
    - TCP and WebSocket transport
- Hardware Tree
    - How to retrieve the complete DNA hardware tree and fetch child nodes.    
- Cardholder
    - Complete lifecyle management of a cardholder and its credentials
    - Adding, editing, deleting cardholders and credentials
    - Assigning and removing access levels
- Authenticating to Flex Identity server using OpenID Connect 
- Client and server side data validation
- API rate limiting support
- Output response as JSON data using the `--json` option


## Helpful information

- [Swagger API Explorer](https://flextest.ooaccess.net/apiexplorer/index.html)
- [Identity Client Setup](https://bitbucket.org/ooaccess/flex-api-samples/wiki/Identity%20Client)
- [JSend Message Response](https://bitbucket.org/ooaccess/flex-api-samples/wiki/JSend%20-%20Json%20Message%20Structure%20Overview)

## Screenshots ##

### Login Procedure ###

![Login Procedure](/Images/LoginProcedure.png)

### Setup Gui ###

`dotnet run setup` or `flex setup`

![Setup Gui](/Images/SetupGui.png)

### Subscribing to Events ###

`dotnet run mqtt events`

![MQTT Events](/Images/MQTTEvents.gif)


### Hardware Tree ###

`dotnet run hardware tree`

![Hardware Tree](/Images/HardwareTreeDemo.gif)

### Searching Cardholders ###

`dotnet run cardholder get --where 'LastName == "Brown"' --orderBy FirstName`

![CardholderSearch](/Images/CardholderSearchTable.png)

### Viewing Cardholder Outputed as JSON ###

`dotnet run cardholder --json view 49acee78-5f0d-4ff0-ad7f-2a918f21c650`

![CardholderView](/Images/CardholderJsonOutput.png)

### Validation Errors ###

`dotnet run credential edit 2db27f49-ec76-493b-91aa-10f90623f091 --CardNumber 365 --IssueCode 2000`
![ValidationErrors](/Images/ValidationErrors.png)

### Editing Cardholder ###

`dotnet run cardholder edit 49acee78-5f0d-4ff0-ad7f-2a918f21c650 --Zip 76226 --MiddleName Glenn`

![EditingCardholder](/Images/EditingCardholder.png)