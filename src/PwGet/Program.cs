using System.Net.Http.Headers;
using System.Text.Json;
using CommandLine;
using PwPush.Core;

namespace PwGet;

class Program
{
    public static async Task Main(string[] args)
    {
        ParserResult<GetOptions>? parsingResult = Parser.Default.ParseArguments<GetOptions>(args);
        await GetPassword(parsingResult.Value);
    }

    private static async Task GetPassword(GetOptions options)
    {
        HttpClient client = CreateHttpClient(options.Url);

        var message = new HttpRequestMessage(HttpMethod.Get, new Uri($"/p/{options.Secret}.json", UriKind.Relative));
        HttpResponseMessage httpResponse = await client.SendAsync(message);
        if (!httpResponse.IsSuccessStatusCode)
        {
            Console.WriteLine($"Failed to get password from {options.Url}");
            return;
        }

        var response = await JsonSerializer.DeserializeAsync<GetResponse>(await httpResponse.Content.ReadAsStreamAsync());
        Console.WriteLine("Your secret is:");
        Console.WriteLine(response!.payload);
    }

    private static HttpClient CreateHttpClient(string url)
        => new()
           {
               BaseAddress = new Uri(url),
               Timeout = TimeSpan.FromSeconds(30)
           };
}