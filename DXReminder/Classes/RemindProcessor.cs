using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DXReminder.Classes {
    public class RemindProcessor {
        private ObservableCollection<Reminder> reminders;
        public RemindProcessor(ObservableCollection<Reminder> _reminders) {
            this.reminders = _reminders;
        }
        Timer timer;
        public void Start() {
            timer = new Timer();
            timer.Tick += OnTimer;
            timer.Interval = 1000;
            timer.Start();

        }

        void OnTimer(object sender, EventArgs e) {
            int currDayOfWeek =(int) DateTime.Today.DayOfWeek;
            int currHour = DateTime.Today.Hour;
            int currMinute = DateTime.Today.Minute;

            var list = reminders.Where(x => x.DayOfWeek == currDayOfWeek && x.Time.Hour == currHour && x.Time.Minute == currMinute).ToList();

        }

    }
}
