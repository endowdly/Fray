using System.Management.Automation;

namespace Fray;

[Cmdlet(VerbsLifecycle.Invoke, "Fray")]
[OutputType(typeof(string))]
public class InvokeFrayCommand : PSCmdlet
{

	[Parameter(
		ValueFromPipeline = true,
		ValueFromPipelineByPropertyName = true,
		Position = 0)]
	public string InputObject { get; set; } = string.Empty;

	[Parameter]
	[Alias("Case")]
	public Case TargetCase { get; set; } = Case.Kebab;

	protected override void ProcessRecord()
	{
		try
		{
			string result = Fray.Invoke(InputObject, TargetCase);

			WriteObject(result);
		}
		catch (Exception ex)
		{
			WriteError(new ErrorRecord(
				ex,
				"FrayTransformError",
				ErrorCategory.NotSpecified,
				InputObject));
		}
	}
}
