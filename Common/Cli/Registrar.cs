using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace Common.Cli
{
    /// <summary>
    /// Custom type registrar for Spectre.Console.Cli
    /// </summary>
    public class Registrar : ITypeRegistrar
    {
        private readonly IServiceCollection _services;

        private readonly IServiceProvider _provider;

        public Registrar(IServiceCollection services, IServiceProvider provider)
        {
            _services = services;
            _provider = provider;
        }

        #region Implementation of ITypeRegistrar

        public void Register(Type service, Type implementation)
        {
            _services.AddSingleton(service, implementation);
        }

        public void RegisterInstance(Type service, object implementation)
        {
            _services.AddSingleton(service, implementation);
        }

        public void RegisterLazy(Type service, Func<object> factory)
        {
            _services.AddSingleton(service, _ => factory());
        }

        public ITypeResolver Build()
        {
            return new Resolver(_provider);
        }

        #endregion
    }
}