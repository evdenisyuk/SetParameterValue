#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Media.Imaging;

#endregion

namespace SetpValue
{
    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            string assemblyName = Assembly.GetExecutingAssembly().Location;
            string path = System.IO.Path.GetDirectoryName(assemblyName);

            string tabName = "PERI R";
            string panelName = "Input";

            a.CreateRibbonTab(tabName);

            // Create PERI R tab
            var panel = a.CreateRibbonPanel(tabName, panelName);

            // Create button and assign function to it
            PushButtonData btnData1 = new PushButtonData("Set parameter value", "Set value", assemblyName, "SetpValue.Command");
            PushButtonData btnData2 = new PushButtonData("Info", "Info", assemblyName, "SetpValue.Info");

            btnData1.Image = new BitmapImage(new Uri(path + @"\icons8-key-16.png"));
            btnData2.Image = new BitmapImage(new Uri(path + @"\icons8-support-16.png"));

            panel.AddStackedItems(btnData1, btnData2);
            panel.AddSeparator();

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
