namespace proyectoCursoDotNet;

public class ProxyService : IProxyService
{
    private readonly HttpClient _client;

    public ProxyService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("ChuckNorrisAPI");
    }

    public async Task<string> GetAuthorAsync()
    {
        var responseString = await _client.GetStringAsync("jokes/random");
        Console.WriteLine(responseString);
        return responseString;
    }
}