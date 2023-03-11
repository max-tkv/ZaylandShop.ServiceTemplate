using ZaylandShop.ServiceTemplate.Abstractions.HttpClients;
using ZaylandShop.ServiceTemplate.Utils.Http;

namespace ZaylandShop.ServiceTemplate.Integration.Test;

public class TestHttpApiClient : JsonHttpApiClient, ITestHttpApiClient
{
    public TestHttpApiClient(HttpClient httpClient) : base(httpClient)
    {
    }
}