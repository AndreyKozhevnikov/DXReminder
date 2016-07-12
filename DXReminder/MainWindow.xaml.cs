using DXReminder.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DXReminder {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            vm = new BaseViewModel();
            vm.Deserialize();
            DataContext = vm;
         
        }
        BaseViewModel vm;

        private void Button_Click(object sender, RoutedEventArgs e) {
            vm.Temp_Serialize();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            vm.Temp_CreateReminder();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            List<DateTime> newList = new List<DateTime>();
            newList.Add(new DateTime(1, 1, 1, 16, 1, 1));
            Reminder r = new Reminder("test", null, newList);
            Debug.Print(r.TimeList.Count.ToString());

            newList.Add(new DateTime(1, 1, 1, 4, 4, 1));
            Debug.Print(r.TimeList.Count.ToString());
        }
    }
}
