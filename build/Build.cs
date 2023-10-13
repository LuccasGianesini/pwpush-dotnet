using System;
using System.Runtime.InteropServices;
using Coleman.Build;
using Coleman.Build.Config;

sealed class Build : ColemanBuild
{
    public Build()
    {
        string githubToken = RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                                 ? Environment.GetEnvironmentVariable("GITHUB_TOKEN", EnvironmentVariableTarget.Process)!
                                 : Environment.GetEnvironmentVariable("GITHUB_TOKEN", EnvironmentVariableTarget.User)!;
        RuntimeIdentifier[] defaultRuntimeIdentifiers = new[]
                                                        {
                                                            RuntimeIdentifier.WinX64,
                                                            RuntimeIdentifier.WinArm64,
                                                            RuntimeIdentifier.LinuxX64,
                                                            RuntimeIdentifier.LinuxArm64,
                                                            RuntimeIdentifier.OsxX64,
                                                            RuntimeIdentifier.OsxArmX64,
                                                        };
        var artifactsToInclude = new[] { "pwpush-*", "pwget-*" };
        CiMode = "gitflow";
        Dotnet = new DotnetConfiguration
                 {
                     SolutionName = "PwPush.sln"
                 };
        GitHub = new GitHubConfiguration
                 {
                     Owner = "luccasgianesini",
                     Token = githubToken,
                     CreateRelease = true,
                     PullTags = true,
                     RepositoryName = "pwpush" 
                 };
        PublishableArtifactsTargets
           .AddTarget(new GitHubReleaseArtifactTarget(artifactsToInclude));
        PublishableArtifacts
           .AddArtifact(new DotnetCliArtifact("PwPush")
                        {
                            DisableDebugSymbols = true,
                            OutputNameOverride = "pwpush",
                            RuntimeIdentifiers = defaultRuntimeIdentifiers,
                            EnableReadyToRun = true
                        })
           .AddArtifact(new DotnetCliArtifact("PwGet")
                        {
                            DisableDebugSymbols = true,
                            OutputNameOverride = "pwget",
                            RuntimeIdentifiers = defaultRuntimeIdentifiers,
                            EnableReadyToRun = true
                        });
    }

    public static int Main()
    {
        return Execute<Build>(x => x.RecompileSolution, x => x.ConfigureRelease, x => x.CreateDotnetCliArtifacts, x => x.CreateReleaseOnScm, x => x.Cleanup);
    }
}