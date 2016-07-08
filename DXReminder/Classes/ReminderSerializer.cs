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
        internal ObservableCollection<Reminder> Deserialize() {
            if (!File.Exists(fileName))
                return new ObservableCollection<Reminder>();
            StreamReader sr = new StreamReader(fileName);
            string st = sr.ReadToEnd();
            sr.Close();
            XElement xl = XElement.Parse(st);
            return GetRemindersFromXML(xl);

        }

        private void CheckIfFileExistAndCreateBackup() {
            var b = File.Exists(fileName);
            if (b) {
                File.Copy(fileName, fileName + ".bak",true);
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
        private ObservableCollection<Reminder> GetRemindersFromXML(XElement xl) {
            ObservableCollection<Reminder> col = new ObservableCollection<Reminder>();
            var list = xl.Elements();
            foreach (XElement x in list) {
                Reminder r = new Reminder(x);
                col.Add(r);
            }
            return col;
        }

        public XElement Test_GetXMLFromReminders(ObservableCollection<Reminder> reminders) {
            return GetXMLFromReminders(reminders);
        }
        public ObservableCollection<Reminder> Test_GetRemindersFromXML(XElement xlString) {
            return GetRemindersFromXML(xlString);
        }

    }


}
