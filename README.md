# Fray

This is a quickly written command-line utility.
There is also a packaged PowerShell cmdlet.

It's possibly performant; I ensured tail-call optimization and memoization.
It does not use Regex.
I used a span to minimize reallocating new frames.

In any case, it's a quick-dirty thing I built fast to do one thing: Code String Transforms in the .NET environment.

## Install

Since I did this fast just for me, no fancy packages or uploads to Nuget/PSGallery.
If you want it--

1. Clone this repo.
2. Install the PowerShell Module/Script `Invoke-Build`, if you haven't already, however you want.
3. Run `Invoke-Build Install`.

If you have .NET 9, it should be seamless and smooth;
`Invoke-Fray` will be added to your Modules directory and `fray.exe` will be installed as a `dotnet` tool.

Fray is NativeAoT compatible, so if you want to add that build task, go ahead. It should work.
You'll get some errors, but it's just FSharp stuff, and it should be fine.
