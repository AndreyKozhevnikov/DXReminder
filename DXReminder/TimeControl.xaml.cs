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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace DXReminder {
    /// <summary>
    /// Interaction logic for TimeControl.xaml
    /// </summary>
    public partial class TimeControl : UserControl {
        public TimeControl() {
            InitializeComponent();
            this.Loaded+=TimeControl_Loaded;
        }

        void TimeControl_Loaded(object sender, RoutedEventArgs e) {
            EditValue = new List<object>();
        }



        public List<object> EditValue {
            get { return (List<object>)GetValue(EditValueProperty); }
            set { SetValue(EditValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EditValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditValueProperty =
            DependencyProperty.Register("EditValue", typeof(List<object>), typeof(TimeControl), new PropertyMetadata(new List<object>()));

   




        public DateTime SingleTime { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e) {
            EditValue.Add(SingleTime);
            UpdateListBox();
        }

        private void UpdateListBox() {
            var v = this.EditValue;
            EditValue = null;
            EditValue = v;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            EditValue.Remove(lstTimes.SelectedItem);
            UpdateListBox();
        }

        #region Test
        public void Test_Button_Click() {
            Button_Click(null, null);
        }
        #endregion
    }
}
