using System.Diagnostics;
using Flex.Cli.Setup.Views;
using Flex.Utils;
using ReactiveUI;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Reactive.Concurrency;
using Terminal.Gui;

namespace Flex.Cli.Setup
{
    internal class SetupCommand : Spectre.Console.Cli.Command
    {
        private readonly TerminalScheduler _scheduler;
        private readonly SetupView _defaultView;

        public SetupCommand(TerminalScheduler scheduler, SetupView defaultView)
        {
            _scheduler = scheduler;
            _defaultView = defaultView;
        }

        #region Overrides of Command

        public override int Execute(CommandContext context)
        {
            try
            {
                Application.Init();

                RxApp.MainThreadScheduler = _scheduler;
                RxApp.TaskpoolScheduler = TaskPoolScheduler.Default;

                Application.Run(_defaultView.Initialize());

                return CommandLineSuccess;
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex.Demystify(), ExceptionFormats.ShortenEverything);

                return CommandLineGeneralError;
            }
            finally
            {
                Application.Shutdown();
            }
        }

        #endregion
    }
}
