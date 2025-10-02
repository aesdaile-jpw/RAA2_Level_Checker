using System;
using System.Collections.Generic;
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
        public Modeless1(ExternalEvent _Event)
        {
            InitializeComponent();
            this.myEvent = _Event;
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            myEvent.Raise();
        }
    }
}
