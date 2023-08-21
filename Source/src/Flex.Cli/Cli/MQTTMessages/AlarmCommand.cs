using System.Collections.Concurrent;
using System.Net;
using System.Text;
using Flex.DataObjects.Hardware;
using Flex.Services.Abstractions;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using Newtonsoft.Json;
using Spectre.Console;

namespace Flex.Cli.MQTTMessages
{
    internal class AlarmCommand : DefaultCommand
    {
        public AlarmCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory, IManagedMqttClient mqttClient, MqttClientOptions clientOptions)
            : base(options, cache, factory, mqttClient, clientOptions)
        {
        }

        private ConcurrentDictionary<int, Alarm> _alarms = new();

        #region Overrides of DefaultCommand

        protected override async Task<bool> Initialize(HttpClient client, Table table, Dictionary<int, string> eventDescriptions)
        {
            var response = await AnsiConsole
                .Status()
                .StartAsync("Retrieving alarms ...", _ => client.GetJsendAsync($"{Settings.Api}/api/v2/hardware/alarms"));

            if (response.IsSuccess())
            {
                _alarms = new ConcurrentDictionary<int, Alarm>(response.Deserialize<Alarm[]>().ToDictionary(p => p.UniqueId, p => p));
                foreach (var item in _alarms.Values)
                {
                    table.AddRow(
                        item.Transaction.ToLongTimeString(),
                        eventDescriptions[item.EventDescriptionId],
                        item.HardwareAddress,
                        item.HardwareDescription,
                        item.HardwareType,
                        item.Count.ToString(),
                        item.Status.ToString());
                }
                return true;
            }

            DisplayError(response);
            return false;
        }

        protected override void DefineTable(Table table)
        {
            table.AddColumn("[yellow]Time[/]");
            table.AddColumn("[yellow]Alarm Description[/]");

            table.AddColumn("[yellow]Address[/]");
            table.AddColumn("[yellow]Description[/]");
            table.AddColumn("[yellow]Type[/]");

            table.AddColumn("[yellow]Count[/]");
            table.AddColumn("[Yellow]Status[/]");
        }

        protected override bool DisplayPayload(MqttApplicationMessage message, Table table, Dictionary<int, string> eventDescriptions)
        {
            if (message?.PayloadSegment == null || (message?.PayloadSegment.Array?.Length ?? 0) == 0)
                return false;

            if (MqttTopicFilterComparer.Compare(message.Topic, "flex/+/+/+/alarm") != MqttTopicFilterCompareResult.IsMatch)
                return false;

            var json = Encoding.UTF8.GetString(
                message.PayloadSegment.Array,
                message.PayloadSegment.Offset,
                message.PayloadSegment.Count);

            var data = JsonConvert.DeserializeObject<Alarm>(json);
            if (data == null)
                return false;

            if (data.Notification == AlarmNotification.ClearAll)
                _alarms.Clear();
            else
            {
                if (_alarms.TryGetValue(data.UniqueId, out var found))
                {
                    if (found.Status == AlarmStatus.Normal || found.Notification is AlarmNotification.Purge or AlarmNotification.Remove)
                        _alarms.TryRemove(data.UniqueId, out _);
                    else
                    {
                        if (data.Status == AlarmStatus.Alarm)
                            data.Count = found.Count + 1;

                        if (!_alarms.TryUpdate(data.UniqueId, data, found))
                            return false;
                    }
                }
                else
                {
                    if (data.Count <= 0)
                        data.Count++;

                    _alarms.TryAdd(data.UniqueId, data);
                }
            }

            table.Rows.Clear();
            foreach (var item in _alarms.Values)
            {
                table.AddRow(
                    item.Transaction.ToLongTimeString(),
                    eventDescriptions[item.EventDescriptionId],
                    item.HardwareAddress,
                    item.HardwareDescription,
                    item.HardwareType,
                    item.Count.ToString(),
                    item.Status.ToString());
            }

            return true;
        }

/*
        protected override bool DisplayPayload(MqttApplicationMessage message, Table table, Dictionary<int, string> eventDescriptions)
        {
            if (MqttTopicFilterComparer.Compare(message.Topic, "flex/+/+/+/alarm") != MqttTopicFilterCompareResult.IsMatch)
                return false;

            // Remove any alarms that don't exist anymore
            var notFound = _alarms.Where(x => !_alarms.Select(p => p.Key).Contains(x.Key));
            foreach (var item in notFound)
                _alarms.TryRemove(item.Key, out _);

            var json = Encoding.UTF8.GetString(message.Payload);
            var data = JsonConvert.DeserializeObject<Alarm>(json);
            if (data == null)
                return false;

            if (_alarms.TryGetValue(data.UniqueId, out var found))
            {
                data.Count = found.Count + 1;
                if (!_alarms.TryUpdate(data.UniqueId, data, found))
                    return false;
            }
            else
                _alarms.TryAdd(data.UniqueId, data);

            table.Rows.Clear();
            foreach (var item in _alarms.Values)
            {
                table.AddRow(
                    item.Transaction.ToLongTimeString(),
                    eventDescriptions[item.EventDescriptionId],
                    item.HardwareAddress,
                    item.HardwareDescription,
                    item.HardwareType,
                    item.Count.ToString(),
                    item.Status.ToString());
            }

            return true;
        }
*/
        protected override void Subscribe(MqttTopicFilterBuilder builder)
        {
            // Flex                 Root topic
            //   +                  Hardware System Source  (int)
            //     +                Hardware Type           (string) Example, Door, Controller, Input, Output, Reader
            //       +              Hardware Unique Key     (guid)
            //         alarm
            builder.WithTopic("flex/+/+/+/alarm");
        }

        #endregion
    }
}
