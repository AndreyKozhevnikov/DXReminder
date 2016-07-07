using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace DXReminder.Classes {
    public class Reminder {
        
        public int DayOfWeek { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }

        public Reminder(string _description, int _dayOfWeek, DateTime _time) {
            Description = _description;
            DayOfWeek = _dayOfWeek;
            Time = _time;
        }
        public override string ToString() {
            var s = Enum.GetName(typeof(System.DayOfWeek), DayOfWeek);
            string st = string.Format("{0} - {1} - {2}", Description, s, Time.ToString("HH:mm"));
            return st;
        }

        internal XElement GetXML() {
            XElement xl = new XElement("Reminder");
            xl.Add(new XAttribute("Description", Description));
            xl.Add(new XAttribute("Time", Time.ToString("HH:mm")));
            xl.Add(new XAttribute("DayOfWeek", DayOfWeek));
            return xl;
        }


    }

    public class BaseViewModel {
        public BaseViewModel() {
            Reminders = new ObservableCollection<Reminder>();
            Temp_CreateReminder();
        
        }
        public ObservableCollection<Reminder> Reminders { get; set; }
        ICommand _addNewReminderCommand;
        ReminderSerializer serializer;
        public string TempDescription { get; set; }
        public int TempDayOfWeek { get; set; }
        public DateTime TempTime { get; set; }

        public ICommand AddNewReminderCommand {
            get {
                if (_addNewReminderCommand == null)
                    _addNewReminderCommand = new DelegateCommand(AddNewReminder);
                return _addNewReminderCommand;
            }
        }

        private void AddNewReminder() {
            Reminder r = new Reminder(TempDescription, TempDayOfWeek, TempTime);
            Reminders.Add(r);
        }

        void Temp_CreateReminder() {
            Reminders.Add(new Reminder("test", 1, new DateTime(1, 1, 1, 23, 23, 0)));
            Reminders.Add(new Reminder("test2", 1, new DateTime(1, 1, 1, 11, 24, 0)));
        }

        void Serialize() {
            CreateSerializeIfNeeded();
            serializer.Serialize(Reminders);
        }

        private void CreateSerializeIfNeeded() {
            if (serializer == null)
                serializer = new ReminderSerializer();
        }





        #region For_Test
        public void Test_CreateSerializerIfNeeded() {
            CreateSerializeIfNeeded();
        }
        public ReminderSerializer Test_Serializer {
            get { return serializer; }
        }

        #endregion
    }

    public class ReminderSerializer {

        internal void Serialize(ObservableCollection<Reminder> reminders) {
            XElement st = GetXMLFromReminders(reminders);
        }

        private XElement GetXMLFromReminders(ObservableCollection<Reminder> reminders) {
            XElement xlBase = new XElement("Reminders");
            foreach (Reminder r in reminders) {
                XElement x = r.GetXML();
                xlBase.Add(x);
            }
            return xlBase;
        }

        public XElement Test_GetXMLFromReminders(ObservableCollection <Reminder> reminders) {
            return GetXMLFromReminders(reminders);
        }

    }




}
