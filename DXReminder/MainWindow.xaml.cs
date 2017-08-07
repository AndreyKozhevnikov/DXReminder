using DevExpress.Mvvm;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DXReminder.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
#if DEBUG
            this.Title = "Debug";
#endif
        }

        NotifyIconService serv;
        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            this.Visibility = Visibility.Collapsed;
            serv = new NotifyIconService();
            IconBitmapDecoder ibd = new IconBitmapDecoder(

               new Uri(@"pack://application:,,/Resources/warning.ico", UriKind.RelativeOrAbsolute),

               BitmapCreateOptions.None,

               BitmapCacheOption.Default);
            var v = new System.Drawing.Icon(Application.GetResourceStream(new Uri(@"pack://application:,,/Resources/warning.ico")).Stream);
            serv.Icon = v;
            serv.LeftClickCommand = new DelegateCommand(OnLeftClick);

            PopupMenu menu = new PopupMenu();
            BarButtonItem item = new BarButtonItem() { Content = "Close application" };
            item.ItemClick += item_ItemClick;
            menu.Items.Add(item);

            serv.ContextMenu = menu;

            Type vs1 = typeof(AttachableObjectBase);
            var v1 = vs1.GetField("associatedObject", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            v1.SetValue(serv, new FrameworkElement());

            Type vs = typeof(NotifyIconService);
            MethodInfo fi = vs.GetMethod("InitializeWpfNotifyIcon", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            fi.Invoke(serv, null);

            MethodInfo setMenu = vs.GetMethod("SetActualContextMenu", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            setMenu.Invoke(serv, null);

            serv.ThemeName = "MetropolisDark";




            (serv as INotifyIconService).SetStatusIcon(v);

            vm.StartProcessCommand.Execute(null);
        }

        private void item_ItemClick2(object sender, ItemClickEventArgs e) {
            if (this.Visibility == Visibility.Visible) {
                this.Visibility = Visibility.Collapsed;
            }
            else {
                this.Visibility = Visibility.Visible;
            }
        }

        protected override void OnClosing(CancelEventArgs e) {
            e.Cancel = shouldLive;
            if (shouldLive) {
                this.Visibility = Visibility.Collapsed;
            }
            else {
                Type vs = typeof(NotifyIconService);
                MethodInfo fi = vs.GetMethod("OnWindowClosing", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                fi.Invoke(serv, new object[2] { null, null });
            }
        }
        void item_ItemClick(object sender, ItemClickEventArgs e) {
            this.vm.Processor.CloseStreamWriter();
            shouldLive = false;
            Environment.Exit(0);
        }
        bool shouldLive = true;
        private void OnLeftClick() {

            this.Visibility = Visibility.Visible;
            this.WindowState = WindowState.Normal;
            this.Topmost = true;
            this.Topmost = false;
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
            vm.Processor.Test_ProccessTime(new DateTime(2016, 7, 15, 15, 0, 0));
        }

    }
}
