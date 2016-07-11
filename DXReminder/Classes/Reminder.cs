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
        public List<DateTime> TimeList { get; set; }
        public string Description { get; set; }

        public Reminder(string _description, int _dayOfWeek, List<DateTime> _times) {
            Description = _description;
            DayOfWeek = _dayOfWeek;
            TimeList = _times;
        }
        public Reminder(XElement xl) {
            Description = xl.Attribute("Description").Value;
            var tmpTimeList = xl.Element("TimeList").Elements();
            TimeList = new List<DateTime>();
            foreach(XElement x in tmpTimeList) {
                DateTime dt = DateTime.Parse(x.Value);
                TimeList.Add(dt);
            }
            //var _time = xl.Attribute("Time").Value;
            //  Times = DateTime.Parse(_time);
            var _dayOfWeek = xl.Attribute("DayOfWeek").Value;
        
            DayOfWeek = int.Parse(_dayOfWeek);
        }
        public override string ToString() {
            var s = Enum.GetName(typeof(System.DayOfWeek), DayOfWeek);
            string st = string.Format("{0} - {1} - {2}", Description, s, TimesToString());
            return st;
        }

        string TimesToString() {
            var stList = TimeList.Select(x => x.ToString("HH:mm"));
            var s = String.Join(", ", stList);
            return s;
        }

        internal XElement GetXML() {
            XElement xl = new XElement("Reminder");
            xl.Add(new XAttribute("Description", Description));

            XElement tm = new XElement("TimeList");
            foreach (DateTime dt in TimeList) {
                tm.Add(new XElement("Time", dt.ToString("HH:mm")));
            }
            xl.Add(tm);
            xl.Add(new XAttribute("DayOfWeek", DayOfWeek));
            return xl;
        }

        #region
        public string Test_TimesToString() {
            return this.TimesToString();
        }
        #endregion
    }
}
