using DevExpress.Mvvm.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class RemindProcessor : INotifyPropertyChanged {
        public List<Reminder> Reminders { get; set; }
        public ObservableCollection<String> LogList { get; set; }
        string logFileName;
        string _currentTimeId;
        string _currentTime;
        public string CurrentTimeId {
            get {
                return _currentTimeId;
            }
            set {
                _currentTimeId = value;
                RaisePropertyChanged("CurrentTimeId");
            }
        }
        public string CurrentTime {
            get {
                return _currentTime;
            }

            set {
                _currentTime = value;
                RaisePropertyChanged("CurrentTime");
            }
        }
        public RemindProcessor(List<Reminder> _reminders) {
            this.Reminders = _reminders;
            CurrentTime = "not working";
            LogList = new ObservableCollection<string>();

        }
        System.Windows.Forms.Timer timer;
        public void Start() {
            timer = new System.Windows.Forms.Timer();
            timer.Tick += OnTimer;
            timer.Interval = 2000;
            timer.Start();

        }

        void OnTimer(object sender, EventArgs e) {
            ProccessTime(DateTime.Now);
            CurrentTime = DateTime.Now.ToString();
        }

        private void ProccessTime(DateTime dt) {
            string st = GetTimeIdFromTime(dt);
            if (CurrentTimeId != st) {
                CurrentTimeId = st;
                var list = GetRemindersForTime(dt);
                foreach (Reminder r in list) {
                    ShowNotification(r);
                }
            }

        }

        private void ShowNotification(Reminder r) {

            NotificationService serv = new NotificationService();
            string st = @" <DataTemplate  xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" > " +
            "<Grid>" +
                "<Grid.ColumnDefinitions>" +
                    "<ColumnDefinition Width = \"Auto\" />" +

                   "  <ColumnDefinition/>" +

               "  </Grid.ColumnDefinitions>" +

                " <Border Background = \"Blue\" CornerRadius = \"10,0,0,10\">" +

                       " <Image Source = \"Resources/excl.jpg\" Stretch = \"Fill\" Margin = \"10\"/>" +

                       "  </Border>" +

                        " <Border Grid.Column = \"1\" CornerRadius = \"0,10,10,0\" Background = \"Blue\">" +

                                  "<Label Content = \"{Binding}\" FontSize = \"35\" FontWeight = \"Bold\" " +
                           "Background=\"Turquoise\" " +
                                          "Margin = \"10,15,15,15\" " +
                                          "VerticalAlignment = \"Stretch\" " +
                                          "HorizontalAlignment = \"Stretch\" " +
                           "/>" +
                "</Border>" +
            "</Grid>" +
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
            WriteLog(r);
        }

        private void WriteLog(Reminder r) {
            string st = String.Format("{0} - {1}", DateTime.Now.ToString(), r.Description);
            Debug.Print(st);
            AddToLogList(st);
#if !DEBUG
            AddToLogFile(st);
#endif
        }
        StreamWriter sw;
        private void AddToLogFile(string st) {
            logFileName = DateTime.Today.ToString("yyyy-dd-MM") + ".log";
            if (sw == null) {
                sw = new StreamWriter(logFileName, true);
                sw.AutoFlush = true;
            }
            sw.WriteLine(st);
        }
        public void CloseStreamWriter() {
            sw.Flush();
            sw.Close();
        }
        private void AddToLogList(string st) {
            LogList.Insert(0, st);
        }

        private List<Reminder> GetRemindersForTime(DateTime dt) {
            var lst = Reminders.Where(x => x.DayOfWeekList.Contains((int)dt.DayOfWeek) && x.TimeList.Contains(new DateTime(1, 1, 1, dt.Hour, dt.Minute, 0))).ToList();
            return lst;
        }


        private string GetTimeIdFromTime(DateTime dt) {

            var st = string.Format("{0}-{1}-{2}", (int)dt.DayOfWeek, dt.Hour, dt.Minute);
            return st;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(String propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        ~RemindProcessor() {
            Debug.Print("dispose");
            if (sw != null) {
             
                sw.Close();

            }
        }
        #region
        public List<Reminder> Test_GetAllRemindersForTime(DateTime dt) {
            return GetRemindersForTime(dt);
        }
        public string Test_currentItemId {
            get {
                return this.CurrentTimeId;
            }
            set {
                this.CurrentTimeId = value;
            }
        }



        public string Test_GetTimeIdFromTime(DateTime tm) {
            return GetTimeIdFromTime(tm);
        }
        public void Test_ShowNotification(Reminder r) {
            this.ShowNotification(r);
        }
        public void Test_ProccessTime(DateTime dt) {
            ProccessTime(dt);
        }
        public void Test_AddToLogList(string st) {
            this.AddToLogList(st);
        }
        #endregion
    }
}
