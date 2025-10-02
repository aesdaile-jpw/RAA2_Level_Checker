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

            FilteredElementCollector levelCollector = new FilteredElementCollector(doc);
            List<Level> allLevels = levelCollector.OfClass(typeof(Level)).Cast<Level>().OrderBy(lv => lv.Elevation).ToList();

            Modeless1 currentform = new Modeless1(myEvent, allLevels)
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

            //List<ElementId> selectedElems = uidoc.Selection.GetElementIds().ToList();
            //TaskDialog.Show("Test", "You selected " + selectedElems.Count.ToString() + " elements.");

            if(Globals.SetColour == false)
            {
                // RESET: get all elements in view
                FilteredElementCollector collector = new FilteredElementCollector(doc, doc.ActiveView.Id);
                List<Element> viewElements = collector.Cast<Element>().ToList();

                OverrideGraphicSettings newSettings = new OverrideGraphicSettings();

                using (Transaction t = new Transaction(doc))
                {
                    t.Start("Reset elements");
                    foreach (Element curElem in viewElements)
                    {
                        try
                        {
                            doc.ActiveView.SetElementOverrides(curElem.Id, newSettings);
                        }
                        catch (Exception ex)
                        {
                            Debug.Print("Error resetting element ID " + curElem.Id.ToString() + ": " + ex.Message);
                        }
                    }
                    t.Commit();
                    t.Dispose();
                }
                return;
            }

            if (Globals.LevelToSet == null)
            {
                TaskDialog.Show("Error", "Please select a level.");
                return;
            }

            //FilteredElementCollector collector2 = new FilteredElementCollector(doc, doc.ActiveView.Id);
            //collector2.Cast<Element>().Where<Element>(e =>
            //(Globals.ChkWalls && e is Wall) && (e.LevelId == Globals.LevelToSet.Id));

            var collector2 = new FilteredElementCollector(doc, doc.ActiveView.Id)
                .WhereElementIsNotElementType()
                .WherePasses(new ElementLevelFilter(Globals.LevelToSet.Id))
                .Where(e =>
                    (Globals.ChkWalls && e is Wall) ||
                    (Globals.ChkFloors && e is Floor) ||
                    (Globals.ChkRoofs && e is RoofBase) ||
                    (Globals.ChkCeilings && e is Ceiling) ||
                    (Globals.ChkDoors && e is FamilyInstance fi1 && fi1.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Doors) ||
                    (Globals.ChkWindows && e is FamilyInstance fi2 && fi2.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Windows) ||
                    (Globals.ChkColumns && e is FamilyInstance fi3 && fi3.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Columns) ||
                    (Globals.ChkStructuralFraming && e is FamilyInstance fi4 && fi4.Category.Id.IntegerValue == (int)BuiltInCategory.OST_StructuralFraming) ||
                    (Globals.ChkFurniture && e is FamilyInstance fi5 && fi5.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Furniture) ||
                    (Globals.ChkCasework && e is FamilyInstance fi6 && fi6.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Casework) ||
                    (Globals.ChkSpecialityEquipment && e is FamilyInstance fi7 && fi7.Category.Id.IntegerValue == (int)BuiltInCategory.OST_SpecialityEquipment)
                );

            ElementCategoryFilter wallFilter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);

            List<Element> levelElements = collector2.Cast<Element>().ToList();
            OverrideGraphicSettings ogs = new OverrideGraphicSettings();
            FillPatternElement solidFill = GetFillPatternByName(doc, "<Solid fill>");
            if (solidFill == null)
            {
                TaskDialog.Show("Error", "Could not find 'Solid fill' pattern.");
                return;
            }
            ogs.SetSurfaceForegroundPatternColor(new Color(255, 0, 0)); // Red
            ogs.SetSurfaceForegroundPatternId(solidFill.Id);
            using (Transaction t = new Transaction(doc))
            {
                t.Start("Override elements");
                foreach (Element curElem in levelElements)
                {
                    doc.ActiveView.SetElementOverrides(curElem.Id, ogs);
                }
                t.Commit();
                t.Dispose();
            }
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
