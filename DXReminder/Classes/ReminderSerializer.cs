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
        const string fileName = "Reminders.xml";
        internal void Serialize(ObservableCollection<Reminder> reminders) {
            XElement st = GetXMLFromReminders(reminders);

            
            CheckIfFileExistAndCreateBackup();
            StreamWriter sw = new StreamWriter(fileName);
            sw.Write(st);
            sw.Close();
        }

        private void CheckIfFileExistAndCreateBackup() {
            var b = File.Exists(fileName);
            if (b) {
                File.Copy(fileName, fileName + ".bak");
            }
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
