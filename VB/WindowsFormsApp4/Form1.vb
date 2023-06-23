Imports DevExpress.Data.Filtering
Imports DevExpress.Diagram.Core
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Reflection
Imports System.Windows.Forms

Namespace WindowsFormsApp4

    Public Partial Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
            CreateArcPoint.Register()
            RegisterStencil()
        End Sub

        Private Sub RegisterStencil()
            Dim stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WindowsFormsApp4.CustomShapes.xml")
            Dim stencil = DiagramStencil.Create("CustomStencil", "Custom Shapes", stream, Function(shapeName) shapeName)
            DiagramToolboxRegistrator.RegisterStencil(stencil)
            diagramControl1.SelectedStencils = New StencilCollection() From {"CustomStencil"}
        End Sub
    End Class

    Public Class CreateArcPoint
        Implements ICustomFunctionOperator

        Private Shared ReadOnly instance As CreateArcPoint = New CreateArcPoint()

        Public Shared Sub Register()
            CriteriaOperator.RegisterCustomFunction(instance)
        End Sub

        Public Shared Sub Unregister()
            CriteriaOperator.UnregisterCustomFunction(instance)
        End Sub

        Public ReadOnly Property Name As String Implements ICustomFunctionOperator.Name
            Get
                Return NameOf(CreateArcPoint)
            End Get
        End Property

        Public Function ResultType(ParamArray operands As Type()) As Type Implements ICustomFunctionOperator.ResultType
            Return GetType(Double)
        End Function

        Public Function Evaluate(ParamArray operands As Object()) As Object Implements ICustomFunctionOperator.Evaluate
            Dim X As Double = Nothing, Y As Double = Nothing, axe As String = Nothing
            If operands.Length = 3 AndAlso CSharpImpl.__Assign(X, TryCast(operands(0), Double)) IsNot Nothing AndAlso CSharpImpl.__Assign(Y, TryCast(operands(1), Double)) IsNot Nothing AndAlso CSharpImpl.__Assign(axe, TryCast(operands(2), String)) IsNot Nothing Then
                If axe Is "X" Then Return If(X > 0.5, X, 0.5)
                Return If(X > 0.5, Y, 1)
            Else
                Return Nothing
            End If
        End Function

        Private Class CSharpImpl

            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class
End Namespace
