using DevExpress.Mvvm.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;

namespace DXReminder.Classes {
    public class RemindProcessor {
        private List<Reminder> reminders;
        public RemindProcessor(List<Reminder> _reminders) {
            this.reminders = _reminders;
        }
        System.Windows.Forms.Timer timer;
        public void Start() {
            timer = new System.Windows.Forms.Timer();
            timer.Tick += OnTimer;
            timer.Interval = 10000;
            timer.Start();

        }
        string currentItemId;
        void OnTimer(object sender, EventArgs e) {
            string st = GetTimeIdFromTime(DateTime.Now);
            if (currentItemId != st) {
                currentItemId = st;
                var list = GetRemindersForTime(DateTime.Now);
                foreach (Reminder r in list) {
                    ShowNotification(r);
                }
            }
           

        }

        private void ShowNotification(Reminder r) {
            Debug.Print(r.Description);
            NotificationService serv = new NotificationService();
            string st = @" <DataTemplate  xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" > " +
            "<Grid>"+
                "<Grid.ColumnDefinitions>"+
                    "<ColumnDefinition Width = \"Auto\" />"+
 
                   "  <ColumnDefinition/>"+
 
               "  </Grid.ColumnDefinitions>"+
 
                " <Border Background = \"Crimson\" CornerRadius = \"10,0,0,10\">"+
    
                       " <Image Source = \"Resources/excl.jpg\" Stretch = \"Fill\" Margin = \"10\"/>"+
         
                       "  </Border>"+
         
                        " <Border Grid.Column = \"1\" CornerRadius = \"0,10,10,0\" Background = \"Crimson\">"+
              
                                  "<Label Content = \"{Binding}\" FontSize = \"35\" FontWeight = \"Bold\" "+
                           "Background=\"SandyBrown\" "+
                                          "Margin = \"10,15,15,15\" "+
                                          "VerticalAlignment = \"Stretch\" "+
                                          "HorizontalAlignment = \"Stretch\" "+
                           "/>"+
                "</Border>"+
            "</Grid>"+
        "</DataTemplate> ";
           
            StringReader sr = new StringReader(st);
            XmlReader xmlReader = XmlReader.Create(sr);
            DataTemplate dt = XamlReader.Load(xmlReader) as DataTemplate;
            serv.CustomNotificationTemplate = dt;
         //   serv.CustomNotificationTemplate = (DataTemplate)Application.Current.Resources["CustomNotificationTemplate"];
            serv.CustomNotificationDuration = new TimeSpan(12, 0, 0);
            serv.CustomNotificationVisibleMaxCount = 5;
            var not = serv.CreateCustomNotification(r.Description);
            not.ShowAsync();
        }

        private List<Reminder> GetRemindersForTime(DateTime dt) {
            var lst = reminders.Where(x => x.DayOfWeekList.Contains((int)dt.DayOfWeek) && x.TimeList.Contains(new DateTime(1, 1, 1, dt.Hour, dt.Minute, 0))).ToList();
            return lst;
        }


        private string GetTimeIdFromTime(DateTime dt) {

            var st = string.Format("{0}-{1}-{2}",(int) dt.DayOfWeek, dt.Hour, dt.Minute);
            return st;
        }


        #region
        public List<Reminder> Test_GetAllRemindersForTime(DateTime dt) {
            return GetRemindersForTime(dt);
        }
        public string Test_currentItemId {
            get {
                return this.currentItemId;
            }
            set {
                this.currentItemId = value;
            }
        }
     
        public string Test_GetTimeIdFromTime( DateTime tm) {
            return GetTimeIdFromTime(tm);
        }
     public void Test_ShowNotification(Reminder r) {
            this.ShowNotification(r);
        }
        #endregion
    }
}
