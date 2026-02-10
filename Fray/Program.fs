open Fray
open System
open System.CommandLine
open System.CommandLine.Invocation

type 'a argument = Argument<'a>
type 'a clioption = Option<'a>
type 'a action = Action<'a>

let addOption opt (command: Command) =
    command.Options.Add opt
    command

let addSet (args: string array) (opt: string clioption) = opt.AcceptOnlyFromAmong args

let addArgument arg (command: Command) =
    command.Arguments.Add arg
    command

let setAction (f: ParseResult action) (command: Command) =
    command.SetAction f
    command

let defaultCase _ = "kebab"
let defaultInput _ =
    if Console.IsInputRedirected then
        Console.In.ReadToEnd().Trim ()
    else
        String.Empty

[<EntryPoint>]
let main argv =
    let inputArg =
        argument<string> (
            "input",
            Description = "The input string to Fray or change.",
            DefaultValueFactory = defaultInput
        )
    let caseOpt =
        clioption<string> (
            "--case",
            Description = "The target case to Fray the input string to.",
            DefaultValueFactory = defaultCase,
            aliases = [| "-c" |]
        )
        |> addSet (Fray.GetCaseNames ())
    let frayHandler (pr: ParseResult) =
        let inputVal = pr.GetValue inputArg
        let caseVal = pr.GetValue caseOpt
        let result = Fray.Invoke (inputVal, caseVal)
        printfn "%s" result

    let rootCommand =
        RootCommand "Fray is a semi-performant string-changer utility."
        |> addArgument inputArg
        |> addOption caseOpt
        |> setAction frayHandler

    rootCommand.Parse(argv).Invoke ()
