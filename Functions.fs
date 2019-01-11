namespace MyFunctions

open Microsoft.Azure.WebJobs
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Logging

module Functions =

    [<FunctionName("HelloYou")>]
    let helloYou
        ([<HttpTrigger(Extensions.Http.AuthorizationLevel.Anonymous, "post", Route = null)>]
        req: HttpRequest,
        log: ILogger) =
            HelloYou.run req log
    
    [<FunctionName("ChristmasCounter")>]
    let whenIsChristmas ([<TimerTrigger("0 * * * * *")>]myTimer: TimerInfo, log: ILogger) =
        Say.hello(myTimer,log)
