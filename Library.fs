namespace MyFunctions

open System
open Microsoft.Azure.WebJobs
open Microsoft.Azure.WebJobs.Host
open Microsoft.Extensions.Logging

module Say =
  let private daysUntil (d: DateTime) =
    (d - DateTime.Now).TotalDays |> int

  let hello (timer: TimerInfo, log: ILogger) =
    let christmas = new DateTime(2019, 12, 25)

    daysUntil christmas
    |> sprintf "%d days until Christmas"
    |> log.LogInformation