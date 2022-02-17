# Flex API Samples

Sample code demonstrating how to do various aspects of the Flex API.

## Requirements

The projects all require the [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download).

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
    - API rate limiting support
- Cardholder
    - Complete lifecyle management of a cardholder and its credentials
    - Adding, editing, deleting cardholders and credentials
    - Assigning and removing access levels
    - Data validation handling

## Helpful information

- [Swagger API Explorer](https://flextest.ooaccess.net/apiexplorer/index.html)
- [Identity Client Setup](https://bitbucket.org/ooaccess/flex-api-samples/wiki/Identity%20Client)
- [JSend Message Response](https://bitbucket.org/ooaccess/flex-api-samples/wiki/JSend%20-%20Json%20Message%20Structure%20Overview)

## Screenshots ##

Subscribing to Events

![MQTT Events](/Images/MQTT Events.gif)


Hardware Tree

![Hardware Tree](/Images/Hardware Tree Demo.gif)