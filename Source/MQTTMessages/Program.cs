using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTMessages.Cli;
using Spectre.Console;
using Spectre.Console.Cli;

var info = new Grid();
info.AddColumn(new GridColumn().PadLeft(1).PadRight(1));
info.AddRow("Demonstrates connecting to the Flex MQTT Broker");
info.AddRow("You can subscribe to various topics, but this example focuses on all alarms, events, or status.");

AnsiConsole.Write(
    new Panel(info)
        .Header("MQTT Client"));

// Setup our services
using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Add our services that are shared between all of the samples
        services.AddDefaultServices(context.Configuration);

        // Setup Spectre.Console.Cli which handles command line arguments
        services.AddSingleton(provider =>
        {
            var app = new CommandApp(new Common.Cli.Registrar(services, provider));
            app.Configure(config =>
            {
                config.AddCommand<EventCommand>("events")
                    .WithDescription("Subscribe to events and display them in a live table.");
                config.AddCommand<AlarmCommand>("alarms")
                    .WithDescription("Subscribe to alarms and display them in a live table.");
                config.AddCommand<StatusCommand>("status")
                    .WithDescription("Subscribe to hardware status");
            });
            return app;
        });
    })
    .Build();

try
{
    var app = host.Services.GetService<CommandApp>();
    if (app == null)
        return 1;
    return await app.RunAsync(args);
}
catch (Exception ex)
{
    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
    return 1;
}

/*

static string CreateHash(string value)
{
    using var sha256 = SHA256.Create();
    var challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));
    return Convert.ToBase64String(challengeBytes);
}

Console.WriteLine("MQTT Messaging Examples ");
Console.WriteLine("--------------------------------------------------------------------");
Console.WriteLine("\tThis sample will demonstrate how to...");
Console.WriteLine("\t\tSetup and receive MQTT Messages from events, status, alarms");
Console.WriteLine("\t\tSimple example how to process the json return");
Console.WriteLine("\t\tReceive messages from TCP/IP or Web sockets" );
Console.WriteLine("--------------------------------------------------------------------");
Console.WriteLine();
// Connect Using TCP/IP settings
var factory = new MqttFactory();
var mqttClient = factory.CreateMqttClient();

var clientId = FlexApiExtensions.MqttClientUserNameFromConfig();
var clientSecret = CreateHash(FlexApiExtensions.MqttClientPasswordFromConfig());

//Connect via TCP/IP
var options = new MqttClientOptionsBuilder()
    .WithTcpServer(FlexApiExtensions.MqttBaseUrlFromConfig(), FlexApiExtensions.MqttPortFromConfig())
    .WithCredentials(clientId, clientSecret)
    .Build();

//Connect via Websockets
//var wshost = $"{FlexApiExtensions.MqttBaseUrlFromConfig()}/mqtt";
//var options = new MqttClientOptionsBuilder()
//    .WithWebSocketServer(wshost)
//    .WithCredentials(clientId, clientSecret)
//    .Build();

//Callback when connected to the server
mqttClient.UseConnectedHandler(async e =>
{
    Console.WriteLine("### CONNECTED WITH SERVER ###");
    
    // Format to filter messages
    // flex/{source system}/{hardware type}/{unique key}/{ alarm / event / status }
    // 

    //Example 1: All events
    await mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder().WithTopicFilter("flex/+/+/+/event").Build());
    Console.WriteLine("### SUBSCRIBED Events ###");

    //Example 2: All alarms
    //await mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder().WithTopicFilter("flex/+/+/+/alarm").Build());
    //Console.WriteLine("### SUBSCRIBED Alarms ###");

    // Example 3: All statuses
    //await mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder().WithTopicFilter("flex/+/+/+/status").Build());
    //Console.WriteLine("### SUBSCRIBED Status ###");

    //Example 4: Only items with a specific uniqueKey
    //await mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder().WithTopicFilter("flex/+/+/a20fe7a3-d842-4ba2-b43a-fa234f87aa0f/+").Build());

    //Example 5: Only mercury doors
    //await mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder().WithTopicFilter("flex/+/Door/+/+").Build());
});

//Callback when messages received and need to be processed.
mqttClient.UseApplicationMessageReceivedHandler(e =>
{
    Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
    Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
    Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
    Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
    Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");

    Console.WriteLine();
});


await mqttClient.ConnectAsync(options, CancellationToken.None);


//so application won't immediately terminate. 
Console.ReadLine();

*/