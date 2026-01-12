namespace Fray

open System
open System.Management.Automation

[<Cmdlet(VerbsLifecycle.Invoke, "Fray")>]
[<OutputType(typeof<string>)>]
type InvokeFrayCommand() =
    inherit PSCmdlet()

    [<Parameter(ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)>]
    member val InputObject: obj = "" with get, set

    [<Parameter>]
    member val TargetCase: Case = Case.Kebab with get, set

    override this.BeginProcessing () : unit = base.BeginProcessing ()

    override this.ProcessRecord () : unit =

        let strVal =
            match this.InputObject with
            | :? string as s -> s
            | o -> o.ToString ()

        let result = Fray.Invoke (strVal, this.TargetCase)

        this.WriteObject result

    override this.EndProcessing () : unit = base.EndProcessing ()
