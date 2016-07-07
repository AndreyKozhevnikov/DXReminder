using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace DXReminder.Classes {
    [TestFixture]
    class Tests {
        [Test]
        public void ReminderStoreConstructor() {
            //arrange
            //act
            var r = new Reminder("testdescrption", 4, new DateTime(1, 1, 1, 12, 25, 0));
            //assert
            Assert.AreEqual("testdescrption", r.Description);
            Assert.AreEqual(4, r.DayOfWeek);
            Assert.AreEqual(12, r.Time.Hour);
            Assert.AreEqual(25, r.Time.Minute);

        }
        [Test]
        public void BaseViewModel_AddNewReminder() {
            //arrange
            BaseViewModel vm = new BaseViewModel();
            vm.TempDescription = "testd";
            vm.TempDayOfWeek = 6;
            vm.TempTime = new DateTime(1, 1, 1, 18, 56, 0);
            //act
            vm.AddNewReminderCommand.Execute(null);
            //assert
            Assert.AreEqual(1, vm.Reminders.Count);
            Assert.AreEqual("testd", vm.Reminders[0].Description);
            Assert.AreEqual(6, vm.Reminders[0].DayOfWeek);
            Assert.AreEqual(18, vm.Reminders[0].Time.Hour);
            Assert.AreEqual(56, vm.Reminders[0].Time.Minute);
        }
        [Test]
        public void ReminderStore_ToString() {
            //arrange
            Reminder r = new Reminder("test", 1, new DateTime(1, 1, 1, 15, 15, 0));
            //act
            string st = r.ToString();
            //assert
            Assert.AreEqual("test - Monday - 15:15", st);
        }
        [Test]
        public void BaseViewModel_CreateSerializerIfNeeded_Yes() {
            //arrange
            BaseViewModel vm = new BaseViewModel();

            //act
            vm.Test_CreateSerializerIfNeeded();
            var ser1 = vm.Test_Serializer;
            vm.Test_CreateSerializerIfNeeded();
            var ser2 = vm.Test_Serializer;
            //assert
            Assert.AreEqual(ser1, ser2);
        }
        [Test]
        public void BaseViewModel_CreateSerializerIfNeeded_No() {
            //arrange
            BaseViewModel vm = new BaseViewModel();

            //act
            vm.Test_CreateSerializerIfNeeded();
            var ser1 = vm.Test_Serializer;
          
            //assert
            Assert.AreNotEqual(null, ser1);
        }
        [Test]
        public void Reminder_GetXML() {
            //arrange
            Reminder r = new Reminder("test", 3, new DateTime(1,1,1,14,15,0));
            XElement xlB = new XElement("Reminder");
            xlB.Add(new XAttribute("Description", "test"));
            xlB.Add(new XAttribute("Time", "14:15"));
            xlB.Add(new XAttribute("DayOfWeek", "3"));
            //act
            XElement xl = r.GetXML();
            var b = XElement.DeepEquals(xl, xlB);

            //assert
            Assert.AreEqual(true, b);
        }
        [Test]
        public void ReminderSerializer_GetXMLFromReminders() {
            //arrange
            Reminder r1 = new Reminder("rem1", 1, new DateTime(1, 1, 1, 11, 11, 0));
            Reminder r2 = new Reminder("rem2", 2, new DateTime(1, 1, 1, 9, 9, 0)); 
            ObservableCollection<Reminder> col = new ObservableCollection<Reminder>();
            col.Add(r1);
            col.Add(r2);

            XElement xlBase = new XElement("Reminders");
            XElement xl1 = new XElement("Reminder");
            xl1.Add(new XAttribute("Description", "rem1"));
            xl1.Add(new XAttribute("Time", "11:11"));
            xl1.Add(new XAttribute("DayOfWeek", "1"));
            XElement xl2 = new XElement("Reminder");
            xl2.Add(new XAttribute("Description", "rem2"));
            xl2.Add(new XAttribute("Time", "09:09"));
            xl2.Add(new XAttribute("DayOfWeek", "2"));
            xlBase.Add(xl1);
            xlBase.Add(xl2);

            //act
            ReminderSerializer r = new ReminderSerializer();
            XElement xRes = r.Test_GetXMLFromReminders(col);
            Boolean b = XElement.DeepEquals(xlBase, xRes);
        }

    }
}
