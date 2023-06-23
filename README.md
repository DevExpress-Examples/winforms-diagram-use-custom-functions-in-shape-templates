# WinForms DiagramControl - Complex expressions and custom functions in ShapeTemplates

This example demonstrates how to write complex expressions and use custom functions to calculate a `Parameter`'s value in `ShapeTemplates`. You can use custom expressions or functions when you need to implement complex logic for `Parameters` like circular motion.

ShapeTemplates accept common functions that implement the [ICustomFunctionOperator](https://docs.devexpress.com/CoreLibraries/DevExpress.Data.Filtering.ICustomFunctionOperator) interface. The language syntaxt used to calculate `Parameters` is the as the [Criteria Language Syntax](https://docs.devexpress.com/CoreLibraries/4928/devexpress-data-library/criteria-language-syntax).

If you need to create a custom function that performs specific calculation, you need to implement the`ICustomFunctionOperator` interface and register the function using the `CriteriaOperator.RegisterCustomFunction` method.

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

- link.cs (VB: link.vb)
- link.js
- ...

## Documentation

- link
- link
- ...

## More Examples

- link
- link
- ...
