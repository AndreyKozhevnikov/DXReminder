using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DXReminder.Classes {
    public class BaseViewModel {
        public BaseViewModel() {
            Reminders = new ObservableCollection<Reminder>();
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

     

        void Serialize() {
            CreateSerializeIfNeeded();
            serializer.Serialize(Reminders);
        }

        private void CreateSerializeIfNeeded() {
            if (serializer == null)
                serializer = new ReminderSerializer();
        }



        public void Temp_Serialize() {
            this.Serialize();
        }

    public    void Temp_CreateReminder() {
            Reminders.Add(new Reminder("test", 1, new DateTime(1, 1, 1, 23, 23, 0)));
            Reminders.Add(new Reminder("test2", 1, new DateTime(1, 1, 1, 11, 24, 0)));
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
}
