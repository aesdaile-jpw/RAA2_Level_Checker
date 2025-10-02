using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RAA2_Level_Checker.Forms
{
    /// <summary>
    /// Interaction logic for Modeless1.xaml
    /// </summary>
    public partial class Modeless1 : Window
    {
        ExternalEvent myEvent;

        ObservableCollection<Level> Levels { get; set; }
        //ObservableCollection<EventForm> _Eventform { get; set; }
        public Modeless1(ExternalEvent _Event, List<Level> _Levels)
        {
            InitializeComponent();
            Levels = new ObservableCollection<Level>(_Levels);
            cmbLevel.ItemsSource = Levels;
            //_Eventform = new ObservableCollection<EventForm>();

            this.myEvent = _Event;
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (rdbColour.IsChecked == true)
            {
                Globals.SetColour = true;
            }
            else
            {
                Globals.SetColour = false;
            }
            Globals.LevelToSet = (Level)cmbLevel.SelectedItem;
            Globals.ChkWalls = (bool)chkWalls.IsChecked;
            Globals.ChkFloors = (bool)chkFloors.IsChecked;
            Globals.ChkRoofs = (bool)chkRoofs.IsChecked;
            Globals.ChkCeilings = (bool)chkCeilings.IsChecked;
            Globals.ChkDoors = (bool)chkDoors.IsChecked;
            Globals.ChkWindows = (bool)chkWindows.IsChecked;
            Globals.ChkColumns = (bool)chkColumns.IsChecked;
            Globals.ChkStructuralFraming = (bool)chkStructuralFraming.IsChecked;
            Globals.ChkFurniture = (bool)chkFurniture.IsChecked;
            Globals.ChkCasework = (bool)chkCasework.IsChecked;
            Globals.ChkSpecialityEquipment = (bool)chkSpecialityEquipment.IsChecked;

            SetColour();

            myEvent.Raise();
        }

        private void SetColour()
        {
            if (rdbColour.IsChecked == true)
                Globals.ColourToSet = new Color(255,0,0);
            if (rdbGreen.IsChecked == true)
                Globals.ColourToSet = new Color(0, 255, 0);
            if (rdbBlue.IsChecked == true)
                Globals.ColourToSet = new Color(0, 0, 255);
            if (rdbYellow.IsChecked == true)
                Globals.ColourToSet = new Color(255, 255, 0);
        }
    }
}
