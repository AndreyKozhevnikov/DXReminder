using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Text;
using System.Threading.Tasks;

namespace DXReminder.Classes {
    [TestFixture]
    class Tests {
        [Test]
        public void ReminderStoreConstructor() {
            //arrange
            //act
            var r = new ReminderStore("testdescrption", 4, new DateTime(1, 1, 1, 12, 25, 0));
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
            ReminderStore r = new ReminderStore("test", 1, new DateTime(1, 1, 1, 15, 15, 0));
            //act
            string st = r.ToString();
            //assert
            Assert.AreEqual("test - Monday - 15:15", st);
        }

    }
}
