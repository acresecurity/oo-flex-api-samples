using Flex.Cli.Setup.Models;
using Flex.Cli.TerminalGui;
using ReactiveUI;
using Terminal.Gui;

namespace Flex.Cli.Setup.Views
{
    internal class SetupView : BaseReactiveView, IViewFor<SetupModel>
    {
        public SetupView(SetupModel viewModel)
        {
            ViewModel = viewModel;
        }

        #region Implementation for IViewFor<Models.SetupModel>

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (SetupModel)value!;
        }

        public SetupModel ViewModel { get; set; }

        #endregion

        public Toplevel Initialize()
        {
            ColorScheme = Colors.Base;

            var menu = new MenuBar(new[]
            {
                new MenuBarItem("_File", new List<MenuItem[]>
                {
                    new MenuItem[]
                    {
                        new ("_Save", string.Empty, () => ViewModel?.Save(), () =>
                        {
                            ViewModel?.Validate();
                            return ViewModel?.Validation?.IsValid == true;
                        })
                    },
                    new MenuItem[1],
                    new MenuItem[]
                    {
                        new ("_Quit", string.Empty, () => Application.RequestStop())
                    }
                })
            });
            
            var tabView = new TabView
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(1),
                Height = Dim.Fill(1)
            };

            tabView.AddTab(new TabView.Tab("API Config", CreateApiConfigView()), true);
            tabView.AddTab(new TabView.Tab("MQTT Config", CreateMqttConfigView()), false);

            var statusBar = new StatusBar(new[]
            {
                new StatusItem (Key.CtrlMask | Key.Q, "~CTRL-Q~ Quit", () => Application.RequestStop()),
            });
            
            Add(menu, tabView, statusBar);

            return this;
        }

        private View CreateApiConfigView()
        {
            var result = new View
            {
                X = 1,
                Y = 1,
                Height = Dim.Fill(1),
                Width = Dim.Fill(1)
            };

            var (label, _) = CreateTextFieldView(result, null, "API Url:", ViewModel?.Options, p => p.Api);
            (label, _) = CreateTextFieldView(result, label, "Authority:", ViewModel?.Options, p => p.Authority);
            (label, _) = CreateTextFieldView(result, label, "Client ID:", ViewModel?.Options, p => p.ClientId);
            CreateTextFieldView(result, label, "Client Secret:", ViewModel?.Options, p => p.ClientSecret);

            return result;
        }

        private View CreateMqttConfigView()
        {
            var result = new View
            {
                X = 1,
                Y = 1,
                Height = Dim.Fill(1),
                Width = Dim.Fill(1)
            };

            var (label, input) = CreateRadioGroupFieldView(result, null, "Transport:", ViewModel?.Options.Mqtt, p => p.Transport);
            (label, _) = CreateTextFieldView(result, label, "Host:", ViewModel?.Options.Mqtt, p => p.Host);
            (label, input) = CreateMaskedTextFieldView(result, label, "Port:", "99999", ViewModel?.Options.Mqtt, p => p.Port);
            input.Width = 10;

            (label, _) = CreateRadioGroupFieldView(result, label, "TLS Version", ViewModel?.Options.Mqtt, p => p.TlsVersion);
            (label, _) = CreateRadioGroupFieldView(result, label, "Protocol Version", ViewModel?.Options.Mqtt, p => p.ProtocolVersion);

            input = CreateCheckBoxFieldView(result, label, "Use External Broker:", ViewModel?.Options.Mqtt, p => p.ExternalServer);
            (label, input) = CreateTextFieldView(result, input, "Username:", ViewModel?.Options.Mqtt, p => p.Username);
            (label, input) = CreateTextFieldView(result, label, "Password:", ViewModel?.Options.Mqtt, p => p.Password);

            return result;
        }
    }
}
