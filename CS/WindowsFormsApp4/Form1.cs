using DevExpress.Data.Filtering;
using DevExpress.Diagram.Core;
using DevExpress.Utils.Serializing;
using DevExpress.XtraDiagram;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            CreateArcPoint.Register();
            RegisterStencil();
        }

        void RegisterStencil()
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("WindowsFormsApp4.CustomShapes.xml");
            var stencil = DiagramStencil.Create("CustomStencil", "Custom Shapes", stream, shapeName => shapeName);
            DiagramToolboxRegistrator.RegisterStencil(stencil);

            diagramControl1.SelectedStencils = new StencilCollection() { "CustomStencil" };
        }
    }

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
                && operands[0] is double
                && operands[1] is double
                && operands[2] is string) {

                var X = (double)operands[0];
                var Y = (double)operands[1];
                var axe = (string)operands[2];

                if (axe is "X")
                    return X > 0.5 ? X : 0.5;
                return X > 0.5 ? Y : 1;
            }
            else
                return null;
        }
    }
}
