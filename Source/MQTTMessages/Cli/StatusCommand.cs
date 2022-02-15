using System.Text;
using Common.Configuration;
using IdentityModel.OidcClient;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using Newtonsoft.Json;
using Spectre.Console;

namespace MQTTMessages.Cli
{
    internal class StatusCommand : DefaultCommand
    {
        public StatusCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
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
            if (MqttTopicFilterComparer.Compare(message.Topic, "flex/+/+/+/status") != MqttTopicFilterCompareResult.IsMatch)
                return false;

            var json = Encoding.UTF8.GetString(message.Payload);
            var data = JsonConvert.DeserializeObject<Common.DataObjects.Status>(json);
            if (data == null)
                return false;

            table.AddRow(data.Type, json.EscapeMarkup());

            return true;
        }

        protected override void Subscribe(MqttClientSubscribeOptionsBuilder builder)
        {
            builder.WithTopicFilter(p =>
            {
                // Flex                 Root topic
                //   +                  Hardware System Source  (int)
                //     +                Hardware Type           (string) Example, Door, Controller, Input, Output, Reader
                //       +              Hardware Unique Key     (guid)
                //         status
                p.WithTopic("flex/+/+/+/status");
            });
        }

        #endregion
    }
}
