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
            var tList = new List<DateTime>();
            tList.Add(new DateTime(1, 1, 1, 12, 25, 0));
            var r = new Reminder("testdescrption", 4,tList );
            //assert
            Assert.AreEqual("testdescrption", r.Description);
            Assert.AreEqual(4, r.DayOfWeek);
            Assert.AreEqual(12, r.TimeList[0].Hour);
            Assert.AreEqual(25, r.TimeList[0].Minute);

        }
        [Test]
        public void BaseViewModel_AddNewReminder() {
            //arrange
            BaseViewModel vm = new BaseViewModel();
            vm.Reminders = new ObservableCollection<Reminder>();
            vm.UIDescription = "testd";
            vm.UIDayOfWeek = 6;
            var tList = new List<DateTime>();
            tList.Add(new DateTime(1, 1, 1, 18, 56, 0));
            vm.UITimeList = tList;
            //act
            vm.AddNewReminderCommand.Execute(null);
            //assert
            Assert.AreEqual(1, vm.Reminders.Count);
            Assert.AreEqual("testd", vm.Reminders[0].Description);
            Assert.AreEqual(6, vm.Reminders[0].DayOfWeek);
            Assert.AreEqual(18, vm.Reminders[0].TimeList[0].Hour);
            Assert.AreEqual(56, vm.Reminders[0].TimeList[0].Minute);
        }
        [Test]
        public void BaseViewModel_AddNewReminderWithEmptyDescription() {
            //arrange
            BaseViewModel vm = new BaseViewModel();
            vm.Reminders = new ObservableCollection<Reminder>();
            vm.UIDayOfWeek = 6;
            var tList = new List<DateTime>();
            tList.Add(new DateTime(1, 1, 1, 18, 56, 0));
            vm.UITimeList = tList;
            //act
            vm.AddNewReminderCommand.Execute(null);
            //assert
            Assert.AreEqual(0, vm.Reminders.Count);
        }
        [Test]
        public void ReminderStore_ToString() {
            //arrange
            var tList = new List<DateTime>();
            tList.Add(new DateTime(1, 1, 1, 15, 15, 0));
            tList.Add(new DateTime(1, 1, 1, 5, 0, 0));
            Reminder r = new Reminder("test", 1,tList );
            //act
            string st = r.ToString();
            //assert
            Assert.AreEqual("test - Monday - 15:15, 05:00", st);
        }

        [Test]
        public void Reminder_GetXML() {
            //arrange
            var tList = new List<DateTime>();
            tList.Add(new DateTime(1, 1, 1, 14, 15, 0));
            tList.Add(new DateTime(1, 1, 1, 9, 1, 0));
            Reminder r = new Reminder("test", 3,tList);
            XElement xlB = new XElement("Reminder");
            xlB.Add(new XAttribute("Description", "test"));
            XElement xlTm = new XElement("TimeList");
            xlTm.Add(new XElement("Time", "14:15"));
            xlTm.Add(new XElement("Time", "09:01"));
            xlB.Add(xlTm);
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
            var tList1 = new List<DateTime>();
            tList1.Add(new DateTime(1, 1, 1, 11, 11, 0));
            tList1.Add(new DateTime(1, 1, 1, 8, 7, 0));
            Reminder r1 = new Reminder("rem1", 1,tList1 );
            var tList2 = new List<DateTime>();
            tList2.Add(new DateTime(1, 1, 1, 9, 9, 0));
            tList2.Add(new DateTime(1, 1, 1, 23, 59, 0));
            Reminder r2 = new Reminder("rem2", 2,tList2 );
            ObservableCollection<Reminder> col = new ObservableCollection<Reminder>();
            col.Add(r1);
            col.Add(r2);

            XElement xlBase = new XElement("Reminders");
            XElement xl1 = new XElement("Reminder");
            xl1.Add(new XAttribute("Description", "rem1"));
            XElement xl1Time = new XElement("TimeList");
            xl1Time.Add(new XElement("Time", "11:11"));
            xl1Time.Add(new XElement("Time", "08:07"));
            xl1.Add(xl1Time);
            xl1.Add(new XAttribute("DayOfWeek", "1"));
            XElement xl2 = new XElement("Reminder");
            xl2.Add(new XAttribute("Description", "rem2"));
            XElement xl2Time = new XElement("TimeList");
            xl2Time.Add(new XElement("Time", "09:09"));
            xl2Time.Add(new XElement("Time", "23:59"));
            xl2.Add(xl2Time);
            xl2.Add(new XAttribute("DayOfWeek", "2"));
            xlBase.Add(xl1);
            xlBase.Add(xl2);
            ReminderSerializer r = new ReminderSerializer();
            //act

            XElement xRes = r.Test_GetXMLFromReminders(col);
            Boolean b = XElement.DeepEquals(xlBase, xRes);

            //assert
            Assert.AreEqual(true, b);
        }
        [Test]
        public void ReminderSerializer_GetRemindersFromXML() {
            //arrange
            ReminderSerializer r = new ReminderSerializer();
            XElement xlBase = new XElement("Reminders");
            XElement xl1 = new XElement("Reminder");
            xl1.Add(new XAttribute("Description", "rem1"));
            XElement xl1Time = new XElement("TimeList");
            xl1Time.Add(new XElement("Time", "11:11"));
            xl1.Add(xl1Time);
            xl1.Add(new XAttribute("DayOfWeek", "1"));
            XElement xl2 = new XElement("Reminder");
            xl2.Add(new XAttribute("Description", "rem2"));
            XElement xl2Time = new XElement("TimeList");
            xl2Time.Add(new XElement("Time", "09:09"));
            xl2.Add(xl2Time);
            xl2.Add(new XAttribute("DayOfWeek", "2"));
            xlBase.Add(xl1);
            xlBase.Add(xl2);

            //act
            ObservableCollection<Reminder> col = r.Test_GetRemindersFromXML(xlBase);

            //assert
            Assert.AreEqual(2, col.Count);
            Assert.AreEqual("rem1", col[0].Description);
            Assert.AreEqual(new DateTime(1,1,1,11,11,0).Hour, col[0].TimeList[0].Hour);
            Assert.AreEqual(new DateTime(1, 1, 1, 11, 11, 0).Minute, col[0].TimeList[0].Minute);
            Assert.AreEqual(1, col[0].DayOfWeek);
            Assert.AreEqual("rem2", col[1].Description);
            Assert.AreEqual(new DateTime(1, 1, 1, 9, 9, 0).Hour, col[1].TimeList[0].Hour);
            Assert.AreEqual(new DateTime(1, 1, 1, 9, 9, 0).Minute, col[1].TimeList[0].Minute);
            Assert.AreEqual(2, col[1].DayOfWeek);
        }
        [Test]
        public void Reminder_XMLConstructor() {
            //arrange
            XElement xl2 = new XElement("Reminder");
            xl2.Add(new XAttribute("Description", "rem2"));
            XElement xlTm = new XElement("TimeList");
            xlTm.Add(new XElement("Time", "14:15"));
            xlTm.Add(new XElement("Time", "09:01"));
            xl2.Add(xlTm);
            xl2.Add(new XAttribute("DayOfWeek", "2"));
            //act
            Reminder r = new Reminder(xl2);
            //assert
            Assert.AreEqual("rem2", r.Description);
            Assert.AreEqual(new DateTime(1, 1, 1, 14, 15, 0).Hour, r.TimeList[0].Hour);
            Assert.AreEqual(new DateTime(1, 1, 1, 14, 15, 0).Minute, r.TimeList[0].Minute);
            Assert.AreEqual(new DateTime(1, 1, 1, 9, 1, 0).Hour, r.TimeList[1].Hour);
            Assert.AreEqual(new DateTime(1, 1, 1, 9, 1, 0).Minute, r.TimeList[1].Minute);
            Assert.AreEqual(2, r.DayOfWeek);
        }

        [Test]
        public void Reminder_TimesToString_1() {
            //arrange
            var tList = new List<DateTime>();
            tList.Add(new DateTime(1, 1, 1, 15, 29, 1));
            Reminder r = new Reminder("test", 1, tList);
            //act
            string st = r.Test_TimesToString();
            //assert
            Assert.AreEqual("15:29", st);
        }
        [Test]
        public void Reminder_TimesToString_2() {
            //arrange
            var tList = new List<DateTime>();
            tList.Add(new DateTime(1, 1, 1, 15, 29, 1));
            tList.Add(new DateTime(1, 1, 1, 9, 5, 1));
            Reminder r = new Reminder("test", 1, tList);
            //act
            string st = r.Test_TimesToString();
            //assert
            Assert.AreEqual("15:29, 09:05", st);
        }
    }
}
