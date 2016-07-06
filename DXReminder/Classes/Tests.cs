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
            Assert.AreEqual(r.Description, "testdescrption");
            Assert.AreEqual(r.DayOfWeek, 4);
            Assert.AreEqual(r.Time.Hour,12 );
            Assert.AreEqual(r.Time.Minute, 25);
            
        }

    }
}
