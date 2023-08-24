
// ReSharper disable once CheckNamespace
namespace Spectre.Console.Cli
{
    // ReSharper disable once InconsistentNaming
    internal static class IConfiguratorExtensions
    {
        public static IConfigurator AddBuiltInCommands(this IConfigurator self)
        {
            self.AddSetupCommand()
                .AddAlarmCommands()
                .AddAccessLevelCommands()
                .AddCardholderCommands()
                .AddCredentialCommands()
                .AddHardwareCommands()
                .AddMqttCommands();

            return self;
        }

        private static IConfigurator AddSetupCommand(this IConfigurator self)
        {
            self.AddBranch("setup", branch =>
            {
                branch.AddExample(new[] { "setup" });
                branch.SetDescription("Provide a user interface for settings and displaying them.");
                branch.SetDefaultCommand<Flex.Cli.Setup.SetupCommand>();

                branch.AddCommand<Flex.Cli.Setup.InfoCommand>("info")
                    .WithDescription("Display the current settings.")
                    .WithExample("setup", "info");
            });

            return self;
        }

        private static IConfigurator AddAlarmCommands(this IConfigurator self)
        {
            self.AddBranch("alarms", branch =>
            {
                branch.SetDescription("View, acknowledge, clear, and dismiss alarms");
                branch.AddCommand<Flex.Cli.Alarms.DisplayAlarmsCommand>("list")
                    .WithDescription("Display the current alarms.");

                branch.AddCommand<Flex.Cli.Alarms.AcknowledgeCommand>("acknowledge");
                branch.AddCommand<Flex.Cli.Alarms.ClearCommand>("clear");
                branch.AddCommand<Flex.Cli.Alarms.DismissCommand>("dismiss");
            });

            return self;
        }

        private static IConfigurator AddCardholderCommands(this IConfigurator self)
        {
            self.AddBranch<Flex.Cli.DataEntry.Cardholder.Settings.CardholderSettings>("cardholder", branch =>
            {
                branch.SetDescription("Manage cardholders");
                branch.SetDefaultCommand<Flex.Cli.DataEntry.Cardholder.GetCardholdersCommand>();

                branch.AddCommand<Flex.Cli.DataEntry.Cardholder.AddCardholderCommand>("add")
                    .WithExample("cardholder", "add", "--FirstName", "Sheldon", "--LastName", "Copper");

                branch.AddCommand<Flex.Cli.DataEntry.Cardholder.DeleteCardholderCommand>("delete")
                    .WithExample("cardholder", "delete", Guid.NewGuid().ToString());

                branch.AddCommand<Flex.Cli.DataEntry.Cardholder.EditCardholderCommand>("edit")
                    .WithExample("cardholder", "edit", Guid.NewGuid().ToString(), "--FirstName", "Marco", "--LastName", "Polo");

                branch.AddCommand<Flex.Cli.DataEntry.Cardholder.GetCardholdersCommand>("get")
                    .WithExample("cardholder", "get", "--where", "\"LastName == \\\"Brown\\\"", "--orderBy", "FirstName")
                    .WithExample("cardholder", "get", "--where", "\"LastName.Contains(\\\"Brown\\\")\"", "--orderBy", "FirstName")
                    .WithExample("cardholder", "get", "--where", "\"LastName.StartsWith(\\\"Brown\\\")\"", "--orderBy", "FirstName")
                    .WithExample("cardholder", "get", "--where", "\"LastName.EndsWith(\\\"Brown\\\")\"", "--orderBy", "\"FirstName Descending\"")
                    .WithExample("cardholder", "get", "--where", "\"Updated == Updated > DateTime.Now.AddDays(-1)\"");

                branch.AddCommand<Flex.Cli.DataEntry.Cardholder.ViewCardholderCommand>("view")
                    .WithExample("cardholder", "view", Guid.NewGuid().ToString())
                    .WithExample("cardholder", "view", Guid.NewGuid().ToString(), "--credentials");

                branch.AddBranch("accessLevels", p =>
                {
                    p.SetDescription("Manage access levels for the cardholder.");
                    p.AddCommand<Flex.Cli.DataEntry.Cardholder.AssignAccessLevelsCommand>("assign")
                        .WithDescription("Apply access level groups to all the credentials assigned to a cardholder");
                });

                branch.AddBranch("photos", p =>
                {
                    p.SetDescription("Upload and delete photos for a cardholder");
                    p.AddCommand<Flex.Cli.DataEntry.Cardholder.UploadPhotoCommand>("upload")
                        .WithDescription("Upload a photo and assign it to a cardholder")
                        .WithExample("cardholder", "photos", "upload", Guid.NewGuid().ToString(), "D:\\Downloads\\GeorgeWashington.png");

                    p.AddCommand<Flex.Cli.DataEntry.Cardholder.DeletePhotoCommand>("delete")
                        .WithDescription("Delete a photo that has been assigned to a cardholder")
                        .WithExample("cardholder", "photos", "delete", Guid.NewGuid().ToString());
                });
            });

            return self;
        }

        private static IConfigurator AddCredentialCommands(this IConfigurator self)
        {
            self.AddBranch<Flex.Cli.DataEntry.Credential.Settings.CredentialSettings>("credential", branch =>
            {
                branch.SetDescription("Manage credentials assigned to a cardholder.");
                branch.AddCommand<Flex.Cli.DataEntry.Credential.AddCredentialCommand>("add")
                    .WithDescription("Add a credential to a specified cardholder")
                    .WithExample("credential", "add", Guid.NewGuid().ToString(), "--CardNumber", "4571");

                branch.AddCommand<Flex.Cli.DataEntry.Credential.DeleteCredentialCommand>("delete")
                    .WithExample("credential", "delete", Guid.NewGuid().ToString());

                branch.AddCommand<Flex.Cli.DataEntry.Credential.EditCredentialCommand>("edit")
                    .WithExample("credential", "edit", Guid.NewGuid().ToString(), "--Active", "false", "--CardType", "3");

                branch.AddCommand<Flex.Cli.DataEntry.Credential.GetCredentialsCommand>("get")
                    .WithExample("credential", "get", "--where", "\"Updated == Updated > \\\"2/14/2015 2:0 PM\\\"", "--orderBy", "CardNumber")
                    .WithExample("credential", "get", "--where", "\"Updated == Updated > DateTime.Now.AddDays(-1)\"");

                branch.AddCommand<Flex.Cli.DataEntry.Credential.ViewCredentialCommand>("view")
                    .WithExample("credential", "view", Guid.NewGuid().ToString());

                branch.AddBranch("accessLevels", p =>
                {
                    p.SetDescription("Manage access levels assigned to a credential.");
                    p.AddCommand<Flex.Cli.DataEntry.Credential.ViewAssignedAccessLevelsCommand>("view");

                    p.AddCommand<Flex.Cli.DataEntry.Credential.AssignAccessLevelsCommand>("assign")
                        .WithDescription("Apply access level groups to a specific credential");

                    p.AddCommand<Flex.Cli.DataEntry.Credential.RemoveAccessLevelsCommand>("remove")
                        .WithDescription("Remove access level groups from a specific credential");
                });
            });

            return self;
        }

        private static IConfigurator AddAccessLevelCommands(this IConfigurator self)
        {
            self.AddBranch<Flex.Cli.DataEntry.AccessLevels.Settings.AccessLevelSettings>("accessLevels", branch =>
            {
                branch.SetDescription("Manage access level groups");
                branch.AddCommand<Flex.Cli.DataEntry.AccessLevels.GetAccessLevelGroupsCommand>("list")
                    .WithDescription("Return a list of the available access level groups");
            });

            return self;
        }

        private static IConfigurator AddHardwareCommands(this IConfigurator self)
        {
            self.AddBranch("hardware", branch =>
            {
                branch.SetDescription("Display a hardware and control hardware.");

                branch.AddCommand<Flex.Cli.Hardware.TreeCommand>("tree")
                    .WithDescription("Display the hardware tree.")
                    .WithExample("hardware", "tree", "--filter", "door")
                    .WithExample("hardware", "tree", "--flatten", "true", "--filter", "door");

                branch.AddBranch<Flex.Cli.Hardware.Settings.HardwareSettings>("door", p =>
                {
                    p.AddCommand<Flex.Cli.Hardware.MomentarilyUnlockDoorCommand>("momentary")
                        .WithExample("hardware", "door", "momentary", Guid.NewGuid().ToString());

                    p.AddCommand<Flex.Cli.Hardware.DoorModeCommand>("mode")
                        .WithExample("hardware", "door", "mode", Guid.NewGuid().ToString(), "Unlocked");
                });
            });
            return self;
        }

        private static IConfigurator AddMqttCommands(this IConfigurator self)
        {
            self.AddBranch("mqtt", branch =>
            {
                branch.SetDescription("Listen to alarms, events, and status messages from the MQTT broker.");
                branch.AddCommand<Flex.Cli.MQTTMessages.EventCommand>("events")
                    .WithDescription("Subscribe to events and display them in a live table.")
                    .WithExample("mqtt", "events");
                branch.AddCommand<Flex.Cli.MQTTMessages.AlarmCommand>("alarms")
                    .WithDescription("Subscribe to alarms and display them in a live table.")
                    .WithExample("mqtt", "alarms");
                branch.AddCommand<Flex.Cli.MQTTMessages.StatusCommand>("status")
                    .WithDescription("Subscribe to hardware status")
                    .WithExample("mqtt", "status");
            });

            return self;
        }
    }
}
