#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Principal;
using System.Text;

#endregion

namespace SetpValue
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            MainWindow window = new MainWindow();
            window.ShowDialog();

            string parameterText = window.parameterText;

            FilteredElementCollector viewCollector = new FilteredElementCollector(doc, uidoc.ActiveView.Id);
            viewCollector.OfCategory(BuiltInCategory.OST_MechanicalEquipment).WhereElementIsNotElementType();

            using (Transaction t = new Transaction(doc, "SetParameter"))
            {
                t.Start();
                foreach (Element i in viewCollector.ToElements())
                {
                    Parameter p = i.LookupParameter("Note_Shared");
                    p.Set(parameterText);
                }
                t.Commit();
            }

            return Result.Succeeded;
        }
    }
}
