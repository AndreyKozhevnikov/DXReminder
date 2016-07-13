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
        
        public List<int> DayOfWeekList { get; set; }
        public List<DateTime> TimeList { get; set; }
        public string Description { get; set; }

        public Reminder(string _description, List<int> _dayOfWeekList, List<DateTime> _timeList) {
            Description = _description;
            DayOfWeekList = _dayOfWeekList;
            TimeList = _timeList;
        }
        public Reminder(XElement xl) {
            Description = xl.Attribute("Description").Value;
            var tmpTimeList = xl.Element("TimeList").Elements();
            TimeList = new List<DateTime>();
            foreach(XElement x in tmpTimeList) {
                DateTime dt = DateTime.Parse(x.Value);
                DateTime dt1 = new DateTime(1, 1, 1, dt.Hour, dt.Minute, 0);
                TimeList.Add(dt1);
            }
            var tmpDayOfWeekList = xl.Element("DayOfWeekList").Elements();
            DayOfWeekList = new List<int>();
            foreach (XElement x in tmpDayOfWeekList) {
                int val =int.Parse(x.Value);
                DayOfWeekList.Add(val);
            }
        
       //     DayOfWeekList = int.Parse(_dayOfWeek);
        }
        public override string ToString() {
          
            string st = string.Format("{0} - {1} - {2}", Description, DayOfWeekListToString(), TimeListToString());
            return st;
        }

        string TimeListToString() {
            var stList = TimeList.Select(x => x.ToString("HH:mm"));
            var s = String.Join(", ", stList);
            return s;
        }

        string DayOfWeekListToString() {
            var stDays = DayOfWeekList.Select(x => GetDayNameFromInt(x));
            var s = String.Join(", ", stDays);
            return s;
        }

        string GetDayNameFromInt(int c) {
            return Enum.GetName(typeof(System.DayOfWeek), c);
        }

        internal XElement GetXML() {
            XElement xl = new XElement("Reminder");
            xl.Add(new XAttribute("Description", Description));

            XElement tm = new XElement("TimeList");
            foreach (DateTime dt in TimeList) {
                tm.Add(new XElement("Time", dt.ToString("HH:mm")));
            }
            xl.Add(tm);

            XElement dm = new XElement("DayOfWeekList");
            foreach(int d in DayOfWeekList) {
                dm.Add(new XElement("DayOfWeek", d));
            }
            xl.Add(dm);
            return xl;
        }

        #region
        public string Test_TimeListToString() {
            return this.TimeListToString();
        }
        public string Test_DayOfWeekListToString() {
            return this.DayOfWeekListToString();
        }
        public string Test_GetDayNameFromInt(int c) {
            return this.GetDayNameFromInt(c);
        }
    
        #endregion
    }
}
