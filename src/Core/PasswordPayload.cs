namespace PwPush.Core;

public sealed record PasswordPayload
{
    public string Payload { get; init; }
    public int ExpireAfterDays { get; init; }
    public int ExpireAfterViews { get; init; }
    public string? Passphrase { get; init; }
    public bool DeletableByViewer { get; init; }

    public override string ToString()
    {
        return string.IsNullOrEmpty(Passphrase)
                   ? $"password[payload]={Payload}&password[expire_after_days]={ExpireAfterDays}&password[expire_after_views]={ExpireAfterViews}&password[deletable_by_viewer]={DeletableByViewer}"
                   : $"password[payload]={Payload}&password[passphrase]={Passphrase}&password[expire_after_days]={ExpireAfterDays}&password[expire_after_views]={ExpireAfterViews}&password[deletable_by_viewer]={DeletableByViewer}";
    }
}