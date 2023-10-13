using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using CommandLine;
using PwPush.Core;

namespace PwPush;

class Program
{
    public static async Task Main(string[] args)
    {
        ParserResult<PushOptions>? parsingResult = Parser.Default.ParseArguments<PushOptions>(args);
        await SendPassword(parsingResult.Value);
    }

    private static async Task SendPassword(PushOptions options)
    {
        HttpClient client = CreateHttpClient(options.Url);

        var payload = new PasswordPayload
                      {
                          Passphrase = options.Password,
                          Payload = options.Secret,
                          DeletableByViewer = options.Removable,
                          ExpireAfterDays = options.Days <= 0 ? 1
                                            : options.Days >= 90 ? 90 : options.Days,
                          ExpireAfterViews = options.Views
                      };

        var message = new HttpRequestMessage(HttpMethod.Post, new Uri("/p.json", UriKind.Relative))
                      {
                          Content = new StringContent(payload.ToString(), new MediaTypeHeaderValue("application/x-www-form-urlencoded"))
                      };
        HttpResponseMessage httpResponse = await client.SendAsync(message);
        if (!httpResponse.IsSuccessStatusCode)
        {
            Console.WriteLine($"Failed to push password to {options.Url}");
            return;
        }

        var response = await JsonSerializer.DeserializeAsync<PushResponse>(await httpResponse.Content.ReadAsStreamAsync());
        Console.WriteLine($"Your secret will expire after {options.Days} day(s) and after {options.Views} views");
        Console.WriteLine("To get your secret go to the following url or use pwget");
        Console.WriteLine($"Url: {options.Url}/p/{response!.url_token}");
        Console.WriteLine($"Pwget: pwget {response!.url_token}");
    }

    private static HttpClient CreateHttpClient(string url)
        => new()
           {
               BaseAddress = new Uri(url),
               Timeout = TimeSpan.FromSeconds(30)
           };
}