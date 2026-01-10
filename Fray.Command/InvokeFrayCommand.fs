namespace Fray

open System.Management.Automation

[<Cmdlet("Get", "Foo")>]
[<OutputType(typeof<string>)>]
type InvokeFrayCommand() =
    inherit PSCmdlet()

    [<Parameter(ValueFromPipeline = true)>]
    [<ValidateNotNull>]
    member val InputObject: obj = "" with get, set

    [<Parameter>]
    [<ValidateSet(typeof<Case>)>]
    member val TargetCase: Case = Kebab with get, set

    override this.BeginProcessing () = base.BeginProcessing ()

    override this.ProcessRecord () : unit =
        let str =
            match this.InputObject with
            | :? string as s -> s
            | o -> o.ToString ()

        let result = Root.transform str this.TargetCase
        this.WriteObject result

    override this.EndProcessing () : unit = base.EndProcessing ()
