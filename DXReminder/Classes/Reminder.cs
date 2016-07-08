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
        public Reminder(XElement xl) {
            Description = xl.Attribute("Description").Value;
            var _time = xl.Attribute("Time").Value;
            var _dayOfWeek = xl.Attribute("DayOfWeek").Value;
            Time = DateTime.Parse(_time);
            DayOfWeek = int.Parse(_dayOfWeek);
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
}
