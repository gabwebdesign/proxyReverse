namespace proyectoCursoDotNet;

public class ProxyService : IProxyService
{
    private readonly HttpClient _client;

    public ProxyService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("ChuckNorrisApi");
    }

    public async Task<string> GetAuthorAsync()
    {
        var responseString = await _client.GetStringAsync("indicadores/tc/dolar");
        Console.WriteLine(responseString);
        return responseString;
    }
}