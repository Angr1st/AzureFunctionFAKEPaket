namespace MyFunctions

open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open System.IO
open Newtonsoft.Json
open System

module HelloYou =
    type InputModel = {
        FirstName: string
        LastName: string
    }

    exception InvalidInputException of string

    let run (req: HttpRequest) (log: ILogger) =
        log.LogInformation "[Enter] HelloYou.run"
        async {
            use stream = new StreamReader(req.Body)
            let! body = stream.ReadToEndAsync() |> Async.AwaitTask
            let input = JsonConvert.DeserializeObject<InputModel>(body)
            if (String.IsNullOrWhiteSpace input.FirstName) || (String.IsNullOrWhiteSpace input.LastName) then
                log.LogInformation "Received by input"
                return BadRequestObjectResult "Please pass a JSON Object with a FirstName and a LastName." :> IActionResult
            else
                log.LogInformation "Received good input"
                return OkObjectResult (sprintf "Hello, %s %s" input.FirstName input.LastName) :>IActionResult
        }
        |> Async.RunSynchronously