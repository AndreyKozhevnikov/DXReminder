using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Core.Native;
using DevExpress.Mvvm.UI;
using System.Windows;
using DevExpress.Xpf.Editors;
using System.Windows.Data;
using DevExpress.Mvvm.UI.Native;
using System.Windows.Controls;
using System.Threading;

namespace DXReminder.Classes {
    [TestFixture]
    public class ReminderSerializer_Test {
        [Test]
        public void ReminderSerializer_GetXMLFromReminders() {
            //arrange
            var tList1 = new List<DateTime>();
            tList1.Add(new DateTime(1, 1, 1, 11, 11, 0));
            tList1.Add(new DateTime(1, 1, 1, 8, 7, 0));
            var dList1 = new List<int>();
            dList1.Add(1);
            dList1.Add(5);
            Reminder r1 = new Reminder("rem1", dList1, tList1);
            var tList2 = new List<DateTime>();
            tList2.Add(new DateTime(1, 1, 1, 9, 9, 0));
            tList2.Add(new DateTime(1, 1, 1, 23, 59, 0));
            var dList2 = new List<int>();
            dList2.Add(2);
            dList2.Add(6);
            Reminder r2 = new Reminder("rem2", dList2, tList2);
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
            XElement xl1Day = new XElement("DayOfWeekList");
            xl1Day.Add(new XElement("DayOfWeek", "1"));
            xl1Day.Add(new XElement("DayOfWeek", "5"));
            xl1.Add(xl1Day);

            XElement xl2 = new XElement("Reminder");
            xl2.Add(new XAttribute("Description", "rem2"));
            XElement xl2Time = new XElement("TimeList");
            xl2Time.Add(new XElement("Time", "09:09"));
            xl2Time.Add(new XElement("Time", "23:59"));
            xl2.Add(xl2Time);
            XElement xl2Day = new XElement("DayOfWeekList");
            xl2Day.Add(new XElement("DayOfWeek", "2"));
            xl2Day.Add(new XElement("DayOfWeek", "6"));
            xl2.Add(xl2Day);
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
            XElement xl1Day = new XElement("DayOfWeekList");
            xl1Day.Add(new XElement("DayOfWeek", "1"));
            xl1Day.Add(new XElement("DayOfWeek", "4"));
            xl1.Add(xl1Day);

            XElement xl2 = new XElement("Reminder");
            xl2.Add(new XAttribute("Description", "rem2"));
            XElement xl2Time = new XElement("TimeList");
            xl2Time.Add(new XElement("Time", "09:09"));
            xl2.Add(xl2Time);
            XElement xl2Day = new XElement("DayOfWeekList");
            xl2Day.Add(new XElement("DayOfWeek", "2"));
            xl2Day.Add(new XElement("DayOfWeek", "3"));
            xl2.Add(xl2Day);

            xlBase.Add(xl1);
            xlBase.Add(xl2);

            //act
            ObservableCollection<Reminder> col = r.Test_GetRemindersFromXML(xlBase);

            //assert
            Assert.AreEqual(2, col.Count);
            Assert.AreEqual("rem1", col[0].Description);
            Assert.AreEqual(new DateTime(1, 1, 1, 11, 11, 0).Hour, col[0].TimeList[0].Hour);
            Assert.AreEqual(new DateTime(1, 1, 1, 11, 11, 0).Minute, col[0].TimeList[0].Minute);
            Assert.AreEqual(2, col[0].DayOfWeekList.Count);
            Assert.AreEqual(1, col[0].DayOfWeekList[0]);
            Assert.AreEqual(4, col[0].DayOfWeekList[1]);
            Assert.AreEqual("rem2", col[1].Description);
            Assert.AreEqual(new DateTime(1, 1, 1, 9, 9, 0).Hour, col[1].TimeList[0].Hour);
            Assert.AreEqual(new DateTime(1, 1, 1, 9, 9, 0).Minute, col[1].TimeList[0].Minute);
            Assert.AreEqual(2, col[1].DayOfWeekList.Count);
            Assert.AreEqual(2, col[1].DayOfWeekList[0]);
            Assert.AreEqual(3, col[1].DayOfWeekList[1]);
        }
    }

    [TestFixture]
    public class Reminder_Test {
        [Test]
        public void ReminderConstructor() {
            //arrange
            //act
            var tList = new List<DateTime>();
            tList.Add(new DateTime(1, 1, 1, 12, 25, 0));
            tList.Add(new DateTime(1, 1, 1, 5, 5, 0));
            var dList = new List<int>();
            dList.Add(4);
            dList.Add(1);
            dList.Add(2);
            var r = new Reminder("testdescrption", dList, tList);
            //assert
            Assert.AreEqual("testdescrption", r.Description);
            Assert.AreEqual(3, r.DayOfWeekList.Count);
            Assert.AreEqual(4, r.DayOfWeekList[0]);
            Assert.AreEqual(1, r.DayOfWeekList[1]);
            Assert.AreEqual(2, r.DayOfWeekList[2]);
            Assert.AreEqual(2, r.TimeList.Count);
            Assert.AreEqual(12, r.TimeList[0].Hour);
            Assert.AreEqual(25, r.TimeList[0].Minute);
            Assert.AreEqual(5, r.TimeList[1].Hour);
            Assert.AreEqual(5, r.TimeList[1].Minute);

        }
        [Test]
        public void Reminder_ToString() {
            //arrange
            var tList = new List<DateTime>();
            tList.Add(new DateTime(1, 1, 1, 15, 15, 0));
            tList.Add(new DateTime(1, 1, 1, 5, 0, 0));
            var dList = new List<int>();
            dList.Add(1);
            dList.Add(6);
            Reminder r = new Reminder("test", dList, tList);
            //act
            string st = r.ToString();
            //assert
            Assert.AreEqual("test - Monday, Saturday - 15:15, 05:00", st);
        }

        [Test]
        public void Reminder_GetXML() {
            //arrange
            var tList = new List<DateTime>();
            tList.Add(new DateTime(1, 1, 1, 14, 15, 0));
            tList.Add(new DateTime(1, 1, 1, 9, 1, 0));
            var dList = new List<int>();
            dList.Add(3);
            dList.Add(5);
            Reminder r = new Reminder("test", dList, tList);
            XElement xlB = new XElement("Reminder");
            xlB.Add(new XAttribute("Description", "test"));

            XElement xlTm = new XElement("TimeList");
            xlTm.Add(new XElement("Time", "14:15"));
            xlTm.Add(new XElement("Time", "09:01"));
            xlB.Add(xlTm);

            XElement xlDm = new XElement("DayOfWeekList");
            xlDm.Add(new XElement("DayOfWeek", "3"));
            xlDm.Add(new XElement("DayOfWeek", "5"));

            xlB.Add(xlDm);
            //act
            XElement xl = r.GetXML();
            var b = XElement.DeepEquals(xl, xlB);

            //assert
            Assert.AreEqual(true, b);
        }

        [Test]
        public void Reminder_XMLConstructor() {
            //arrange
            XElement xlBase = new XElement("Reminder");
            xlBase.Add(new XAttribute("Description", "rem2"));
            XElement xlTm = new XElement("TimeList");
            xlTm.Add(new XElement("Time", "14:15"));
            xlTm.Add(new XElement("Time", "09:01"));
            xlBase.Add(xlTm);

            XElement xlDm = new XElement("DayOfWeekList");
            xlDm.Add(new XElement("DayOfWeek", "2"));
            xlDm.Add(new XElement("DayOfWeek", "6"));
            xlBase.Add(xlDm);
            //act
            Reminder r = new Reminder(xlBase);
            //assert
            Assert.AreEqual("rem2", r.Description);
            Assert.AreEqual(new DateTime(1, 1, 1, 14, 15, 0), r.TimeList[0]);
            Assert.AreEqual(new DateTime(1, 1, 1, 9, 1, 0), r.TimeList[1]);
            Assert.AreEqual(2, r.DayOfWeekList.Count);
            Assert.AreEqual(2, r.DayOfWeekList[0]);
            Assert.AreEqual(6, r.DayOfWeekList[1]);
        }

        [Test]
        public void Reminder_TimesToString_1() {
            //arrange
            var tList = new List<DateTime>();
            tList.Add(new DateTime(1, 1, 1, 15, 29, 1));
            var dList = new List<int>();
            dList.Add(1);
            Reminder r = new Reminder("test", dList, tList);
            //act
            string st = r.Test_TimeListToString();
            //assert
            Assert.AreEqual("15:29", st);
        }
        [Test]
        public void Reminder_TimesToString_2() {
            //arrange
            var tList = new List<DateTime>();
            tList.Add(new DateTime(1, 1, 1, 15, 29, 1));
            tList.Add(new DateTime(1, 1, 1, 9, 5, 1));
            var dList = new List<int>();
            dList.Add(1);
            Reminder r = new Reminder("test", dList, tList);
            //act
            string st = r.Test_TimeListToString();
            //assert
            Assert.AreEqual("15:29, 09:05", st);
        }
        [Test]
        public void Reminder_GetDayNameFromInt() {
            //arrange
            Reminder r = new Reminder(null, null, null);
            //act
            string st = r.Test_GetDayNameFromInt(3);
            //Assert
            Assert.AreEqual("Wednesday", st);
        }
        [Test]
        public void Reminder_DayOfWeekListToString_1() {
            //arrange
            var dList = new List<int>();
            dList.Add(1);
            Reminder r = new Reminder(null, dList, null);
            //act
            string st = r.Test_DayOfWeekListToString();
            //assert
            Assert.AreEqual("Monday", st);
        }
        [Test]
        public void Reminder_DayOfWeekListToString_2() {
            //arrange
            var dList = new List<int>();
            dList.Add(1);
            dList.Add(4);
            Reminder r = new Reminder(null, dList, null);
            //act
            string st = r.Test_DayOfWeekListToString();
            //assert
            Assert.AreEqual("Monday, Thursday", st);
        }


    }

    [TestFixture]
    class BaseViewModel_Test {
        [Test]
        public void BaseViewModel_AddNewReminder() {
            //arrange
            BaseViewModel vm = new BaseViewModel();
            vm.Reminders = new ObservableCollection<Reminder>();
            vm.UIDescription = "testd";
            var dList = new List<int>();
            dList.Add(6);
            vm.UIDayOfWeekList = dList;
            var tList = new List<DateTime>();
            tList.Add(new DateTime(1, 1, 1, 18, 56, 0));
            vm.UITimeList = tList;
            //act
            vm.AddNewReminderCommand.Execute(null);
            //assert
            Assert.AreEqual(1, vm.Reminders.Count);
            Assert.AreEqual("testd", vm.Reminders[0].Description);
            Assert.AreEqual(6, vm.Reminders[0].DayOfWeekList[0]);
            Assert.AreEqual(18, vm.Reminders[0].TimeList[0].Hour);
            Assert.AreEqual(56, vm.Reminders[0].TimeList[0].Minute);
        }
        [Test]
        public void BaseViewModel_AddNewReminderDescriptionNull() {
            //arrange
            BaseViewModel vm = new BaseViewModel();
            vm.Reminders = new ObservableCollection<Reminder>();
            var dList = new List<int>();
            dList.Add(6);
            vm.UIDayOfWeekList = dList;
            var tList = new List<DateTime>();
            tList.Add(new DateTime(1, 1, 1, 18, 56, 0));
            vm.UITimeList = tList;
            //act
            vm.AddNewReminderCommand.Execute(null);
            //assert
            Assert.AreEqual(0, vm.Reminders.Count);
        }
        [Test]
        public void BaseViewModel_AddNewReminderDayListNull() {
            //arrange
            BaseViewModel vm = new BaseViewModel();
            vm.UIDescription = "test";

            vm.Reminders = new ObservableCollection<Reminder>();


            var tList = new List<DateTime>();
            tList.Add(new DateTime(1, 1, 1, 18, 56, 0));
            vm.UITimeList = tList;
            //act
            vm.AddNewReminderCommand.Execute(null);
            //assert
            Assert.AreEqual(0, vm.Reminders.Count);
        }
        [Test]
        public void BaseViewModel_AddNewReminderTimeListNull() {
            //arrange
            BaseViewModel vm = new BaseViewModel();
            vm.UIDescription = "test";

            vm.Reminders = new ObservableCollection<Reminder>();
            var dList = new List<int>();
            dList.Add(6);
            vm.UIDayOfWeekList = dList;

            //act
            vm.AddNewReminderCommand.Execute(null);
            //assert
            Assert.AreEqual(0, vm.Reminders.Count);
        }




    }
    [TestFixture]
    public class RemindProcessor_Test {

        [Test]
        public void Test_GetTimeIdFromTime() {
            //arrange
            RemindProcessor p = new RemindProcessor(null);
            //act
            string st = p.Test_GetTimeIdFromTime(new DateTime(2016, 7, 12, 15, 59, 34));
            //assert
            Assert.AreEqual("2-15-59", st);
        }

        [Test]
        public void Test_GetAllRemindersForTime() {
            //arrange
            Reminder r1 = new Reminder("r1", new List<int>() { 1, 2 }, new List<DateTime>() { new DateTime(1, 1, 1, 15, 15, 0) });
            Reminder r2 = new Reminder("r2", new List<int>() { 3, 2 }, new List<DateTime>() { new DateTime(1, 1, 1, 15, 15, 0) });
            Reminder r3 = new Reminder("r3", new List<int>() { 5, 6 }, new List<DateTime>() { new DateTime(1, 1, 1, 2, 2, 0) });
            List<Reminder> lReminders = new List<Reminder>();
            lReminders.Add(r1);
            lReminders.Add(r2);
            lReminders.Add(r3);
            RemindProcessor p = new RemindProcessor(lReminders);
            DateTime dt = new DateTime(2016, 7, 12, 15, 15, 45);
            //act
            var lst = p.Test_GetAllRemindersForTime(dt);
            //assert
            Assert.AreEqual(2, lst.Count);
            Assert.AreEqual(1, lst[0].DayOfWeekList[0]);
            Assert.AreEqual(2, lst[0].DayOfWeekList[1]);
            Assert.AreEqual(3, lst[1].DayOfWeekList[0]);
            Assert.AreEqual(2, lst[1].DayOfWeekList[1]);

        }
        [Test]
        public void ShowNotification() {
            //arrange
            Reminder r = new Reminder("testReminder", null, null);
            RemindProcessor proc = new RemindProcessor(null);
            Application a = null;
            if (Application.Current == null)
                a = new Application() { ShutdownMode = ShutdownMode.OnExplicitShutdown };

            //act
            proc.Test_ShowNotification(r);
            var tw = App.Current.Windows[0] as ToastWindow;
            Label lb = LayoutTreeHelper.GetVisualChildren(tw).OfType<Label>().First() as Label;
            var cont = lb.Content;
            tw.Close();
            //assert
            Assert.AreEqual(0, App.Current.Windows.Count);
            Assert.AreEqual("testReminder", cont);

        }
        [Test]
        public void ShowSeveralNotifications() {
            //arrange
            var lDay1 = new List<int>();
            lDay1.Add(3);
            lDay1.Add(1);
            var lTime1 = new List<DateTime>();
            lTime1.Add(new DateTime(1, 1, 1, 12, 22, 0));
            lTime1.Add(new DateTime(1, 1, 1, 20, 59, 0));
            Reminder r1 = new Reminder("testReminder1", lDay1, lTime1);

            var lDay2 = new List<int>();
            lDay2.Add(3);
            lDay2.Add(6);
            var lTime2 = new List<DateTime>();
            lTime2.Add(new DateTime(1, 1, 1, 12, 22, 0));
            lTime2.Add(new DateTime(1, 1, 1, 1, 1, 0));
            Reminder r2 = new Reminder("testReminder2", lDay2, lTime2);

            var rList = new List<Reminder>();
            rList.Add(r1);
            rList.Add(r2);
            RemindProcessor proc = new RemindProcessor(rList);
            Application a = null;
            if (Application.Current == null)
                a = new Application() { ShutdownMode = ShutdownMode.OnExplicitShutdown };

            //act
            proc.Test_ProccessTime(new DateTime(2016, 7, 13, 12, 22, 15));
            // Thread.Sleep(500);
            Assert.AreEqual(2, Application.Current.Windows.Count);
            var tw1 = App.Current.Windows[0] as ToastWindow;
            var tw2 = App.Current.Windows[1] as ToastWindow;
            Label lb1 = LayoutTreeHelper.GetVisualChildren(tw1).OfType<Label>().First() as Label;
            Label lb2 = LayoutTreeHelper.GetVisualChildren(tw2).OfType<Label>().First() as Label;
            var cont1 = lb1.Content;
            var cont2 = lb2.Content;
            tw1.Close();
            tw2.Close();
            //assert
            Assert.AreEqual(0, App.Current.Windows.Count);
            Assert.AreEqual("testReminder1", cont1);
            Assert.AreEqual("testReminder2", cont2);
        }

        [Test]
        public void Test_WriteLog_1() {
            //arrange
            RemindProcessor p = new RemindProcessor(null);

            //act
            p.Test_AddToLogList("testReminder");

            //assert
            Assert.AreEqual(1, p.LogList.Count);
            var b = p.LogList[0].Contains("testReminder");
            Assert.AreEqual(true, b);
        }
        [Test]
        public void Test_WriteLog_2() {
            //arrange
            RemindProcessor p = new RemindProcessor(null);
            //act
            p.Test_AddToLogList("testReminder1");
            p.Test_AddToLogList("testReminder2");

            //assert
            Assert.AreEqual(2, p.LogList.Count);
            var b1 = p.LogList[0].Contains("testReminder2");
            var b2 = p.LogList[1].Contains("testReminder1");
            Assert.AreEqual(true, b1);
            Assert.AreEqual(true, b2);
        }
    }


    [TestFixture]
    public class TimeControl_Test {
        public class SimpleViewModel {
            public List<DateTime> TimeList { get; set; }
        }
        [Test]
        public void TimeControl_AddValue() {
            //arrange
            SimpleViewModel vm = new SimpleViewModel();
            vm.TimeList = new List<DateTime>();
            TimeControl tc = new TimeControl();
            tc.SingleTime = new DateTime(1, 1, 1, 1, 1, 1);
            Binding b = new Binding("TimeList");
            b.Mode = BindingMode.TwoWay;
            b.Converter = new TimeListConverter();
            tc.SetBinding(TimeControl.EditValueProperty, b);
            Window w = new Window();
            w.DataContext = vm;
            w.Content = tc;
            w.Show();
            //act
            tc.Test_Button_Click();
            //assert
            Assert.AreEqual(1, vm.TimeList.Count);

        }

    }

}
