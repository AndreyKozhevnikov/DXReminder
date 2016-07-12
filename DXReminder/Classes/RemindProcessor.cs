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
        private List<Reminder> reminders;
        public RemindProcessor(List<Reminder> _reminders) {
            this.reminders = _reminders;
            currentMinuteReminders = new List<Reminder>();
        }
        Timer timer;
        public void Start() {
            timer = new Timer();
            timer.Tick += OnTimer;
            timer.Interval = 1000;
            timer.Start();

        }
        List<Reminder> currentMinuteReminders;
        string currentItemId;
        void OnTimer(object sender, EventArgs e) {
            ChangeCurrentTimeIdIfRequired();
            var list = GetAllRemindersForTime(DateTime.Now);

        }

        private List<Reminder> GetAllRemindersForTime(DateTime dt) {

            var lst = reminders.Where(x => x.DayOfWeekList.Contains((int)dt.DayOfWeek) && x.TimeList.Contains(new DateTime(1, 1, 1, dt.Hour, dt.Minute, 0))).ToList();
            return lst;
        }

        private void ChangeCurrentTimeIdIfRequired() {
            string st = GetTimeIdFromTime(DateTime.Now);
            if (currentItemId != st) {
                currentItemId = st;
                currentMinuteReminders.Clear();
            }
        }

        private string GetTimeIdFromTime(DateTime dt) {

            var st = string.Format("{0}-{1}-{2}",(int) dt.DayOfWeek, dt.Hour, dt.Minute);
            return st;
        }


        #region
        public List<Reminder> Test_GetAllRemindersForTime(DateTime dt) {
            return GetAllRemindersForTime(dt);
        }
        public string Test_currentItemId {
            get {
                return this.currentItemId;
            }
            set {
                this.currentItemId = value;
            }
        }
        public List<Reminder> Test_CurrentMinuteReminders {
            get {
                return currentMinuteReminders;
            }
        }
        public string Test_GetTimeIdFromTime( DateTime tm) {
            return GetTimeIdFromTime(tm);
        }
        public void Test_ChangeCurrentTimeIdIfRequired() {
            this.ChangeCurrentTimeIdIfRequired();
        }
        #endregion
    }
}
