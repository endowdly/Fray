module internal Seed

open System

[<Literal>]
let kebabConnector = "-"

[<Literal>]
let snakeConnector = "_"

[<Literal>]
let space = " "

type Case =
    | Kebab
    | Pascal
    | Constant
    | Snake
    | Camel

type 'a span = ReadOnlySpan<'a>
type 'a memory = ReadOnlyMemory<'a>

module Span =
    // These will throw exceptions if you are not careful! ...I should do some handling but eh.
    let head (a: 'a span) = a[0]
    let isEmpty (a: 'a span) = a.IsEmpty
    let next (a: 'a span) = a[1]
    let nextNext (a: 'a span) = a[2]
    let tail (a: 'a span) = a.Slice 1

module Char =
    let toString (c: char) = c.ToString ()
    let toUpper (c: char) = Char.ToUpperInvariant c

module String =
    let asSpan (s: string) = s.AsSpan ()
    let asTitle (s: string) =
        s.Substring(0, 1).ToUpperInvariant ()
        + s.Substring(1).ToLowerInvariant ()
    let head (s: string) = s[0]
    let isEmpty (s: string) = String.IsNullOrEmpty s || String.IsNullOrWhiteSpace s
    let length (s: string) = s.Length
    let subString (s: string) n = s.Substring n
    let toLower (s: string) = s.ToLowerInvariant ()
    let toUpper (s: string) = s.ToUpperInvariant ()
