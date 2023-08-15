
namespace Flex.Services.Abstractions
{
    internal interface IFlexHttpClientFactory
    {
        Task<HttpClient> GetClient();
    }
}
