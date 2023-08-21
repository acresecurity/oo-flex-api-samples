using System.Text;
using Flex.Services.Abstractions;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using Newtonsoft.Json;
using Spectre.Console;
using Status = Flex.DataObjects.Hardware.Status;

namespace Flex.Cli.MQTTMessages
{
    internal class StatusCommand : DefaultCommand
    {
        public StatusCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory, IManagedMqttClient mqttClient, MqttClientOptions clientOptions)
            : base(options, cache, factory, mqttClient, clientOptions)
        {
        }

        #region Overrides of DefaultCommand

        protected override void DefineTable(Table table)
        {
            table.AddColumn("[yellow]Type[/]");
            table.AddColumn("[yellow]Data[/]");
        }

        protected override bool DisplayPayload(MqttApplicationMessage message, Table table, Dictionary<int, string> eventDescriptions)
        {
            if (message?.PayloadSegment == null || (message?.PayloadSegment.Array?.Length ?? 0) == 0)
                return false;

            if (MqttTopicFilterComparer.Compare(message.Topic, "flex/+/+/+/status") != MqttTopicFilterCompareResult.IsMatch)
                return false;

            var json = Encoding.UTF8.GetString(
                message.PayloadSegment.Array,
                message.PayloadSegment.Offset,
                message.PayloadSegment.Count);

            var data = JsonConvert.DeserializeObject<Status>(json);
            if (data == null)
                return false;

            table.AddRow(data.Type ?? "Unknown", json.EscapeMarkup());

            return true;
        }

        protected override void Subscribe(MqttTopicFilterBuilder builder)
        {
            // Flex                 Root topic
            //   +                  Hardware System Source  (int)
            //     +                Hardware Type           (string) Example, Door, Controller, Input, Output, Reader
            //       +              Hardware Unique Key     (guid)
            //         status
            builder.WithTopic("flex/+/+/+/status");
        }

        #endregion
    }
}
