open Root

open System
open System.CommandLine
open System.CommandLine.Invocation

type 'a argument = Argument<'a>
type 'a clioption = Option<'a>
type 'a action = Action<'a>

let addOption opt (command: Command) =
    command.Options.Add opt
    command

let addArgument arg (command: Command) =
    command.Arguments.Add arg
    command

let setAction (f: ParseResult action) (command: Command) =
    command.SetAction f
    command

let defaultCase _ = Kebab

[<EntryPoint>]
let main argv =

    let inputArg =
        argument<string> ("input", Description = "The input string to Fray or change.")
    let caseOpt =
        clioption<Case> (
            "--case",
            Description = "The target case to Fray the input string to.",
            DefaultValueFactory = defaultCase,
            aliases = [| "-c" |]
        )

    let frayHandler (pr: ParseResult) =
        let inputVal =
            if Console.IsInputRedirected then
                Console.In.ReadToEnd().Trim ()
            else
                pr.GetValue inputArg
        let caseVal = pr.GetValue caseOpt
        let result = transform inputVal caseVal

        printfn "%s" result

    let rootCommand =
        RootCommand "Fray is a semi-performant string-changer utility."
        |> addArgument inputArg
        |> addOption caseOpt
        |> setAction frayHandler

    rootCommand.Parse(argv).Invoke ()
