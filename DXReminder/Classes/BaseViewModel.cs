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
            serializer = new ReminderSerializer();

        }
        public ObservableCollection<Reminder> Reminders { get; set; }
        ICommand _addNewReminderCommand;
        ReminderSerializer serializer;
        public string UIDescription { get; set; }
        public List<int> UIDayOfWeekList { get; set; }
        public List<DateTime> UITimeList { get; set; }

        public ICommand AddNewReminderCommand {
            get {
                if (_addNewReminderCommand == null)
                    _addNewReminderCommand = new DelegateCommand(AddNewReminder);
                return _addNewReminderCommand;
            }
        }

        private void AddNewReminder() {
            if (string.IsNullOrEmpty(UIDescription)||UIDayOfWeekList==null || UITimeList==null) {
                return;
            }
            Reminder r = new Reminder(UIDescription, UIDayOfWeekList, UITimeList);
            Reminders.Add(r);
        }



        void Serialize() {

            serializer.Serialize(Reminders);
        }

        public void Deserialize() {
            Reminders = serializer.Deserialize();
        }


        #region temp
        public void Temp_Serialize() {
            this.Serialize();
        }


        public void Temp_CreateReminder() {
          //  Reminders.Add(new Reminder("test", 1, new DateTime(1, 1, 1, 23, 23, 0)));
          //  Reminders.Add(new Reminder("test2", 1, new DateTime(1, 1, 1, 11, 24, 0)));
        }
        #endregion

        #region For_Test
        public ReminderSerializer Test_Serializer {
            get { return serializer; }
        }

        #endregion
    }
}
