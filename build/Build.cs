using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.Publish);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;

    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath TestDirectory => RootDirectory / "src"  / "LMPT.Core.Tests";
    AbsolutePath DBDirectory => RootDirectory / "src"  / "LMPT.Core.DB";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";


    Target EfMigrate => _ => _
        .Executes(() =>
        {
            var nameOfMigration = "initial";
            var process = ProcessTasks.StartProcess("dotnet", 
                $"ef migrations add {nameOfMigration} --project {DBDirectory}");
            process.AssertZeroExitCode();
            return process.Output;
        });


    Target Clean => _ => _
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/*/bin", "**/*/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(ArtifactsDirectory);
        });



    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
            .SetArgumentConfigurator(a => a
                .Add("/p:CollectCoverage=true")
                .Add("/p:CoverletOutputFormat=\\\"opencover,lcov\\\"")
                .Add("/p:CoverletOutput=../lcov"))
            .SetProjectFile(TestDirectory));
        });
    Target Publish => _ => _
        .DependsOn(Test)
        .Executes(() =>
        {
            DotNetPublish(s => s
            .EnableNoBuild()
            .SetOutput(ArtifactsDirectory)
            .SetProject(Solution));
        });

}
