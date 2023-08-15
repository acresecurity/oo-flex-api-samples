using System.Text;
using Flex.Configuration;
using Flex.DataObjects.Hardware;
using Flex.Services.Abstractions;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using Newtonsoft.Json;
using Spectre.Console;

namespace Flex.Cli.MQTTMessages
{
    internal class EventCommand : DefaultCommand
    {
        public EventCommand(Microsoft.Extensions.Options.IOptions<Options> options, ICacheStore cache, IFlexHttpClientFactory factory, IManagedMqttClient mqttClient, MqttClientOptions clientOptions)
            : base(options, cache, factory, mqttClient, clientOptions)
        {
        }

        #region Overrides of DefaultCommand

        protected override void DefineTable(Table table)
        {
            table.AddColumn("[yellow]Event Time[/]");
            table.AddColumn("[yellow]Address[/]");
            table.AddColumn("[yellow]Description[/]");
            table.AddColumn("[yellow]Type[/]");
            table.AddColumn("[yellow]Index[/]");
            table.AddColumn("[yellow]Event Description[/]");
        }

        protected override bool DisplayPayload(MqttApplicationMessage message, Table table, Dictionary<int, string> eventDescriptions)
        {
            if (message?.PayloadSegment == null || (message?.PayloadSegment.Array?.Length ?? 0) == 0)
                return false;

            if (MqttTopicFilterComparer.Compare(message.Topic, "flex/+/+/+/event") != MqttTopicFilterCompareResult.IsMatch)
                return false;

            var json = Encoding.UTF8.GetString(
                message.PayloadSegment.Array,
                message.PayloadSegment.Offset,
                message.PayloadSegment.Count);

            var data = JsonConvert.DeserializeObject<Event>(json);
            if (data == null)
                return false;

            table.AddRow(
                data.Transaction == null ? string.Empty : data.Transaction.Value.ToLongTimeString(),
                data.HardwareAddress,
                data.HardwareDescription,
                data.HardwareType,
                data.EventDescriptionId.ToString(),
                eventDescriptions[data.EventDescriptionId]);

            return true;
        }

        protected override void Subscribe(MqttTopicFilterBuilder builder)
        {
            // Flex                 Root topic
            //   +                  Hardware System Source  (int)
            //     +                Hardware Type           (string) Example, Door, Controller, Input, Output, Reader
            //       +              Hardware Unique Key     (guid)
            //         event
            builder.WithTopic("flex/+/+/+/event");
        }

        #endregion
    }
}