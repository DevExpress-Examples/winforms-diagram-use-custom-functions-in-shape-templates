<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/657656532/17.1.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1174052)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# WinForms DiagramControl - Complex Expressions and Custom Functions in Shape Templates

This example demonstrates how to specify complex expressions and use custom functions to calculate the `ShapeTemplate`'s [Parameter](https://docs.devexpress.com/CoreLibraries/DevExpress.Diagram.Core.Shapes.Parameter) value. You can use custom expressions or functions to implement complex logic for `Parameters` (for example, a circular motion).

In this example, the custom **Sector** shape allows users to specify its angle:

![image](https://github.com/DevExpress-Examples/winforms-diagram-use-custom-functions-in-shape-templates/assets/65009440/2400b977-2c89-4991-94c0-987d58f58ff3)

## Implementation Details

`ShapeTemplates` accept functions that implement the [ICustomFunctionOperator](https://docs.devexpress.com/CoreLibraries/DevExpress.Data.Filtering.ICustomFunctionOperator) interface. The [Criteria Language Syntax](https://docs.devexpress.com/CoreLibraries/4928/devexpress-data-library/criteria-language-syntax) allows you to calculate shape `Parameters`.

Follow the steps below to create a custom calculation function:
1. Create a function class that implements the [ICustomFunctionOperator](https://docs.devexpress.com/CoreLibraries/DevExpress.Data.Filtering.ICustomFunctionOperator) interface.
2. Use the [CriteriaOperator.RegisterCustomFunction](https://docs.devexpress.com/CoreLibraries/DevExpress.Data.Filtering.CriteriaOperator.RegisterCustomFunction(DevExpress.Data.Filtering.ICustomFunctionOperator)) method to register this function.

```cs
public class CreateArcPoint : ICustomFunctionOperator {
	private static readonly CreateArcPoint instance = new CreateArcPoint();

	public static void Register() {
		CriteriaOperator.RegisterCustomFunction(instance);
	}
	public static void Unregister() {
		CriteriaOperator.UnregisterCustomFunction(instance);
	}

	public string Name => nameof(CreateArcPoint);

	public Type ResultType(params Type[] operands) {
		return typeof(double);
	}

	public object Evaluate(params object[] operands) {
		if (operands.Length == 3
			&& operands[0] is double X
			&& operands[1] is double Y
			&& operands[2] is string axe) {

			if (axe is "X")
				return X > 0.5 ? X : 0.5;
			return X > 0.5 ? Y : 1;
		}
		else
			return null;
	}
}
```

```xml
<ShapeTemplate Id="Sector" DefaultSize="120, 120">
	<Start X="0.5" Y="0" />
	<Line X="0.5" Y="0.5"/>
	<Line X="(COS([P0]) + 1)/2" Y="(SIN([P0]) + 1)/2" />
	<Arc X="CreateArcPoint((COS([P0]) + 1)/2, (SIN([P0]) + 1)/2, 'X')"
		 Y="CreateArcPoint((COS([P0]) + 1)/2, (SIN([P0]) + 1)/2, 'Y')"
		 Direction="Counterclockwise"
		 Size="CreateSize([W]/2, [H]/2)"/>

	<Arc X="0.5" Y="0" Direction="Counterclockwise" Size="CreateSize([W]/2, [H]/2)" />
	<ConnectionPoints>
		<ShapePoint X="0.5" Y="1" />
		<ShapePoint X="1" Y="0.5" />
		<ShapePoint X="0.5" Y="0" />
		<ShapePoint X="0" Y="0.5" />
	</ConnectionPoints>
	<Parameters>
		<Parameter DefaultValue="0"
			   Point="CreatePoint((COS([P]) + 1)/2*[W], (SIN([P]) + 1)/2*[H])"
			   Value="atn2([P.Y]/[H]*2-1, [P.X]/[W]*2-1)"
			   Min="-3.14" Max="3.14" />
	</Parameters>
</ShapeTemplate>
```

## Files to Review

- [Form1.cs](./CS/WindowsFormsApp4/Form1.cs) (VB: [Form1.vb](./VB/WindowsFormsApp4/Form1.vb))
- [CustomShapes.xml](./CS/WindowsFormsApp4/CustomShapes.xml)

## Documentation

- [ICustomFunctionOperator](https://docs.devexpress.com/CoreLibraries/DevExpress.Data.Filtering.ICustomFunctionOperator)
- [Shapes](https://docs.devexpress.com/WindowsForms/116882/controls-and-libraries/diagrams/diagram-items/shapes)
- [Use Shape Templates to Create Shapes and Containers](https://docs.devexpress.com/WindowsForms/17764/controls-and-libraries/diagrams/diagram-items/creating-shapes-and-containers-using-shape-templates)

## More Examples

- [Diagram Control for WinForms - Create Custom Shapes with Connection Points](https://github.com/DevExpress-Examples/winforms-diagram-create-custom-shapes-with-connection-points)
