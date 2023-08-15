using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli
{
    /// <summary>
    /// Custom type resolver for Spectre.Console.Cli
    /// </summary>
    internal class Resolver : ITypeResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public Resolver(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        #region Implementation of ITypeResolver

        public object Resolve(Type type)
        {
            try
            {
                return ActivatorUtilities.GetServiceOrCreateInstance(_serviceProvider, type);
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex.Demystify());
                return null;
            }
        }

        #endregion
    }
}