using CommandLine;
namespace PwPush.Core;

[Verb("get", isDefault: false, HelpText = "Gets a secret from a PwPush server")]
public sealed class GetOptions
{
    [Option('p', "password", Required = false, HelpText = "The user defined password required to access the value of the secret")]
    public string? Password { get; set; } = default;

    [Option('u', "url", Required = false, HelpText = "The user defined password required to access the value of the secret", Default = "https://pwpush.com")]
    public string Url { get; set; }

    [Value(2, Required = true, HelpText = "Url secret")]
    public string Secret { get; set; }
}