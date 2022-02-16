using System.Text;
using Common.Configuration;
using Common.DataObjects;
using IdentityModel.OidcClient;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using Newtonsoft.Json;
using Spectre.Console;

namespace MQTTMessages.Cli
{
    internal class EventCommand : DefaultCommand
    {
        public EventCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
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
            if (MqttTopicFilterComparer.Compare(message.Topic, "flex/+/+/+/event") == MqttTopicFilterCompareResult.IsMatch)
            {
                var json = Encoding.UTF8.GetString(message.Payload);
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

            return false;
        }

        protected override void Subscribe(MqttClientSubscribeOptionsBuilder builder)
        {
            builder.WithTopicFilter(p =>
            {
                // Flex                 Root topic
                //   +                  Hardware System Source  (int)
                //     +                Hardware Type           (string) Example, Door, Controller, Input, Output, Reader
                //       +              Hardware Unique Key     (guid)
                //         event
                p.WithTopic("flex/+/+/+/event");
            });
        }

        #endregion
    }
}
