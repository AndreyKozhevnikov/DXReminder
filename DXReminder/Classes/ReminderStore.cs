using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

    public class BaseViewModel {
        public ObservableCollection<ReminderStore> Reminders { get; set; }
    }

}
