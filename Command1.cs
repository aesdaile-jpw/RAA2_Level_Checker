using RAA2_Level_Checker.Forms;
using System.Windows.Controls;

namespace RAA2_Level_Checker
{
    [Transaction(TransactionMode.Manual)]
    public class Command1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Your code goes here
            EventAction myAction = new EventAction();
            ExternalEvent myEvent = ExternalEvent.Create(myAction);

            //EventAction2 myActionReset = new EventAction2();
            //ExternalEvent myEventReset = ExternalEvent.Create(myActionReset);

            Modeless1 currentform = new Modeless1(myEvent)
            {
                Width = 400,
                Height = 600,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = true,
            };

            currentform.Show();

            return Result.Succeeded;
        }
        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnRAA2_Level_Checker";
            string buttonTitle = "Level Checker";

            Common.ButtonDataClass myButtonData = new Common.ButtonDataClass(
                buttonInternalName,
                buttonTitle,
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                Properties.Resources.Green_32,
                Properties.Resources.Green_16,
                "Runs the Level Checker app");

            return myButtonData.Data;
        }
    }



    public class EventAction : IExternalEventHandler
    {
        public void Execute(UIApplication uiapp)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            // Your code goes here

            List<ElementId> selectedElems = uidoc.Selection.GetElementIds().ToList();

            TaskDialog.Show("Test", "You selected " + selectedElems.Count.ToString() + " elements.");
        }
        public string GetName()
        {
            return "Level Checker Modeless Form";
        }

        private FillPatternElement GetFillPatternByName(Document doc, string name)
        {
            FillPatternElement curFPE = null;

            curFPE = FillPatternElement.GetFillPatternElementByName(doc, FillPatternTarget.Drafting, name);

            return curFPE;
        }
    }

    public class EventAction2 : IExternalEventHandler
    {
        public void Execute(UIApplication uiapp)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            // Your code goes here
            // get all elements in view
            FilteredElementCollector collector = new FilteredElementCollector(doc, doc.ActiveView.Id);
            List<Element> viewElements = collector.Cast<Element>().ToList();

            OverrideGraphicSettings newSettings = new OverrideGraphicSettings();

            using (Transaction t = new Transaction(doc))
            {
                t.Start("Reset elements");

                foreach (Element curElem in viewElements)
                {
                    doc.ActiveView.SetElementOverrides(curElem.Id, newSettings);
                }

                t.Commit();
                t.Dispose();
            }

        }
        public string GetName()
        {
            return "Level Checker Modeless Form Reset Colour";
        }
    }

}
