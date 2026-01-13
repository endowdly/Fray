module Soothing

open Xunit

open FsCheck
open FsCheck.Xunit
open FsUnit
open FsUnit.Xunit

open Fray

[<Theory>]
[<InlineData("helloWorld", "hello-world")>]
[<InlineData("simple_snake_case", "simple-snake-case")>]
[<InlineData("PascalCaseString", "pascal-case-string")>]
[<InlineData("CONSTANT_CASE_STRING", "constant-case-string")>]
let ``Basic transformations result in correct Kebab case`` (input, expected) =
    Fray.Invoke (input, Case.Kebab) |> should equal expected

[<Theory>]
[<InlineData("HTTPRequest", "http-request")>]
[<InlineData("parseJSONData", "parse-json-data")>]
[<InlineData("XMLParser", "xml-parser")>]
[<InlineData("getUDPStream", "get-udp-stream")>]
let ``Acronyms are preserved and separated correctly`` (input, expected) =
    Fray.Invoke (input, Case.Kebab) |> should equal expected

[<Fact>]
let ``Constant case handles words correctly`` () =
    Fray.Invoke ("myVariable", Case.Constant)
    |> should equal "MY_VARIABLE"

[<Fact>]
let ``Pascal case capitalizes every word`` () =
    Fray.Invoke ("kebab-to-pascal", Case.Pascal)
    |> should equal "KebabToPascal"

[<Fact>]
let ``Empty or whitespace input returns empty string`` () =
    Fray.Invoke ("", Case.Kebab) |> should equal ""
    Fray.Invoke ("      ", Case.Kebab) |> should equal ""

[<InlineData("kebab", "test-string")>]
[<InlineData("snake", "test_string")>]
[<InlineData("constant", "TEST_STRING")>]
[<InlineData("Pascal", "TestString")>]
[<InlineData("Camel", "testString")>]
let ``Fray.Invoke works with string cases`` (inputCase: string, expected: string) =
    let inStr = "testString"
    Fray.Invoke (inStr, inputCase) |> should equal expected

[<Property>]
let ``Transforming to Kebab case never contains uppercase letters`` (input: string) =
    Fray.Invoke (input, Case.Kebab)
    |> Seq.forall (fun c -> not (System.Char.IsUpper c))

[<Property>]
let ``Transforming to Constant case never contains lowercase letters`` (input: string) =
    Fray.Invoke (input, Case.Constant)
    |> Seq.forall (fun c -> not (System.Char.IsLower c))
