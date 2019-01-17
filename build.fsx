#load ".fake/build.fsx/intellisense.fsx"
open Fake.Core
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.Core.TargetOperators

//Properties
let paketPath = Path.combine ".paket" "paket.exe"

let debugBuildOptions buildConfig= (
    fun (options:DotNet.BuildOptions) ->
        {options with Configuration=buildConfig})


//Targes
Target.create "Restore" (fun _ ->
    Shell.Exec(paketPath, "restore", "") |> ignore
)

Target.create "Install" (fun _ ->
    Shell.Exec(paketPath, "install", "") |> ignore
)

Target.create "Clean" (fun _ ->
    !! "**/bin"
    ++ "**/obj"
    |> Shell.cleanDirs 
)

Target.create "Build" (fun _ ->
    !! "**/*.*proj"
    |> Seq.iter (DotNet.build (debugBuildOptions DotNet.BuildConfiguration.Release))
)

Target.create "Publish" (fun _ ->
    !! "**/*.*proj"
    |> Seq.iter (DotNet.publish id))

Target.create "BuildDebug" (fun _ ->
    !! "**/*.*proj"
    |> Seq.iter ( DotNet.build (debugBuildOptions DotNet.BuildConfiguration.Debug)))

Target.create "All" ignore

"Clean"
  ==> "Restore"
  ==> "BuildDebug"
  ==> "Build"
  ==> "Publish"
  ==> "All"

"Clean"
  ==> "Restore"
  ==> "Publish"

"Clean"
  ==> "Restore"
  ==> "BuildDebug"



Target.runOrDefault "All"
