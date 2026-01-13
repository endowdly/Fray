namespace Fray

open Seed
open Root
open System

type Case =
    | Kebab = 0
    | Snake = 1
    | Constant = 2
    | Camel = 3
    | Pascal = 4

type Fray() =
    static member GetCaseNames () =
        Enum.GetNames typeof<Case>
        |> Array.map (fun s -> s.ToLowerInvariant ())

    static member Invoke (inputStr, case) =
        let getCase =
            function
            | Case.Kebab -> Kebab
            | Case.Snake -> Snake
            | Case.Constant -> Constant
            | Case.Camel -> Camel
            | Case.Pascal -> Pascal
            | _ -> Constant

        transform inputStr (getCase case)

    // There should be some sort of cli validation above this so its okay if we just return a "default"
    static member Invoke (inputStr, caseStr: string) =
        let titled = String.asTitle caseStr
        let success, case = Enum.TryParse<Case> titled

        if success then
            Fray.Invoke (inputStr, case)
        else
            Fray.Invoke (inputStr, Case.Kebab)
