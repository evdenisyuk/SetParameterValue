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
    public class Detele : IExternalCommand
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

            FilteredElementCollector viewCollector = new FilteredElementCollector(doc);
            viewCollector.OfCategory(BuiltInCategory.OST_MechanicalEquipment).WhereElementIsNotElementType();
            IList<Element> filteredElement = viewCollector.ToElements() as List<Element>;

            string msg = "";

            List<object> seen = new List<object> { };
            List<int> result = new List<int> { };

            List<string> container = new List<string> { };

            foreach (var item in filteredElement)
            {
                string s = "";

                // Define location point of filtered element
                LocationPoint lp = item.Location as LocationPoint;
                s += item.Name.ToString();
                s += lp.Point.ToString();
                s += lp.Rotation.ToString();

                container.Add(s);
            }

            for (int i = 0; i < container.Count; i++)
            {
                if (!seen.Contains(container[i]))
                {
                    seen.Add(container[i]);
                }
                else
                {
                    using (Transaction t = new Transaction(doc, "Delete elements"))
                    {
                        t.Start();
                        msg += $"Deleted IDs: {filteredElement[i].Id}\n";
                        doc.Delete(filteredElement[i].Id);
                        t.Commit();
                    }
                }
            }

            // Check if string is empty
            if (string.IsNullOrEmpty(msg))
            {
                msg += "None";
            }

            // Show popup window
            TaskDialog.Show("Revit", msg);


            return Result.Succeeded;
        }
    }
}
