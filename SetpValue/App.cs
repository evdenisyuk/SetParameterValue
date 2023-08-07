#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Reflection;

#endregion

namespace SetpValue
{
    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            string tabName = "PERI R";
            string panelName = "Info";

            a.CreateRibbonTab(tabName);

            // Create PERI R custom tab
            var panel = a.CreateRibbonPanel(tabName, panelName);

            // Create button and assign function to it
            var button1 = new PushButtonData("Info", "Set NOTE" + "\r" + "Parameters",
                Assembly.GetExecutingAssembly().Location, "SetpValue.Command");
            var btn1 = panel.AddItem(button1) as PushButton;


            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
