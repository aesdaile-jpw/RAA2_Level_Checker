using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAA2_Level_Checker
{
    public static class Globals
    {
        public const string AppName = "RAA2 Level Checker";
        public const string AppVersion = "1.0.0";
        public const string AppAuthor = "Adrian.Esdaile";
        public const string AppCompany = "JPW Architects";
        public const string AppCopyright = "© 2025 JPW. All rights reserved.";

        public static bool SetColour;
        //public static Color ColourToSet;
        public static Level LevelToSet;
        public static bool ChkWalls;
        public static bool ChkFloors;
        public static bool ChkRoofs;
        public static bool ChkCeilings;
        public static bool ChkDoors;
        public static bool ChkWindows;
        public static bool ChkColumns;
        public static bool ChkStructuralFraming;
        public static bool ChkFurniture;
        public static bool ChkCasework;
        public static bool ChkSpecialityEquipment;
    }

    public class EventForm
    {
        public Level LevelToSet { get; set; }
        public bool SetColour { get; set; }
        public bool ChkWalls { get; set; }
        public bool ChkFloors { get; set; }
        public bool ChkRoofs { get; set; }
        public bool ChkCeilings { get; set; }
        public bool ChkDoors { get; set; }
        public bool ChkWindows { get; set; }
        public bool ChkColumns { get; set; }
        public bool ChkStructuralFraming { get; set; }
        public bool ChkFurniture { get; set; }
        public bool ChkCasework { get; set; }
        public bool ChkSpecialityEquipment { get; set; }
    }
}
