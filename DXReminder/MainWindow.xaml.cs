﻿using DXReminder.Classes;
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
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            this.Visibility = Visibility.Collapsed;
        }

        BaseViewModel vm;

        private void Button_Click(object sender, RoutedEventArgs e) {
            vm.Temp_Serialize();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            vm.Temp_CreateReminder();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            // vm.Test_Proccessor.Test_ShowNotification(new Reminder("newrem",null,null));
            vm.Test_Proccessor.Test_ProccessTime(new DateTime(2016, 7, 13, 15, 7, 1));
        }
    }
}
