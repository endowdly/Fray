module Root

open System

let (|Acronym|Transition|Normal|Delimiter|) (input: char span) =
    let curr = Span.head input

    // This is done iteratively to avoid byref errors and exception handling
    if not (Char.IsLetterOrDigit curr) then // punct or symbol or seperator that is not wordy
        Delimiter
    elif input.Length > 1 then
        let next = Span.next input

        // Two chars together like AA
        if Char.IsUpper curr && Char.IsUpper next then
            Acronym

        // Hit a Transition like in camelCase (e.g. lC)
        elif Char.IsLower curr && Char.IsUpper next then
            Transition

        // Just another letter or digit
        else
            Normal
    else
        Normal

let toString (ls: char list) =
    ls |> List.rev |> List.toSeq |> String.Concat

// Tail-recursive to keep one frame active; uses spans to be memory efficient
// Does not check well for out of index errors on the span; relies on wrapping function to do that. Ew.
let rec private getWords (input: char span) (currentWord: char list) (acc: string list) =
    let wordWith char = char :: currentWord
    let push word = toString word :: acc

    // Check IsEmpty first to not create byref on frame
    if Span.isEmpty input then // no more chars so return acc with the currentWord and end frame
        push currentWord |> List.rev
    else
        let currentChar = Span.head input

        match input with
        | Acronym ->
            // look-ahead check; if 3rd char lower...
            if input.Length > 2 && Char.IsLower(Span.nextNext input) then // current character ends the currentWord
                getWords (Span.tail input) [] (push (wordWith currentChar))
            else // still in the acronym, so add the current character and move to next char
                getWords (Span.tail input) (wordWith currentChar) acc
        | Transition -> getWords (Span.tail input) [] (push (wordWith currentChar))
        | Delimiter -> getWords (Span.tail input) [] (push currentWord)
        | Normal -> getWords (Span.tail input) (wordWith currentChar) acc


let transform (input: string) (targetCase: Case) =
    if String.isEmpty input then
        String.Empty
    else

        let words = getWords (String.asSpan input) [] []

        let f words =
            match targetCase with
            | Kebab -> words |> List.map String.toLower |> String.concat kebabConnector
            | Snake -> words |> List.map String.toLower |> String.concat snakeConnector
            | Constant -> words |> List.map String.toUpper |> String.concat snakeConnector
            | Pascal -> words |> List.map String.asTitle |> String.concat String.Empty
            | Camel ->
                match words with
                | firstWord :: rest ->
                    String.toLower firstWord
                    + (rest |> List.map String.asTitle |> String.concat String.Empty)
                | _ -> String.Empty

        f words
