using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DXReminder.Classes {
    public class ReminderStore {
        
        public int DayOfWeek { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }

        public ReminderStore(string _description, int _dayOfWeek, DateTime _time) {
            Description = _description;
            DayOfWeek = _dayOfWeek;
            Time = _time;
        }
        public override string ToString() {
            var s = Enum.GetName(typeof(System.DayOfWeek), DayOfWeek);
            string st = string.Format("{0} - {1} - {2}", Description, s, Time.ToString("HH:mm"));
            return st;
        }
    }

    public class BaseViewModel {
        public BaseViewModel() {
            Reminders = new ObservableCollection<ReminderStore>();
        
        }
        public ObservableCollection<ReminderStore> Reminders { get; set; }
        ICommand _addNewReminderCommand;

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
            ReminderStore r = new ReminderStore(TempDescription, TempDayOfWeek, TempTime);
            Reminders.Add(r);
        }
    }



}
