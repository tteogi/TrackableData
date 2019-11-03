#r "paket:
nuget Fake.Core.ReleaseNotes
nuget Fake.Core.Xml
nuget Fake.DotNet.Cli
nuget Fake.DotNet.Paket
nuget Fake.Tools.Git
nuget Fake.Core.Process
nuget Fake.IO.FileSystem
nuget Fake.DotNet.MSBuild
nuget Fake.DotNet.NuGet
nuget Fake.Core.Target //"
#load "./.fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.IO
open Fake.Core.TargetOperators
open System
open System.IO
open Fake.DotNet
open Fake.DotNet.NuGet


let nugetApiKye = "oy2krlvnusssg3pxpbhqkgzxyxnureoetm5imuovixvtqq"
let buildDir = "./build/"
let testDir  = "./test/"
let configuration = DotNet.BuildConfiguration.Release


// Properties
let summary = "Helper class for generating code concisely."
let copyright = "Copyright © 2019 tteogi"
let authors = ["TenY"]
let owner = "TenY"
let solutionFile = "CodeWriter"
let nugetVersion = "1.0.3";
let gitHome = "https://github.com/tteogi"
let gitName = "TrackableData"
let projectUrl = sprintf "%s/%s" gitHome gitName
let licenceUrl = "https://raw.githubusercontent.com/tteogi/tteogi.github.io/master/MIT-LICENSE"

type ProjectObject = struct
   val Name : string
   val BasePath : string
   val Tag : string
   val Description : float
end

Target.create "Pack" (fun _ ->
    let pack basePath project description tag =
        let projectPath = sprintf "%s/%s/%s.csproj" basePath project project
        let args =
            let defaultArgs = MSBuild.CliArguments.Create()
            { defaultArgs with
                      Properties = [
                          "Title", project
                          "PackageVersion", nugetVersion
                          "Authors", (String.Join(" ", authors))
                          "Owners", owner
                          "PackageRequireLicenseAcceptance", "false"
                          "Description", description
                          "Summary", summary
                          "Copyright", copyright
                          "PackageTags", tag
                          "PackageProjectUrl", projectUrl
                          "PackageLicenseUrl", licenceUrl
                      ] }

        DotNet.pack (fun p ->
            { p with
                  Configuration = configuration
                  OutputPath = Some "./build"
                  MSBuildParams = args
              }) projectPath

    pack "core" "CodeGenerator.Core"
        """Visual Studio project template to generate codes for POCO and container of TrackableData For DotNet Core."""
        "trackable data"
    pack "core" "TrackableData.Core"
        """POCO, list, dictionary, set and container which can track changes. These changes can be saved or rollbacked or replayed to another object For DotNet Core."""
        "trackable data"
    pack "plugins" "TrackableData.Core.Json"
        """Json.NET converters for tracker classes of TrackableData for DotNet Core."""
        "trackable data json"
    pack "plugins" "TrackableData.Core.MongoDB"
        """Object-document mapper between TrackableData and MongoDB for DotNet Core."""
        "trackable data mongodb nosql odm"
    pack "plugins" "TrackableData.Core.MsSql"
        """Object-relational mapper between TrackableData and Microsoft SQL Server for DotNet Core."""
        "trackable data sqlserver sql orm mssql"
    pack "plugins" "TrackableData.Core.MySql"
        """Object-relational mapper between TrackableData and MySQL for DotNet Core."""
        "trackable data mysql sql orm"
    pack "plugins" "TrackableData.Core.PostgreSql"
        """Object-relational mapper between TrackableData and PostgreSQL for DotNet Core."""
        "trackable data postgresql sql orm"
    pack "plugins" "TrackableData.Core.Protobuf"
        """Protobuf-net surrogates for tracker classes of TrackableData for DotNet Core."""
        "trackable data protobuf"
    pack "plugins" "TrackableData.Core.MessagePack"
        """neuecc/MessagePack-CSharp Resolver for tracker classes of TrackableData for DotNet Core."""
        "trackable data messagepack"
    pack "plugins" "TrackableData.Core.Redis"
        """Object-document mapper between TrackableData and Redis for DotNet Core."""
        "trackable data redis nosql odm"
    pack "plugins" "TrackableData.Core.Sql"
        """Object-relational mapper between TrackableData and common SQL for DotNet Core."""
        "trackable data sql orm"
)


// Targets
Target.create "Clean" (fun _ ->
  Shell.cleanDirs [buildDir; testDir]
)

Target.create "Default" (fun _ ->
  Trace.trace "Hello World from FAKE"
)

Target.create "Push" (fun _ ->
    let setNugetPushParams (defaults:NuGet.NuGetPushParams) =
            { defaults with
                Source = Some "https://api.nuget.org/v3/index.json"
                ApiKey = Some nugetApiKye

             }
    let setParams (defaults:DotNet.NuGetPushOptions) =
            { defaults with
                PushParams = setNugetPushParams defaults.PushParams
             }

    IO.Directory.EnumerateFiles(buildDir, "*.nupkg", SearchOption.TopDirectoryOnly)
    |> Seq.iter (fun nupkg ->
        DotNet.nugetPush setParams nupkg
    )
)

open Fake.Core.TargetOperators

"Clean"
  ==> "Pack"
  ==>  "Push"
  ==> "Default"

// start build
Target.runOrDefault "Default"
