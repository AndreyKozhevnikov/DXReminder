using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DXReminder.Classes {
    public class ReminderSerializer {

        internal void Serialize(ObservableCollection<Reminder> reminders) {
            XElement st = GetXMLFromReminders(reminders);
            StreamWriter sw = new StreamWriter("Reminders.xml");
            sw.Write(st);
            sw.Close();
        }

        private XElement GetXMLFromReminders(ObservableCollection<Reminder> reminders) {
            XElement xlBase = new XElement("Reminders");
            foreach (Reminder r in reminders) {
                XElement x = r.GetXML();
                xlBase.Add(x); 
            }
            return xlBase;
        }

        public XElement Test_GetXMLFromReminders(ObservableCollection<Reminder> reminders) {
            return GetXMLFromReminders(reminders);
        }

    }


}
