module Soothing

open Xunit

open FsCheck
open FsCheck.Xunit
open FsUnit
open FsUnit.Xunit

open Root

[<Theory>]
[<InlineData("helloWorld", "hello-world")>]
[<InlineData("simple_snake_case", "simple-snake-case")>]
[<InlineData("PascalCaseString", "pascal-case-string")>]
[<InlineData("CONSTANT_CASE_STRING", "constant-case-string")>]
let ``Basic transformations result in correct Kebab case`` (input, expected) =
    transform input Kebab |> should equal expected

[<Theory>]
[<InlineData("HTTPRequest", "http-request")>]
[<InlineData("parseJSONData", "parse-json-data")>]
[<InlineData("XMLParser", "xml-parser")>]
[<InlineData("getUDPStream", "get-udp-stream")>]
let ``Acronyms are preserved and separated correctly`` (input, expected) =
    transform input Kebab |> should equal expected

[<Fact>]
let ``Constant case handles words correctly`` () =
    transform "myVariable" Constant |> should equal "MY_VARIABLE"

[<Fact>]
let ``Pascal case capitalizes every word`` () =
    transform "kebab-to-pascal" Pascal |> should equal "KebabToPascal"

[<Fact>]
let ``Empty or whitespace input returns empty string`` () =
    transform "" Kebab |> should equal ""
    transform "     " Kebab |> should equal ""

[<Property>]
let ``Transforming to Kebab case never contains uppercase letters`` (input: string) =
    let result = transform input Kebab
    transform input Kebab |> Seq.forall (fun c -> not (System.Char.IsUpper c))

[<Property>]
let ``Transforming to Constant case never contains lowercase letters`` (input: string) =
    transform input Constant |> Seq.forall (fun c -> not (System.Char.IsLower c))
