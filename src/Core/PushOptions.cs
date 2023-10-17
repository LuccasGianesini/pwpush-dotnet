using CommandLine;

namespace PwPush.Core;

public sealed record PushOptions
{
    [Option('d', "days", Required = false, HelpText = "Number of days to keep the secret alive (1-90)", Default = 1)]
    public int Days { get; set; } = 1;

    [Option('v', "views", Required = false, HelpText = "Number views the secret can have before expiring", Default = 2)]
    public int Views { get; set; } = 2;

    [Option('p', "password", Required = false, HelpText = "The user defined password required to access the value of the secret")]
    public string? Password { get; set; } = default;

    [Option('u', "url", Required = false, HelpText = "The user defined password required to access the value of the secret", Default = "https://pwpush.com")]
    public string Url { get; set; }

    [Option('r', "removable", Required = false, HelpText = "The user defined password required to access the value of the secret", Default = true)]
    public bool Removable { get; set; } = true;

    [Value(5, Required = true, HelpText = "Secret value to be protected")]
    public string Secret { get; set; }
}